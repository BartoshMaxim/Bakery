using BakeryApi.Interfaces;
using BakeryApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace BakeryApi.Controllers
{
    public class CustomerController : ApiController
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        // GET: api/Customer/5
        public HttpResponseMessage Post(int id, [FromBody]LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                if (id >= 0)
                {
                    var customer = _customerRepository.GetCustomer(id, loginModel);

                    return customer != null ?
                        Request.CreateResponse(HttpStatusCode.OK, customer)
                        : Request.CreateResponse(HttpStatusCode.BadRequest, $"Can not find the customer with {id} ID or wrong credentials");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "The identifier must be the same or greater than 0!");
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"Wrong credentials");
            }
        }

        public HttpResponseMessage Post([FromBody]LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var customers = _customerRepository.GetCustomers(loginModel);
                
                return customers!=null? Request.CreateResponse(HttpStatusCode.OK, customers)
                                        : Request.CreateResponse(HttpStatusCode.BadRequest, "Wrong credentials");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"Wrong credentials");
            }
        }

        public HttpResponseMessage Put([FromBody]CustomerLoginRequest customerLogin)
        {
            if (ModelState.IsValid)
            {
                var result = _customerRepository.InsertCustomer(customerLogin.GetCustomer(), customerLogin.GetLoginModel());
                if (result)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, $"The customer was added");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, $"The customer was not added");
                }
            }
            else
            {
                var errorList = new List<ModelError>();
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        errorList.Add(error);
                    }
                }

                return Request.CreateResponse(HttpStatusCode.BadRequest, errorList);
            }
        }

        public HttpResponseMessage Delete(int id, [FromBody]LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var result = _customerRepository.DeleteCustomer(id, loginModel);
                if (result)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, $"The customer with {id} ID was deleted!");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, $"The customer with {id}");
                }
            }
            else
            {
                var errorList = new List<ModelError>();
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        errorList.Add(error);
                    }
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, errorList);
            }
        }
    }
}
