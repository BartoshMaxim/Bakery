using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.DB.Interfaces
{
    public interface ISupplementRepository
    {
        List<ISupplement> GetSupplements();

        ISupplement GetSupplement(int supplementid);

        bool InsertSupplement(ISupplement supplement);

        bool DeleteSupplement(int supplementid);

        bool UpdateSupplement(ISupplement updateSupplement);
    }
}
