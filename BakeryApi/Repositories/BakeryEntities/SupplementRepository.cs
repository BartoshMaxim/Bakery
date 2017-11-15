using BakeryApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BakeryApi.Respositories
{
    public class SupplementRepository : ISupplementRepository
    {
        public bool DeleteSupplement(int supplementid, LoginModel loginModel)
        {
            throw new NotImplementedException();
        }

        public Supplement GetSupplement(int supplementid)
        {
            throw new NotImplementedException();
        }

        public List<Supplement> GetSupplements()
        {
            throw new NotImplementedException();
        }

        public bool InsertSupplement(Supplement supplement, LoginModel loginModel)
        {
            throw new NotImplementedException();
        }

        public bool UpdateSupplement(Supplement updateSupplement, LoginModel loginModel)
        {
            throw new NotImplementedException();
        }
    }
}