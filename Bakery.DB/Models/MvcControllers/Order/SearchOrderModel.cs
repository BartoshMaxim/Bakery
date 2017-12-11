using Bakery.DB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bakery.DB;

namespace Bakery.DB
{
    public class SearchOrderModel : IOrder, IPage
    {
        public int OrderId { get; set; }

        public int CakeId { get; set; }

        public int CustomerId { get; set; }

        public float OrderWeight { get; set; }

        public OrderType OrderType { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public int Rows { get; set; }

        public int Page { get; set; }

        public bool IsOrderNotNull()
        {
            if (OrderId == 0 && CakeId == 0 && CustomerId == 0 && OrderDate.Year.Equals(1))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}