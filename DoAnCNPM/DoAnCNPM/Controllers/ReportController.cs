using DoAnCNPM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnCNPM.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        private StoreEntities db = new StoreEntities();

        public ActionResult RevenueByMonth()
        {
            var revenueData = db.OrderProes
                .Join(db.OrderDetails,
                      o => o.ID,
                      d => d.IDOrder,
                      (o, d) => new { o.DateOrder, d.Quantity, d.UnitPrice })
                .GroupBy(x => new { x.DateOrder.Value.Year, x.DateOrder.Value.Month })
                .Select(g => new RevenueByMonth
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Revenue = g.Sum(x => x.Quantity * x.UnitPrice) ?? 0
                })
                .OrderBy(x => x.Year).ThenBy(x => x.Month)
                .ToList();

            return View(revenueData);
        }
    }
}