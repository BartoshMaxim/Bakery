using BakeryApi.Interfaces;
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

        public CakeController(ICakeRepository cakeRepository)
        {
            _cakeRepository = cakeRepository;
        }

        // GET: api/Cake
        public HttpResponseMessage Get()
        {
            var cakes = _cakeRepository.GetCakes();
            return cakes != null ?
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
            return cakes != null ?
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

        // PUT: api/Cake/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Cake/5
        public void Delete(int id)
        {
        }
    }
}
