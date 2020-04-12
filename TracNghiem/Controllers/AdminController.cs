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
                        MaxScore = c.ActiveTest.QuizTest.TotalMark,
                        LessonName = c.ActiveTest.QuizTest.Lesson.Name
                    })
                    .ToList();
                return View("StudentDashboard",resultList);
            }
            if (User.IsInRole("teacher"))
            {
                ViewBag.countQuiz = db.Quizzes.Count(c => c.CreatorID == UserID && c.status != QuizStatusAd.Deleted);
                ViewBag.countTest = db.QuizTests.Count(c => c.CreatorID == UserID);
                ViewBag.countRoom = db.ActiveTests.Count(c => c.CreatorID == UserID);
                return View("TeacherDashboard");
            }
            ViewBag.countQuiz = db.Quizzes.Count(c => c.status != QuizStatusAd.Deleted);
            ViewBag.countTest = db.QuizTests.Count();
            ViewBag.countRoom = db.ActiveTests.Count();
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
                           LessonName = c.ActiveTest.QuizTest.Lesson.Name
                       }).ToList();
                ViewBag.TestName = db.ActiveTests.Where(c => c.ID == roomID).First().QuizTest.name;
                return View(resultList);
            }
            catch
            {
                return RedirectToAction("Index");
            }
            
        }
    }
}