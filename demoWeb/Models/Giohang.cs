﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using demoWeb.Models;

namespace demoWeb.Models
{
    public class Giohang
    {
        MyDataDataContext data = new MyDataDataContext();
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public string Hinh { get; set; }
        public double GiaSP { get; set; }
        public int SoLuong { get; set; }
        public double ThanhTien
        {
            get { return SoLuong * GiaSP; }
        }
        public Giohang(int id)
        {
            MaSP = id;
            SANPHAM sanpham = data.SANPHAMs.Single(n => n.MASANPHAM == MaSP);
            TenSP = sanpham.TENSANPHAM;
            Hinh = sanpham.HINH;
            GiaSP = double.Parse(sanpham.GIABAN.ToString());
            SoLuong = 1;
        }
    }
}