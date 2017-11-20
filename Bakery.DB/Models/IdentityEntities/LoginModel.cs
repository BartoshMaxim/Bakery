using Bakery.DB.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bakery.DB
{
    /// <summary>
    /// Login Model - use for Authentication
    /// </summary>
    public class LoginModel : ILoginModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}