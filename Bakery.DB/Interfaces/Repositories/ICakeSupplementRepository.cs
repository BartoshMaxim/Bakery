using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.DB.Interfaces
{
    public interface ICakeSupplementRepository
    {
        List<Supplement> GetSupplements(int cakeid);

        int GetCakeSupplementId(int cakeid, int supplementid);

        bool InsertCakeSupplementReference(int cakeid, int supplementid, LoginModel loginModel);

        bool DeleteCakeSupplementReference(int cakeid, int supplementid, LoginModel loginModel);
    }
}
