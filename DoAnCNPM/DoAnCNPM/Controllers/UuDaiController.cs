using DoAnCNPM.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace DoAnCNPM.Controllers
{
    public class UuDaiController : Controller
    {
        private StoreEntities db = new StoreEntities();

        // GET: UuDai - Hiển thị danh sách mã giảm giá (Admin)
        public ActionResult Index(string searchString, string statusFilter)
        {
            var coupons = db.Coupons.AsQueryable();

            // Tìm kiếm theo mã hoặc mô tả
            if (!string.IsNullOrEmpty(searchString))
            {
                coupons = coupons.Where(c => c.Code.Contains(searchString) || 
                                           c.Description.Contains(searchString));
            }

            // Lọc theo trạng thái
            if (!string.IsNullOrEmpty(statusFilter))
            {
                var now = DateTime.Now.Date;
                switch (statusFilter)
                {
                    case "active":
                        coupons = coupons.Where(c => c.IsActive && 
                                                   now >= c.StartDate && 
                                                   now <= c.EndDate && 
                                                   c.UsedQuantity < c.Quantity);
                        break;
                    case "inactive":
                        coupons = coupons.Where(c => !c.IsActive || 
                                                   now < c.StartDate || 
                                                   now > c.EndDate || 
                                                   c.UsedQuantity >= c.Quantity);
                        break;
                    case "expired":
                        coupons = coupons.Where(c => now > c.EndDate);
                        break;
                    case "not_started":
                        coupons = coupons.Where(c => now < c.StartDate);
                        break;
                    case "used_up":
                        coupons = coupons.Where(c => c.UsedQuantity >= c.Quantity);
                        break;
                }
            }

            // Sắp xếp theo ngày tạo mới nhất
            coupons = coupons.OrderByDescending(c => c.CreatedDate);

            ViewBag.SearchString = searchString;
            ViewBag.StatusFilter = statusFilter;

            return View(coupons.ToList());
        }

        // GET: UuDai - Trang hiển thị mã giảm giá cho khách hàng
        public ActionResult UuDai(string searchString)
        {
            var activeCoupons = db.Coupons
                .Where(c => c.IsActive && c.StartDate <= DateTime.Now && c.EndDate >= DateTime.Now && c.UsedQuantity < c.Quantity);

            // Tìm kiếm theo mã hoặc mô tả
            if (!string.IsNullOrEmpty(searchString))
            {
                activeCoupons = activeCoupons.Where(c => c.Code.Contains(searchString) || 
                                                        c.Description.Contains(searchString));
            }

            activeCoupons = activeCoupons.OrderBy(c => c.MinimumOrderValue);

            ViewBag.SearchString = searchString;
            return View(activeCoupons.ToList());
        }

        // GET: Tạo mã ưu đãi
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tạo mã ưu đãi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Code,Description,DiscountType,DiscountValue,MinimumOrderValue,Quantity,StartDate,EndDate,IsActive")] Coupon coupon)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra mã đã tồn tại chưa
                if (db.Coupons.Any(c => c.Code == coupon.Code))
                {
                    ModelState.AddModelError("Code", "Mã giảm giá đã tồn tại!");
                    return View(coupon);
                }

                coupon.CreatedDate = DateTime.Now;
                coupon.UsedQuantity = 0;
                db.Coupons.Add(coupon);
                db.SaveChanges();
                TempData["Success"] = "Tạo mã giảm giá thành công!";
                return RedirectToAction("Index");
            }

            return View(coupon);
        }

        // GET: Chỉnh sửa ưu đãi
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coupon coupon = db.Coupons.Find(id);
            if (coupon == null)
            {
                return HttpNotFound();
            }
            return View(coupon);
        }

        // POST: Chỉnh sửa ưu đãi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Code,Description,DiscountType,DiscountValue,MinimumOrderValue,Quantity,UsedQuantity,StartDate,EndDate,IsActive,CreatedDate")] Coupon coupon)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra mã đã tồn tại chưa (trừ chính nó)
                if (db.Coupons.Any(c => c.Code == coupon.Code && c.ID != coupon.ID))
                {
                    ModelState.AddModelError("Code", "Mã giảm giá đã tồn tại!");
                    return View(coupon);
                }

                db.Entry(coupon).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Success"] = "Cập nhật mã giảm giá thành công!";
                return RedirectToAction("Index");
            }
            return View(coupon);
        }

        // GET: Xóa ưu đãi
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coupon coupon = db.Coupons.Find(id);
            if (coupon == null)
            {
                return HttpNotFound();
            }
            return View(coupon);
        }

        // POST: Xóa ưu đãi
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Coupon coupon = db.Coupons.Find(id);
            db.Coupons.Remove(coupon);
            db.SaveChanges();
            TempData["Success"] = "Xóa mã giảm giá thành công!";
            return RedirectToAction("Index");
        }

        // POST: kiểm tra mã giá trị giảm giá
        [HttpPost]
        public JsonResult ValidateCoupon(string code, decimal orderTotal)
        {
            try
            {
                var coupon = db.Coupons.FirstOrDefault(c => c.Code == code);
                
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

                decimal discount = CalculateDiscount(coupon, orderTotal);
                decimal finalTotal = orderTotal - discount;

                return Json(new { 
                    success = true, 
                    message = "Áp dụng mã giảm giá thành công!",
                    coupon = new {
                        id = coupon.ID,
                        code = coupon.Code,
                        description = coupon.Description,
                        discountType = coupon.DiscountType,
                        discountValue = coupon.DiscountValue,
                        discount = discount,
                        finalTotal = finalTotal
                    }
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
        }

        // Để tính toán giảm giá
        private decimal CalculateDiscount(Coupon coupon, decimal orderTotal)
        {
            if (!IsValidCoupon(coupon) || orderTotal < coupon.MinimumOrderValue)
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

        // Kiểm tra mã có hợp lệ không
        private bool IsValidCoupon(Coupon coupon)
        {
            var now = DateTime.Now.Date;
            return coupon.IsActive && 
                   now >= coupon.StartDate && 
                   now <= coupon.EndDate && 
                   coupon.UsedQuantity < coupon.Quantity;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}