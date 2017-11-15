using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryApi.Interfaces
{
    public interface IOrderTypeRepository
    {
        List<OrderType> GetOrderTypes();

        OrderType GetOrder(int ordertypeid);
    }
}
