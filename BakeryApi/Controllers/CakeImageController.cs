using Bakery.DB;
using Bakery.DB.Interfaces;
using Bakery.DB.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Linq;

namespace BakeryApi.Controllers
{
    public class CakeImageController : ApiController
    {
        private readonly ICakeImageRepository _cakeImageRepository;

        private readonly ICustomerRepository _customerRepository;

        public CakeImageController(ICakeImageRepository cakeImageRepository, ICustomerRepository customerRepository)
        {
            _cakeImageRepository = cakeImageRepository;
            _customerRepository = customerRepository;
        }

        public HttpResponseMessage Get(int id)
        {
            if (id >= 0)
            {
                var cake = _cakeImageRepository.GetImages(id);
                return cake.Any() ?
                    Request.CreateResponse(HttpStatusCode.OK, cake)
                    : Request.CreateResponse(HttpStatusCode.BadRequest, $"Can not find images of the cake with {id} ID!");
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
                var cake = _cakeImageRepository.GetImages(id);
                return cake.Any() ?
                    Request.CreateResponse(HttpStatusCode.OK, cake)
                    : Request.CreateResponse(HttpStatusCode.BadRequest, $"Can not find images of the cake with {id} ID!");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "The identifier must be the same or greater than 0!");
            }
        }


        public HttpResponseMessage Put([FromBody]CakeImageLoginRequest cakeImageLogin)
        {
            if (ModelState.IsValid && _customerRepository.IsAdmin(cakeImageLogin))
            {
                var result = _cakeImageRepository.InsertCakeImageReference(cakeImageLogin);
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

                var result = _cakeImageRepository.DeleteCakeImageReference(id);
                if (result)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, $"The cakeimage with {id} ID was deleted!");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"The cakeimage with {id} ID was not deleted");
                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid credentials!");
            }
        }

        public HttpResponseMessage Delete([FromBody]CakeImageLoginRequest cakeImageLogin)
        {
            if (ModelState.IsValid && _customerRepository.IsAdmin(cakeImageLogin))
            {
                var cakeImageId = _cakeImageRepository.GetCakeImageId(cakeImageLogin);

                var result = _cakeImageRepository.DeleteCakeImageReference(cakeImageLogin);
                if (result)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, $"The cakeimage with {cakeImageId} ID was deleted!");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"The cakeimage with {cakeImageLogin.CakeImageId} ID was not deleted");
                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid credentials!");
            }
        }
    }
}
