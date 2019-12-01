using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TracNghiem.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            if(User.IsInRole("student"))
            {
                return View("StudentDashboard");
            }
            return View();
        }
    }
}