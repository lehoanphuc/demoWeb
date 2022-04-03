using demoWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace demoWeb.Controllers
{
    public class SanPhamController : Controller
    {
        MyDataDataContext data = new MyDataDataContext();
        // GET: SanPham
        public ActionResult Nikon()
        {
            List<SANPHAM> Nikon = data.SANPHAMs.Where(p => p.LOAIMAY.MANCC.Equals("2")).ToList();
            return View(Nikon);
        }

        public ActionResult Canon()
        {
            List<SANPHAM> Canon = data.SANPHAMs.Where(p => p.LOAIMAY.MANCC.Equals("1")).ToList();
            return View(Canon);
        }
        public ActionResult Sony()
        {
            List<SANPHAM> Sony = data.SANPHAMs.Where(p => p.LOAIMAY.MANCC.Equals("3")).ToList();
            return View(Sony);
        }
        public ActionResult Lens()
        {
            List<SANPHAM> Lens = data.SANPHAMs.Where(p => p.TENSANPHAM.Contains("Lens")).ToList();
            return View(Lens);
        }
    }
}