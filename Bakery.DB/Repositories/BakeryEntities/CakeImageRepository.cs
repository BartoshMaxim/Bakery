using Bakery.DB.Interfaces;
using System.Linq;
using System.Collections.Generic;
using Dapper;

namespace Bakery.DB.Repositories
{
    public class CakeImageRepository : ICakeImageRepository
    {
        public bool DeleteCakeImageReference(int cakeid, int imageid)
        {
            using (var context = Bakery.Sql())
            {
                return context.Execute(@"
                    DELETE FROM CakeImages
                    WHERE
                        CakeId  = @cakeid
                    AND
                        ImageId = @imageid
                ", new
                {
                    cakeid = cakeid,
                    imageid = imageid
                }) != 0;
            }
        }

        public int GetCakeImageId(int cakeid, int imageid)
        {
            using (var context = Bakery.Sql())
            {
                return context.ExecuteScalar<int>(@"
                    SELECT
                        CakeImageId
                    FROM
                        CakeImages
                    WHERE
                        CakeId  = @cakeid
                    AND
                        ImageId = @imageid
                ", new
                {
                    cakeid = cakeid,
                    imageid = imageid
                });
            }
        }

        public List<Image> GetImages(int cakeid)
        {
            using (var context = Bakery.Sql())
            {
                return context.Query<Image>(@"
                    SELECT
                        i.ImageId
                        ,i.ImageName
                        ,i.ImagePath
                    FROM
                        Images as i
                    JOIN
                        CakeImages as ci
                            ON ci.ImageId = i.ImageId
                            AND ci.CakeId = @cakeid
                        ", new
                {
                    cakeid = cakeid
                }).ToList(); 
            }
        }

        public bool InsertCakeImageReference(int cakeid, int imageid)
        {
            using (var context = Bakery.Sql())
            {
                return context.Execute(@"
                    INSERT
                        CakeImages (CakeId, ImageId)
                    VALUES
                        (@cakeid, @imageid)
                    ", new
                {
                    cakeid = cakeid,
                    imageid = imageid
                }) != 0;
            }
        }
    }
}