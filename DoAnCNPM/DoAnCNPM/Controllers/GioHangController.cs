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
    }
}
