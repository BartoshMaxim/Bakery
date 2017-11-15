using BakeryApi.Interfaces;
using System;
using System.Collections.Generic;
using Dapper;

namespace BakeryApi.Repositories
{
    public class CakeImageRepository : ICakeImageRepository
    {
        public bool DeleteCakeImageReference(int cakeid, int imageid, LoginModel loginModel)
        {
            throw new NotImplementedException();
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
                            ON i.ImageId = ci.ImageId
                            AND ci.CakeId = @cakeid
                        ", new
                {
                    cakeid = cakeid
                }).AsList();
            }
        }

        public bool InsertCakeImageReference(int cakeid, int imageid, LoginModel loginModel)
        {
            using (var context = Bakery.Sql())
            {
                context.Execute(@"
                    INSERT
                        CakeImages (CakeId, ImageId)
                    VALUES
                        (@cakeid, @imageid)
                    ", new
                {
                    cakeid = cakeid,
                    imageid = imageid
                });
            }
            return false;
        }
    }
}