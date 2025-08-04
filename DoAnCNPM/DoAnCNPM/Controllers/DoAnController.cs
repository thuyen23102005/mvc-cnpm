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
        StoreEntities db = new StoreEntities();

        public ActionResult DoAn()
        {
            // 1. Lấy dữ liệu 
            var danhSachDoAn = db.Products
                .Where(p => p.Category.NameCate == "Mì"
                         || p.Category.NameCate == "Bánh Bao"
                         || p.Category.NameCate == "Bánh Mì"
                         || p.Category.NameCate == "Xôi"
                         || p.Category.NameCate == "Tráng Miệng")
                .ToList(); 

            return View(danhSachDoAn);
        }
    }
}