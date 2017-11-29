using Bakery.DB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;

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
                    FROM
                        Orders").ToList();
            }
        }

        public bool InsertOrder(IOrder order)
        {
            order.OrderId = GetIdForNextOrder();
            order.OrderDate = DateTime.Now;
            order.OrderType = OrderType.Unconfirmed;

            if (order.OrderId == 0)
            {
                order.OrderId++;
            }

            using (var context = Bakery.Sql())
            {
                return context.Execute(@"
                    INSERT
                        Orders(OrderId, CakeId, CustomerId, OrderWeight, OrderDate, OrderTypeId)
                    VALUES (@orderid, @cakeid, @customerid, @orderweight, @orderdate, @ordertypeid)
                ", new
                {
                    orderid = order.OrderId,
                    cakeid = order.CakeId,
                    customerid = order.CustomerId,
                    orderweight = order.OrderWeight,
                    orderdate = order.OrderDate,
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
                    WHERE
                        OrderId = @orderid
                ", new
                {
                    orderid = updateOrder.OrderId,
                    customerid = updateOrder.CakeId,
                    cakeid = updateOrder.CakeId,
                    orderweight = updateOrder.OrderWeight,
                    orderdate = updateOrder.OrderDate
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