using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryApi
{
    public class Order
    {
        public int OrderId { get; set; }  

        public int CakeId { get; set; }

        public int CustomerId { get; set; }

        public float OrderWeight { get; set; }

        public OrderType OrderType { get; set; }

        public DateTime OrderDate { get; set; }
    }

    public class FullOrder
    {
        public int OrderId { get; set; }

        public Cake Cake { get; set; }

        public Customer Customer { get; set; }

        public float OrderWeight { get; set; }

        public OrderType OrderType { get; set; }

        public DateTime OrderDate { get; set; }
    }
}
