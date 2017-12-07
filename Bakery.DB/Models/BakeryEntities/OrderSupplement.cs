using Bakery.DB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.DB
{
    public class OrderSupplement : IOrderSupplement
    {
        public int OrderSupplementId { get; set; }

        public int OrderId { get; set; }

        public int SupplementId { get; set; }
    }
}
