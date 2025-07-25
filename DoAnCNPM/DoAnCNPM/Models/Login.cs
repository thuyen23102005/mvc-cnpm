using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoAnCNPM.Models
{
    public class Login
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Tên đăng nhập là bắt buộc")]
        public string UserName { get; set; }

        public string RoleUser { get; set; }

        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        [DataType(DataType.Password)]
        public string PasswordUser { get; set; }
    }
}