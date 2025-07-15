using DoAnCNPM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnCNPM.Controllers
{
    public class DoUongController : Controller
    {
        // GET: DoUong
        public ActionResult DoUong()
        {
            var danhSach = new List<DoUong>
            {
                new DoUong { Id = 1, Ten = "PhaPhin Cà Phê Phin Đen Đá", HinhAnh = "~/Content/Image/phaphin_den.png", DanhMuc = "Cafe Việt Nam" },
                new DoUong { Id = 2, Ten = "PhaPhin Cà Phê Phin Sữa Đá", HinhAnh = "~/Content/Image/phaphin_sua.png", DanhMuc = "Cafe Việt Nam" },
                new DoUong { Id = 3, Ten = "PhaPhin Sữa Tươi Cà Phê", HinhAnh = "~/Content/Image/phaphin_tuoi.png", DanhMuc = "Sản phẩm Nestlé" },
                new DoUong { Id = 4, Ten = "PhaTea Trà Sữa Thái (Đỏ)", HinhAnh = "~/Content/Image/trathai_do.png", DanhMuc = "Thức uống pha chế" },
                new DoUong { Id = 5, Ten = "Milo", HinhAnh = "~/Content/Image/milo.png", DanhMuc = "Sản phẩm Nestlé" },
                new DoUong { Id = 6, Ten = "PhaTea Trà Chanh", HinhAnh = "~/Content/Image/trachanh.png", DanhMuc = "Thức uống pha chế" },
                new DoUong { Id = 7, Ten = "PhaTea Trà Sữa Thái (Xanh)", HinhAnh = "~/Content/Image/trathai_xanh.png", DanhMuc = "Thức uống pha chế" },
            };
            return View(danhSach);
        }
    }
}