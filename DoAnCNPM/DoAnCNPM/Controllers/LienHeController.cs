using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnCNPM.Controllers
{
    public class LienHeController : Controller
    {
        // GET: LienHe
        public ActionResult LienHe()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LienHe(string hoTen, string email, string noiDung)
        {
            // TODO: xử lý dữ liệu, gửi email hoặc lưu database
            ViewBag.ThongBao = "Cảm ơn bạn đã liên hệ với Circle K! Chúng tôi sẽ phản hồi sớm nhất.";
            return View();
        }
    }
}