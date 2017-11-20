using Bakery.DB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace AdminDashboard.Models
{
    public class IdentityServices
    {
        public bool Login(LoginModel loginModel)
        {
            var customerid = BakeryRepository.GetCustomerRepository().GetCustomerId(loginModel.Email, loginModel.Password);
            if (customerid != 0)
                return false;

            // Create ticket
            var ticket = new FormsAuthenticationTicket(
                1,
                loginModel.Email,
                DateTime.Now,
                DateTime.Now.AddMinutes(120),
                false,
                customerid.ToString());

            // Encrypt the ticket
            var encTicket = FormsAuthentication.Encrypt(ticket);

            // Create the cookie.
            var cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName]; //saved user
            if (cookie == null)
            {
                HttpContext.Current.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
            }
            else
            {
                cookie.Value = encTicket;
                HttpContext.Current.Response.Cookies.Set(cookie);
            }
            return true;
        }

        public void Logout()
        {
            FormsAuthentication.SignOut();
        }
    }
}