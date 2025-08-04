using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnCNPM.Models
{
    public class RevenueByMonth
    {
        public int Month { get; set; }
        public int Year { get; set; }
        // mới:
        public double Revenue { get; set; }
    }
}