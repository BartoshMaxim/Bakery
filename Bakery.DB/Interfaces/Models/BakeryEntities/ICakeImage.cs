using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.DB.Interfaces
{
    interface ICakeImage
    {
        int CakeImageId { get; set; }

        int CakeId { get; set; }

        int ImageId { get; set; }
    }
}
