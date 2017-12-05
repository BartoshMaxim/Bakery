using Bakery.DB;
using Bakery.DB.Interfaces;
using Bakery.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BakeryApi.Controllers
{
    public class CakeController : ApiController
    {
        private readonly ICakeRepository _cakeRepository;

        private readonly ICustomerRepository _customerRepository;

        public CakeController(ICakeRepository cakeRepository, ICustomerRepository customerRepository)
        {
            _cakeRepository = cakeRepository;
            _customerRepository = customerRepository;
        }

        // GET: api/Cake
        public HttpResponseMessage Get()
        {
            var cakes = _cakeRepository.GetCakes();
            return cakes.Any()?
                Request.CreateResponse(HttpStatusCode.OK, cakes)
                : Request.CreateResponse(HttpStatusCode.InternalServerError, "Can not find any cakes!");
        }

        // GET: api/Cake/5
        public HttpResponseMessage Get(int id)
        {
            if (id >= 0)
            {
                var cake = _cakeRepository.GetCake(id);
                return cake != null ?
                    Request.CreateResponse(HttpStatusCode.OK, cake)
                    : Request.CreateResponse(HttpStatusCode.BadRequest, $"Can not find cake with {id} ID!");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "The identifier must be the same or greater than 0!");
            }
        }

        // POST: api/Cake
        public HttpResponseMessage Post()
        {
            var cakes = _cakeRepository.GetCakes();
            return cakes.Any()?
                Request.CreateResponse(HttpStatusCode.OK, cakes)
                : Request.CreateResponse(HttpStatusCode.InternalServerError, "Can not find any cakes!");
        }

        public HttpResponseMessage Post(int id)
        {
            if (id >= 0)
            {
                var cake = _cakeRepository.GetCake(id);
                return cake != null ?
                    Request.CreateResponse(HttpStatusCode.OK, cake)
                    : Request.CreateResponse(HttpStatusCode.BadRequest, $"Can not find cake with {id} ID!");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "The identifier must be the same or greater than 0!");
            }
        }


        public HttpResponseMessage Put([FromBody]CakeLoginRequest cakeLogin)
        {
            if (ModelState.IsValid && _customerRepository.IsAdmin(cakeLogin))
            {
                var result = _cakeRepository.InsertCake(cakeLogin);
                if (result!=0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, $"The cake was added");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"The cake was not added");
                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid credentials!");
            }
        }

        public HttpResponseMessage Delete(int id, [FromBody]LoginModel loginModel)
        {
            if (ModelState.IsValid && _customerRepository.IsAdmin(loginModel))
            {

                var result = _cakeRepository.DeleteCake(id);
                if (result)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, $"The cake with {id} ID was deleted!");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"The cake with {id} ID was not deleted");
                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid credentials!");
            }
        }
    }
}
