using demoWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace demoWeb.Areas.Admin.Controllers
{
    public class NhanVienController : Controller
    {
        MyDataDataContext data = new MyDataDataContext();
        // GET: Admin/NhanVien
        public ActionResult ListNV()
        {
            var all_NV = from ss in data.NHANVIENs select ss;
            return View(all_NV);
        }
        public ActionResult DetailNV(int id)
        {
            var D_NhanVien = data.NHANVIENs.Where(m => m.MANV == id).First();
            return View(D_NhanVien);
        }
        public ActionResult CreateNV()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateNV(FormCollection collection, NHANVIEN s, TAIKHOAN tk)
        {
            var E_TenNV = collection["TENNV"];
            var E_SDT = collection["SDT"];
            var E_GioiTinh = Convert.ToInt32(collection["GIOITINH"]);
            var E_DiaChi = collection["DIACHI"];
            var E_TrangThai = Convert.ToInt32(collection["TRANGTHAI"]);
            var E_Username = collection["USERNAME"];
            var E_Quyen = Convert.ToInt32(collection["QUYEN"]);
            var E_Pass = collection["PASS"];
            if (String.IsNullOrEmpty(E_TenNV))
            {
                ViewData["Error1"] = "Họ tên nhân viên không được để trống";
            }
            else if (String.IsNullOrEmpty(E_SDT))
            {
                ViewData["Error2"] = "Phải nhập số điện thoại";
            }
            if (String.IsNullOrEmpty(E_DiaChi))
            {
                ViewData["Error3"] = "Phải nhập địa chỉ";
            }
            if (String.IsNullOrEmpty(E_Username))
            {
                ViewData["Error4"] = "Username không được để trống";
            }
            if (String.IsNullOrEmpty(E_Pass))
            {
                ViewData["Error5"] = "Mật khẩu không được để trống";
            }
            else
            {
                s.TENNV = E_TenNV.ToString();
                s.SDT = E_SDT.ToString();
                s.GIOITINH = E_GioiTinh;
                s.DIACHI = E_DiaChi.ToString();
                s.TRANGTHAI = E_TrangThai;
                tk.USERNAME = E_Username.ToString();
                tk.QUYEN = E_Quyen;
                tk.PASS = E_Pass.ToString();
                data.NHANVIENs.InsertOnSubmit(s);
                data.SubmitChanges();
                data.TAIKHOANs.InsertOnSubmit(tk);
                data.SubmitChanges();
                return RedirectToAction("ListNV");
            }
            return this.CreateNV();
        }
        public ActionResult EditNV(int id)
        {
            var E_NhanVien = data.NHANVIENs.First(m => m.MANV == id);
            return View(E_NhanVien);
        }
        [HttpPost] 
        public ActionResult EditNV(int id, FormCollection collection)
        {
            var E_NhanVien = data.NHANVIENs.First(m => m.MANV == id);
            var E_TKNV = data.TAIKHOANs.First(m => m.MANV == id);
            var E_TenNV = collection["TENNV"];
            var E_SDT = collection["SDT"];
            var E_GioiTinh = Convert.ToInt32(collection["GIOITINH"]);
            var E_DiaChi = collection["DIACHI"];
            var E_TrangThai = Convert.ToInt32(collection["TRANGTHAI"]);
            var E_Quyen = Convert.ToInt32(collection["QUYEN"]);
            var E_Pass = collection["PASS"];
            E_NhanVien.MANV = id;
            E_TKNV.MANV = id;
            if (string.IsNullOrEmpty(E_TenNV))
            {
                ViewData["Error"] = "Tên nhân viên không được bỏ trống";
            }
            else
            {
                E_NhanVien.TENNV = E_TenNV;
                E_NhanVien.SDT = E_SDT;
                E_NhanVien.GIOITINH = E_GioiTinh;
                E_NhanVien.DIACHI = E_DiaChi;
                E_NhanVien.TRANGTHAI = E_TrangThai;
                E_TKNV.QUYEN = E_Quyen;
                E_TKNV.PASS = E_Pass;
                UpdateModel(E_NhanVien);
                data.SubmitChanges();
                UpdateModel(E_TKNV);
                data.SubmitChanges();
                return RedirectToAction("ListNV");
            }
            return this.EditNV(id);
        }
        public ActionResult DeleteNV(int id)
        {
            var D_NhanVien = data.NHANVIENs.First(m => m.MANV == id);
            return View(D_NhanVien);
        }
        [HttpPost]
        public ActionResult DeleteNV(int id, FormCollection collection)
        {
            var D_NhanVien = data.NHANVIENs.Where(m => m.MANV == id).First();
            data.NHANVIENs.DeleteOnSubmit(D_NhanVien);
            data.SubmitChanges();
            return RedirectToAction("ListNV");
        }
    }
}