using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryApi.Interfaces
{
    public interface ICakeSupplementRepository
    {
        List<Supplement> GetSupplements(int cakeid);

        Supplement GetSupplement(int cakeid, int supplementid);

        bool InsertCakeSupplementReference(int cakeid, int supplementid, LoginModel loginModel);

        bool DeleteCakeSupplementReference(int cakeid, int supplementid, LoginModel loginModel);
    }
}
