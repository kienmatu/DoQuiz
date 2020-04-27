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
    public class DoQuizController : Controller
    {
        QuizContext db = new QuizContext();
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "teacher,admin")]
        public ViewResult MyActiveTest()
        {
            int UserID = (int)Session["UserID"];
            List<ActiveTest> activeTests = db.ActiveTests.Where(a => a.CreatorID == UserID).ToList();
            return View(activeTests);
        }
        /// <summary>
        /// Tạo phòng thi AJAX
        /// </summary>
        /// <param name="QuizTestID"></param>
        /// <returns></returns>
        public JsonResult CreateActiveTest(int QuizTestID)
        {
            var model = db.ActiveTests.Where(x => x.QuizTestID == QuizTestID).SingleOrDefault();
            if(model == null)
            {
                ActiveTest test = new ActiveTest
                {
                    CreatorID = (int)Session["UserID"],
                    QuizTestID = QuizTestID,
                    IsActive = true,
                };
                db.ActiveTests.Add(test);
                db.SaveChanges();
                return Json(new { Message = "Tạo thành công", Success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Message = "Bài tập đã trùng", Success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// Vào phòng thi
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles ="student")]
        public JsonResult EnterRoom(int id)
        {
            int UserID = (int)Session["UserID"];
            ActiveTest test = db.ActiveTests.Where(c => c.QuizTest.LessonId == id).FirstOrDefault();
            if(test != null)
            {
                DoTestViewModel model = new DoTestViewModel
                {
                    RoomID = test.ID,
                    LessonId = test.QuizTest.LessonId,
                    TestName = test.QuizTest.name,
                    LessonName = test.QuizTest.Lesson.Name,
                    TotalMark = test.QuizTest.TotalMark,
                    TotalTime = test.QuizTest.TotalTime,
                    QuizList = test.QuizTest.Quiz.Select(c => new ShowQuiz
                    {
                        answerA = c.answerA,
                        Content = c.content,
                        answerC = c.answerC,
                        answerB = c.answerB,
                        answerD = c.answerD,
                        ID = c.QuizID
                    }),
                };
                return Json(new { Quiz = model, Success = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new {Success = false }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult OpenRoom(int id)
        {
            var model = db.QuizTests.Where(x => x.LessonId == id && x.status == TestStatusAd.Active).SingleOrDefault();
            var test = db.ActiveTests.Where(x=>x.IsActive == true).SingleOrDefault();
            if (model != null && test != null)
            {
                var lesson = db.Lessons.Where(x => x.ID == id).Single();
                LessonViewModel result = new LessonViewModel
                {
                    Id = lesson.ID,
                    Name = lesson.Name
                };
                return View(result);
            }
            else
            {
                return RedirectToAction("NotFoundQuizTest");
            }
            
        }
        public ViewResult NotFoundQuizTest() 
        {
            return View();
        } 
        /// <summary>
        /// Bắt đầu bài thi
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult StartExam(int id)
        {
            int UserID = (int)Session["UserID"];
            ActiveTest test = db.ActiveTests.Where(c => c.QuizTest.LessonId == id && c.IsActive == true).First();
            var result = db.QuizResults.Where(x => x.StudentID == UserID && x.ActiveTestID == test.ID).SingleOrDefault();
            if(result == null)
            {
                QuizResult qr = new QuizResult
                {
                    DoneAt = DateTime.Now,
                    Score = 0,
                    Status = DoQuizStatus.Doing,
                    StudentID = (int)Session["UserID"],
                    ActiveTestID = test.ID,
                };
                db.QuizResults.Add(qr);
                db.SaveChanges();
                return Json(new { Message = "Start doing exam", Success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Message = "Start doing exam", Success = true }, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// Nộp bài thi
        /// </summary>
        /// <param name="id"></param>
        /// <param name="answerList"></param>
        /// <returns></returns>
        [Authorize(Roles = "student")]
        public JsonResult SubmitExam(int id,List<SubmitExamViewModel> answerList)
        {
            ActiveTest test = db.ActiveTests.Where(c => c.QuizTest.LessonId == id && c.IsActive == true).FirstOrDefault();
            List<Quiz> quiz = test.QuizTest.Quiz.ToList();
            double score = 0;
            double trueAnswer = 0;
            int countQuestion = quiz.Count();
            double maxScore = test.QuizTest.TotalMark;
            double scorePerQuestion = maxScore / quiz.Count();
            if(answerList == null || answerList.Count == 0)
            {
                return Json(new { score = score, maxScore = maxScore, trueAnswer = trueAnswer, success = true }, JsonRequestBehavior.AllowGet);
            }
            foreach (var item in quiz)
            {
                foreach(var answer in answerList)
                {
                    if(item.QuizID == answer.QuizID)
                    {
                        if (answer.Answer != SubmitAnswer.NotCheck)
                        {
                            if (answer.Answer == (SubmitAnswer)item.trueAnswer)
                            {
                                trueAnswer++;
                            }
                        }
                    }
                }
            }
            int UserID = (int)Session["UserID"];
            QuizResult result = db.QuizResults.Where(c => c.ActiveTestID == test.ID && c.StudentID == UserID).SingleOrDefault();
            if (trueAnswer > 0)
            {
                score = scorePerQuestion * trueAnswer;
                //update điểm                
                result.Score = score;
                result.DoneAt = DateTime.Now;
                result.Status = DoQuizStatus.Finished;
                db.SaveChanges();
            }
            else
            {
                score = 0;
                //update điểm
                result.Score = score;
                result.DoneAt = DateTime.Now;
                result.Status = DoQuizStatus.Finished;
                db.SaveChanges();
            }
            return Json(new { score = score,maxScore =  maxScore, trueAnswer = trueAnswer , success = true }, JsonRequestBehavior.AllowGet);
        }

    }
}