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
    public class AdminController : Controller
    {
        QuizContext db = new QuizContext();
        public ActionResult Index()
        {
            int UserID = (int)Session["UserID"];
            if(User.IsInRole("student"))
            {
                var resultList = db.QuizResults
                    .Where(c => c.StudentID == UserID)
                    .Select(c => new QuizResultViewModel
                    {
                        Name = c.ActiveTest.QuizTest.name,
                        SubmitDate = c.DoneAt,
                        Score = c.Score,
                        Status = c.Status,
                        SubjectName = c.ActiveTest.QuizTest.Subject.name,
                    }).ToList();
                return View("StudentDashboard",resultList);
            }
            return View();
        }
        public ActionResult TestResult(int? roomID)
        {
            if(roomID == null)
            {
                return RedirectToAction("Index");
            }
            try
            {
                var resultList = db.QuizResults
                       .Where(c => c.ActiveTestID == roomID)
                       .Select(c => new QuizResultViewModel
                       {
                           StudentName = c.Student.fullname,
                           Name = c.ActiveTest.QuizTest.name,
                           SubmitDate = c.DoneAt,
                           Score = c.Score,
                           Status = c.Status,
                           MaxScore = c.ActiveTest.QuizTest.TotalMark,
                           //SubjectName = c.ActiveTest.QuizTest.Subject.name,
                       }).ToList();
                return View(resultList);
            }
            catch
            {
                return RedirectToAction("Index");
            }
            
        }
    }
}