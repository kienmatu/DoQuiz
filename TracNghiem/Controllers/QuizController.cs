using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TracNghiem.Models;
using TracNghiem.ViewModel;

namespace TracNghiem.Controllers
{
    [Authorize]
    public class QuizController : Controller
    {
        QuizContext db = new QuizContext();
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
        public ViewResult AllQuiz(string sortOrder, string CurrentSort, int? page, string titleStr)
        {
            int pageSize = 100;
            int pageIndex = 1;
            ViewBag.Sort = "tăng dần";
            ViewBag.CurrentSort = sortOrder;
            sortOrder = String.IsNullOrEmpty(sortOrder) ? "Title" : sortOrder;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<QuizViewModelAd> lstQuiz = null;
            var id = db.Users.Where(e => e.username == User.Identity.Name).First().ID;
            if (String.IsNullOrWhiteSpace(titleStr))
            {
                switch (sortOrder)
                {
                    case "title":
                        lstQuiz = db.Quizzes.Where(a=>a.status != QuizStatusAd.NotActive).OrderBy(e => e.name).Select(q =>
                        new QuizViewModelAd
                        {
                            status = q.status,
                            ID = q.QuizID,
                            LessonName = q.lesson.Name,
                            name = q.name,
                            HardType = q.HardType,
                            CreatorName = q.Creator.username,
                            CreateDate = q.CreateDate,
                        }
                        ).ToPagedList(pageIndex, pageSize);
                        break;
                    case "createdate":
                        lstQuiz = db.Quizzes.Where(a => a.status != QuizStatusAd.NotActive).OrderBy(e => e.CreateDate).Select(q =>
                        new QuizViewModelAd
                        {
                            status = q.status,
                            ID = q.QuizID,
                            name = q.name,
                            HardType = q.HardType,
                            LessonName = q.lesson.Name,
                            CreatorName = q.Creator.username,
                            CreateDate = q.CreateDate,
                        }
                        ).ToPagedList(pageIndex, pageSize);
                        break;
                    default:
                        lstQuiz = db.Quizzes.Where(a => a.status != QuizStatusAd.NotActive).OrderBy(e => e.name).Select(q =>
                        new QuizViewModelAd
                        {
                            status = q.status,
                            ID = q.QuizID,
                            name = q.name,
                            HardType = q.HardType,
                            LessonName = q.lesson.Name,
                            CreatorName = q.Creator.username,
                            CreateDate = q.CreateDate,
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
                            lstQuiz = db.Quizzes.Where(t => t.name.Contains(titleStr) && t.status != QuizStatusAd.NotActive).OrderBy(e => e.name).Select(q =>
                           new QuizViewModelAd
                           {
                               status = q.status,
                               ID = q.QuizID,
                               name = q.name,
                               HardType = q.HardType,
                               LessonName = q.lesson.Name,
                               CreatorName = q.Creator.username,
                               CreateDate = q.CreateDate,
                           }
                            ).ToPagedList(pageIndex, pageSize);
                        }
                        else
                        {
                            lstQuiz = db.Quizzes.Where(t => t.name.Contains(titleStr) && t.status != QuizStatusAd.NotActive).OrderByDescending(e => e.name).Select(q =>
                            new QuizViewModelAd
                            {
                                status = q.status,
                                ID = q.QuizID,
                                name = q.name,
                                HardType = q.HardType,
                                LessonName = q.lesson.Name,
                                CreatorName = q.Creator.username,
                                CreateDate = q.CreateDate,
                            }
                            ).ToPagedList(pageIndex, pageSize);
                        }
                        break;
                    case "createdate":
                        ViewBag.sortname = "ngày tạo";
                        if (sortOrder.Equals(CurrentSort))
                        {
                            lstQuiz = db.Quizzes.Where(t => t.name.Contains(titleStr) && t.status != QuizStatusAd.NotActive).OrderBy(e => e.CreateDate).Select(q =>
                            new QuizViewModelAd
                            {
                                status = q.status,
                                ID = q.QuizID,
                                name = q.name,
                                HardType = q.HardType,
                                LessonName = q.lesson.Name,
                                CreatorName = q.Creator.username,
                                CreateDate = q.CreateDate,
                            }
                            ).ToPagedList(pageIndex, pageSize);
                        }
                        else
                        {
                            lstQuiz = db.Quizzes.Where(t => t.name.Contains(titleStr) && t.status != QuizStatusAd.NotActive).OrderByDescending(e => e.CreateDate).Select(q =>
                            new QuizViewModelAd
                            {
                                status = q.status,
                                ID = q.QuizID,
                                name = q.name,
                                HardType = q.HardType,
                                LessonName = q.lesson.Name,
                                CreatorName = q.Creator.username,
                                CreateDate = q.CreateDate,
                            }
                            ).ToPagedList(pageIndex, pageSize);
                        }
                        break;
                    default:
                        lstQuiz = db.Quizzes.Where(t => t.name.Contains(titleStr) && t.status != QuizStatusAd.NotActive).OrderBy(e => e.name).Select(q =>
                        new QuizViewModelAd
                        {
                            status = q.status,
                            ID = q.QuizID,
                            name = q.name,
                            HardType = q.HardType,
                            LessonName = q.lesson.Name,
                            CreatorName = q.Creator.username,
                            CreateDate = q.CreateDate,
                        }
                        ).ToPagedList(pageIndex, pageSize);
                        break;
                }
            }
            return View(lstQuiz);
        }
        /// <summary>
        /// Câu hỏi của tôi
        /// </summary>
        /// <param name="sortOrder"></param>
        /// <param name="CurrentSort"></param>
        /// <param name="page"></param>
        /// <param name="titleStr"></param>
        /// <returns></returns>
        public ViewResult MyQuiz(string sortOrder, string CurrentSort, int? page, string titleStr)
        {
            int pageSize = 100;
            int pageIndex = 1;
            ViewBag.Sort = "tăng dần";
            ViewBag.CurrentSort = sortOrder;
            sortOrder = String.IsNullOrEmpty(sortOrder) ? "Title" : sortOrder;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<QuizViewModel> lstQuiz = null;
            var id = db.Users.Where(e => e.username == User.Identity.Name).First().ID;
            if(String.IsNullOrWhiteSpace(titleStr))
            {
                switch (sortOrder)
                {
                    case "title":
                        lstQuiz = db.Quizzes.Where(t => t.CreatorID == id && t.status != QuizStatusAd.Deleted).OrderBy(e => e.name).Select(q =>
                        new QuizViewModel
                        {
                            ID = q.QuizID,
                            name = q.name,
                            HardType = q.HardType,

                            CreatorName = q.Creator.username,
                            CreateDate = q.CreateDate,
                            status = (QuizStatus)q.status,
                        }
                        ).ToPagedList(pageIndex, pageSize);
                        break;
                    case "createdate":
                        lstQuiz = db.Quizzes.Where(t => t.CreatorID == id && t.status != QuizStatusAd.Deleted).OrderBy(e => e.CreateDate).Select(q =>
                        new QuizViewModel
                        {
                            ID = q.QuizID,
                            name = q.name,
                            HardType = q.HardType,
                            LessonName = q.lesson.Name,
                            CreatorName = q.Creator.username,
                            CreateDate = q.CreateDate,
                            status = (QuizStatus)q.status,
                        }
                        ).ToPagedList(pageIndex, pageSize);
                        break;
                    default:
                        lstQuiz = db.Quizzes.Where(t => t.CreatorID == id && t.status != QuizStatusAd.Deleted).OrderBy(e => e.name).Select(q =>
                        new QuizViewModel
                        {
                            ID = q.QuizID,
                            name = q.name,
                            HardType = q.HardType,
                            LessonName = q.lesson.Name,
                            CreatorName = q.Creator.username,
                            CreateDate = q.CreateDate,
                            status = (QuizStatus)q.status,
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
                            lstQuiz = db.Quizzes.Where(t => t.CreatorID == id && t.status != QuizStatusAd.Deleted && t.name.Contains(titleStr)).OrderBy(e => e.name).Select(q =>
                            new QuizViewModel
                            {
                                ID = q.QuizID,
                                name = q.name,
                                HardType = q.HardType,
                                LessonName = q.lesson.Name,
                                CreatorName = q.Creator.username,
                                CreateDate = q.CreateDate,
                                status = (QuizStatus)q.status,
                            }
                            ).ToPagedList(pageIndex, pageSize);
                        }
                        else
                        {
                            lstQuiz = db.Quizzes.Where(t => t.CreatorID == id && t.status != QuizStatusAd.Deleted && t.name.Contains(titleStr)).OrderByDescending(e => e.name).Select(q =>
                            new QuizViewModel
                            {
                                ID = q.QuizID,
                                name = q.name,
                                HardType = q.HardType,
                                LessonName = q.lesson.Name,
                                CreatorName = q.Creator.username,
                                CreateDate = q.CreateDate,
                                status = (QuizStatus)q.status,
                            }
                            ).ToPagedList(pageIndex, pageSize);
                        }
                        break;
                    case "createdate":
                        ViewBag.sortname = "ngày tạo";
                        if (sortOrder.Equals(CurrentSort))
                        {
                            lstQuiz = db.Quizzes.Where(t => t.CreatorID == id && t.status != QuizStatusAd.Deleted && t.name.Contains(titleStr)).OrderBy(e => e.CreateDate).Select(q =>
                            new QuizViewModel
                            {
                                ID = q.QuizID,
                                name = q.name,
                                HardType = q.HardType,
                                LessonName = q.lesson.Name,
                                CreatorName = q.Creator.username,
                                CreateDate = q.CreateDate,
                                status = (QuizStatus)q.status,
                            }
                            ).ToPagedList(pageIndex, pageSize);
                        }
                        else
                        {
                            lstQuiz = db.Quizzes.Where(t => t.CreatorID == id && t.status != QuizStatusAd.Deleted && t.name.Contains(titleStr)).OrderByDescending(e => e.CreateDate).Select(q =>
                            new QuizViewModel
                            {
                                ID = q.QuizID,
                                name = q.name,
                                HardType = q.HardType,
                                LessonName = q.lesson.Name,
                                CreatorName = q.Creator.username,
                                CreateDate = q.CreateDate,
                                status = (QuizStatus)q.status,
                            }
                            ).ToPagedList(pageIndex, pageSize);
                        }
                        break;
                    default:
                        lstQuiz = db.Quizzes.Where(t => t.CreatorID == id && t.status != QuizStatusAd.Deleted && t.name.Contains(titleStr)).OrderBy(e => e.name).Select(q =>
                        new QuizViewModel
                        {
                            ID = q.QuizID,
                            name = q.name,
                            HardType = q.HardType,
                            LessonName = q.lesson.Name,
                            CreatorName = q.Creator.username,
                            CreateDate = q.CreateDate,
                            status = (QuizStatus)q.status,
                        }
                        ).ToPagedList(pageIndex, pageSize);
                        break;
                }
            }
            
            return View(lstQuiz);
        }
        [Authorize(Roles = "admin,teacher")]
        [HttpGet]
        public ViewResult Create()
        {
            QuizViewModel model = new QuizViewModel
            {
                Lessons = Common.Helper.getLessonItem()
            };
            return View(model);
        }
        [Authorize(Roles = "admin,teacher")]
        [HttpPost]
        public ActionResult Create(QuizViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userID = db.Users.Where(t => t.username == User.Identity.Name).First().ID;
                Quiz q = new Quiz
                {
                    name = model.name,
                    content = model.content,
                    CreatorID = userID,
                    answerA = model.answerA,
                    answerB = model.answerB,
                    answerC = model.answerC,
                    answerD = model.answerD,
                    trueAnswer = model.trueAnswer,
                    LessonId = model.LessonId,
                    status = (QuizStatusAd)model.status,
                    HardType = model.HardType,
                };
                db.Quizzes.Add(q);
                db.SaveChanges();
                return RedirectToAction("MyQuiz");
            }
            return View(model);
        }
        /// <summary>
        /// Sửa câu hỏi
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "admin,teacher")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Quiz q = db.Quizzes.Find(id);
            if (q.status != QuizStatusAd.Deleted)
            {
                QuizViewModel model = new QuizViewModel
                {
                    LessonId = q.LessonId,
                    answerD = q.answerD,
                    answerA = q.answerA,
                    status = (QuizStatus)q.status,
                    answerB = q.answerB,
                    answerC = q.answerC,
                    trueAnswer = q.trueAnswer,
                    content = q.content,
                    ID = q.QuizID,
                    name = q.name,
                    HardType = q.HardType,
                };
                return View(model);
            }
            else
            {
                return RedirectToAction("MyQuiz");
            }
            
        }
        [Authorize(Roles = "admin,teacher")]
        [HttpPost]
        public ActionResult Edit(QuizViewModel model)
        {
            if (ModelState.IsValid)
            {
                Quiz q = db.Quizzes.Find(model.ID);
                q.name = model.name;
                q.LessonId = model.LessonId;
                q.content = model.content;
                q.answerA = model.answerA;
                q.answerB = model.answerB;
                q.answerC = model.answerC;
                q.answerD = model.answerD;
                q.trueAnswer = model.trueAnswer;
                q.status = (QuizStatusAd)model.status;
                q.HardType = model.HardType;
                db.SaveChanges();
                return RedirectToAction("MyQuiz");
            }
            return View(model);
        }
        /// <summary>
        /// Xóa câu hỏi
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteQuiz(int id)
        {
            User u = db.Users.Where(t => t.username == User.Identity.Name).First();
            Quiz q = db.Quizzes.Find(id);
            if (q.CreatorID == u.ID || User.IsInRole("admin"))
            {
                string title = q.name;
                q.status = QuizStatusAd.Deleted;
                db.SaveChanges();
                return Json(new { Message =  "Xóa \"" + title + "\" thành công" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Message = "Hack thành công, chúc mừng :>" }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Đổi status câu hỏi
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ChangeStatus(int id, QuizStatusAd state)
        {
            User u = db.Users.Where(t => t.username == User.Identity.Name).First();
            Quiz q = db.Quizzes.Find(id);
            if(q.CreatorID == u.ID || User.IsInRole("admin"))
            {
                string title = q.name;
                q.status = state;
                string prefix = state == QuizStatusAd.Active ? "Đăng" : "Hủy đăng";
                db.SaveChanges();
                return Json(new { Message = prefix + " \"" + title + "\" thành công" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Message = "Hack thành công, chúc mừng :>" }, JsonRequestBehavior.AllowGet);
        }
    }
}