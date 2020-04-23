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
    public class UserController : Controller
    {
        private QuizContext db = new QuizContext();
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
        public ActionResult Manage()
        {
            var lst = (from a in db.Users
                       select new UserViewModel
                       {
                           ID = a.ID,
                           gender = a.gender,
                           AvatarImage = a.AvatarImage != null ? a.AvatarImage : "default-avatar.jpg",
                           fullname = a.fullname,
                           status = a.status,
                           role = a.role,
                           type = a.type,
                           username = a.username,
                           CountQuestion = a.Quizzes.Count,
                           CountTest = a.QuizTests.Count,
                       }).ToList();
            return View(lst);
        }
        
        [Authorize(Roles = "admin")]
        public JsonResult ChangeStatus(int userid,UserStatus status)
        {
            try
            {
                string prefix = "";
                switch (status)
                {
                    case UserStatus.Activated:
                        prefix = "Đã kích hoạt";
                        break;
                    case UserStatus.Blocked:
                        prefix = "Đã block";
                        break;
                    case UserStatus.Deleted:
                        prefix = "Đã xóa";
                        break;
                    case UserStatus.NotActivated:
                        prefix = "Đã bỏ kích hoạt";
                        break;
                }
                User u = db.Users.Find(userid);
                if (u.username != "admin")
                {
                    u.status = status;
                    db.SaveChanges();
                    return Json(new { Message = prefix + " \"" + u.username + "\"" }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { Message = "Không được cấm admin" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                Response.StatusCode = 500;
                return Json(new { Message = "Lỗi hệ thống" }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}