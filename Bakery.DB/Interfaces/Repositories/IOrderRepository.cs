using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.DB.Interfaces
{
    public interface IOrderRepository
    {
        List<IOrder> GetOrders();

        IOrder GetOrder(int orderid);

        FullOrder GetFullOrder(int orderid);

        bool InsertOrder(IOrder order);

        bool DeleteOrder(int orderid);

        bool UpdateOrder(IOrder updateOrder);
    }
}
