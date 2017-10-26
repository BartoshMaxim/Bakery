using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryDb
{
    public class User
    {
        public int UserId;

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public int Phone { get; set; }

        public string Email { get; set; }

        public virtual Role Role { get; set; }
    }
}
