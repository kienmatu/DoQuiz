﻿using PagedList;
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
    public class QuizTestController : Controller
    {
        private QuizContext db = new QuizContext();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "admin,teacher")]
        public ActionResult Create()
        {
            var model = new QuizTestViewModel
            {
                 Lessons= Common.Helper.getLessonItem(),
            };
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "admin,teacher")]
        public JsonResult Create(QuizTestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                
                return Json(new { success = false, Message = "Bạn nhập thiếu các trường yêu cầu" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                User u = db.Users.Where(i => i.username == User.Identity.Name).First();
                var quiz = db.QuizTests.Where(x => x.name == model.name && x.LessonId == model.LessonId).SingleOrDefault();
                if (quiz != null)
                {
                    return Json(new { success = false, Message = "Bài học này đã có bài tập" }, JsonRequestBehavior.AllowGet);
                }
                QuizTest test = new QuizTest
                {
                    CreatorID = u.ID,
                    Creator = u,
                    CreateDate = DateTime.Now,
                    LessonId = model.LessonId,
                    TotalMark = model.TotalMark,
                    name = model.name,
                    TotalTime = (int)model.TotalTime,
                    status = (TestStatusAd)model.status,
                };
                foreach(var item in model.quizID)
                {
                    Quiz q = db.Quizzes.Find(item);
                    test.Quiz.Add(q);
                }
                db.QuizTests.Add(test);
                db.SaveChanges();
                return  Json(new { success = true, Message = "Tạo đề thi thành công !" }, JsonRequestBehavior.AllowGet);
            }
            
        }
        /// <summary>
        /// Quản lý đề thi của tôi
        /// </summary>
        /// <param name="sortOrder"></param>
        /// <param name="CurrentSort"></param>
        /// <param name="page"></param>
        /// <param name="titleStr"></param>
        /// <returns></returns>
        public ViewResult MyQuizTest(string sortOrder, string CurrentSort, int? page, string titleStr)
        {
            int pageSize = 100;
            int pageIndex = 1;
            ViewBag.Sort = "tăng dần";
            ViewBag.CurrentSort = sortOrder;
            sortOrder = String.IsNullOrEmpty(sortOrder) ? "Title" : sortOrder;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<QuizTestViewModel> lstQuiz = null;
            var id = db.Users.Where(e => e.username == User.Identity.Name).First().ID;
            if (String.IsNullOrWhiteSpace(titleStr))
            {
                switch (sortOrder)
                {
                    case "title":
                        lstQuiz = db.QuizTests.Where(e => e.CreatorID == id && e.status != TestStatusAd.Deleted && e.Lesson.Status != LessonStatus.IsDelete).OrderBy(e => e.name).Select(q =>
                        new QuizTestViewModel
                        {
                            status = (TestStatus)q.status,
                            TestID = q.TestID,
                            LessonName = q.Lesson.Name,
                            name = q.name,
                            CreatorName = q.Creator.username,
                            CreateDate = q.CreateDate,
                        }
                        ).ToPagedList(pageIndex, pageSize);
                        break;
                    case "createdate":
                        lstQuiz = db.QuizTests.Where(e => e.CreatorID == id && e.status != TestStatusAd.Deleted && e.Lesson.Status != LessonStatus.IsDelete).OrderBy(e => e.CreateDate).Select(q =>
                        new QuizTestViewModel
                        {
                            status = (TestStatus)q.status,
                            TestID = q.TestID,
                            LessonName = q.Lesson.Name,
                            name = q.name,
                            CreatorName = q.Creator.username,
                            CreateDate = q.CreateDate,
                        }
                        ).ToPagedList(pageIndex, pageSize);
                        break;
                    default:
                        lstQuiz = db.QuizTests.Where(e => e.CreatorID == id && e.status != TestStatusAd.Deleted && e.Lesson.Status != LessonStatus.IsDelete).OrderBy(e => e.name).Select(q =>
                        new QuizTestViewModel
                        {
                            status = (TestStatus)q.status,
                            TestID = q.TestID,
                            LessonName = q.Lesson.Name,
                            name = q.name,
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
                            lstQuiz = db.QuizTests.Where(t => t.name.Contains(titleStr) && t.CreatorID == id && t.status != TestStatusAd.Deleted && t.Lesson.Status != LessonStatus.IsDelete).OrderBy(e => e.name).Select(q =>
                              new QuizTestViewModel
                              {
                                  status = (TestStatus)q.status,
                                  TestID = q.TestID,
                                  LessonName = q.Lesson.Name,
                                  name = q.name,
                                  CreatorName = q.Creator.username,
                                  CreateDate = q.CreateDate,
                              }
                            ).ToPagedList(pageIndex, pageSize);
                        }
                        else
                        {
                            lstQuiz = db.QuizTests.Where(t => t.name.Contains(titleStr) && t.CreatorID == id && t.status != TestStatusAd.Deleted && t.Lesson.Status != LessonStatus.IsDelete).OrderByDescending(e => e.name).Select(q =>
                           new QuizTestViewModel
                           {
                               status = (TestStatus)q.status,
                               TestID = q.TestID,
                               name = q.name,
                               LessonName = q.Lesson.Name,
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
                            lstQuiz = db.QuizTests.Where(t => t.name.Contains(titleStr) && t.CreatorID == id && t.status != TestStatusAd.Deleted && t.Lesson.Status != LessonStatus.IsDelete).OrderBy(e => e.CreateDate).Select(q =>
                            new QuizTestViewModel
                            {
                                status = (TestStatus)q.status,
                                TestID = q.TestID,
                                LessonName = q.Lesson.Name,
                                name = q.name,
                                CreatorName = q.Creator.username,
                                CreateDate = q.CreateDate,
                            }
                            ).ToPagedList(pageIndex, pageSize);
                        }
                        else
                        {
                            lstQuiz = db.QuizTests.Where(t => t.name.Contains(titleStr) && t.CreatorID == id && t.status != TestStatusAd.Deleted && t.Lesson.Status != LessonStatus.IsDelete).OrderByDescending(e => e.CreateDate).Select(q =>
                            new QuizTestViewModel
                            {
                                status = (TestStatus)q.status,
                                TestID = q.TestID,
                                name = q.name,
                                LessonName = q.Lesson.Name,
                                CreatorName = q.Creator.username,
                                CreateDate = q.CreateDate,
                            }
                            ).ToPagedList(pageIndex, pageSize);
                        }
                        break;
                    default:
                        lstQuiz = db.QuizTests.Where(t => t.name.Contains(titleStr) && t.CreatorID == id).OrderBy(e => e.name).Select(q =>
                        new QuizTestViewModel
                        {
                            status = (TestStatus)q.status,
                            TestID = q.TestID,
                            LessonName = q.Lesson.Name,
                            name = q.name,
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
        /// Tất cả đề thi (admin)
        /// </summary>
        /// <param name="sortOrder"></param>
        /// <param name="CurrentSort"></param>
        /// <param name="page"></param>
        /// <param name="titleStr"></param>
        /// <returns></returns>
        [Authorize(Roles = "admin")]
        public ViewResult AllQuizTest(string sortOrder, string CurrentSort, int? page, string titleStr){
            int pageSize = 100;
            int pageIndex = 1;
            ViewBag.Sort = "tăng dần";
            ViewBag.CurrentSort = sortOrder;
            sortOrder = String.IsNullOrEmpty(sortOrder) ? "Title" : sortOrder;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<QuizTestViewModelAd> lstQuiz = null;
            var id = Session["UserID"]; //db.Users.Where(e => e.username == User.Identity.Name).First().ID;
            if (String.IsNullOrWhiteSpace(titleStr))
            {
                switch (sortOrder)
                {
                    case "title":
                        lstQuiz = db.QuizTests.Where(e=>e.status != TestStatusAd.Deleted && e.Lesson.Status != LessonStatus.IsDelete).OrderBy(e => e.name).Select(q =>
                        new QuizTestViewModelAd
                        {
                            status = q.status,
                            TestID = q.TestID,
                            LessonName = q.Lesson.Name,
                            name = q.name,
                            CreatorName = q.Creator.username,
                            CreateDate = q.CreateDate,
                        }
                        ).ToPagedList(pageIndex, pageSize);
                        break;
                    case "createdate":
                        lstQuiz = db.QuizTests.Where(e => e.status != TestStatusAd.Deleted && e.Lesson.Status != LessonStatus.IsDelete).OrderBy(e => e.CreateDate).Select(q =>
                        new QuizTestViewModelAd
                        {
                            status = q.status,
                            TestID = q.TestID,
                            LessonName = q.Lesson.Name,
                            name = q.name,
                            CreatorName = q.Creator.username,
                            CreateDate = q.CreateDate,
                        }
                        ).ToPagedList(pageIndex, pageSize);
                        break;
                    default:
                        lstQuiz = db.QuizTests.Where(e => e.status != TestStatusAd.Deleted && e.Lesson.Status != LessonStatus.IsDelete).OrderBy(e => e.name).Select(q =>
                        new QuizTestViewModelAd
                        {
                            status = q.status,
                            TestID = q.TestID,
                            name = q.name,
                            LessonName = q.Lesson.Name,
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
                            lstQuiz = db.QuizTests.Where(t => t.name.Contains(titleStr) && t.status != TestStatusAd.Deleted && t.Lesson.Status != LessonStatus.IsDelete).OrderBy(e => e.name).Select(q =>
                          new QuizTestViewModelAd
                          {
                              status = q.status,
                              TestID = q.TestID,
                              name = q.name,
                              LessonName = q.Lesson.Name,
                              CreatorName = q.Creator.username,
                              CreateDate = q.CreateDate,
                          }
                            ).ToPagedList(pageIndex, pageSize);
                        }
                        else
                        {
                            lstQuiz = db.QuizTests.Where(t => t.name.Contains(titleStr) && t.status != TestStatusAd.Deleted && t.Lesson.Status != LessonStatus.IsDelete).OrderByDescending(e => e.name).Select(q =>
                            new QuizTestViewModelAd
                            {
                                status = q.status,
                                TestID = q.TestID,
                                name = q.name,
                                LessonName = q.Lesson.Name,
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
                            lstQuiz = db.QuizTests.Where(t => t.name.Contains(titleStr) && t.status != TestStatusAd.Deleted && t.Lesson.Status != LessonStatus.IsDelete).OrderBy(e => e.CreateDate).Select(q =>
                            new QuizTestViewModelAd
                            {
                                status = q.status,
                                TestID = q.TestID,
                                name = q.name,
                                LessonName = q.Lesson.Name,
                                CreatorName = q.Creator.username,
                                CreateDate = q.CreateDate,
                            }
                            ).ToPagedList(pageIndex, pageSize);
                        }
                        else
                        {
                            lstQuiz = db.QuizTests.Where(t => t.name.Contains(titleStr) && t.status != TestStatusAd.Deleted && t.Lesson.Status != LessonStatus.IsDelete).OrderByDescending(e => e.CreateDate).Select(q =>
                            new QuizTestViewModelAd
                            {
                                status = q.status,
                                TestID = q.TestID,
                                name = q.name,
                                LessonName = q.Lesson.Name,
                                CreatorName = q.Creator.username,
                                CreateDate = q.CreateDate,
                            }
                            ).ToPagedList(pageIndex, pageSize);
                        }
                        break;
                    default:
                        lstQuiz = db.QuizTests.Where(t => t.name.Contains(titleStr) && t.status != TestStatusAd.Deleted && t.Lesson.Status != LessonStatus.IsDelete).OrderBy(e => e.name).Select(q =>
                        new QuizTestViewModelAd
                        {
                            status = q.status,
                            TestID = q.TestID,
                            name = q.name,
                            LessonName = q.Lesson.Name,
                            CreatorName = q.Creator.username,
                            CreateDate = q.CreateDate,
                        }
                        ).ToPagedList(pageIndex, pageSize);
                        break;
                }
            }
            return View(lstQuiz);
        }
        [HttpGet]
        [Authorize(Roles ="admin,teacher")]
        public ActionResult Edit(int id)
        {
            var CurrentID = Session["UserID"];
            try
            {
                var exist = db.QuizTests.Any(e => e.TestID == id);
                if (!exist)
                {
                    return RedirectToAction("MyQuizTest");
                }
                QuizTest test = db.QuizTests.Find(id);
                //Chỉ có admin và chủ nhân của bài test mới có thể sửa, còn lại bị redirect
                //Bài thi đã xóa cũng ko xem được
                if ((test.CreatorID != (int)CurrentID && User.IsInRole("teacher")) || test.status == TestStatusAd.Deleted)
                {
                    return RedirectToAction("MyQuizTest");
                }
                QuizTestViewModel quiz = new QuizTestViewModel
                {
                    name = test.name,
                    status = (TestStatus)test.status,
                    TestID = test.TestID,
                    Lessons = Common.Helper.getLessonItem(),
                    TotalMark = test.TotalMark,
                    TotalTime = (TimeQuiz)test.TotalTime,
                };
                ViewBag.id = test.TestID;
                return View(quiz);
            }
            catch
            {
                return RedirectToAction("MyQuizTest");
            }
        }
    
        [Authorize(Roles = "admin,teacher")]
        [HttpPost]
        public JsonResult Edit(QuizTestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, Message = "Bạn nhập thiếu các trường yêu cầu" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                User u = db.Users.Where(i => i.username == User.Identity.Name).First();
                var quiztest = db.QuizTests.Where(x => x.TestID == model.LessonId).SingleOrDefault();
                quiztest.CreatorID = u.ID;
                quiztest.Creator = u;
                quiztest.CreateDate = DateTime.Now;
                quiztest.LessonId = model.LessonId;
                quiztest.TotalMark = model.TotalMark;
                quiztest.name = model.name;
                quiztest.TotalTime = (int)model.TotalTime;
                quiztest.status = (TestStatusAd)model.status;
                foreach (var item in model.quizID)
                {
                    Quiz q = db.Quizzes.Find(item);
                    quiztest.Quiz.Add(q);
                }
                db.SaveChanges();
                return Json(new { success = true, Message = "Cập nhật bài tập thành công !" }, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// Lấy danh sách câu hỏi từ đề thi
        /// </summary>
        /// <param name="testid"></param>
        /// <returns></returns>
        public  JsonResult GetQuizFromTest(int testid)
        {
            QuizTest test = db.QuizTests.Find(testid);
            List<QuizViewModel> quizzes = test.Quiz.Select(
                c => new QuizViewModel
                {
                    ID = c.QuizID,
                    HardType = c.HardType,
                    name = c.name,
                }).ToList();
            return Json(quizzes);
        }
        /// <summary>
        /// Tìm câu hỏi
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="name"></param>
        /// <param name="hard"></param>
        /// <returns></returns>
        public JsonResult SearchQuiz(int? subject, string name, HardType? hard)
        {
            try
            {
                List<QuizSearchViewModel> lst = null;
                if (String.IsNullOrWhiteSpace(name))
                {
                    if (hard.HasValue)
                    {
                        lst = db.Quizzes.Where(i =>  i.HardType == hard && i.LessonId == subject && i.status == QuizStatusAd.Active).Take(30).Select(a => new QuizSearchViewModel
                        {
                            HardType = a.HardType,
                            id = a.QuizID,
                            Name = a.name,
                        }).ToList();
                    }
                }
                else
                {
                    if (hard.HasValue)
                    {
                        lst = db.Quizzes.Where(i =>  i.HardType == hard && i.name.Contains(name) && i.LessonId == subject && i.status == QuizStatusAd.Active).Take(30).Select(a => new QuizSearchViewModel
                        {
                            HardType = a.HardType,
                            id = a.QuizID,
                            Name = a.name,
                        }).ToList();
                    }
                    else
                    {
                        lst = db.Quizzes.Where(i =>  i.name.Contains(name) && i.LessonId == subject && i.status == QuizStatusAd.Active).Take(30).Select(a => new QuizSearchViewModel
                        {
                            HardType = a.HardType,
                            id = a.QuizID,
                            Name = a.name,
                        }).ToList();
                    }
                }

                return Json(new { Message = "Thành công", QuizList = lst }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                Response.StatusCode = 500;
                return Json(new { Message = "Lỗi hệ thống" }, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// Xóa đề thi
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteQuizTest(int id)
        {
            User u = db.Users.Where(t => t.username == User.Identity.Name).First();
            QuizTest q = db.QuizTests.Find(id);
            if (q != null || User.IsInRole("admin"))
            {
                string title = q.name;
                q.status = TestStatusAd.Deleted;
                db.SaveChanges();
                return Json(new { Message = "Xóa \"" + title + "\" thành công" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Message = "Hack thành công, chúc mừng :>" }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Đổi trạng thái
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ChangeStatus(int id, TestStatusAd state = TestStatusAd.Active)
        {
            User u = db.Users.Where(t => t.username == User.Identity.Name).First();
            QuizTest q = db.QuizTests.Find(id);
            if (q.CreatorID == u.ID || User.IsInRole("admin"))
            {
                string title = q.name;
                q.status = state;
                string prefix = state == TestStatusAd.Active ? "Đăng" : "Hủy đăng";
                db.SaveChanges();
                return Json(new { Message = prefix + " \"" + title + "\" thành công" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Message = "Hack thành công, chúc mừng :>" }, JsonRequestBehavior.AllowGet);
        }
    }
}