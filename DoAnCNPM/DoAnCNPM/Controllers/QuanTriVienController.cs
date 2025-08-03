using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace DoAnCNPM.Controllers
{
    public class QuanTriVienController : Controller
    {
        DoAnCNPM.Models.StoreEntities db = new DoAnCNPM.Models.StoreEntities();

        // Dashboard
        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                TempData["ReturnUrl"] = Request.Url.ToString();
                return RedirectToAction("Login", "Account");
            }

            ViewBag.SectionTitle = "Dashboard";
            return View();
        }

        // Quản lý người dùng
        public ActionResult Users()
        {
            ViewBag.SectionTitle = "Quản lý người dùng";
            return View("Index");
        }

        // Quản lý sản phẩm
        public ActionResult Products()
        {
            if (Session["UserName"] == null)
            {
                TempData["ReturnUrl"] = Request.Url.ToString();
                return RedirectToAction("Login", "Account");
            }

            ViewBag.SectionTitle = "Index";
            var list = db.Products.Include(p => p.Category).ToList();
            return View("~/Views/Products/Index.cshtml", list);
        }
    }
}