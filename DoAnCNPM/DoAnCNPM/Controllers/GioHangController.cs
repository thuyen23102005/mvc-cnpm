using DoAnCNPM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DoAnCNPM.Controllers
{
    public class GioHangController : Controller
    {
        private StoreEntities db = new StoreEntities();

        public ActionResult Cart()
        {
            var cart = Session["Cart"] as List<CartItem> ?? new List<CartItem>();
            return View(cart);
        }

        public class CartItemInput
        {
            public int id { get; set; }
            public int soLuong { get; set; }
        }

        [HttpPost]
        public ActionResult ThemVaoGio(CartItemInput input)
        {
            var db = new StoreEntities();
            var sanPham = db.Products.FirstOrDefault(p => p.ProductID == input.id);
            if (sanPham == null)
                return new HttpStatusCodeResult(400, "Không tìm thấy sản phẩm");

            var cart = Session["Cart"] as List<CartItem>;
            if (cart == null) cart = new List<CartItem>();

            var existing = cart.FirstOrDefault(p => p.Id == input.id);
            if (existing != null)
            {
                existing.SoLuong += input.soLuong;
            }
            else
            {
                cart.Add(new CartItem
                {
                    Id = sanPham.ProductID,
                    Ten = sanPham.NamePro,
                    HinhAnh = sanPham.ImagePro,
                    DonGia = (decimal)sanPham.Price,
                    SoLuong = input.soLuong
                });
            }

            Session["Cart"] = cart;
            return new HttpStatusCodeResult(200);
        }

        public ActionResult Xoa(int id)
        {
            var cart = Session["Cart"] as List<CartItem>;
            if (cart != null)
            {
                var itemToRemove = cart.FirstOrDefault(p => p.Id == id);
                if (itemToRemove != null)
                {
                    cart.Remove(itemToRemove);
                }
            }

            Session["Cart"] = cart;
            return RedirectToAction("Cart");
        }

        // POST: GioHang/ApplyCoupon - Áp dụng mã giảm giá
        [HttpPost]
        public JsonResult ApplyCoupon(string couponCode)
        {
            try
            {
                var cart = Session["Cart"] as List<CartItem>;
                if (cart == null || !cart.Any())
                {
                    return Json(new { success = false, message = "Giỏ hàng trống!" });
                }

                decimal orderTotal = cart.Sum(item => item.ThanhTien);
                var coupon = db.Coupons.FirstOrDefault(c => c.Code == couponCode);

                if (coupon == null)
                {
                    return Json(new { success = false, message = "Mã giảm giá không tồn tại!" });
                }

                if (!coupon.IsActive)
                {
                    return Json(new { success = false, message = "Mã giảm giá đã bị vô hiệu hóa!" });
                }

                if (DateTime.Now < coupon.StartDate || DateTime.Now > coupon.EndDate)
                {
                    return Json(new { success = false, message = "Mã giảm giá chưa có hiệu lực hoặc đã hết hạn!" });
                }

                if (coupon.UsedQuantity >= coupon.Quantity)
                {
                    return Json(new { success = false, message = "Mã giảm giá đã hết lượt sử dụng!" });
                }

                if (orderTotal < coupon.MinimumOrderValue)
                {
                    return Json(new { success = false, message = $"Đơn hàng tối thiểu phải từ {coupon.MinimumOrderValue:N0} VNĐ!" });
                }

                // Tính toán giảm giá
                decimal discount = CalculateDiscount(coupon, orderTotal);
                decimal finalTotal = orderTotal - discount;

                // Lưu thông tin mã giảm giá vào session
                Session["AppliedCoupon"] = new
                {
                    Id = coupon.ID,
                    Code = coupon.Code,
                    Description = coupon.Description,
                    DiscountType = coupon.DiscountType,
                    DiscountValue = coupon.DiscountValue,
                    Discount = discount,
                    FinalTotal = finalTotal
                };

                return Json(new { 
                    success = true, 
                    message = "Áp dụng mã giảm giá thành công!",
                    data = new {
                        originalTotal = orderTotal,
                        discount = discount,
                        finalTotal = finalTotal,
                        couponInfo = new {
                            code = coupon.Code,
                            description = coupon.Description,
                            discountType = coupon.DiscountType,
                            discountValue = coupon.DiscountValue
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
        }

        // Helper method để tính toán giảm giá
        private decimal CalculateDiscount(Coupon coupon, decimal orderTotal)
        {
            if (orderTotal < coupon.MinimumOrderValue)
                return 0;

            decimal discount = 0;
            if (coupon.DiscountType == "Percentage")
            {
                discount = orderTotal * (coupon.DiscountValue / 100);
            }
            else if (coupon.DiscountType == "Fixed")
            {
                discount = coupon.DiscountValue;
            }

            return Math.Min(discount, orderTotal); // Không giảm quá tổng đơn hàng
        }

        // POST: GioHang/RemoveCoupon - Xóa mã giảm giá
        [HttpPost]
        public JsonResult RemoveCoupon()
        {
            Session["AppliedCoupon"] = null;
            return Json(new { success = true, message = "Đã xóa mã giảm giá!" });
        }

        public ActionResult MuaHang()
        {
            var username = Session["UserName"]?.ToString();
            if (username == null)
                return RedirectToAction("Login", "Account");

            var address = Session["AddressDeliveryTemp"] as string;
            if (string.IsNullOrEmpty(address))
                address = "Không có địa chỉ"; // hoặc redirect yêu cầu nhập địa chỉ

            using (var db = new StoreEntities())
            {
                var customer = db.Customers.FirstOrDefault(c => c.UserName == username);
                if (customer == null) return RedirectToAction("Login", "Account");

                var cart = Session["Cart"] as List<CartItem>;
                if (cart == null || !cart.Any())
                {
                    TempData["Error"] = "Giỏ hàng trống.";
                    return RedirectToAction("Cart");
                }

                // Tính toán tổng tiền và áp dụng mã giảm giá
                decimal orderTotal = cart.Sum(item => item.ThanhTien);
                decimal discount = 0;
                int? couponId = null;

                var appliedCoupon = Session["AppliedCoupon"];
                if (appliedCoupon != null)
                {
                    // Sử dụng reflection để lấy giá trị từ anonymous object
                    var couponType = appliedCoupon.GetType();
                    discount = (decimal)couponType.GetProperty("Discount").GetValue(appliedCoupon);
                    couponId = (int)couponType.GetProperty("Id").GetValue(appliedCoupon);
                }

                decimal finalTotal = orderTotal - discount;

                // 👉 Tạo OrderPro
                var order = new OrderPro
                {
                    IDCus = customer.IDCus,
                    DateOrder = DateTime.Now,
                    AddressDeliverry = address
                };
                db.OrderProes.Add(order);
                db.SaveChanges();

                // 👉 Tạo từng dòng OrderDetail
                foreach (var item in cart)
                {
                    var detail = new OrderDetail
                    {
                        IDProduct = item.Id,
                        IDOrder = order.ID,
                        Quantity = item.SoLuong,
                        UnitPrice = (double?)item.DonGia
                    };
                    db.OrderDetails.Add(detail);
                }

                // Cập nhật số lượng sử dụng của mã giảm giá
                if (couponId.HasValue)
                {
                    var coupon = db.Coupons.Find(couponId.Value);
                    if (coupon != null)
                    {
                        coupon.UsedQuantity++;
                        db.Entry(coupon).State = System.Data.Entity.EntityState.Modified;
                    }
                }

                db.SaveChanges();

                // Xoá giỏ hàng và mã giảm giá
                Session["Cart"] = null;
                Session["AppliedCoupon"] = null;
                
                TempData["Success"] = "Đặt hàng thành công!";
                return RedirectToAction("NguoiDung", "Account");
            }
        }
    }
}
