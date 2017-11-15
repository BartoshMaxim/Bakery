using AdminDashboard;
using BakeryApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace AdminDashboard.Models
{
    public class Identity
    {
        public static CustomerIdentity Current
        {
            get
            {
                try
                {
                    return HttpContext.Current.User.Identity as CustomerIdentity;
                }
                catch
                {
                    HttpContext.Current.Response.Redirect("Account/Logout");
                    return null;
                }
            }
        }

        public bool Login(LoginModel loginModel)
        {
            var customerid = CustomerService.GetCustomerId(loginModel.Email, loginModel.Password);
            if (customerid != 0)
                return false;

            // Create ticket
            var ticket = new FormsAuthenticationTicket(
                1,
                loginModel.Email,
                DateTime.Now,
                DateTime.Now.AddMinutes(120),
                false,
                loginModel.Email + customerid);

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
    }
}