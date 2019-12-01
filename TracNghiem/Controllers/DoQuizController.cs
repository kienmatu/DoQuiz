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
        /// <param name="Code"></param>
        /// <param name="FromTime"></param>
        /// <param name="ToTime"></param>
        /// <returns></returns>
        public JsonResult CreateActiveTest(int QuizTestID,string Code,DateTime FromTime,DateTime ToTime)
        {
            bool ExistCode = db.ActiveTests.Any(a => a.Code == Code);
            if(ExistCode)
            {
                return Json(new { Message = "Trùng mã phòng thi", Success = false }, JsonRequestBehavior.AllowGet);
            }
            ActiveTest test = new ActiveTest
            {
                CreatorID = (int)Session["UserID"],
                Code = Code,
                QuizTestID = QuizTestID,
                FromTime = FromTime,
                ToTime = ToTime,
                IsActive = true,
            };
            db.ActiveTests.Add(test);
            db.SaveChanges();
            return Json(new { Message = "Tạo thành công", Success = true }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Vào phòng thi
        /// </summary>
        /// <param name="roomCode"></param>
        /// <returns></returns>
        [Authorize(Roles ="student")]
        public JsonResult EnterRoom(string roomCode)
        {
            bool ExistCode = db.ActiveTests.Any(a => a.Code == roomCode);
            if (!ExistCode)
            {
                return Json(new { Message = "Sai mã phòng thi", Success = false }, JsonRequestBehavior.AllowGet);
            }
            ActiveTest test = db.ActiveTests.Where(c => c.Code == roomCode).First();
            //chỉ join vào thời gian cho phép
            if (test.FromTime <= DateTime.Now && test.ToTime >= DateTime.Now)
            {
                DoTestViewModel model = new DoTestViewModel
                {
                    RoomID = test.ID,
                    RoomCode = test.Code,
                    SubjectName = test.QuizTest.Subject.name,
                    TestName = test.QuizTest.name,
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
            return Json(new { Message = "Sai mã phòng thi", Success = false }, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "student")]
        public ViewResult OpenRoom()
        {
            return View();
        }
        /// <summary>
        /// Bắt đầu bài thi
        /// </summary>
        /// <param name="roomCode"></param>
        /// <returns></returns>
        public JsonResult StartExam(string roomCode)
        {
            ActiveTest test = db.ActiveTests.Where(c => c.Code == roomCode).First();
            if(test.ToTime >= DateTime.Now)
            {
                return Json(new { Message = "Start doing exam", Success = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Message = "Sai mã phòng thi hoặc hết hạn", Success = false }, JsonRequestBehavior.AllowGet);
        }

    }
}