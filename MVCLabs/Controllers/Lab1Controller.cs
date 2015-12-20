using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCLabs.Controllers
{
    public class Lab1Controller : Controller
    {
        // GET: Lab1
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Show()
        {
            return View();
        }

        public string CheckUsername(string usrn)
        {
            if (string.IsNullOrEmpty(usrn))
                return "用户名不能为空";
            if (usrn.Equals("wustzz"))
                return "用户名已被占用";
            return "true";
        }
    }
}