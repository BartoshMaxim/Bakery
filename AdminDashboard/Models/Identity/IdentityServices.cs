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
            var customer = BakeryRepository.GetCustomerRepository().GetCustomer(loginModel.Email, loginModel.Password);
            if (customer == null)
                return false;

            // Create ticket
            var ticket = new FormsAuthenticationTicket(
                1,
                customer.Email,
                DateTime.Now,
                DateTime.Now.AddMinutes(120),
                false,
                customer.CustomerId.ToString());

            // Encrypt the ticket
            var encTicket = FormsAuthentication.Encrypt(ticket);

            // Create the cookie.
            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            HttpContext.Current.Response.Cookies.Add(faCookie);

            return true;
        }

        public void Logout()
        {
            FormsAuthentication.SignOut();
        }
    }
}