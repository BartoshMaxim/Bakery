using Bakery.DB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace Bakery.DB.Repositories
{
    public class OrderTypeRepository : IOrderTypeRepository
    {
        public IList<Order> GetOrders(int ordertypeid)
        {
            using (var context = Bakery.Sql())
            {
                return context.Query<Order>(@"
                    SELECT
                        OrderId,
                        CakeId,
                        OrderTypeId,
                        CustomerId,
                        OrderWeight,
                        OrderDate,
                        OrderType = OrderTypeId
                    FROM
                        Orders
                    WHERE
                        OrderTypeId = @ordertypeid
                ", new
                {
                    ordertypeid = ordertypeid
                }).ToList();
            }
        }

        public IList<OrderType> GetOrderTypes()
        {
            using (var context = Bakery.Sql())
            {
                return context.Query<OrderType>(@"
                    SELECT
                        OrderTypeId
                    FROM
                        OrderTypes
                ").ToList();
            }
        }
    }
}