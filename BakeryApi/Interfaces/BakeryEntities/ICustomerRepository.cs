using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryApi.Interfaces
{
    public interface ICustomerRepository
    {
        List<Customer> GetCustomers();

        Customer GetCustomer(int customerid);

        bool InsertCustomer(Customer customer, LoginModel loginModel);

        bool DeleteCustomer(int customerid, LoginModel loginModel);

        bool UpdateCustomer(Customer updateCustomer, LoginModel loginModel);
    }
}
