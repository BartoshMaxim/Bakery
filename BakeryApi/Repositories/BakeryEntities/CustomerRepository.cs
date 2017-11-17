using BakeryApi.Interfaces;
using System;
using System.Collections.Generic;
using Dapper;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

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

        public Customer GetCustomer(int customerid, LoginModel loginModel)
        {
            if (IsAdmin(loginModel))
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
            else
            {
                return null;
            }
        }

        public List<Customer> GetCustomers(LoginModel loginModel)
        {
            if (IsAdmin(loginModel))
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
            else
            {
                return null;
            }
        }

        public bool InsertCustomer(Customer customer, LoginModel loginModel)
        {
            var isExists = IsExistsEmail(customer.Email);
            var isAdmin = IsAdmin(loginModel);

            if (!isExists && isAdmin)
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

        public bool IsExistsEmail(string email)
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

        public bool IsExistsId(int customerid)
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

        public bool IsAdmin(LoginModel loginModel)
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

            while (IsExistsId(customerID))
            {
                customerID++;
            }
            return customerID;
        }
    }
}