﻿
using demoWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace demoWeb.Controllers
{
    public class HomeController : Controller
    {
        MyDataDataContext data = new MyDataDataContext();
 
        public ActionResult Index(int ? page)
        {
            if (page == null) page = 1;
            var all_sanPham = (from tt in data.SANPHAMs select tt).OrderBy(m => m.MASANPHAM);
            int pageSize = 6;
            int pageNum = page ?? 1;
            return View(all_sanPham.ToPagedList(pageNum, pageSize));

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
            var all_sanPham = from tt in data.SANPHAMs select tt;
            return View(all_sanPham);
            
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(FormCollection collection, KHACHHANG kh, TAIKHOAN tk)
        {
            var TenKH = collection["TENKHACHHANG"];
            var username = collection["USERNAME"];
            var pass = collection["PASS"];
            var Gmail = collection["GMAIL"];
            var sdt = collection["SDT"];
            var GioiTinh = collection["GIOITINH"];
            var NamSinh = string.Format("{0:dd/MM/yyyy}", collection["NAMSINH"]);
            var DiaChi = collection["DIACHI"];

            if (String.IsNullOrEmpty(TenKH))
            {
                ViewData["Error1"] = "Họ tên khách hàng không được để trống";
            }
            else if (String.IsNullOrEmpty(username))
            {
                ViewData["Error2"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(pass))
            {
                ViewData["Error3"] = "Phải nhập mật khẩu";
            }
            if (String.IsNullOrEmpty(Gmail))
            {
                ViewData["Error4"] = "Email không được để trống";
            }
            if (String.IsNullOrEmpty(sdt))
            {
                ViewData["Error5"] = "Điện thoại không được để trống";
            }
            if (String.IsNullOrEmpty(GioiTinh))
            {
                ViewData["Error6"] = "Giới tính không được để trống";
            }
            if (String.IsNullOrEmpty(NamSinh))
            {
                ViewData["Error7"] = "Ngày sinh không được để trống";
            }
            if (String.IsNullOrEmpty(DiaChi))
            {
                ViewData["Error8"] = "Địa chỉ không được để trống";
            }
            
            else
            {
                //Gán giá trị cho đổi tượng được tạo mới (kh)
                kh.TENKHACHHANG = TenKH;
                kh.GMAIL = Gmail;
                kh.DIACHI = DiaChi;
                kh.SDT = sdt;
                kh.NAMSINH = DateTime.Parse(NamSinh);
                tk.USERNAME = username;
                tk.PASS = pass;

                data.KHACHHANGs.InsertOnSubmit(kh);
                data.SubmitChanges();
                data.TAIKHOANs.InsertOnSubmit(tk);
                data.SubmitChanges();
                return RedirectToAction("Login");
            }
            return this.Register();
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