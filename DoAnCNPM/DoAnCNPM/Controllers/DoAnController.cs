using DoAnCNPM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnCNPM.Controllers
{
    public class DoAnController : Controller
    {
        public ActionResult DoAn()
        {
            var danhSachDoAn = new List<DoAn>
            {
               new DoAn { Id = 1, Ten = "Mì Trộn", HinhAnh = "~/Content/Image/mi_tron.png", DanhMuc = "Mi", Gia = 22000, MoTa = "Mì trộn cay đặc biệt" },
               new DoAn { Id = 2, Ten = "Mì Trộn Indome", HinhAnh = "~/Content/Image/mi_tron_indo.png", DanhMuc = "Mi", Gia = 25000, MoTa = "Mì Indome thơm ngon" },
               new DoAn { Id = 3, Ten = "Mì Trộn thập cẩm", HinhAnh = "~/Content/Image/mi_tron_thap_cam.png", DanhMuc = "Mi", Gia = 28000, MoTa = "Mì với xúc xích, trứng và rau củ" },
               new DoAn { Id = 4, Ten = "Bánh Bao khoai môn", HinhAnh = "~/Content/Image/banh_bao_sweet.png", DanhMuc = "BanhBao", Gia = 15000, MoTa = "Bánh bao nhân khoai môn ngọt" },
               new DoAn { Id = 5, Ten = "Bánh Bao thịt", HinhAnh = "~/Content/Image/banh_bao_meaty.png", DanhMuc = "BanhBao", Gia = 18000, MoTa = "Bánh bao nhân thịt đậm đà" },
               new DoAn { Id = 6, Ten = "Bánh Mì Xúc Xích", HinhAnh = "~/Content/Image/hot_dog.png", DanhMuc = "BanhMi", Gia = 20000, MoTa = "Bánh mì với xúc xích nóng" },
               new DoAn { Id = 7, Ten = "Xôi lá chuối", HinhAnh = "~/Content/Image/xoi_la_chuoi.png", DanhMuc = "Xoi", Gia = 12000, MoTa = "Xôi bắp gói lá chuối" },
               new DoAn { Id = 8, Ten = "Chuối", HinhAnh = "~/Content/Image/banana.png", DanhMuc = "TrangMieng", Gia = 8000, MoTa = "Chuối tươi sạch" },
            };

            return View(danhSachDoAn);
        }
    }
}