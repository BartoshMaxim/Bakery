using Bakery.DB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BakeryApi.Controllers
{
    public class CakeImageController : ApiController
    {
        private ICakeImageRepository _cakeImageRepository;

        public CakeImageController(ICakeImageRepository cakeImageRepository)
        {
            _cakeImageRepository = cakeImageRepository;
        }

        // GET: api/CakeImage
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/CakeImage/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/CakeImage
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/CakeImage/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/CakeImage/5
        public void Delete(int id)
        {
        }
    }
}
