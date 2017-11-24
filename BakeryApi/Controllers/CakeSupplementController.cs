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
    public class CakeSupplementController : ApiController
    {
        private readonly ICakeSupplementRepository _cakeSupplementRepository;

        private readonly ICustomerRepository _customerRepository;

        public CakeSupplementController(ICakeSupplementRepository cakeSupplementRepository, ICustomerRepository customerRepository)
        {
            _cakeSupplementRepository = cakeSupplementRepository;
            _customerRepository = customerRepository;
        }

        public HttpResponseMessage Get(int id)
        {
            if (id >= 0)
            {
                var supplements = _cakeSupplementRepository.GetSupplements(id);
                return supplements.Any()?
                    Request.CreateResponse(HttpStatusCode.OK, supplements)
                    : Request.CreateResponse(HttpStatusCode.BadRequest, $"Can not find supplements of the cake with {id} ID!");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "The identifier must be the same or greater than 0!");
            }
        }

        public HttpResponseMessage Post(int id)
        {
            if (id >= 0)
            {
                var supplements = _cakeSupplementRepository.GetSupplements(id);
                return supplements.Any() ?
                    Request.CreateResponse(HttpStatusCode.OK, supplements)
                    : Request.CreateResponse(HttpStatusCode.BadRequest, $"Can not find supplements of the cake with {id} ID!");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "The identifier must be the same or greater than 0!");
            }
        }


        public HttpResponseMessage Put([FromBody]CakeSupplementLoginRequest cakeImageLogin)
        {
            if (ModelState.IsValid && _customerRepository.IsAdmin(cakeImageLogin))
            {
                var result = _cakeSupplementRepository.InsertCakeSupplementReference(cakeImageLogin);
                if (result)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, $"The cakeimage was added");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"The cakeimage was not added");
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

                var result = _cakeSupplementRepository.DeleteCakeSupplementReference(id);
                if (result)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, $"The cakesupplement with {id} ID was deleted!");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"The cakesupplement with {id} ID was not deleted");
                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid credentials!");
            }
        }

        public HttpResponseMessage Delete([FromBody]CakeSupplementLoginRequest cakeSupplementLogin)
        {
            if (ModelState.IsValid && _customerRepository.IsAdmin(cakeSupplementLogin))
            {
                var cakeSupplementId = _cakeSupplementRepository.GetCakeSupplementId(cakeSupplementLogin);

                var result = _cakeSupplementRepository.DeleteCakeSupplementReference(cakeSupplementLogin);
                if (result)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, $"The cakesupplement with {cakeSupplementId} ID was deleted!");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"The cakesupplement with {cakeSupplementLogin.CakeSupplementId} ID was not deleted");
                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid credentials!");
            }
        }
    }
}
