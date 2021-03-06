﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.DB.Interfaces
{
    public interface IOrderTypeRepository
    {
        IList<OrderType> GetOrderTypes();

        IList<Order> GetOrders(int ordertypeid);
    }
}
