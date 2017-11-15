using BakeryApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BakeryApi.Respositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public bool DeleteCustomer(int customerid, LoginModel loginModel)
        {
            throw new NotImplementedException();
        }

        public Customer GetCustomer(int customerid)
        {
            throw new NotImplementedException();
        }

        public List<Customer> GetCustomers()
        {
            throw new NotImplementedException();
        }

        public bool InsertCustomer(Customer customer, LoginModel loginModel)
        {
            throw new NotImplementedException();
        }

        public bool UpdateCustomer(Customer updateCustomer, LoginModel loginModel)
        {
            throw new NotImplementedException();
        }
    }
}