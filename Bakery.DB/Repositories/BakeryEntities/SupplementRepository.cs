using Bakery.DB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace Bakery.DB.Repositories
{
    public class SupplementRepository : ISupplementRepository
    {
        public bool DeleteSupplement(int supplementid)
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

        public ISupplement GetSupplement(int supplementid)
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
                    WHERE
                        SupplementId = @supplementid
                ", new
                {
                    supplementid = supplementid
                }).FirstOrDefault();
            }
        }

        public IList<Supplement> GetSupplements()
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

        public bool InsertSupplement(ISupplement supplement)
        {
            supplement.SupplementId = GetIdForNextSupplement();

            if (supplement.SupplementId == 0)
            {
                supplement.SupplementId++;
            }

            using (var context = Bakery.Sql())
            {
                return context.Execute(@"
                    INSERT
                        Supplements(SupplementId, SupplementName, SupplementDescription, SupplementPrice)
                    VALUES
                        (@supplementid, @supplementname, @supplementdescription, @supplementprice)
                ", new
                {
                    supplementid = supplement.SupplementId,
                    supplementname = supplement.SupplementName,
                    supplementdescription = supplement.SupplementDescription,
                    supplementprice = supplement.SupplementPrice
                }) != 0;
            }
        }

        public bool UpdateSupplement(ISupplement updateSupplement)
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
                }) != 0;
            }
        }

        public int GetCountRows()
        {
            using (var context = Bakery.Sql())
            {
                return context.ExecuteScalar<int>(@"
                    SELECT COUNT(SupplementId)       
                    FROM 
                        Supplements");
            }
        }

        private int GetIdForNextSupplement()
        {
            var supplementID = GetCountRows();

            while (IsExists(supplementID))
            {
                supplementID++;
            }
            return supplementID;
        }

        public bool IsExists(int supplementid)
        {
            using (var context = Bakery.Sql())
            {
                return context.ExecuteScalar<int>(@"
                SELECT COUNT(SupplementId)
                FROM
                    Supplements
                WHERE
                    SupplementId = @supplementid
                ", new
                {
                    supplementid = supplementid
                }) != 0;
            }
        }
    }
}