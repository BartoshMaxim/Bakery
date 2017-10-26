using BakeryDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bakery.Controllers
{
    public class HomeController : Controller
    {
        private BakeryContext bakeryContext = new BakeryContext();

        // GET: Home
        public ActionResult Index()
        {
           
            return View();
        }
    }
}
