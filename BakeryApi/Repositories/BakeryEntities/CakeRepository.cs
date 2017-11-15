using BakeryApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BakeryApi.Respositories
{
    public class CakeRepository : ICakeRepository
    {
        public bool DeleteCake(int cakeid, LoginModel loginModel)
        {
            throw new NotImplementedException();
        }

        public Cake GetCake(int cakeid)
        {
            throw new NotImplementedException();
        }

        public List<Cake> GetCakes()
        {
            throw new NotImplementedException();
        }

        public bool InsertCake(Cake cake, LoginModel loginModel)
        {
            throw new NotImplementedException();
        }

        public bool UpdateCake(Cake updateCake, LoginModel loginModel)
        {
            throw new NotImplementedException();
        }
    }
}