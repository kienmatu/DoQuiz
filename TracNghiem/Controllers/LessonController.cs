using PagedList;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TracNghiem.Models;
using TracNghiem.ViewModel;

namespace TracNghiem.Controllers
{
    [Authorize]
    public class LessonController : Controller
    {
        QuizContext db = new QuizContext();
        // GET: Lesson
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "admin,teacher")]
        [HttpGet]
        public ViewResult AllLesson(string sortOrder, string CurrentSort, int? page, string titleStr)
        {
            int pageSize = 10;
            int pageIndex = 1;
            ViewBag.Sort = "tăng dần";
            ViewBag.CurrentSort = sortOrder;
            sortOrder = String.IsNullOrEmpty(sortOrder) ? "Title" : sortOrder;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<LessonViewModel> lstLesson = null;
            var id = db.Users.Where(e => e.username == User.Identity.Name).First().ID;
            if (String.IsNullOrWhiteSpace(titleStr))
            {
                switch (sortOrder)
                {
                    case "title":
                        lstLesson = db.Lessons.OrderBy(e => e.Name).Select(q =>
                          new LessonViewModel
                          {
                              Name = q.Name,
                              Id = q.ID,
                              Time = q.Time,
                              File = q.File,
                              CreatedBy = q.CreatedBy,
                              CreatedDate = q.CreatedDate
                          }
                        ).ToPagedList(pageIndex, pageSize);
                        break;
                    case "createdate":
                        lstLesson = db.Lessons.OrderBy(e => e.CreatedDate).Select(q =>
                        new LessonViewModel
                        {
                            Name = q.Name,
                            Id = q.ID,
                            Time = q.Time,
                            File = q.File,
                            CreatedBy = q.CreatedBy,
                            CreatedDate = q.CreatedDate
                        }
                        ).ToPagedList(pageIndex, pageSize);
                        break;
                    default:
                        lstLesson = db.Lessons.OrderBy(e => e.Name).Select(q =>
                        new LessonViewModel
                        {
                            Name = q.Name,
                            Id = q.ID,
                            Time = q.Time,
                            File = q.File,
                            CreatedBy = q.CreatedBy,
                            CreatedDate = q.CreatedDate
                        }
                        ).ToPagedList(pageIndex, pageSize);
                        break;
                }
            }
            else
            {
                ViewBag.titleStr = titleStr;
                switch (sortOrder)
                {
                    case "title":
                        ViewBag.sortname = "tiêu đề";
                        if (sortOrder.Equals(CurrentSort))
                        {
                            lstLesson = db.Lessons.Where(t => t.Name.Contains(titleStr)).OrderBy(e => e.Name).Select(q =>
                           new LessonViewModel
                           {
                               Name = q.Name,
                               Id = q.ID,
                               Time = q.Time,
                               File = q.File,
                               CreatedBy = q.CreatedBy,
                               CreatedDate = q.CreatedDate
                           }
                            ).ToPagedList(pageIndex, pageSize);
                        }
                        else
                        {
                            lstLesson = db.Lessons.Where(t => t.Name.Contains(titleStr)).OrderByDescending(e => e.Name).Select(q =>
                            new LessonViewModel
                            {
                                Name = q.Name,
                                Id = q.ID,
                                Time = q.Time,
                                CreatedBy = q.CreatedBy,
                                File = q.File,
                                CreatedDate = q.CreatedDate
                            }
                            ).ToPagedList(pageIndex, pageSize);
                        }
                        break;
                    case "createdate":
                        ViewBag.sortname = "ngày tạo";
                        if (sortOrder.Equals(CurrentSort))
                        {
                            lstLesson = db.Lessons.Where(t => t.Name.Contains(titleStr)).OrderBy(e => e.CreatedDate).Select(q =>
                            new LessonViewModel
                            {
                                Name = q.Name,
                                Id = q.ID,
                                Time = q.Time,
                                File = q.File,
                                CreatedBy = q.CreatedBy,
                                CreatedDate = q.CreatedDate
                            }
                            ).ToPagedList(pageIndex, pageSize);
                        }
                        else
                        {
                            lstLesson = db.Lessons.Where(t => t.Name.Contains(titleStr)).OrderByDescending(e => e.CreatedDate).Select(q =>
                            new LessonViewModel
                            {
                                Name = q.Name,
                                Id = q.ID,
                                Time = q.Time,
                                File = q.File,
                                CreatedBy = q.CreatedBy,
                                CreatedDate = q.CreatedDate
                            }
                            ).ToPagedList(pageIndex, pageSize);
                        }
                        break;
                    default:
                        lstLesson = db.Lessons.Where(t => t.Name.Contains(titleStr)).OrderBy(e => e.Name).Select(q =>
                        new LessonViewModel
                        {
                            Name = q.Name,
                            Id = q.ID,
                            Time = q.Time,
                            CreatedBy = q.CreatedBy,
                            File = q.File,
                            CreatedDate = q.CreatedDate
                        }
                        ).ToPagedList(pageIndex, pageSize);
                        break;
                }
            }
            return View(lstLesson);
        }
        public ActionResult AllMyLesson(int? page)
        {
            int pageSize = 6;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<LessonViewModel> lstLesson = null;
            var id = db.Users.Where(e => e.username == User.Identity.Name).First().ID;
            lstLesson = db.Lessons.Where(x => x.Status == LessonStatus.Open).OrderBy(x => x.ID).Select(
                x => new LessonViewModel
                {
                    Id = x.ID,
                    Name = x.Name,
                    Time = x.Time,
                    File = x.File,
                    Description = x.Description,
                    CreatedBy = x.CreatedBy,
                    YoutubeLink = x.YoutubeLink
                }
                ).ToPagedList(pageIndex, pageSize);
            return View(lstLesson);
        }
        [HttpGet]
        [Authorize(Roles = "admin,teacher")]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "admin,teacher")]
        public ActionResult Create(LessonViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    HttpPostedFileBase file = Request.Files[0];
                    string fileExt = Path.GetExtension(file.FileName).ToUpper();
                    if (file.ContentLength > 0 && fileExt == ".PDF")
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        model.File = Path.Combine(Server.MapPath("~/UploadedFiles"), fileName);
                        file.SaveAs(model.File);
                        var user = db.Users.Where(t => t.username == User.Identity.Name).First();
                        Lesson l = new Lesson()
                        {
                            Name = model.Name,
                            Time = model.Time,
                            CreatorID = user.ID,
                            File = model.File,
                            YoutubeLink = model.YoutubeLink,
                            CreatedDate = DateTime.Now,
                            Description = model.Description,
                            CreatedBy = user.fullname,
                            Status = LessonStatus.Close
                        };
                        db.Lessons.Add(l);
                        db.SaveChanges();
                        return RedirectToAction("AllLesson");
                    }
                }
                catch (Exception error)
                {
                    ModelState.AddModelError("", "Tệp tài liệu không được trống");
                }
            }
            return RedirectToAction("Create");
        }
        [HttpGet]
        [Authorize(Roles = "admin,teacher")]
        public ActionResult Edit(int id)
        {
            var model = db.Lessons.Where(x => x.ID == id).SingleOrDefault();
            if (model != null)
            {
                LessonViewModel lesson = new LessonViewModel()
                {
                    Name = model.Name,
                    Time = model.Time,
                    File = model.File,
                    Description = model.Description,
                    YoutubeLink = model.YoutubeLink
                };
                return View(lesson);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        [Authorize(Roles = "admin,teacher")]
        public ActionResult Edit(LessonViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Where(t => t.username == User.Identity.Name).First();
                Lesson l = db.Lessons.Where(x => x.ID == model.Id).SingleOrDefault();
                if (l != null)
                {
                    l.Name = model.Name;
                    l.Time = model.Time;
                    l.Description = model.Description;
                    l.File = model.File;
                    l.YoutubeLink = model.YoutubeLink;
                    l.ModifiedBy = user.fullname;
                    l.ModifiedDate = DateTime.Now;
                    db.Lessons.Add(l);
                    db.SaveChanges();
                    return RedirectToAction("AllLesson");
                }
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult LessonDetail(int id)
        {
            var lesson = db.Lessons.Where(x => x.ID == id && x.Status == LessonStatus.Open).SingleOrDefault();
            if (lesson != null)
            {
                LessonViewModel l = new LessonViewModel()
                {
                    Id = lesson.ID,
                    Name = lesson.Name,
                    File = lesson.File,

                    YoutubeLink = lesson.YoutubeLink,
                    Description = lesson.Description
                };
                return View(l);
            }
            return RedirectToAction("AllMyLesson");
        }
        [HttpPost]
        [Authorize(Roles ="admin,teacher")]
        public JsonResult ChangeStatus(int id, LessonStatus status)
        {
            Lesson lesson = db.Lessons.Find(id);
            User user = db.Users.Where(x => x.username == User.Identity.Name).First();
            if(lesson.CreatorID == user.ID && User.IsInRole("admin") || User.IsInRole("teacher"))
            {
                string title = lesson.Name;
                lesson.Status = status;
                string prefix = status == LessonStatus.Open ? "Mở" : "Đóng";
                db.SaveChanges();
                return Json(new { Message = prefix + " \"" + title + "\" thành công" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Message = "Hack thành công, chúc mừng :>" }, JsonRequestBehavior.AllowGet);
        }
    }
}