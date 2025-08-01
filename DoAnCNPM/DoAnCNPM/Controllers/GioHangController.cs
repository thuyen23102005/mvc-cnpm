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
        public ActionResult Cart()
        {
            var cart = new List<CartItem>
    {
        new CartItem
        {
            Id = 1,
            Ten = "Chuối Circle K",
            HinhAnh = "/Content/Image/banana.png",
            DonGia = 12000,
            SoLuong = 2
        },
        new CartItem
        {
            Id = 2,
            Ten = "HotDog",
            HinhAnh = "/Content/Image/hot_dog.png",
            DonGia = 10000,
            SoLuong = 1
        }
    };

            return View(cart);
        }
    }
}
