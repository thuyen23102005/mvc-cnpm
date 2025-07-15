using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnCNPM.Models
{
    public class DoUong
    {
        public int Id { get; set; }

        [Required]
        public string Ten { get; set; }

        public string HinhAnh { get; set; }

        public string DanhMuc { get; set; } // VD: "Cafe Việt Nam", "Thức uống pha chế"
    }
}