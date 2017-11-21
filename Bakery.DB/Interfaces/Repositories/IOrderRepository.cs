using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.DB.Interfaces
{
    public interface IOrderRepository
    {
        List<Order> GetOrders();

        Order GetOrder(int orderid);

        FullOrder GetFullOrder(int orderid);

        bool InsertOrder(Order order);

        bool DeleteOrder(int orderid);

        bool UpdateOrder(Order updateOrder);
    }
}
