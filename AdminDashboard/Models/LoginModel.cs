using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdminDashboard.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Not correct email")]
        public string Email { get; set; }

        [Required]
        [MinLength(6,ErrorMessage = "Password has to longer than 6 symbols")]
        [MaxLength(256, ErrorMessage = "Password has to smaller than 30 symbols")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}