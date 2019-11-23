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

        [Authorize(Roles = "teacher")]
        public ViewResult MyActiveTest()
        {
            List<ActiveTest> activeTests = db.ActiveTests.ToList();
            return View(activeTests);
        }
    }
}