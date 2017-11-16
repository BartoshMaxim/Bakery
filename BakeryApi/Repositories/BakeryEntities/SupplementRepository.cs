using BakeryApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace BakeryApi.Respositories
{
    public class SupplementRepository : ISupplementRepository
    {
        public bool DeleteSupplement(int supplementid, LoginModel loginModel)
        {
            using (var context = Bakery.Sql())
            {
                return context.Execute(@"
                    DELETE FROM Supplements
                    WHERE
                        SupplementId = @supplementid
                ", new
                {
                    supplementid = supplementid
                }) != 0;
            }
        }

        public Supplement GetSupplement(int supplementid)
        {
            using (var context = Bakery.Sql())
            {
                return context.ExecuteScalar<Supplement>(@"
                    SELECT
                        SupplementId
                        ,SupplementName
                        ,SupplementDescription
                        ,SupplementPrice
                    FROM
                        Supplements
                    WHERE
                        SupplementId = @supplementid
                ", new
                {
                    supplementid = supplementid
                });
            }
        }

        public List<Supplement> GetSupplements()
        {
            using (var context = Bakery.Sql())
            {
                return context.Query<Supplement>(@"
                    SELECT
                        SupplementId
                        ,SupplementName
                        ,SupplementDescription
                        ,SupplementPrice
                    FROM
                        Supplements
                ").ToList();
            }
        }

        public bool InsertSupplement(Supplement supplement, LoginModel loginModel)
        {
            using (var context = Bakery.Sql())
            {
                return context.Execute(@"
                    INSERT
                        Supplements(SupplementName, SupplementDescription, SupplementPrice)
                    VALUE
                        (@supplementname, @supplementdescription, @supplementprice)
                ", new
                {
                    supplementname = supplement.SupplementName,
                    supplementdescription = supplement.SupplementDescription,
                    supplementprice = supplement.SupplementPrice
                }) != 0;
            }
        }

        public bool UpdateSupplement(Supplement updateSupplement, LoginModel loginModel)
        {
            using (var context = Bakery.Sql())
            {
                return context.Execute(@"
                    UPDATE
                        Supplements
                    SET
                        SupplementName         = @supplementname
                        ,SupplementDescription = @supplementdescription
                        ,SupplementPrice       = @supplementprice
                    WHERE
                        SupplementId           = @supplementid
                ", new
                {
                    supplementid = updateSupplement.SupplementId,
                    supplementname = updateSupplement.SupplementName,
                    supplementdescription = updateSupplement.SupplementDescription,
                    supplementprice = updateSupplement.SupplementPrice
                })!=0;
            }
        }
    }
}