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
                new DoAn { TenMonAn = "Mì Trộn", HinhAnh = "~/Content/Image/mi_tron.png", LoaiMonAn = "Mi" },
                new DoAn { TenMonAn = "Mì Trộn Indome", HinhAnh = "~/Content/Image/mi_tron_indo.png", LoaiMonAn = "Mi" },
                new DoAn { TenMonAn = "Mì Trộn thập cẩm", HinhAnh = "~/Content/Image/mi_tron_thap_cam.png", LoaiMonAn = "Mi" },
                new DoAn { TenMonAn = "Bánh Bao khoai môn", HinhAnh = "~/Content/Image/banh_bao_sweet.png", LoaiMonAn = "BanhBao" },
                new DoAn { TenMonAn = "Bánh Bao thịt", HinhAnh = "~/Content/Image/banh_bao_meaty.png", LoaiMonAn = "BanhBao" },
                new DoAn { TenMonAn = "Bánh Mì Xúc Xích", HinhAnh = "~/Content/Image/hot_dog.png", LoaiMonAn = "BanhMi" },
                new DoAn { TenMonAn = "Xôi lá chuối", HinhAnh = "~/Content/Image/xoi_la_chuoi.png", LoaiMonAn = "Xoi" },
                new DoAn { TenMonAn = "Chuối", HinhAnh = "~/Content/Image/banana.png", LoaiMonAn = "TrangMieng" },
            };

            return View(danhSachDoAn);
        }
    }
}