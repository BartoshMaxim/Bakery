using BakeryApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BakeryApi.Respositories
{
    public class OrderRepository : IOrderRepository
    {
        public bool DeleteOrder(int orderid, LoginModel loginModel)
        {
            throw new NotImplementedException();
        }

        public Order GetOrder(int orderid)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetOrders()
        {
            throw new NotImplementedException();
        }

        public bool InsertOrder(Order order, LoginModel loginModel)
        {
            throw new NotImplementedException();
        }

        public bool UpdateOrder(Order updateOrder, LoginModel loginModel)
        {
            throw new NotImplementedException();
        }
    }
}