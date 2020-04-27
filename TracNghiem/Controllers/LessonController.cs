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
        //All lesson
        [Authorize(Roles = "admin")]
        [HttpGet]
        public ViewResult AllLesson(string sortOrder, string CurrentSort, int? page, string titleStr)
        {
            int pageSize = 100;
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
                        lstLesson = db.Lessons.Where(x=>x.Status != LessonStatus.IsDelete).OrderBy(e => e.Name).Select(q =>
                          new LessonViewModel
                          {
                              Name = q.Name,
                              Id = q.ID,
                              OrderId = q.OrderId,
                              Status = q.Status,
                              Time = q.Time,
                              File = q.File,
                              CreatedBy = q.CreatedBy,
                              CreatedDate = q.CreatedDate
                          }
                        ).ToPagedList(pageIndex, pageSize);
                        break;
                    case "createdate":
                        lstLesson = db.Lessons.Where(x=>x.Status!= LessonStatus.IsDelete).OrderBy(e => e.CreatedDate).Select(q =>
                        new LessonViewModel
                        {
                            Name = q.Name,
                            Id = q.ID,
                            OrderId = q.OrderId,
                            Status = q.Status,
                            Time = q.Time,
                            File = q.File,
                            CreatedBy = q.CreatedBy,
                            CreatedDate = q.CreatedDate
                        }
                        ).ToPagedList(pageIndex, pageSize);
                        break;
                    default:
                        lstLesson = db.Lessons.Where(x=>x.Status!=LessonStatus.IsDelete).OrderBy(e => e.Name).Select(q =>
                        new LessonViewModel
                        {
                            Name = q.Name,
                            Id = q.ID,
                            OrderId = q.OrderId,
                            Status = q.Status,
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
                            lstLesson = db.Lessons.Where(t => t.Name.Contains(titleStr) && t.Status != LessonStatus.IsDelete).OrderBy(e => e.Name).Select(q =>
                           new LessonViewModel
                           {
                               Name = q.Name,
                               Id = q.ID,
                               OrderId = q.OrderId,
                               Status = q.Status,
                               Time = q.Time,
                               File = q.File,
                               CreatedBy = q.CreatedBy,
                               CreatedDate = q.CreatedDate
                           }
                            ).ToPagedList(pageIndex, pageSize);
                        }
                        else
                        {
                            lstLesson = db.Lessons.Where(t => t.Name.Contains(titleStr) && t.Status != LessonStatus.IsDelete).OrderByDescending(e => e.Name).Select(q =>
                            new LessonViewModel
                            {
                                Name = q.Name,
                                Id = q.ID,
                                Time = q.Time,
                                OrderId = q.OrderId,
                                Status = q.Status,
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
                            lstLesson = db.Lessons.Where(t => t.Name.Contains(titleStr) && t.Status != LessonStatus.IsDelete).OrderBy(e => e.CreatedDate).Select(q =>
                            new LessonViewModel
                            {
                                Name = q.Name,
                                Id = q.ID,
                                Time = q.Time,
                                OrderId = q.OrderId,
                                File = q.File,
                                Status = q.Status,
                                CreatedBy = q.CreatedBy,
                                CreatedDate = q.CreatedDate
                            }
                            ).ToPagedList(pageIndex, pageSize);
                        }
                        else
                        {
                            lstLesson = db.Lessons.Where(t => t.Name.Contains(titleStr) && t.Status != LessonStatus.IsDelete).OrderByDescending(e => e.CreatedDate).Select(q =>
                            new LessonViewModel
                            {
                                Name = q.Name,
                                Id = q.ID,
                                Time = q.Time,
                                Status = q.Status,
                                File = q.File,
                                OrderId = q.OrderId,
                                CreatedBy = q.CreatedBy,
                                CreatedDate = q.CreatedDate
                            }
                            ).ToPagedList(pageIndex, pageSize);
                        }
                        break;
                    default:
                        lstLesson = db.Lessons.Where(t => t.Name.Contains(titleStr) && t.Status != LessonStatus.IsDelete).OrderBy(e => e.Name).Select(q =>
                        new LessonViewModel
                        {
                            Name = q.Name,
                            Id = q.ID,
                            Time = q.Time,
                            Status = q.Status,
                            OrderId = q.OrderId,
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
        [HttpGet]
        [Authorize(Roles = "admin,teacher")]
        public ActionResult MyLesson(string sortOrder, string CurrentSort, int? page, string titleStr)
        {
            int pageSize = 100;
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
                        lstLesson = db.Lessons.Where(x => x.Status != LessonStatus.IsDelete && x.CreatorID == id).OrderBy(e => e.Name).Select(q =>
                            new LessonViewModel
                            {
                                Name = q.Name,
                                Id = q.ID,
                                OrderId = q.OrderId,
                                Status = q.Status,
                                Time = q.Time,
                                File = q.File,
                                CreatedBy = q.CreatedBy,
                                CreatedDate = q.CreatedDate
                            }
                        ).ToPagedList(pageIndex, pageSize);
                        break;
                    case "createdate":
                        lstLesson = db.Lessons.Where(x => x.Status != LessonStatus.IsDelete && x.CreatorID == id).OrderBy(e => e.CreatedDate).Select(q =>
                           new LessonViewModel
                           {
                               Name = q.Name,
                               Id = q.ID,
                               OrderId = q.OrderId,
                               Status = q.Status,
                               Time = q.Time,
                               File = q.File,
                               CreatedBy = q.CreatedBy,
                               CreatedDate = q.CreatedDate
                           }
                        ).ToPagedList(pageIndex, pageSize);
                        break;
                    default:
                        lstLesson = db.Lessons.Where(x => x.Status != LessonStatus.IsDelete && x.CreatorID == id).OrderBy(e => e.Name).Select(q =>
                            new LessonViewModel
                            {
                                Name = q.Name,
                                Id = q.ID,
                                OrderId = q.OrderId,
                                Status = q.Status,
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
                            lstLesson = db.Lessons.Where(t => t.Name.Contains(titleStr) && t.Status != LessonStatus.IsDelete && t.CreatorID == id).OrderBy(e => e.Name).Select(q =>
                           new LessonViewModel
                           {
                               Name = q.Name,
                               Id = q.ID,
                               OrderId = q.OrderId,
                               Status = q.Status,
                               Time = q.Time,
                               File = q.File,
                               CreatedBy = q.CreatedBy,
                               CreatedDate = q.CreatedDate
                           }
                            ).ToPagedList(pageIndex, pageSize);
                        }
                        else
                        {
                            lstLesson = db.Lessons.Where(t => t.Name.Contains(titleStr) && t.Status != LessonStatus.IsDelete && t.CreatorID == id).OrderByDescending(e => e.Name).Select(q =>
                            new LessonViewModel
                            {
                                Name = q.Name,
                                Id = q.ID,
                                Time = q.Time,
                                OrderId = q.OrderId,
                                Status = q.Status,
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
                            lstLesson = db.Lessons.Where(t => t.Name.Contains(titleStr) && t.Status != LessonStatus.IsDelete && t.CreatorID == id).OrderBy(e => e.CreatedDate).Select(q =>
                            new LessonViewModel
                            {
                                Name = q.Name,
                                Id = q.ID,
                                Time = q.Time,
                                OrderId = q.OrderId,
                                File = q.File,
                                Status = q.Status,
                                CreatedBy = q.CreatedBy,
                                CreatedDate = q.CreatedDate
                            }
                            ).ToPagedList(pageIndex, pageSize);
                        }
                        else
                        {
                            lstLesson = db.Lessons.Where(t => t.Name.Contains(titleStr) && t.Status != LessonStatus.IsDelete && t.CreatorID == id).OrderByDescending(e => e.CreatedDate).Select(q =>
                            new LessonViewModel
                            {
                                Name = q.Name,
                                Id = q.ID,
                                Time = q.Time,
                                Status = q.Status,
                                File = q.File,
                                OrderId = q.OrderId,
                                CreatedBy = q.CreatedBy,
                                CreatedDate = q.CreatedDate
                            }
                            ).ToPagedList(pageIndex, pageSize);
                        }
                        break;
                    default:
                        lstLesson = db.Lessons.Where(t => t.Name.Contains(titleStr) && t.Status != LessonStatus.IsDelete && t.CreatorID == id).OrderBy(e => e.Name).Select(q =>
                        new LessonViewModel
                        {
                            Name = q.Name,
                            Id = q.ID,
                            Time = q.Time,
                            Status = q.Status,
                            OrderId = q.OrderId,
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
        //Lesson is Active
        public ActionResult LessonActive(int? page)
        {
            int pageSize = 6;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<LessonViewModel> lstLesson = null;
            lstLesson = db.Lessons.Where(x => x.Status == LessonStatus.Open).OrderBy(x => x.ID).Select(
                x => new LessonViewModel
                {
                    Id = x.ID,
                    Name = x.Name,
                    Time = x.Time,
                    Status = x.Status,
                    File = x.File,
                    OrderId = x.OrderId,
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
                        model.File = Path.Combine(Server.MapPath("~/UploadedFiles/web"), fileName);     
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
                            OrderId = model.OrderId,
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
            var user = db.Users.Where(t => t.username == User.Identity.Name).First();
            if (model != null)
            {
                LessonViewModel lesson = new LessonViewModel()
                {
                    OrderId = model.OrderId,
                    Name = model.Name,
                    Time = model.Time,
                    Description = model.Description,
                    YoutubeLink = model.YoutubeLink,
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
                Lesson l = db.Lessons.Where(x => x.ID == model.Id).SingleOrDefault();
                l.Name = model.Name;
                l.Time = model.Time;
                l.OrderId = model.OrderId;
                l.Description = model.Description;             
                l.YoutubeLink = model.YoutubeLink;
                l.CreatedDate = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("AllLesson");
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult ChangeFile(int id)
        {
            var model = db.Lessons.Where(x => x.ID == id).SingleOrDefault();
            string[] arr = model.File.Split('\\');
            string fileName = arr[arr.Length - 1];
            var user = db.Users.Where(t => t.username == User.Identity.Name).First();
            if (model != null)
            {
                LessonViewModelFile lesson = new LessonViewModelFile()
                {
                    File = fileName,                   
                };
                return View(lesson);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public ActionResult ChangeFile(LessonViewModelFile model)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files[0];
                string fileExt = Path.GetExtension(file.FileName).ToUpper();
                if (file.ContentLength > 0 && fileExt == ".PDF")
                {
                    var fileName = Path.GetFileName(file.FileName);
                    model.File = Path.Combine(Server.MapPath("~/UploadedFiles/web"), fileName);
                    file.SaveAs(model.File);
                    Lesson l = db.Lessons.Where(x => x.ID == model.Id).SingleOrDefault();
                    if (l.File != model.File)
                    {
                        System.IO.File.Delete(l.File);
                        l.File = model.File;
                    }
                    l.File = l.File;
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
            string[] arr = lesson.File.Split('\\');
            if (lesson != null)
            {
                LessonViewModel l = new LessonViewModel()
                {
                    Id = lesson.ID,
                    OrderId = lesson.OrderId,
                    Name = lesson.Name,
                    File = arr[arr.Length-1],
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
        [HttpPost]
        [Authorize(Roles ="admin,teacher")]
        public JsonResult DeleteLesson(int id, string title)
        {
            var lesson = db.Lessons.Find(id);
            string prefix = lesson.Name;
            if(lesson != null)
            {
                lesson.Status = LessonStatus.IsDelete;
                db.SaveChanges();
                return Json(new { Message = prefix + " \"" + title + "\"xóa thành công" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Message = "Hack thành công, chúc mừng :>" }, JsonRequestBehavior.AllowGet);
        }

    }
}