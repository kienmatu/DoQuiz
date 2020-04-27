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
            if (User.IsInRole("student"))
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
                
                ViewBag.countQuiz = db.Quizzes.Count(c => c.CreatorID == UserID && c.status != QuizStatusAd.Deleted 
                && c.lesson.Status == LessonStatus.Open);
                ViewBag.countTest = db.QuizTests.Count(c => c.CreatorID == UserID && c.Lesson.Status == LessonStatus.Open);
                ViewBag.countLesson = db.Lessons.Count(c => c.CreatorID == UserID && c.Status != LessonStatus.IsDelete);
                ViewBag.countActiveTest = db.ActiveTests.Count(c => c.CreatorID == UserID && c.QuizTest.Lesson.Status == LessonStatus.Open 
                && c.IsActive ==true && c.QuizTest.status != TestStatusAd.Deleted && c.QuizTest.status != TestStatusAd.NotActive);
                return View("TeacherDashboard");
            }
            ViewBag.countQuiz = db.Quizzes.Count(c=> c.status != QuizStatusAd.Deleted && c.lesson.Status != LessonStatus.IsDelete);
            ViewBag.countTest = db.QuizTests.Count(c=>c.status !=TestStatusAd.Deleted && c.Lesson.Status != LessonStatus.IsDelete);
            ViewBag.countLesson = db.Lessons.Count(c=>c.Status != LessonStatus.IsDelete);
            ViewBag.countActiveTest = db.ActiveTests.Count(c=>c.QuizTest.Lesson.Status == LessonStatus.Open && c.IsActive == true 
            && c.QuizTest.status != TestStatusAd.Deleted && c.QuizTest.status != TestStatusAd.NotActive);
            return View();
        }
        [Authorize(Roles ="teacher")]
        public ActionResult TestResult()
        {
            var resultList = db.QuizResults
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
            return View(resultList);
        }
    }
}