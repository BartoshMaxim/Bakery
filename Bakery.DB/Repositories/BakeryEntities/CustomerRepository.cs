using Bakery.DB.Interfaces;
using System;
using System.Collections.Generic;
using Dapper;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Bakery.DB.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public bool DeleteCustomer(int customerid)
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

        public ICustomer GetCustomer(int customerid)
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


        public int GetCustomerId(string login, string password)
        {
            using (var context = Bakery.Sql())
            {
                return context.ExecuteScalar<int>(@"
                    SELECT
                        CustomerId
                    FROM
                        Customers
                    WHERE
                        Email            = @login
                    AND
                        CustomerPassword = @password
                ", new
                {
                    login = login,
                    password = ToMd5(password)
                });
            }
        }

        public ICustomer GetCustomer(string login, string password)
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
                        ,CustomerRole    = CustomerRoleId
                    
                        ,Address1
                        ,Address2
                        ,City
                        ,Country
                    FROM
                        Customers
                    WHERE
                        Email            = @login
                    AND
                        CustomerPassword = @password
                ", new
                {
                    login = login,
                    password = ToMd5(password)
                }).FirstOrDefault();
            }
        }

        public IList<Customer> GetCustomers()
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

        public IList<Customer> GetCustomers(int from, int to)
        {
            to = to - from;

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
                    ORDER BY CustomerId DESC
                    OFFSET @from ROWS
                    FETCH NEXT @to ROWS ONLY
                ", new
                {
                    from = from,
                    to = to
                }
                ).ToList();
            }
        }

        public bool InsertCustomer(ICustomer customer)
        {
            var isExists = IsExists(customer.Email);

            if (!isExists)
            {
                customer.CustomerId = GetIdForNextCustomer();

                customer.CreatedDate = DateTime.Now;

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
                        customerpassword = ToMd5(customer.CustomerPassword),
                        customerphone = customer.CustomerPhone,
                        customerrole = customer.CustomerRole,

                        address1 = customer.Address1,
                        address2 = customer.Address2,
                        city = customer.City,
                        country = customer.Country
                    }) != 0;
                }
            }
            else
            {
                return false;
            }
        }

        public bool UpdateCustomer(ICustomer updateCustomer)
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
                    customerpassword = ToMd5(updateCustomer.CustomerPassword),
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
                    SELECT COUNT(CustomerId)       
                    FROM 
                        Customers");
            }
        }

        public bool IsExists(string email)
        {
            using (var context = Bakery.Sql())
            {
                return context.ExecuteScalar<int>(@"
                SELECT COUNT(CustomerId)
                FROM
                    Customers
                WHERE
                    Email = @email
                ", new
                {
                    email = email
                }) != 0;
            }
        }

        public bool IsExists(int customerid)
        {
            using (var context = Bakery.Sql())
            {
                return context.ExecuteScalar<int>(@"
                SELECT COUNT(CustomerId)
                FROM
                    Customers
                WHERE
                    CustomerId = @customerid
                ", new
                {
                    customerid = customerid
                }) != 0;
            }
        }

        public bool IsAdmin(ILoginModel loginModel)
        {
            using (var context = Bakery.Sql())
            {
                return (RoleType)context.ExecuteScalar<int>(@"
                    SELECT
                        CustomerRoleId
                    FROM
                        Customers
                    WHERE
                        Email            = @email
                    AND
                        CustomerPassword = @password
                ", new
                {
                    email = loginModel.Login,
                    password = ToMd5(loginModel.Password)
                }) == RoleType.Admin;
            }
        }

        private string ToMd5(string password)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(password));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        private int GetIdForNextCustomer()
        {
            var customerID = GetCountRows();

            while (IsExists(customerID))
            {
                customerID++;
            }
            return customerID;
        }
    }
}