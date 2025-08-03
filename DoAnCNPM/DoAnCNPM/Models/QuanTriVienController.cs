using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnCNPM.Models
{
    public class QuanTriVien
    {
        public int Id { get; set; }

        [Required]
        public string TenSanPham { get; set; }
        [Required]
        public string Loai { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Gia { get; set; }

        public string HinhAnh { get; set; }
    }
}