using Bakery.DB.Interfaces;
using System.Linq;
using System.Collections.Generic;
using Dapper;

namespace Bakery.DB.Repositories
{
    public class CakeSupplementRepository : ICakeSupplementRepository
    {
        public bool DeleteCakeSupplementReference(int cakeid, int supplementid)
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
                    cakeid = cakeid,
                    supplementid = supplementid
                }) != 0;
            }
        }

        public int GetCakeSupplementId(int cakeid, int supplementid)
        {
            using (var context = Bakery.Sql())
            {
                return context.ExecuteScalar<int>(@"
                    SELECT
                        CakeSupplements
                    FROM
                        CakeSupplements
                    WHERE
                        CakeId       = @cakeid
                    AND
                        SupplementId = @supplementid
                ", new
                {
                    cakeid = cakeid,
                    supplementid = supplementid
                });
            }
        }

        public List<Supplement> GetSupplements(int cakeid)
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
                            AND cs.CakeId     = @cakeid                               
                ").ToList(); 
            }
        }

        public bool InsertCakeSupplementReference(int cakeid, int supplementid)
        {
            using (var context = Bakery.Sql())
            {
                return context.Execute(@"
                    INSERT
                        CakeSupplements (CakeId, SupplementId)
                    VALUES
                        (@cakeid, @supplementid)
                    ", new
                {
                    cakeid = cakeid,
                    supplementid = supplementid
                }) != 0;
            }
        }
    }
}