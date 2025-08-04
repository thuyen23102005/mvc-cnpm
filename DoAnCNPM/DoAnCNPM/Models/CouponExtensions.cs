using System;

namespace DoAnCNPM.Models
{
    public static class CouponExtensions
    {
        public static decimal CalculateDiscount(this Coupon coupon, decimal orderTotal)
        {
            if (coupon == null) throw new ArgumentNullException(nameof(coupon));
            if (orderTotal < coupon.MinimumOrderValue) return 0;

            // Ví dụ: "Phần trăm" hoặc "Số tiền"
            if (coupon.DiscountType == "Percent")
            {
                return Math.Round(orderTotal * coupon.DiscountValue / 100, 0, MidpointRounding.AwayFromZero);
            }
            else if (coupon.DiscountType == "Amount")
            {
                return Math.Min(coupon.DiscountValue, orderTotal);
            }
            return 0;
        }
    }
}