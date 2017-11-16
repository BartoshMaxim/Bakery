using BakeryApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BakeryApi.Controllers
{
    public class CustomerController : ApiController
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        // GET: api/Customer
        public IEnumerable<Customer> Get()
        {
            return _customerRepository.GetCustomers();
        }

        // GET: api/Customer/5
        public Customer Get(int id)
        {
            if (id > 0)
            {
                return _customerRepository.GetCustomer(id);
            }
            else
            {
                throw new Exception("Invalid data! Id has to bigger then 0.");
            }
        }

        [HttpPost]
        public IEnumerable<Customer> Post()
        {
            return _customerRepository.GetCustomers();

        }

        public void Put([FromBody]Customer customer)
        {
            customer.CreatedDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                _customerRepository.InsertCustomer(customer, null);
            }
        }

        public void Delete(int id, [FromBody]LoginModel loginModel)
        {
            _customerRepository.DeleteCustomer(id, loginModel);
        }
    }
}
