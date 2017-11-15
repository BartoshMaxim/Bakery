using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryApi.Interfaces
{
    public interface ICakeRepository
    {
        List<Cake> GetCakes();

        Cake GetCake(int cakeid);

        bool InsertCake(Cake cake, LoginModel loginModel);

        bool DeleteCake(int cakeid, LoginModel loginModel);

        bool UpdateCake(Cake updateCake, LoginModel loginModel);
    }
}
