using Bakery.DB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace Bakery.DB.Repositories
{
    public class ImageRepository : IImageRepository
    {
        public bool DeleteImage(int imageid)
        {
            using (var context = Bakery.Sql())
            {
                return context.Execute(@"
                    DELETE FROM Images
                    WHERE
                        ImageId = @imageid
                ", new
                {
                    imageid = imageid
                }) != 0;
            }
        }

        public IImage GetImage(int imageid)
        {
            using (var context = Bakery.Sql())
            {
                return context.ExecuteScalar<IImage>(@"
                    SELECT
                        ImageId
                        ,ImageName
                        ,ImagePath
                    FROM
                        Images
                    WHERE
                        ImageId = @imageid
                ", new
                {
                    imageid = imageid
                });
            }
        }

        public List<IImage> GetImages()
        {
            using (var context = Bakery.Sql())
            {
                return context.Query<IImage>(@"
                    SELECT
                        ImageId
                        ,ImageName
                        ,ImagePath
                    FROM
                        Images
                ").ToList();
            }
        }

        public bool InsertImage(IImage image)
        {
            image.ImageId = GetIdForNextImage();

            using (var context = Bakery.Sql())
            {
                return context.Execute(@"
                    INSERT 
                        Images(ImageId, ImageName, ImagePath)
                    VALUES
                        (@imageid, @imagename, @imagepath)
                ", new
                {
                    imageid = image.ImageId,
                    imagename = image.ImageName,
                    imagepath = image.ImagePath
                }) != 0;
            }
        }

        public bool UpdateImage(IImage updateImage)
        {
            using (var context = Bakery.Sql())
            {
                return context.Execute(@"
                    UPDATE
                        Images
                    SET
                        ImageName = @imagename,
                        ImagePath = @imagepath
                ", new
                {
                    imagename = updateImage.ImageName,
                    imagepath = updateImage.ImagePath
                }) != 0;
            }
        }

        public int GetCountRows()
        {
            using (var context = Bakery.Sql())
            {
                return context.ExecuteScalar<int>(@"
                    SELECT COUNT(ImageId)       
                    FROM 
                        Images");
            }
        }

        private int GetIdForNextImage()
        {
            var imageID = GetCountRows();

            while (IsExists(imageID))
            {
                imageID++;
            }
            return imageID;
        }

        public bool IsExists(int imageid)
        {
            using (var context = Bakery.Sql())
            {
                return context.ExecuteScalar<int>(@"
                SELECT COUNT(ImageId)
                FROM
                    Images
                WHERE
                    ImageId = @imageid
                ", new
                {
                    imageid = imageid
                }) != 0;
            }
        }
    }
}