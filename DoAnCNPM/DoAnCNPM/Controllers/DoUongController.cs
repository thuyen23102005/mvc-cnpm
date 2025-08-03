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
        private StoreEntities db = new StoreEntities();
        // GET: DoUong
        public ActionResult DoUong()
        {
            // Lấy dữ liệu từ DB
            var danhSachDoUong = db.Products
                .Where(p => p.Category.NameCate == "Cafe Việt Nam"
                         || p.Category.NameCate == "Sản phẩm Nestlé"
                         || p.Category.NameCate == "Thức uống pha chế")
                .Select(p => new DoUong
                {
                    Id = p.ProductID,
                    Ten = p.NamePro,
                    HinhAnh = p.ImagePro,
                    DanhMuc = p.Category.NameCate,
                    Gia = (decimal)p.Price,
                    MoTa = p.DecriptionPro
                })
                .ToList();

            return View(danhSachDoUong);
        }
    }
}