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

        public Image GetImage(int imageid)
        {
            using (var context = Bakery.Sql())
            {
                return context.ExecuteScalar<Image>(@"
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

        public List<Image> GetImages()
        {
            using (var context = Bakery.Sql())
            {
                return context.Query<Image>(@"
                    SELECT
                        ImageId
                        ,ImageName
                        ,ImagePath
                    FROM
                        Images
                ").ToList();
            }
        }

        public bool InsertImage(Image image)
        {
            using (var context = Bakery.Sql())
            {
                return context.Execute(@"
                    INSERT 
                        Images(ImageName, ImagePath)
                    VALUES
                        (@imagename, @imagepath)
                ", new
                {
                    imagename = image.ImageName,
                    imagepath = image.ImagePath
                }) != 0;
            }
        }

        public bool UpdateImage(Image updateImage)
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
    }
}