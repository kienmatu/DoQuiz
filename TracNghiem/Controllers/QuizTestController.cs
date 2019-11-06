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
    public class QuizTestController : Controller
    {
        private QuizContext db = new QuizContext();
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Tạo đề thi mới
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "admin,teacher")]
        public ActionResult Create()
        {
            var model = new QuizTestViewModel
            {
                Subject = Common.Helper.getSubjectItem(),
            };
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "admin,teacher")]
        public JsonResult Create(QuizTestViewModel model)
        {
            model.Subject = Common.Helper.getSubjectItem();
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, Message = "Bạn nhập thiếu các trường yêu cầu" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                User u = db.Users.Where(i => i.username == User.Identity.Name).First();

                QuizTest test = new QuizTest
                {
                    CreatorID = u.ID,
                    Creator = u,
                    CreateDate = DateTime.Now,
                    SubjectID = model.SubjectID,
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
                        lstQuiz = db.QuizTests.Where(e => e.CreatorID == id).OrderBy(e => e.name).Select(q =>
                        new QuizTestViewModel
                        {
                            status = (TestStatus)q.status,
                            TestID = q.TestID,
                            name = q.name,
                            SubjectName = q.Subject.name,
                            CreatorName = q.Creator.username,
                            CreateDate = q.CreateDate,
                        }
                        ).ToPagedList(pageIndex, pageSize);
                        break;
                    case "createdate":
                        lstQuiz = db.QuizTests.Where(e => e.CreatorID == id).OrderBy(e => e.CreateDate).Select(q =>
                        new QuizTestViewModel
                        {
                            status = (TestStatus)q.status,
                            TestID = q.TestID,
                            name = q.name,
                            SubjectName = q.Subject.name,
                            CreatorName = q.Creator.username,
                            CreateDate = q.CreateDate,
                        }
                        ).ToPagedList(pageIndex, pageSize);
                        break;
                    default:
                        lstQuiz = db.QuizTests.Where(e => e.CreatorID == id).OrderBy(e => e.name).Select(q =>
                        new QuizTestViewModel
                        {
                            status = (TestStatus)q.status,
                            TestID = q.TestID,
                            name = q.name,
                            SubjectName = q.Subject.name,
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
                            lstQuiz = db.QuizTests.Where(t => t.name.Contains(titleStr) && t.CreatorID == id).OrderBy(e => e.name).Select(q =>
                              new QuizTestViewModel
                              {
                                  status = (TestStatus)q.status,
                                  TestID = q.TestID,
                                  name = q.name,
                                  SubjectName = q.Subject.name,
                                  CreatorName = q.Creator.username,
                                  CreateDate = q.CreateDate,
                              }
                            ).ToPagedList(pageIndex, pageSize);
                        }
                        else
                        {
                            lstQuiz = db.QuizTests.Where(t => t.name.Contains(titleStr) && t.CreatorID == id).OrderByDescending(e => e.name).Select(q =>
                           new QuizTestViewModel
                           {
                               status = (TestStatus)q.status,
                               TestID = q.TestID,
                               name = q.name,
                               SubjectName = q.Subject.name,
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
                            lstQuiz = db.QuizTests.Where(t => t.name.Contains(titleStr) && t.CreatorID == id).OrderBy(e => e.CreateDate).Select(q =>
                            new QuizTestViewModel
                            {
                                status = (TestStatus)q.status,
                                TestID = q.TestID,
                                name = q.name,
                                SubjectName = q.Subject.name,
                                CreatorName = q.Creator.username,
                                CreateDate = q.CreateDate,
                            }
                            ).ToPagedList(pageIndex, pageSize);
                        }
                        else
                        {
                            lstQuiz = db.QuizTests.Where(t => t.name.Contains(titleStr) && t.CreatorID == id).OrderByDescending(e => e.CreateDate).Select(q =>
                            new QuizTestViewModel
                            {
                                status = (TestStatus)q.status,
                                TestID = q.TestID,
                                name = q.name,
                                SubjectName = q.Subject.name,
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
                            name = q.name,
                            SubjectName = q.Subject.name,
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
                        lstQuiz = db.QuizTests.OrderBy(e => e.name).Select(q =>
                        new QuizTestViewModelAd
                        {
                            status = q.status,
                            TestID = q.TestID,
                            name = q.name,
                            SubjectName = q.Subject.name,
                            CreatorName = q.Creator.username,
                            CreateDate = q.CreateDate,
                        }
                        ).ToPagedList(pageIndex, pageSize);
                        break;
                    case "createdate":
                        lstQuiz = db.QuizTests.OrderBy(e => e.CreateDate).Select(q =>
                        new QuizTestViewModelAd
                        {
                            status = q.status,
                            TestID = q.TestID,
                            name = q.name,
                            SubjectName = q.Subject.name,
                            CreatorName = q.Creator.username,
                            CreateDate = q.CreateDate,
                        }
                        ).ToPagedList(pageIndex, pageSize);
                        break;
                    default:
                        lstQuiz = db.QuizTests.OrderBy(e => e.name).Select(q =>
                        new QuizTestViewModelAd
                        {
                            status = q.status,
                            TestID = q.TestID,
                            name = q.name,
                            SubjectName = q.Subject.name,
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
                            lstQuiz = db.QuizTests.Where(t => t.name.Contains(titleStr)).OrderBy(e => e.name).Select(q =>
                          new QuizTestViewModelAd
                          {
                              status = q.status,
                              TestID = q.TestID,
                              name = q.name,
                              SubjectName = q.Subject.name,
                              CreatorName = q.Creator.username,
                              CreateDate = q.CreateDate,
                          }
                            ).ToPagedList(pageIndex, pageSize);
                        }
                        else
                        {
                            lstQuiz = db.QuizTests.Where(t => t.name.Contains(titleStr)).OrderByDescending(e => e.name).Select(q =>
                            new QuizTestViewModelAd
                            {
                                status = q.status,
                                TestID = q.TestID,
                                name = q.name,
                                SubjectName = q.Subject.name,
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
                            lstQuiz = db.QuizTests.Where(t => t.name.Contains(titleStr)).OrderBy(e => e.CreateDate).Select(q =>
                            new QuizTestViewModelAd
                            {
                                status = q.status,
                                TestID = q.TestID,
                                name = q.name,
                                SubjectName = q.Subject.name,
                                CreatorName = q.Creator.username,
                                CreateDate = q.CreateDate,
                            }
                            ).ToPagedList(pageIndex, pageSize);
                        }
                        else
                        {
                            lstQuiz = db.QuizTests.Where(t => t.name.Contains(titleStr)).OrderByDescending(e => e.CreateDate).Select(q =>
                            new QuizTestViewModelAd
                            {
                                status = q.status,
                                TestID = q.TestID,
                                name = q.name,
                                SubjectName = q.Subject.name,
                                CreatorName = q.Creator.username,
                                CreateDate = q.CreateDate,
                            }
                            ).ToPagedList(pageIndex, pageSize);
                        }
                        break;
                    default:
                        lstQuiz = db.QuizTests.Where(t => t.name.Contains(titleStr)).OrderBy(e => e.name).Select(q =>
                        new QuizTestViewModelAd
                        {
                            status = q.status,
                            TestID = q.TestID,
                            name = q.name,
                            SubjectName = q.Subject.name,
                            CreatorName = q.Creator.username,
                            CreateDate = q.CreateDate,
                        }
                        ).ToPagedList(pageIndex, pageSize);
                        break;
                }
            }
            return View(lstQuiz);
        }
        [Authorize(Roles = "admin,teacher")]
        public ActionResult Edit(int id)
        {
            var CurrentID = Session["UserID"];
            try
            {
                var exist = db.QuizTests.Any(e => e.TestID == id);
                if(!exist)
                {
                    return RedirectToAction("MyQuizTest");
                }
                QuizTest test = db.QuizTests.Find(id);
                //Chỉ có admin và chủ nhân của bài test mới có thể sửa, còn lại bị redirect
                //Bài thi đã xóa cũng ko xem được
                if((test.CreatorID != (int)CurrentID && User.IsInRole("teacher")) || test.status == TestStatusAd.Deleted)
                {
                    return RedirectToAction("MyQuizTest");
                }
                QuizTestViewModel quiz = new QuizTestViewModel
                {
                    name = test.name,
                    Subject = Common.Helper.getSubjectItem(),
                    SubjectID = test.SubjectID,
                    status = (TestStatus)test.status,
                    TestID = test.TestID,
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
        public JsonResult SearchQuiz(int subject, string name = null, HardType? hard = null)
        {
            try
            {
                List<QuizSearchViewModel> lst = null;
                if (String.IsNullOrWhiteSpace(name))
                {
                    if (hard.HasValue)
                    {
                        lst = db.Quizzes.Where(i => i.SubjectID == subject && i.HardType == hard).Take(30).Select(a => new QuizSearchViewModel
                        {
                            HardType = a.HardType,
                            id = a.QuizID,
                            Name = a.name,
                        }).ToList();
                    }
                    else
                    {
                        lst = db.Quizzes.Where(i => i.SubjectID == subject).Take(30).Select(a => new QuizSearchViewModel
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
                        lst = db.Quizzes.Where(i => i.SubjectID == subject && i.HardType == hard && i.name.Contains(name)).Take(30).Select(a => new QuizSearchViewModel
                        {
                            HardType = a.HardType,
                            id = a.QuizID,
                            Name = a.name,
                        }).ToList();
                    }
                    else
                    {
                        lst = db.Quizzes.Where(i => i.SubjectID == subject && i.name.Contains(name)).Take(30).Select(a => new QuizSearchViewModel
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
            if (q.CreatorID == u.ID || User.IsInRole("admin"))
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