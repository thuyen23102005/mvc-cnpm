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
                new DoAn { TenMonAn = "Mì Trộn", HinhAnh = "~/Content/Images/mi_tron.png", LoaiMonAn = "Mi" },
                new DoAn { TenMonAn = "Mì Trộn Indome", HinhAnh = "~/Content/Images/mi_tron_indo.png", LoaiMonAn = "Mi" },
                new DoAn { TenMonAn = "Mì Trộn thập cẩm", HinhAnh = "~/Content/Images/mi_tron_thap_cam.png", LoaiMonAn = "Mi" },
                new DoAn { TenMonAn = "Bánh Bao khoai môn", HinhAnh = "~/Content/Images/banh_bao_sweet.png", LoaiMonAn = "BanhBao" },
                new DoAn { TenMonAn = "Bánh Bao thịt", HinhAnh = "~/Content/Images/banh_bao_meaty.png", LoaiMonAn = "BanhBao" },
                new DoAn { TenMonAn = "Bánh Mì Xúc Xích", HinhAnh = "~/Content/Images/hot_dog.png", LoaiMonAn = "BanhMi" },
                new DoAn { TenMonAn = "Xôi lá chuối", HinhAnh = "~/Content/Images/xoi_la_chuoi.png", LoaiMonAn = "Xoi" },
                new DoAn { TenMonAn = "Chuối", HinhAnh = "~/Content/Images/banana.png", LoaiMonAn = "TrangMieng" },
            };

            return View(danhSachDoAn);
        }
    }
}