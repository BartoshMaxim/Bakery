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

        public Order GetOrder(int orderid)
        {
            using (var context = Bakery.Sql())
            {
                return context.ExecuteScalar<Order>(@"
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
                });
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

        public List<Order> GetOrders()
        {
            using (var context = Bakery.Sql())
            {
                return context.Query<Order>(@"
                    SELECT
                        OrderId
                        ,CustomerId,
                        ,CakeId
                        ,OrderWeight
                        ,OrderDate
                    FROM
                        Orders").ToList();
            }
        }

        public bool InsertOrder(Order order)
        {
            using (var context = Bakery.Sql())
            {
                return context.Execute(@"
                    INSERT
                        Orders(CakeId, CustomerId, OrderWeight, OrderDate)
                    VALUES (@cakeid, @customerid, @orderweight, @orderdate)
                ", new
                {
                    cakeid = order.CakeId,
                    customerid = order.CustomerId,
                    orderweight = order.OrderWeight,
                    orderdate = order.OrderDate
                }) != 0;
            }
        }

        public bool UpdateOrder(Order updateOrder)
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
    }
}