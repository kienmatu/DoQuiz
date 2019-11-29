using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TracNghiem.Models;

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
    }
}