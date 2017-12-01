using Bakery.DB.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.DB.Models
{
    public class CakeLoginRequest : ICakeLoginRequest
    {
        public int CakeId { get; set; }

        [Required]
        public string CakeName { get; set; }

        [Required]
        public string CakeDescription { get; set; }

        [Required]
        public float CakePrice { get; set; }

        public int ImageId { get; set; }
        
        public DateTime AddedDate { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
