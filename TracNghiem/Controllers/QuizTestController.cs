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
        public ActionResult Create(QuizTestViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                model.Subject = Common.Helper.getSubjectItem();
                return View(model);
            }
            
        }

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
    }
}