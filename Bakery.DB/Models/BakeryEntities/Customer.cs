using Bakery.DB.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;


namespace Bakery.DB
{
    public class Customer : ICustomer
    {
        
        public int CustomerId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string CustomerPassword { get; set; }

        [Required]
        public string CustomerPhone { get; set; }

        [Required]
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public RoleType CustomerRole { get; set; }
    }
}
