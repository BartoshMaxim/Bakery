using BakeryApi.Interfaces;
using System;
using System.Collections.Generic;
using Dapper;
using System.Linq;

namespace BakeryApi.Respositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public bool DeleteCustomer(int customerid, LoginModel loginModel)
        {
            using (var context = Bakery.Sql())
            {
                return context.Execute(@"
                    DELETE FROM Customers
                    WHERE
                        CustomerId = @customerid
                ", new
                {
                    customerid = customerid
                }) != 0;
            }
        }

        public Customer GetCustomer(int customerid)
        {
            using (var context = Bakery.Sql())
            {
                return context.Query<Customer>(@"
                    SELECT
                        CustomerId
                        ,FirstName
                        ,LastName
                        ,CreatedDate
                        ,Email
                        ,CustomerPassword
                        ,CustomerPhone
                        ,CustomerRole = CustomerRoleId
                    
                        ,Address1
                        ,Address2
                        ,City
                        ,Country
                    FROM
                        Customers
                    WHERE
                        CustomerId = @customerid
                ", new
                {
                    customerid = customerid
                }).FirstOrDefault();
            }
        }

        public List<Customer> GetCustomers()
        {
            using (var context = Bakery.Sql())
            {
                return context.Query<Customer>(@"
                 SELECT
                        CustomerId
                        ,FirstName
                        ,LastName
                        ,CreatedDate
                        ,Email
                        ,CustomerPassword
                        ,CustomerPhone
                        ,CustomerRole = CustomerRoleId
                    
                        ,Address1
                        ,Address2
                        ,City
                        ,Country
                    FROM
                        Customers
                ").ToList();
            }
        }

        public bool InsertCustomer(Customer customer, LoginModel loginModel)
        {
            customer.CustomerId = GetCountRows();

            using (var context = Bakery.Sql())
            {
                return context.Execute(@"
                    INSERT 
                        Customers(CustomerId, FirstName, LastName, CreatedDate, Email, CustomerPassword, CustomerPhone, CustomerRoleId, Address1, Address2, City, Country)
                    VALUES
                        (@customerid, @firstname, @lastname, @createddate, @email, @customerpassword, @customerphone, @customerrole, @address1, @address2, @city, @country)
                ", new
                {
                    customerid = customer.CustomerId,
                    firstname = customer.FirstName,
                    lastname = customer.LastName,
                    createddate = customer.CreatedDate,
                    email = customer.Email,
                    customerpassword = customer.CustomerPassword,
                    customerphone = customer.CustomerPhone,
                    customerrole = customer.CustomerRole,

                    address1 = customer.Address1,
                    address2 = customer.Address2,
                    city = customer.City,
                    country = customer.Country
                }) != 0;
            }
        }

        public bool UpdateCustomer(Customer updateCustomer, LoginModel loginModel)
        {
            using (var context = Bakery.Sql())
            {
                return context.Execute(@"
                    UPDATE 
                        Cakes
                    SET
                        FirstName         = @firstname
                        ,LastName         = @lastname
                        ,CreatedDate      = @createddate
                        ,CustomerPassword = @customerpassword
                        ,CustomerPhone    = @customerphone
                        ,CustomerRoleId   = @customerrole
                        ,Address1         = @address1
                        ,Address2         = @address2
                        ,City             = @city
                        ,Country          = @country
                    WHERE
                        CustomerId        = @customerid
                ", new
                {
                    firstname = updateCustomer.FirstName,
                    lastname = updateCustomer.LastName,
                    createddate = updateCustomer.CreatedDate,
                    email = updateCustomer.Email,
                    customerpassword = updateCustomer.CustomerPassword,
                    customerphone = updateCustomer.CustomerPhone,
                    customerrole = updateCustomer.CustomerRole,

                    address1 = updateCustomer.Address1,
                    address2 = updateCustomer.Address1,
                    sity = updateCustomer.City,
                    country = updateCustomer.Country
                }) != 0;
            }
        }

        public int GetCountRows()
        {
            using (var context = Bakery.Sql())
            {
                return context.ExecuteScalar<int>(@"
                    SELECT DISTINCT 
                        CustomerId
                    FROM 
                        Customers");
            }
        }
    }
}