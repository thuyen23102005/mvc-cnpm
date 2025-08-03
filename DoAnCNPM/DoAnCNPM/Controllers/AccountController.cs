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

                if (user.RoleUser.Trim() == "0")
                {
                    return RedirectToAction("Index", "Products"); // Chuyển đến trang dành cho admin
                }
                else
                {
                    return RedirectToAction("Index", "Home"); // Chuyển đến trang người dùng
                }
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
            // Kiểm tra UserName đã tồn tại chưa
            bool isDuplicateUser = db.AdminUsers.Any(u => u.UserName.ToLower() == model.UserName.ToLower());

            if (isDuplicateUser)
            {
                ModelState.AddModelError("UserName", "Tên đăng nhập đã tồn tại.");
                return View(model);
            }
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