using System.Linq;
using System.Web.Mvc;
using DoAnCNPM.Models;

namespace DoAnCNPM.Controllers
{
    public class SearchController : Controller
    {
        private StoreEntities db = new StoreEntities();

        public ActionResult Results(string query)
        {
            var products = db.Products
                             .Where(p => p.NamePro.Contains(query)) // ✅ sửa Ten → NamePro
                             .ToList();

            return View(products); // View: Views/Search/Results.cshtml
        }
    }
}