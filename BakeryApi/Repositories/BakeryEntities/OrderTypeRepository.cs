using BakeryApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BakeryApi.Repositories
{
    public class OrderTypeRepository : IOrderTypeRepository
    {
        public OrderType GetOrder(int ordertypeid)
        {
            throw new NotImplementedException();
        }

        public List<OrderType> GetOrderTypes()
        {
            throw new NotImplementedException();
        }
    }
}