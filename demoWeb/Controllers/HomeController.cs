
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace demoWeb.Controllers
{
    public class HomeController : Controller
    {
        MyDataDataContext data = new MyDataDataContext();
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
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var tendn = collection["USERNAME"];
            var matkhau = collection["PASS"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Error1"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Error2"] = "Phải nhập mật khẩu";
            }
            else
            {
                // Gán giá trị cho đối tượng được tạo mới(kh)
                TAIKHOAN kh = data.TAIKHOANs.SingleOrDefault(n => n.USERNAME == tendn && n.PASS == matkhau);
                if (kh != null)
                {
                    Session["TaiKhoan"] = kh;
                    return RedirectToAction("Index", "Home");
                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
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