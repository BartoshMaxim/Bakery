using Bakery.DB.Interfaces;
using System.Linq;
using System.Collections.Generic;
using Dapper;

namespace Bakery.DB.Repositories
{
    public class CakeImageRepository : ICakeImageRepository
    {
        public bool DeleteCakeImageReference(ICakeImage cakeImage)
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
                    cakeid = cakeImage.CakeId,
                    imageid = cakeImage.ImageId
                }) != 0;
            }
        }

        public bool DeleteCakeImageReference(int cakeImageId)
        {
            using (var context = Bakery.Sql())
            {
                return context.Execute(@"
                    DELETE FROM CakeImages
                    WHERE
                        CakeImageId = @cakeImageId
                ", new
                {
                    cakeImageId = cakeImageId
                }) != 0;
            }
        }

        public int GetCakeImageId(ICakeImage cakeImage)
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
                    cakeid = cakeImage.CakeId,
                    imageid = cakeImage.ImageId
                });
            }
        }

        public List<IImage> GetImages(int cakeid)
        {
            using (var context = Bakery.Sql())
            {
                return context.Query<IImage>(@"
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

        public bool InsertCakeImageReference(ICakeImage cakeImage)
        {
            cakeImage.CakeImageId = GetIdForNextCakeImage();

            using (var context = Bakery.Sql())
            {
                return context.Execute(@"
                    INSERT
                        CakeImages (CakeImageId, CakeId, ImageId)
                    VALUES
                        (@cakeimageid, @cakeid, @imageid)
                    ", new
                {
                    cakeimageid = cakeImage.CakeImageId,
                    cakeid = cakeImage.CakeId,
                    imageid = cakeImage.ImageId
                }) != 0;
            }
        }

        public int GetCountRows()
        {
            using (var context = Bakery.Sql())
            {
                return context.ExecuteScalar<int>(@"
                    SELECT COUNT(CakeImageId)       
                    FROM 
                        CakeImages");
            }
        }

        private int GetIdForNextCakeImage()
        {
            var cakeImageID = GetCountRows();

            while (IsExists(cakeImageID))
            {
                cakeImageID++;
            }
            return cakeImageID;
        }

        public bool IsExists(int cakeimageid)
        {
            using (var context = Bakery.Sql())
            {
                return context.ExecuteScalar<int>(@"
                SELECT COUNT(CakeImageId)
                FROM
                    CakeImages
                WHERE
                    CakeImageId = @cakeimageid
                ", new
                {
                    cakeimageid = cakeimageid
                }) != 0;
            }
        }
    }
}