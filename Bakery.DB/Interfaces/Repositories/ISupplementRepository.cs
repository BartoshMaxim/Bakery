using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.DB.Interfaces
{
    public interface ISupplementRepository
    {
        List<Supplement> GetSupplements();

        Supplement GetSupplement(int supplementid);

        bool InsertSupplement(Supplement supplement, LoginModel loginModel);

        bool DeleteSupplement(int supplementid, LoginModel loginModel);

        bool UpdateSupplement(Supplement updateSupplement, LoginModel loginModel);
    }
}
