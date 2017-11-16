using BakeryApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace BakeryApi.Repositories
{
    public class OrderTypeRepository : IOrderTypeRepository
    {
        public OrderType GetOrder(int ordertypeid)
        {
            using (var context = Bakery.Sql())
            {
                return (OrderType) context.ExecuteScalar<int>(@"
                    SELECT
                        OrderTypeId
                    FROM
                        OrderTypes
                    WHERE
                        OrderTypeId = @ordertypeid
                ", new
                {
                    ordertypeid = ordertypeid
                });
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