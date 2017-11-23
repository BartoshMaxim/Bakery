using Bakery.DB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace Bakery.DB.Repositories
{
    public class OrderTypeRepository : IOrderTypeRepository
    {
        public List<IOrder> GetOrders(int ordertypeid)
        {
            using (var context = Bakery.Sql())
            {
                return context.Query<IOrder>(@"
                    SELECT
                        OrderId,
                        CakeId,
                        OrderTypeId,
                        CustomerId,
                        OrderWeight,
                        OrderDate,
                        OrderType = OrderTypeId
                    FROM
                        OrderTypes
                    WHERE
                        OrderTypeId = @ordertypeid
                ", new
                {
                    ordertypeid = ordertypeid
                }).ToList();
            }
        }

        public List<OrderType> GetOrderTypes()
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