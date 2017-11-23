using Bakery.DB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BakeryApi.Controllers
{
    public class OrderTypeController : ApiController
    {
        private readonly IOrderTypeRepository _orderTypeRepository;

        public OrderTypeController(IOrderTypeRepository orderTypeRepository)
        {
            _orderTypeRepository = orderTypeRepository;
        }

        public HttpResponseMessage Get()
        {
            var orderTypes = _orderTypeRepository.GetOrderTypes();
            return orderTypes != null ?
                Request.CreateResponse(HttpStatusCode.OK, orderTypes)
                : Request.CreateResponse(HttpStatusCode.InternalServerError, "Can not find any ordertype!");
        }

        public HttpResponseMessage Get(int id)
        {
            if (id >= 0)
            {
                var orders = _orderTypeRepository.GetOrders(id);
                return orders != null ?
                    Request.CreateResponse(HttpStatusCode.OK, orders)
                    : Request.CreateResponse(HttpStatusCode.BadRequest, $"Can not find orders of the ordertype with {id} ID!");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "The identifier must be the same or greater than 0!");
            }
        }

        public HttpResponseMessage Post()
        {
            var orderTypes = _orderTypeRepository.GetOrderTypes();
            return orderTypes != null ?
                Request.CreateResponse(HttpStatusCode.OK, orderTypes)
                : Request.CreateResponse(HttpStatusCode.InternalServerError, "Can not find any ordertype!");
        }

        public HttpResponseMessage Post(int id)
        {
            if (id >= 0)
            {
                var orders = _orderTypeRepository.GetOrders(id);
                return orders != null ?
                    Request.CreateResponse(HttpStatusCode.OK, orders)
                    : Request.CreateResponse(HttpStatusCode.BadRequest, $"Can not find orders of the ordertype with {id} ID!");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "The identifier must be the same or greater than 0!");
            }
        }
    }
}
