﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TracNghiem.Common;
using TracNghiem.Models;
using TracNghiem.ViewModel;

namespace TracNghiem.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        private QuizContext db = new QuizContext();
        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }
        public ActionResult Login()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        public void setCookie(string username, bool rememberme = false, string role = "normal")
        {
            var authTicket = new FormsAuthenticationTicket(
                               1,
                               username,
                               DateTime.Now,
                               DateTime.Now.AddMinutes(120),
                               rememberme,
                               role
                               );

            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            Response.Cookies.Add(authCookie);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string ReturnUrl)
        {

            if (ModelState.IsValid)
            {
                var exist = db.Users.Any(e => e.username.Equals(model.Username));
                if (exist)
                {
                    var user = db.Users.Where(e => e.username.Equals(model.Username)).First();
                    if (user != null)
                    {
                        if (user.password == Helper.CalculateMD5Hash(model.Password) && (user.status == UserStatus.Activated || user.status == UserStatus.NotActivated))
                        {
                            setCookie(user.username, model.RememberMe, user.role);
                            if (ReturnUrl != null)
                                return Redirect(ReturnUrl);
                            return RedirectToAction("Index", "Home");
                        }
                        ViewBag.Error = "Sai tài khoản hoặc mật khẩu!";
                        return View();

                    }
                }
                
            }

            ViewBag.Error = "Sai tài khoản hoặc mật khẩu!";
            return View();
        }
        public ActionResult Register()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Admin");
            }
            return View();

        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var exist = db.Users.Any(e => e.username == model.username);
                if (exist)
                {
                    ViewBag.Error = "Tên người dùng " + model.username + " đã tồn tại";
                    return View();
                }
                exist = db.Users.Any(e => e.email == model.email);
                if (exist)
                {
                    ViewBag.Error = "Email " + model.email + " đã tồn tại";
                    return View();
                }
                User u = new User
                {
                    username = model.username,
                    password = Common.Helper.CalculateMD5Hash(model.password),
                    email = model.email,
                    gender = model.gender,
                    register_date = DateTime.Now,
                    role = "normal",
                    status = UserStatus.NotActivated,
                    fullname = model.fullname,
                    type = (UserType)model.type,
                };
                db.Users.Add(u);
                db.SaveChanges();
                FormsAuthentication.SignOut();
                setCookie(u.username, false, u.role);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}