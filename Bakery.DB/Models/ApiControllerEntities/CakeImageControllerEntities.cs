using Bakery.DB.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.DB.Models
{
    public class CakeImageLoginRequest : ICakeImageLoginRequest
    {
        public int CakeImageId { get; set; }

        [Required]
        public int CakeId { get; set; }

        [Required]
        public int ImageId { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
