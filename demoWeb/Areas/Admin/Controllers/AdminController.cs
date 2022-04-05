using demoWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace demoWeb.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        MyDataDataContext data = new MyDataDataContext();
        // GET: Admin/Admin
        public ActionResult ListSP()
        {
            var all_SP = from ss in data.SANPHAMs select ss;
            return View(all_SP);
        }
        public ActionResult Detail(int id)
        {
            var D_SanPham = data.SANPHAMs.Where(m => m.MASANPHAM == id).First();
            return View(D_SanPham);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, SANPHAM s)
        {
            var E_TenSP = collection["TENSANPHAM"];
            var E_giaban = Convert.ToDecimal(collection["GIABAN"]);
            var E_GiaGiam = Convert.ToDecimal(collection["GIAGIAM"]);
            var E_ThongSo = collection["THONGSOKYTHUAT"];
            var E_BaoHanh = Convert.ToInt32(collection["BAOHANH"]);
            var E_Hinh = collection["HINH"];
            var E_SoLuongTon = Convert.ToInt32(collection["SOLUONGTON"]);
            var E_MoTa = collection["MOTASANPHAM"];
            var E_MaLoai = Convert.ToInt32(collection["MALOAI"]);
            if (string.IsNullOrEmpty(E_TenSP))
            {
                ViewData["Error"] = "Tên sản phẩm không được bỏ trống";
            }
            else
            {
                s.TENSANPHAM = E_TenSP.ToString();
                s.GIABAN = E_giaban;
                s.GIAGIAM = (double?)E_GiaGiam;
                s.THONGSOKYTHUAT = E_ThongSo.ToString();
                s.BAOHANH = E_BaoHanh;
                s.HINH = E_Hinh.ToString();
                s.SOLUONGTON = E_SoLuongTon;
                s.MOTASANPHAM = E_MoTa.ToString();
                s.MALOAI = E_MaLoai;
                data.SANPHAMs.InsertOnSubmit(s);
                data.SubmitChanges();
                return RedirectToAction("ListSP");
            }
            return this.Create();
        }
        public ActionResult Edit(int id)
        {
            var E_SanPham = data.SANPHAMs.First(m => m.MASANPHAM == id);
            return View(E_SanPham);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var E_SanPham = data.SANPHAMs.First(m => m.MASANPHAM == id);
            var E_TenSP = collection["TENSANPHAM"];
            var E_giaban = Convert.ToDecimal(collection["GIABAN"]);
            var E_GiaGiam = Convert.ToDecimal(collection["GIAGIAM"]);
            var E_ThongSo = collection["THONGSOKYTHUAT"];
            var E_BaoHanh = Convert.ToInt32(collection["BAOHANH"]);
            var E_Hinh = collection["HINH"];
            var E_SoLuongTon = Convert.ToInt32(collection["SOLUONGTON"]);
            var E_MoTa = collection["MOTASANPHAM"];
            var E_MaLoai = Convert.ToInt32(collection["MALOAI"]);
            E_SanPham.MASANPHAM = id;
            if (string.IsNullOrEmpty(E_TenSP))
            {
                ViewData["Error"] = "Tên sản phẩm không được bỏ trống";
            }
            else
            {
                E_SanPham.TENSANPHAM = E_TenSP;
                E_SanPham.GIABAN = E_giaban;
                E_SanPham.GIAGIAM = (double?)E_GiaGiam;
                E_SanPham.THONGSOKYTHUAT = E_ThongSo;
                E_SanPham.BAOHANH = E_BaoHanh;
                E_SanPham.HINH = E_Hinh;
                E_SanPham.SOLUONGTON = E_SoLuongTon;
                E_SanPham.MOTASANPHAM = E_MoTa;
                E_SanPham.MALOAI = E_MaLoai;
                UpdateModel(E_SanPham);
                data.SubmitChanges();
                return RedirectToAction("ListSP");
            }
            return this.Edit(id);
        }
        public ActionResult Delete(int id)
        {
            var D_SanPham = data.SANPHAMs.First(m => m.MASANPHAM == id);
            return View(D_SanPham);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var D_SanPham = data.SANPHAMs.Where(m => m.MASANPHAM == id).First();
            data.SANPHAMs.DeleteOnSubmit(D_SanPham);
            data.SubmitChanges();
            return RedirectToAction("ListSP");
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
                TAIKHOAN kh = data.TAIKHOANs.SingleOrDefault(n => n.USERNAME == tendn && n.PASS == matkhau && n.QUYEN == 0);
                if (kh != null)
                {
                    Session["TaiKhoan"] = kh;
                    return RedirectToAction("Dashboard", "Admin");
                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }
        public ActionResult Dashboard()
        {
            return View();
        }
        
    }
}