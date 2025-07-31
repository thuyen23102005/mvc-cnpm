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
                new DoUong {Id = 1, Ten = "PhaPhin Cà Phê Phin Đen Đá", HinhAnh = "~/Content/Image/phaphin_den.png", DanhMuc = "Cafe Việt Nam", Gia = 25000, MoTa = "Cà phê đậm vị truyền thống" },
                new DoUong {Id = 2, Ten = "PhaPhin Cà Phê Phin Sữa Đá", HinhAnh = "~/Content/Image/phaphin_sua.png", DanhMuc = "Cafe Việt Nam",Gia = 22000, MoTa = "Cà phê sửa đậm vị truyền thống" },
                new DoUong {Id = 3, Ten = "PhaPhin Sữa Tươi Cà Phê", HinhAnh = "~/Content/Image/phaphin_tuoi.png", DanhMuc = "Cafe Việt Nam",Gia = 21000, MoTa = "Cà phê sửa đậm vị truyền thống" },
                new DoUong {Id = 4, Ten = "PhaTea Trà Sữa Thái (Đỏ)", HinhAnh = "~/Content/Image/phatea_do.png", DanhMuc = "Thức uống pha chế",Gia = 22000, MoTa = "Trà sửa đậm vị đỏ" },
                new DoUong {Id = 5, Ten = "Milo", HinhAnh = "~/Content/Image/nestle_milo.png", DanhMuc = "Sản phẩm Nestlé", Gia = 12000, MoTa = "Thức uống đậm vị lúa mạch"},
                new DoUong {Id = 6, Ten = "PhaTea Trà Chanh", HinhAnh = "~/Content/Image/phatea.png", DanhMuc = "Thức uống pha chế", Gia = 10000, MoTa = "Thức uống đậm vị chanh"},
                new DoUong {Id = 7, Ten = "PhaTea Trà Sữa Thái (Xanh)", HinhAnh = "~/Content/Image/phatea_xanh.png", DanhMuc = "Thức uống pha chế", Gia = 11000, MoTa = "Trà sửa đậm vị xanh"},
            };

            return View(danhSach);
        }
    }
}