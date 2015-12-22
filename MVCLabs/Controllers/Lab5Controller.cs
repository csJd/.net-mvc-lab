using MVCLabs.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MVCLabs.Controllers
{
    public class Lab5Controller : Controller
    {
        MovieDbC db = new MovieDbC();
        // GET: Lab5
        public ActionResult Index()
        {
            return View(db.Movies.ToList());
        }
        [HttpPost]
        public ActionResult Index(string mtitle, string mgenre)
        {
            if (mtitle == null) mtitle = "";
            if (mgenre == null) mgenre = "";
            var movies = from m in db.Movies where m.Title.Contains(mtitle) && m.Genre.Contains(mgenre) select m;
            return View(movies);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  //防CSRF攻击
        public ActionResult Create(Movie movie)
        {
            if(ModelState.IsValid)
            {
                db.Movies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if(movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  //防CSRF攻击
        public ActionResult Edit(Movie movie)
        {
            if(ModelState.IsValid)   //服务端验证
            {
                db.Entry(movie).State = EntityState.Modified; //修改状态 不能少
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  //防CSRF攻击
        public ActionResult Delete(int id)
        {
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Details(int?  id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }
    }
}