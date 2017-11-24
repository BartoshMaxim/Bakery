using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.DB.Interfaces
{
    public interface ICakeSupplementRepository
    {
        IList<Supplement> GetSupplements(int cakeid);

        int GetCakeSupplementId(ICakeSupplement cakeSupplement);

        bool InsertCakeSupplementReference(ICakeSupplement cakeSupplement);

        bool DeleteCakeSupplementReference(ICakeSupplement cakeSupplement);

        bool DeleteCakeSupplementReference(int cakesupplementid);

        ICakeSupplement GetCakeSupplement(int cakesupplementid);

        bool IsExists(int cakesuppmentid);

        int GetCountRows();
    }
}
