using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnCNPM.Models
{
    public class DoAn
    {
        public int Id { get; set; }

        [Required]
        public string TenMonAn { get; set; }
        public string HinhAnh { get; set; }
        public string LoaiMonAn { get; set; }
    }
}