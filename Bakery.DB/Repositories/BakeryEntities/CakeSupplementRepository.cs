using Bakery.DB.Interfaces;
using System.Linq;
using System.Collections.Generic;
using Dapper;

namespace Bakery.DB.Repositories
{
    public class CakeSupplementRepository : ICakeSupplementRepository
    {
        public bool DeleteCakeSupplementReference(ICakeSupplement cakeSupplement)
        {
            using (var context = Bakery.Sql())
            {
                return context.Execute(@"
                    DELETE FROM CakeSupplements
                    WHERE
                        CakeId       = @cakeid
                    AND
                        SupplementId = @supplementid
                ", new
                {
                    cakeid = cakeSupplement.CakeId,
                    supplementid = cakeSupplement.SupplementId
                }) != 0;
            }
        }

        public bool DeleteCakeSupplementReference(int cakesupplementid)
        {
            using (var context = Bakery.Sql())
            {
                return context.Execute(@"
                    DELETE FROM CakeSupplements
                    WHERE
                        CakeSupplementId = @cakesupplementid
                ", new
                {
                    cakesupplementid = cakesupplementid,
                }) != 0;
            }
        }

        public int GetCakeSupplementId(ICakeSupplement cakeSupplement)
        {
            using (var context = Bakery.Sql())
            {
                return context.ExecuteScalar<int>(@"
                    SELECT
                        CakeSupplementId
                    FROM
                        CakeSupplements
                    WHERE
                        CakeId       = @cakeid
                    AND
                        SupplementId = @supplementid
                ", new
                {
                    cakeid = cakeSupplement.CakeId,
                    supplementid = cakeSupplement.SupplementId
                });
            }
        }

        public ICakeSupplement GetCakeSupplement(int cakesupplementid)
        {
            using (var context = Bakery.Sql())
            {
                return context.Query<CakeSupplement>(@"
                    SELECT
                        CakeSupplementId,
                        SupplementId,
                        CakeId
                    FROM
                        CakeSupplements
                    WHERE
                        CakeSupplementId = @cakesupplementid
                ", new
                {
                    cakesupplementid = cakesupplementid
                }).FirstOrDefault();
            }
        }

        public IList<Supplement> GetSupplements(int cakeid)
        {
            using (var context = Bakery.Sql())
            {
                return context.Query<Supplement>(@"
                    SELECT
                        s.SupplementId
                        ,s.SupplementName
                        ,s.SupplementDescription
                    FROM
                        Supplements as s
                            JOIN CakeSupplements as cs
                            ON cs.SupplementId = s.SupplementId
                            AND cs.CakeId      = @cakeid                               
                ", new
                {
                    cakeid = cakeid
                }).ToList(); 
            }
        }

        public bool InsertCakeSupplementReference(ICakeSupplement cakeSupplement)
        {
            cakeSupplement.CakeSupplementId = GetIdForNextCakeSupplement();

            using (var context = Bakery.Sql())
            {
                return context.Execute(@"
                    INSERT
                        CakeSupplements (CakeSupplementId, CakeId, SupplementId)
                    VALUES
                        (@cakesupplementid, @cakeid, @supplementid)
                    ", new
                {
                    cakesupplementid = cakeSupplement.CakeSupplementId,
                    cakeid = cakeSupplement.CakeId,
                    supplementid = cakeSupplement.SupplementId
                }) != 0;
            }
        }

        public int GetCountRows()
        {
            using (var context = Bakery.Sql())
            {
                return context.ExecuteScalar<int>(@"
                    SELECT COUNT(CakeSupplementId)       
                    FROM 
                        CakeSupplements");
            }
        }

        private int GetIdForNextCakeSupplement()
        {
            var cakeSupplementID = GetCountRows();

            while (IsExists(cakeSupplementID))
            {
                cakeSupplementID++;
            }
            return cakeSupplementID;
        }

        public bool IsExists(int cakesuppmentid)
        {
            using (var context = Bakery.Sql())
            {
                return context.ExecuteScalar<int>(@"
                SELECT COUNT(CakeSupplementId)
                FROM
                    CakeSupplements
                WHERE
                    CakeSupplementId = @cakesuppmentid
                ", new
                {
                    cakesuppmentid = cakesuppmentid
                }) != 0;
            }
        }
    }
}