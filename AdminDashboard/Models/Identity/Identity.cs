using AdminDashboard;
using Bakery.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace AdminDashboard.Models
{
    public static class Identity
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
    }
}