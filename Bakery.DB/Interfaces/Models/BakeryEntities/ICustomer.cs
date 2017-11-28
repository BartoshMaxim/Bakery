using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.DB.Interfaces
{
    public interface ICustomer
    {
        int CustomerId { get; set; }

        [Required]
        string FirstName { get; set; }

        [Required]
        string LastName { get; set; }

        DateTime CreatedDate { get; set; }

        [Required]
        [EmailAddress]
        string Email { get; set; }

        [Required]
        string CustomerPassword { get; set; }

        [Required]
        string CustomerPhone { get; set; }

        [Required]
        string Address1 { get; set; }

        string Address2 { get; set; }

        [Required]
        string City { get; set; }

        [Required]
        string Country { get; set; }

        [Required]
        RoleType CustomerRole { get; set; }
    }
}
