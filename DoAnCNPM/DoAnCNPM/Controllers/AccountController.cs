using DoAnCNPM.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace DoAnCNPM.Controllers
{
    public class AccountController : Controller
    {
        StoreEntities db = new StoreEntities();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login model)
        {
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
                    return RedirectToAction("Index", "Products");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ViewBag.Error = "Sai tài khoản hoặc mật khẩu.";
            return View(model);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Register model)
        {
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
                    RoleUser = "1"
                };

                db.AdminUsers.Add(user);
                db.SaveChanges();

                return RedirectToAction("Login");
            }

            return View(model);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        // Người Dùng Mua Hàng
        public ActionResult NguoiDung()
        {
            var username = Session["UserName"]?.ToString();
            if (username == null)
                return RedirectToAction("Login", "Account");

            using (var db = new StoreEntities())
            {
                var customer = db.Customers.FirstOrDefault(c => c.UserName == username);

                if (customer == null)
                {
                    var adminUser = db.AdminUsers.FirstOrDefault(a => a.UserName == username && a.RoleUser == "1");
                    if (adminUser != null)
                    {
                        customer = new Customer
                        {
                            UserName = adminUser.UserName,
                            Password = adminUser.PasswordUser,
                            NameCus = adminUser.UserName,
                            EmailCus = "default@gmail.com",
                            PhoneCus = "0123456789"
                        };

                        db.Customers.Add(customer);
                        db.SaveChanges();
                    }
                }

                // ✅ Lấy địa chỉ giao hàng tạm từ Session (nếu có)
                ViewBag.AddressDelivery = Session["AddressDeliveryTemp"]?.ToString() ?? "";

                // ✅ Lấy lịch sử đơn hàng
                var orders = db.OrderProes.Where(o => o.IDCus == customer.IDCus).ToList();
                ViewBag.OrderHistory = orders;

                return View(customer);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CapNhatNguoiDung(Customer model, string AddressDeliverry)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.AddressDelivery = AddressDeliverry;

                // 👇 Thêm dòng này để truyền lại lịch sử đơn hàng
                var orders = db.OrderProes.Where(o => o.IDCus == model.IDCus).ToList();
                ViewBag.OrderHistory = orders;

                return View("NguoiDung", model);
            }

            using (var db = new StoreEntities())
            {
                var customer = db.Customers.FirstOrDefault(c => c.IDCus == model.IDCus);
                if (customer != null)
                {
                    customer.NameCus = model.NameCus;
                    customer.PhoneCus = model.PhoneCus;
                    customer.EmailCus = model.EmailCus;
                    db.SaveChanges();
                }
            }

            // ✅ Lưu địa chỉ tạm thời vào session
            Session["AddressDeliveryTemp"] = AddressDeliverry;

            TempData["Success"] = "Cập nhật thông tin thành công!";
            return RedirectToAction("NguoiDung");
        }

        public ActionResult ChiTietDonHang(int id)
        {
            var order = db.OrderProes.FirstOrDefault(o => o.ID == id);
            var details = db.OrderDetails.Where(d => d.IDOrder == id).ToList();

            var result = new
            {
                NgayDat = order.DateOrder?.ToString("dd/MM/yyyy"),
                DiaChi = order.AddressDeliverry,
                TongTien = details.Sum(d => d.Quantity * d.UnitPrice),
                SanPham = details.Select(d => new
                {
                    Ten = d.Product?.NamePro,
                    SoLuong = d.Quantity,
                    DonGia = d.UnitPrice,
                    ThanhTien = d.Quantity * d.UnitPrice
                })
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
