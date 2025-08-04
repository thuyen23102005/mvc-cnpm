using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCNPM.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        [Required]
        public string Ten { get; set; }

        public string HinhAnh { get; set; }

        public decimal DonGia { get; set; }

        public int SoLuong { get; set; }

        public decimal ThanhTien => DonGia * SoLuong;
    }
}
