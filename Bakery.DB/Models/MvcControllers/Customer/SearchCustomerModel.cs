using Bakery.DB.Interfaces;
using System;

namespace Bakery.DB
{
    public class SearchCustomerModel : ICustomer, IPage
    {
        public int CustomerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int Rows { get; set; }

        public int Page { get; set; }

        public bool IsCustomerNotNull()
        {
            if(CustomerId == 0 && FirstName==null && LastName == null && Email == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #region useless fields
        public DateTime CreatedDate { get; set; }
        public string CustomerPassword { get; set; }
        public string CustomerPhone { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public RoleType CustomerRole { get; set; }
        #endregion
    }
}