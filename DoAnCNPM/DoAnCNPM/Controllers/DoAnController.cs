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
        private StoreEntities db = new StoreEntities();

        public ActionResult DoAn()
        {
            // 1. Lấy dữ liệu thô từ DB (chưa xử lý chuỗi)
            var danhSachDoAn = db.Products
              .Where(p => p.Category.NameCate == "Mi"
                 || p.Category.NameCate == "BanhBao"
                 || p.Category.NameCate == "BanhMi"
                 || p.Category.NameCate == "Xoi"
                 || p.Category.NameCate == "TrangMieng")
             .Select(p => new
             {
                Id = p.ProductID,
                Ten = p.NamePro,
                HinhAnh = p.ImagePro,
                DanhMuc = p.Category.NameCate,
                Gia = (decimal)p.Price,
                MoTa = p.DecriptionPro
             })
            .ToList();

            // 2. Dùng LINQ trong bộ nhớ để xử lý tiếng Việt
            var doAnModels = danhSachDoAn.Select(p => new DoAn
            {
                Id = p.Id,
                Ten = p.Ten,
                HinhAnh = p.HinhAnh,
                Gia = p.Gia,
                MoTa = p.MoTa,
                DanhMuc = ConvertDanhMuc(p.DanhMuc)  // xử lý chuẩn tên danh mục
            }).ToList();

            return View(doAnModels);
        }

        private string ConvertDanhMuc(string nameCate)
        {
            if (nameCate == "Mì") return "Mi";
            if (nameCate == "Bánh Bao") return "BanhBao";
            if (nameCate == "Bánh Mì") return "BanhMi";
            if (nameCate == "Xôi") return "Xoi";
            if (nameCate == "Tráng Miệng") return "TrangMieng";
            return nameCate;
        }
    }
}