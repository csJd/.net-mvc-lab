using MVCLabs.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace MVCLabs.Controllers
{
    public class Lab5Controller : Controller
    {
        MovieDbC db = new MovieDbC();
        // GET: Lab5
        public ActionResult Index(string mgenre = "", string mtitle = "")
        {
            //根据数据库中所有电影类型动态生成下拉框 验收时的要求还是做出来吧
            var genreSet = new SortedSet<string>();  //set添加时会自动去重
            var genreQry = from d in db.Movies select d.Genre;
            foreach (string s in genreQry)
            {
                var slt = s.Split('/');
                foreach (string ss in slt)
                    genreSet.Add(ss.Trim());  //去掉首尾空白字符
            }
            ViewBag.mgenre = new SelectList(genreSet); //动态返回类型下拉框数据

            // 以下代码实现查找
            var movies = from m in db.Movies
                         where m.Title.Contains(mtitle) && m.Genre.Contains(mgenre)
                         select m;
            return View(movies);
            //return View(db.Movies.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  //防CSRF攻击
        public ActionResult Create(Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        public ActionResult Edit(int? id)
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
        public ActionResult Edit(Movie movie)
        {
            if (ModelState.IsValid)   //服务端验证
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


        public ActionResult Details(int? id)
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
    }
}