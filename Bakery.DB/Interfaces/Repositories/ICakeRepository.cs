using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.DB.Interfaces
{
    public interface ICakeRepository
    {
        List<ICake> GetCakes();

        ICake GetCake(int cakeid);

        bool InsertCake(ICake cake);

        bool DeleteCake(int cakeid);

        bool UpdateCake(ICake updateCake);

        bool IsExists(int cakeid);
    }
}
