using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryApi.Interfaces
{
    public interface ICustomerRepository
    {
        List<Customer> GetCustomers(LoginModel loginModel);

        Customer GetCustomer(int customerid, LoginModel loginModel);

        bool InsertCustomer(Customer customer, LoginModel loginModel);

        bool DeleteCustomer(int customerid, LoginModel loginModel);

        bool UpdateCustomer(Customer updateCustomer, LoginModel loginModel);

        int GetCountRows();

        bool IsExistsEmail(string email);

        bool IsExistsId(int id);

        bool IsAdmin(LoginModel loginModel);
    }
}
