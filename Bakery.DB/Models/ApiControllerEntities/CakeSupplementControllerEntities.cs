using Bakery.DB.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.DB.Models
{
    public class OrderSupplementLoginRequest : IOrderSupplementLoginRequest
    {
        public int OrderSupplementId { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int SupplementId { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
