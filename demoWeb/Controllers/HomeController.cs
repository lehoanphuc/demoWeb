using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace demoWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Book()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Menu() 
        {
            ViewBag.Message = "You menu page";
            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Message = "Your Form Login";
            return View();
        }

        public ActionResult Cart()
        {
            ViewBag.Message = "Your Form Login";
            return View();
        }

        public ActionResult TinhThanh()
        {
            ViewBag.Message = "Your Form Login";
            return View();
        }
    }
}