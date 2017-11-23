﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.DB.Interfaces
{
    public interface IOrderTypeRepository
    {
        List<OrderType> GetOrderTypes();

        List<IOrder> GetOrders(int ordertypeid)
    }
}
