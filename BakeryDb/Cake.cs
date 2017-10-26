using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryDb
{
    public class Cake
    {
        public Cake()
        {
            
        }

        public int CakeId { get; set; }

        public string CakeName { get; set; }

        public string CakeDescription { get; set; }

        public int CakePrice { get; set; }

        public virtual Image PreviewImage { get; set; }

        public virtual ICollection<CakeSupplement> CakeSupplements { get; set; }

        public virtual ICollection<Image> Images { get; set; }

    }
}
