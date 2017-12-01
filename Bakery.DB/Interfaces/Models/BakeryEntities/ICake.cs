using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.DB.Interfaces
{
    public interface ICake
    {
        int CakeId { get; set; }

        string CakeName { get; set; }

        string CakeDescription { get; set; }

        float CakePrice { get; set; }

        /// <summary>
        /// Preview image
        /// </summary>
        int ImageId { get; set; }

        DateTime AddedDate { get; set; }
    }
}
