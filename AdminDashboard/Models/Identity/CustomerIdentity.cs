using System;
using System.Security.Principal;

namespace AdminDashboard.Models
{
    public class CustomerIdentity : IIdentity
    {
        public CustomerIdentity(System.Web.Security.FormsAuthenticationTicket ticket)
        {
            Name = ticket.Name;
            Expires = ticket.Expiration;

            // Populate this object with the properties
            CustomerId = int.Parse(ticket.UserData);
        }
        public CustomerIdentity()
        {

        }

        public string Name { get; set; }

        public string AuthenticationType { get; set; }

        public bool IsAuthenticated { get; set; }

        public int CustomerId { get; set; }

        public DateTime Expires { get; set; }
    }
}