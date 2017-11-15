using BakeryApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BakeryApi.Respositories
{
    public class CakeSupplementRepository : ICakeSupplementRepository
    {
        public bool DeleteCakeSupplementReference(int cakeid, int supplementid, LoginModel loginModel)
        {
            throw new NotImplementedException();
        }

        public Supplement GetSupplement(int cakeid, int supplementid)
        {
            throw new NotImplementedException();
        }

        public List<Supplement> GetSupplements(int cakeid)
        {
            throw new NotImplementedException();
        }

        public bool InsertCakeSupplementReference(int cakeid, int supplementid, LoginModel loginModel)
        {
            throw new NotImplementedException();
        }
    }
}