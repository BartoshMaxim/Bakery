using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryApi.Interfaces
{
    public interface IOrderRepository
    {
        List<Order> GetOrders();

        Order GetOrder(int orderid);

        bool InsertOrder(Order order, LoginModel loginModel);

        bool DeleteOrder(int orderid, LoginModel loginModel);

        bool UpdateOrder(Order updateOrder, LoginModel loginModel);
    }
}
