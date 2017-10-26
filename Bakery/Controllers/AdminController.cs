using BakeryDb;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bakery.Controllers
{
    public class AdminController : Controller
    {
        private BakeryContext bakeryContext = new BakeryContext();

        // GET: Admin
        public ActionResult Index()
        {
            

            return View();
        }
    }
}