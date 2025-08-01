using DoAnCNPM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnCNPM.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        StoreEntities db = new StoreEntities();
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login model)
        {
            // Kiểm tra nếu model không hợp lệ thì hiển thị lại view kèm lỗi
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = db.AdminUsers
                .FirstOrDefault(u => u.UserName == model.UserName && u.PasswordUser.Trim() == model.PasswordUser);

            if (user != null)
            {
                Session["UserName"] = user.UserName;
                Session["Role"] = user.RoleUser;
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Sai tài khoản hoặc mật khẩu.";
            return View(model);
        }

        // GET: Register
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Register model)
        {
            if (ModelState.IsValid)
            {
                var user = new AdminUser
                {
                    UserName = model.UserName,
                    PasswordUser = model.Password,
                    RoleUser = "1" // Gán mặc định là user
                };

                db.AdminUsers.Add(user);
                db.SaveChanges();

                return RedirectToAction("Login");
            }

            return View(model);
        }
        public ActionResult Logout()
        {
            Session.Clear(); // hoặc chỉ Session["UserName"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}