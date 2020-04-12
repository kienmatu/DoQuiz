using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TracNghiem.Models;
using TracNghiem.ViewModel;

namespace TracNghiem.Controllers
{
    public class LessonController : Controller
    {
        QuizContext db = new QuizContext();
        // GET: Lesson
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "admin,teacher")]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "admin,teacher")]
        public ActionResult Create(LessonViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    HttpPostedFileBase file = Request.Files[0];
                    string fileExt = Path.GetExtension(file.FileName).ToUpper();
                    if (file.ContentLength > 0 && fileExt == ".PDF")
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        model.File = Path.Combine(Server.MapPath("~/UploadedFiles"), fileName);
                        file.SaveAs(model.File);
                        var user = db.Users.Where(t => t.username == User.Identity.Name).First();
                        Lesson l = new Lesson()
                        {
                            Name = model.Name,
                            Time = model.Time,
                            CreatorID = user.ID,
                            File = model.File,
                            YoutubeLink = model.YoutubeLink,
                            CreatedDate = DateTime.Now,
                            Description = model.Description,
                            CreatedBy = user.fullname,
                            Status = LessonStatus.Close
                        };
                        db.Lessons.Add(l);
                        db.SaveChanges();
                        return RedirectToAction("MyLesson");
                    }
                }
                catch (Exception error)
                {
                    ModelState.AddModelError("", "Tệp tài liệu không được trống");
                }
                 
            }
            return View("Create");
        }
        [HttpGet]
        [Authorize(Roles ="admin,teacher")]
        public ActionResult Edit(int id)
        {
            var model = db.Lessons.Where(x => x.ID == id).SingleOrDefault();
            if (model != null)
            {
                LessonViewModel lesson = new LessonViewModel()
                {
                    Name = model.Name,
                    Time = model.Time,
                    File = model.File,
                    Description = model.Description,                    
                    YoutubeLink = model.YoutubeLink
                };
                return View(model);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        [Authorize(Roles ="admin,teacher")]
        public ActionResult Edit(LessonViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Where(t => t.username == User.Identity.Name).First();
                Lesson l = db.Lessons.Where(x => x.ID == model.Id).SingleOrDefault();
                if (l != null)
                {
                    l.Name = model.Name;
                    l.Time = model.Time;
                    l.Description = model.Description;
                    l.File = model.File;
                    l.YoutubeLink = model.YoutubeLink;
                    l.ModifiedBy = user.fullname;
                    l.ModifiedDate = DateTime.Now;
                    db.Lessons.Add(l);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }
        [HttpGet]
        [Authorize(Roles ="admin,teacher")]
        public ActionResult AllMyLesson()
        {
            return View();
        }
    }
}