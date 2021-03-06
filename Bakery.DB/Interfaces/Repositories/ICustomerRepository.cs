﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.DB.Interfaces
{
    public interface ICustomerRepository
    {
        IList<Customer> GetCustomers();

        IList<Customer> GetCustomers(int from, int to, ICustomer customer = null);

        ICustomer GetCustomer(int customerid);

        ICustomer GetCustomer(string login, string password);

        int GetCustomerId(string login, string password);

        bool InsertCustomer(ICustomer customer);

        bool DeleteCustomer(int customerid);

        bool UpdateCustomer(ICustomer updateCustomer);

        int GetCountRows();

        int GetCountRows(ICustomer customer);

        bool IsExists(string email);

        bool IsExists(int id);

        bool IsAdmin(ILoginModel loginModel);
    }
}
