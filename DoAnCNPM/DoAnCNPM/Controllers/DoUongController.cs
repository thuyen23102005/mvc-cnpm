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
        StoreEntities db = new StoreEntities();
        // GET: DoUong
        public ActionResult DoUong()
        {
            // Lấy dữ liệu 
            var danhSachDoUong = db.Products
                .Where(p => p.Category.NameCate == "Cafe Việt Nam"
                         || p.Category.NameCate == "Sản phẩm Nestlé"
                         || p.Category.NameCate == "Thức uống pha chế")
                .ToList();

            return View(danhSachDoUong);
        }
    }
}