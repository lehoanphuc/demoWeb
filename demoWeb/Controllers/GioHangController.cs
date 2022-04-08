using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Windows;

namespace demoWeb.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
<<<<<<< Updated upstream
        public ActionResult Index()
        {
            return View();
        }
=======
        MyDataDataContext data = new MyDataDataContext();
        public List<GioHang> Laygiohang()
        {
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if (lstGiohang == null)
            {
                lstGiohang = new List<GioHang>();
                Session["GioHang"] = lstGiohang;
            }
            return lstGiohang;
        }

        public ActionResult ThemGioHang(int id, string strURL)
        {
            List<GioHang> lstGioHang = Laygiohang();
            GioHang sanpham = lstGioHang.Find(n => n.MaSP == id);
            if (sanpham == null)
            {
                sanpham = new GioHang(id);
                lstGioHang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.SoLuong++;
                return Redirect(strURL);
            }
            return RedirectToAction("GioHangPartial");
        }

        private int TongSoLuong()
        {
            int tsl = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                tsl = lstGioHang.Sum(n => n.SoLuong);
            }
            return tsl;
        }

        private int TongSoLuongSanPham()
        {
            int tsl = 0;
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if (lstGiohang != null)
            {
                tsl = lstGiohang.Count;
            }
            return tsl;
        }

        private decimal TongTien()
        {
            decimal tt = 0;
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if (lstGiohang != null)
            {
                tt = (decimal)lstGiohang.Sum(n => n.ThanhTien);
            }
            return tt;
        }

        public ActionResult GioHang()
        {
            List<GioHang> lstGioHang = Laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();
            ViewBag.Employee = "";

            return View(lstGioHang);
        }

        public ActionResult GioHangPartial()
        {
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();

            return PartialView();
        }

        public ActionResult XoaGioHang(int id)
        {
            List<GioHang> lstGiohang = Laygiohang();
            GioHang sanpham = lstGiohang.SingleOrDefault(n => n.MaSP == id);
            if (sanpham != null)
            {
                lstGiohang.RemoveAll(n => n.MaSP == id);
                return RedirectToAction("GioHang");
            }
            return RedirectToAction("GioHang");
        }

        public ActionResult CapNhapGioHang(int id, FormCollection collection)
        {
            SANPHAM sp = data.SANPHAMs.First(n => n.MASANPHAM == id);
            List<GioHang> lstGiohang = Laygiohang();
            GioHang sanpham = lstGiohang.SingleOrDefault(n => n.MaSP == id);
            if (sanpham != null)
            {
                if (int.Parse(collection["txtSoLg"].ToString()) > sp.SOLUONGTON)
                {
                    ViewBag.Employee = "Sai Rồi";
                    //ViewBag.ErrorMessage = "Số Lượng Nhập Quá Lớn";
                    //Session["ErrorMessage"] = "Số lượng lớn hơn số lượng tồn";
                    //Response.Write("<script>alert('Inserted successfully!')</script>");
                    MessageBox.Show("Số lượng nhập lớn hơn số lượng tồn");
                    return RedirectToAction("GioHang");
                }
                if (int.Parse(collection["txtSoLg"].ToString()) < 1)
                {
                    //ViewBag.Employee = "Sai Rồi";
                    //ViewBag.ErrorMessage = "Số Lượng Nhập Quá Lớn";
                    //Session["ErrorMessage"] = "Số lượng lớn hơn số lượng tồn";
                    MessageBox.Show("Số lượng nhập vào không được thấp hơn 1");
                    return RedirectToAction("GioHang");
                }
                else
                {
                    sanpham.SoLuong = int.Parse(collection["txtSoLg"].ToString());
                }
            }
            return RedirectToAction("GioHang");
        }



        public ActionResult XoaTatCaGioHang()
        {
            List<GioHang> lstGiohang = Laygiohang();
            lstGiohang.Clear();
            return RedirectToAction("GioHang");
        }


        [HttpGet]

        public ActionResult DatHang()
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("Login", "Home");
            }
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "ListSP");
            }
            List<GioHang> lstGioHang = Laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();
            return View(lstGioHang);
        }
        public ActionResult DatHang(FormCollection collection)
        {
            CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
            PHIEUMUA dh = new PHIEUMUA();
            TAIKHOAN tk = (TAIKHOAN)Session["TaiKhoan"];
            KHACHHANG kh = new KHACHHANG();
            SANPHAM s = new SANPHAM();
            List<GioHang> gh = Laygiohang();
            var ngaygiao = String.Format("{0:dd/MM/yyyy}", collection["NgayGiao"]);
            //if (DateTime.Parse(ngaygiao) < DateTime.Now)
            //{
            //    ViewData["Date"] = "Ngày giao phải lớn hơn ngày hiện tại!";
            //} 
            //else
            //{

            //}    
            dh.MAKH = kh.MAKH;
            dh.NGAYDAT = DateTime.Now;
            
            data.PHIEUMUAs.InsertOnSubmit(dh);
            data.SubmitChanges();
            foreach (var item in gh)
            {
                CT_PHIEUMUA ctdh = new CT_PHIEUMUA();
                ctdh.MAPHIEUMUA = dh.MAPHIEUMUA;
                ctdh.MASANPHAM = item.MaSP;
                ctdh.SOLUONG = item.SoLuong;
                //double thanhtien = TongTien();
                //decimal.Parse(TongTien().ToString()).ToString("#,###", cul.NumberFormat) +"đ";
                s = data.SANPHAMs.Single(n => n.MASANPHAM == item.MaSP);
                s.SOLUONGTON -= ctdh.SOLUONG;
                data.SubmitChanges();
                data.CT_PHIEUMUAs.InsertOnSubmit(ctdh);
                try
                {
                    if (ModelState.IsValid)
                    {

                        var senderEmail = new MailAddress("haunguyenaaaa6@gmail.com", "Cửa Hàng Sách Hutech");
                        var receiverEmail = new MailAddress(kh.GMAIL, "Receiver");
                        var password = "hau121pk";
                        var sub = "Xác Nhận Mua Hàng Thành Công";
                        var body = "Đơn hàng " + ctdh.MAPHIEUMUA + " đang được giao đến bạn \nCảm ơn bạn";
                        var smtp = new SmtpClient
                        {
                            Host = "smtp.gmail.com",
                            Port = 587,
                            EnableSsl = true,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential(senderEmail.Address, password)
                        };
                        using (var mess = new MailMessage(senderEmail, receiverEmail)
                        {
                            Subject = sub,
                            Body = body
                        })
                        {
                            smtp.Send(mess);
                        }

                    }
                }
                catch (Exception ex)
                {
                    return HttpNotFound();
                }
            }
            data.SubmitChanges();
            Session["GioHang"] = null;

            return RedirectToAction("XacNhanDonHang", "GioHang");
        }

        public ActionResult XacNhanDonHang()
        {
            return View();
        }

        public ActionResult ThanhToanMoMo(/*int id*/)
        {
            List<GioHang> gh = Laygiohang();

            CT_PHIEUMUA ctdh = new CT_PHIEUMUA();
            //request params need to request to MoMo system
            //string endpoint = "https://test-payment.momo.vn/v2/gateway/api/create";
            //string endpoint = "https://test-payment.momo.vn/gw_payment/transactionProcessor";
            //string partnerCode = "MOMO5RGX20191128";
            //string accessKey = "M8brj9K6E22vXoDB";
            //string serectkey = "nqQiVSgDMy809JoPF6OzP5OdBUB550Y4";
            //string orderInfo = "DH" + DateTime.Now.ToString("yyyyMMddHHss");
            //string redirectUrl = "https://webhook.site/b3088a6a-2d17-4f8d-a383-71389a6c600b";
            //string ipnUrl = "https://webhook.site/b3088a6a-2d17-4f8d-a383-71389a6c600b";
            //string requestType = "captureWallet";


            //string amount = ctdh.gia.ToString();
            //string orderId = Guid.NewGuid().ToString();
            //string requestId = Guid.NewGuid().ToString();
            //string extraData = "";


            //public ActionResult XoaGioHang(int id)
            //{
            //    List<GioHang> lstGiohang = Laygiohang();
            //    GioHang sanpham = lstGiohang.SingleOrDefault(n => n.MaSP == id);
            //    if (sanpham != null)
            //    {
            //        lstGiohang.RemoveAll(n => n.MaSP == id);
            //        return RedirectToAction("GioHang");
            //    }
            //    return RedirectToAction("GioHang");
            //}
            //   GioHang sanpham =gh.SingleOrDefault(n => n.MaSP == id);

            //   if (sanpham != null)
            //   {
            //       string endpoint = "https://test-payment.momo.vn/gw_payment/transactionProcessor";
            //       string partnerCode = "MOMOOJOI20210710";
            //       string accessKey = "iPXneGmrJH0G8FOP";
            //       string serectkey = "sFcbSGRSJjwGxwhhcEktCHWYUuTuPNDB";
            //       string orderInfo = "ĐH" + DateTime.Now.ToString("yyyyMMddHHss");
                   //string returnUrl = "https://localhost:44394/GioHang/GioHang";
            //       string notifyurl = "http://ba1adf48beba.ngrok.io/GioHang/SavePayment"; //lưu ý: notifyurl không được sử dụng localhost, có thể sử dụng ngrok để public localhost trong quá trình test
            //       string amount = "amount";
            //       //string amount = decimal.Parse(TongTien().ToString()).ToString(); 
            //       string orderid = DateTime.Now.Ticks.ToString();
            //       string requestId = DateTime.Now.Ticks.ToString();
            //       string extraData = "";


            //       string rawHash = "partnerCode=" +
            //partnerCode + "&accessKey=" +
            //accessKey + "&requestId=" +
            //requestId + "&amount=" +
            //amount + "&orderId=" +
            //orderid + "&orderInfo=" +
            //orderInfo + "&returnUrl=" +
            //returnUrl + "&notifyUrl=" +
            //notifyurl + "&extraData=" +
            //extraData;


            //       MoMoSecurity crypto = new MoMoSecurity();
            //       //sign signature SHA256
            //       string signature = crypto.signSHA256(rawHash, serectkey);

            //       //build body json request
            //       JObject message = new JObject
            //   {
            //       { "partnerCode", partnerCode },
            //       { "accessKey", accessKey },
            //       { "requestId", requestId },
            //       { "amount", amount },
            //       { "orderId", orderid },
            //       { "orderInfo", orderInfo },
            //       { "returnUrl", returnUrl },
            //       { "notifyUrl", notifyurl },
            //       { "extraData", extraData },
            //       { "requestType", "captureMoMoWallet" },
            //       { "signature", signature }

            //   };

            //       string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());

            //       JObject jmessage = JObject.Parse(responseFromMomo);

            //       return Redirect(jmessage.GetValue("payUrl").ToString());
            //   }
            //return RedirectToAction("GioHang");

            string endpoint = "https://test-payment.momo.vn/gw_payment/transactionProcessor";
            string partnerCode = "MOMOOJOI20210710";
            string accessKey = "iPXneGmrJH0G8FOP";
            string serectkey = "sFcbSGRSJjwGxwhhcEktCHWYUuTuPNDB";
            string orderInfo = "ĐH" + DateTime.Now.ToString("yyyyMMddHHss");
            string returnUrl = "https://localhost:44394/GioHang/GioHang";
            string notifyurl = "http://ba1adf48beba.ngrok.io/GioHang/SavePayment"; //lưu ý: notifyurl không được sử dụng localhost, có thể sử dụng ngrok để public localhost trong quá trình test
            string amount = TongTien().ToString();
            //string amount = decimal.Parse(TongTien().ToString()).ToString();
            string orderid = DateTime.Now.Ticks.ToString();
            string requestId = DateTime.Now.Ticks.ToString();
            string extraData = "";

            //Before sign HMAC SHA256 signature
            //string rawHash = "accessKey=" + accessKey +
            //    "&amount=" + amount +
            //    "&extraData=" + extraData +
            //    "&ipnUrl=" + ipnUrl +
            //    "&orderId=" + orderId +
            //    "&orderInfo=" + orderInfo +
            //    "&partnerCode=" + partnerCode +
            //    "&redirectUrl=" + redirectUrl +
            //    "&requestId=" + requestId +
            //    "&requestType=" + requestType
            //    ;


            string rawHash = "partnerCode=" +
         partnerCode + "&accessKey=" +
         accessKey + "&requestId=" +
         requestId + "&amount=" +
         amount + "&orderId=" +
         orderid + "&orderInfo=" +
         orderInfo + "&returnUrl=" +
         returnUrl + "&notifyUrl=" +
         notifyurl + "&extraData=" +
         extraData;

            ////log.Debug("rawHash = " + rawHash);

            //MoMoSecurity crypto = new MoMoSecurity();
            ////sign signature SHA256
            //string signature = crypto.signSHA256(rawHash, serectkey);
            ////log.Debug("Signature = " + signature);

            ////build body json request
            //JObject message = new JObject
            //{
            //    { "partnerCode", partnerCode },
            //    { "partnerName", "Test" },
            //    { "storeId", "MomoTestStore" },
            //    { "requestId", requestId },
            //    { "amount", amount },
            //    { "orderId", orderId },
            //    { "orderInfo", orderInfo },
            //    { "redirectUrl", redirectUrl },
            //    { "ipnUrl", ipnUrl },
            //    { "lang", "en" },
            //    { "extraData", extraData },
            //    { "requestType", requestType },
            //    { "signature", signature }

            //};
            //string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());

            //JObject jmessage = JObject.Parse(responseFromMomo);

            //return Redirect(jmessage.GetValue("payUrl").ToString());
            MoMoSecurity crypto = new MoMoSecurity();
            //sign signature SHA256
            string signature = crypto.signSHA256(rawHash, serectkey);

            //build body json request
            JObject message = new JObject
            {
                { "partnerCode", partnerCode },
                { "accessKey", accessKey },
                { "requestId", requestId },
                { "amount", amount },
                { "orderId", orderid },
                { "orderInfo", orderInfo },
                { "returnUrl", returnUrl },
                { "notifyUrl", notifyurl },
                { "extraData", extraData },
                { "requestType", "captureMoMoWallet" },
                { "signature", signature }

            };

            string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());

            JObject jmessage = JObject.Parse(responseFromMomo);

            return Redirect(jmessage.GetValue("payUrl").ToString());

            //return RedirectToAction("GioHang", "GioHang");

        }

        public ActionResult returnUrl()
        {
            //hiển thị thông báo cho người dùng
            return View();
        }

        [HttpPost]
        public void SavePayment()
        {
            //cập nhật dữ liệu vào db
        }
>>>>>>> Stashed changes
    }
}