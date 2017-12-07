using Bakery.DB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.DB
{
    public class SearchSupplementModel : ISupplement, IPage
    {
        public int SupplementId { get; set; }

        public string SupplementName { get; set; }

        public string SupplementDescription { get; set; }

        public int SupplementPrice { get; set; }

        public float SupplementWeight { get; set; }

        public int Rows { get; set; }

        public int Page { get; set; }

        public bool IsSupplementNotNull()
        {
            if (SupplementId == 0 && SupplementName == null && SupplementWeight == 0 && SupplementPrice == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
