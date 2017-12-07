using Bakery.DB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Text;

namespace Bakery.DB.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public bool DeleteOrder(int orderid)
        {
            using (var context = Bakery.Sql())
            {
                return context.Execute(@"
                    DELETE FROM Images
                    WHERE
                        OrderId = @orderid
                ", new
                {
                    orderid = orderid
                }) != 0;
            }
        }

        public IOrder GetOrder(int orderid)
        {
            using (var context = Bakery.Sql())
            {
                return context.Query<Order>(@"
                    SELECT
                        OrderId
                        ,CustomerId
                        ,CakeId
                        ,OrderWeight
                        ,OrderDate
                        ,CreatedDate
                    FROM
                        Orders
                    WHERE
                        OrderId = @orderid
                ", new
                {
                    orderid = orderid
                }).FirstOrDefault();
            }
        }

        public FullOrder GetFullOrder(int orderid)
        {
            using (var context = Bakery.Sql())
            {
                return context.Query<FullOrder, Cake, Customer, FullOrder>(@"
                    SELECT
                        o.OrderId
                        ,o.OrderWeight
                        ,o.OrderDate
                        ,o.CreatedDate   
                        ,ca.CakeId
                        ,ca.CakeName
                        ,ca.CakeDescription
                        ,ca.CakePrice
                        ,ca.ImageId
                        ,ca.AddedDate
                        
                        ,c.CustomerId
                        ,c.FirstName
                        ,c.LastName
                        ,c.CreatedDate
                        ,c.Email
                        ,c.CustomerPassword
                        ,c.CustomerPhone
                        ,c.CustomerRole
                    
                        ,c.Address1
                        ,c.Address2
                        ,c.City
                        ,c.Country
                    FROM
                        Orders as o
                            JOIN Customers as c
                            ON c.CustomerId = o.CustomerId
                            JOIN Cakes as ca
                            ON ca.CakeId    = o.CakeId
                    WHERE
                        OrderId = @orderid 
                ", (o, ca, c) =>
                {
                    o.Cake = ca;
                    o.Customer = c;
                    return o;
                }, new
                {
                    orderid = orderid
                }, splitOn: "CakeId, CustomerId"
                ).FirstOrDefault();
            }
        }

        public IList<Order> GetOrders()
        {
            using (var context = Bakery.Sql())
            {
                return context.Query<Order>(@"
                    SELECT
                        OrderId
                        ,CustomerId
                        ,CakeId
                        ,OrderWeight
                        ,OrderDate
                        ,CreatedDate
                    FROM
                        Orders").ToList();
            }
        }

        public bool InsertOrder(IOrder order)
        {
            order.OrderId = GetIdForNextOrder();
            order.CreatedDate = DateTime.Now;
            order.OrderType = OrderType.Unconfirmed;

            if (order.OrderId == 0)
            {
                order.OrderId++;
            }

            using (var context = Bakery.Sql())
            {
                return context.Execute(@"
                    INSERT
                        Orders(OrderId, CakeId, CustomerId, OrderWeight, OrderDate, CreatedDate, OrderTypeId)
                    VALUES (@orderid, @cakeid, @customerid, @orderweight, @orderdate, @createdDate, @ordertypeid)
                ", new
                {
                    orderid = order.OrderId,
                    cakeid = order.CakeId,
                    customerid = order.CustomerId,
                    orderweight = order.OrderWeight,
                    orderdate = order.OrderDate,
                    createdDate = order.CreatedDate,
                    ordertypeid = order.OrderType
                }) != 0;
            }
        }

        public bool UpdateOrder(IOrder updateOrder)
        {
            using (var context = Bakery.Sql())
            {
                return context.Execute(@"
                    UPDATE
                        Orders
                    SET
                        CustomerId   = @customerid
                        ,CakeId      = @cakeid
                        ,OrderWeight = @orderweight
                        ,OrderDate   = @orderdate
                        ,CreatedDate   = @createddate
                    WHERE
                        OrderId = @orderid
                ", new
                {
                    orderid = updateOrder.OrderId,
                    customerid = updateOrder.CakeId,
                    cakeid = updateOrder.CakeId,
                    orderweight = updateOrder.OrderWeight,
                    orderdate = updateOrder.OrderDate,
                    createddate = updateOrder.CreatedDate
                }) != 0;
            }
        }

        private int GetIdForNextOrder()
        {
            var orderID = GetCountRows();

            while (IsExists(orderID))
            {
                orderID++;
            }
            return orderID;
        }

        public bool IsExists(int orderid)
        {
            using (var context = Bakery.Sql())
            {
                return context.ExecuteScalar<int>(@"
                SELECT COUNT(OrderId)
                FROM
                    Orders
                WHERE
                    OrderId = @orderid
                ", new
                {
                    orderid = orderid
                }) != 0;
            }
        }

        private string CreateQuery(IOrder order)
        {
            var query = new StringBuilder();

            if (order.OrderId != 0)
            {
                query.Append($"WHERE OrderId={order.OrderId}");
            }

            if (order.CakeId != 0)
            {
                if (query.Length == 0)
                {
                    query.Append("WHERE ");
                }
                else
                {
                    query.Append(" AND ");
                }

                query.Append($"CakeId ={order.CakeId}");
            }

            if (order.CustomerId != 0)
            {
                if (query.Length == 0)
                {
                    query.Append("WHERE ");
                }
                else
                {
                    query.Append(" AND ");
                }

                query.Append($"CustomerId ={order.CustomerId}");
            }

            if (order.OrderDate != null)
            {
                if (query.Length == 0)
                {
                    query.Append("WHERE ");
                }
                else
                {
                    query.Append(" AND ");
                }

                query.Append($"OrderDate ={order.OrderDate}");
            }

            if (query.Length == 0)
            {
                query.Append("WHERE ");
            }
            else
            {
                query.Append(" AND ");
            }

            query.Append($"OrderTypeId={order.OrderType}");

            return query.ToString();
        }

        public IList<Order> GetOrders(int from, int to, IOrder searchOrder)
        {
            var query = string.Empty;
            if (searchOrder != null)
            {
                query = CreateQuery(searchOrder);
            }

            to = to - from;

            using (var context = Bakery.Sql())
            {
                return context.Query<Order>($@"
                    SELECT
                        OrderId
                        ,CustomerId
                        ,CakeId
                        ,OrderWeight
                        ,OrderDate
                        ,CreatedDate
                    FROM
                        Orders
                    {query}
                    ORDER BY SupplementId DESC
                    OFFSET @from ROWS
                    FETCH NEXT @to ROWS ONLY
                    ", new
                {
                    from = from,
                    to = to
                }).ToList();
            }
        }

        public int GetCountRows(IOrder searchOrder)
        {
            string query = string.Empty;

            if (searchOrder != null)
            {
                query = CreateQuery(searchOrder);
            }
            using (var context = Bakery.Sql())
            {
                return context.ExecuteScalar<int>(@"
                    SELECT COUNT(OrderId)       
                    FROM 
                        Orders
                    " + query);
            }
        }

        public int GetCountRows()
        {
            using (var context = Bakery.Sql())
            {
                return context.ExecuteScalar<int>(@"
                    SELECT COUNT(OrderId)       
                    FROM 
                        Orders");
            }
        }
    }
}