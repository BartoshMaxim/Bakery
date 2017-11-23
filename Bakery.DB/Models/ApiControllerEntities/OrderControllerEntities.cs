using Bakery.DB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.DB.Models
{
    public class OrderLoginRequest : IOrderLoginRequest
    {
        public int OrderId { get; set; }

        public int CakeId { get; set; }

        public int CustomerId { get; set; }

        public float OrderWeight { get; set; }

        public OrderType OrderType { get; set; }

        public DateTime OrderDate { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }
    }
}
