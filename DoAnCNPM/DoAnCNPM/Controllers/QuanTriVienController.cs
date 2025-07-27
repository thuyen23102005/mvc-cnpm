using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnCNPM.Controllers
{
    public class QuanTriVienController : Controller
    {
        // GET: QuanTriVien
        public ActionResult Index()
        {
            ViewBag.SectionTitle = "Dashboard";
            return View();
        }

        public ActionResult Users()
        {
            ViewBag.SectionTitle = "Quản lý người dùng";
            return View("Index");
        }

        public ActionResult Products()
        {
            ViewBag.SectionTitle = "Quản lý sản phẩm";
            return View("Index");
        }
    }
}