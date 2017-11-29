using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.DB.Interfaces
{
    public interface IPage
    {
        int Rows { get; set; }

        int Page { get; set; }
    }
}
