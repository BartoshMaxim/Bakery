using AdminDashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminDashboard.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View("Login");
        }

        public ActionResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        public ActionResult Login(LoginModel login)
        {
            return View(login);
        }

        public ActionResult Logout()
        {
            return View("Login");
        }
    }
}