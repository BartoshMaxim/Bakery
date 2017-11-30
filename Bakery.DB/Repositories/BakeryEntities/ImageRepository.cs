using Bakery.DB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Text;

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
                return context.Query<Image>(@"
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
                }).FirstOrDefault();
            }
        }

        public IList<Image> GetImages()
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

        public bool InsertImage(IImage image)
        {
            image.ImageId = GetIdForNextImage();

            if (image.ImageId == 0)
            {
                image.ImageId++;
            }
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

        public IList<Image> GetImages(int from, int to, IImage image = null)
        {
            var query = string.Empty;
            if (image != null)
            {
                query = CreateQuery(image);
            }

            to = to - from;

            using (var context = Bakery.Sql())
            {
                return context.Query<Image>($@"
                    SELECT
                        ImageId
                        ,ImageName
                        ,ImagePath
                    FROM
                        Images
                    {query}
                    ORDER BY ImageId DESC
                    OFFSET @from ROWS
                    FETCH NEXT @to ROWS ONLY
                ", new
                {
                    from = from,
                    to = to
                }
                ).ToList();
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

        public int GetCountRows(IImage image)
        {
            var query = string.Empty;
            if (image != null)
            {
                query = CreateQuery(image);
            }

            using (var context = Bakery.Sql())
            {
                return context.ExecuteScalar<int>(@"
                    SELECT COUNT(ImageId)       
                    FROM 
                        Images
                    " + query);
            }
        }

        private string CreateQuery(IImage image)
        {
            var query = new StringBuilder();

            if (image.ImageId != 0)
            {
                query.Append($"WHERE ImageId={image.ImageId}");
            }

            if (image.ImageName != null && !image.ImageName.Equals(string.Empty))
            {
                if (query.Length == 0)
                {
                    query.Append("WHERE ");
                }
                else
                {
                    query.Append(" AND ");
                }

                query.Append($"ImageName LIKE '%{image.ImageName}%'");
            }

            if (image.ImagePath != null && !image.ImagePath.Equals(string.Empty))
            {
                if (query.Length == 0)
                {
                    query.Append("WHERE ");
                }
                else
                {
                    query.Append(" AND ");
                }

                query.Append($"ImagePath LIKE '%{image.ImagePath}%'");
            }

            return query.ToString();
        }

        public int GetIdForNextImage()
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