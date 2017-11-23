using Bakery.DB.Interfaces;
using System;
using System.Collections.Generic;
using Dapper;
using System.Linq;

namespace Bakery.DB.Repositories
{
    public class CakeRepository : ICakeRepository
    {
        public bool DeleteCake(int cakeid)
        {
            using (var context = Bakery.Sql())
            {
                return context.Execute(@"
                    DELETE FROM Cakes
                    WHERE
                        ImageId = @imageid
                ", new
                {
                    cakeid = cakeid
                }) != 0;
            }
        }

        public ICake GetCake(int cakeid)
        {
            using (var context = Bakery.Sql())
            {
                return context.ExecuteScalar<ICake>(@"
                    SELECT
                        CakeId
                        ,CakeName
                        ,CakeDescription
                        ,CakePrice
                        ,ImageId
                        ,AddedDate
                    FROM
                        Cakes
                    WHERE
                        CakeId = @cakeid
                ", new
                {
                    cakeid = cakeid
                });
            }
        }

        public List<ICake> GetCakes()
        {
            using (var context = Bakery.Sql())
            {
                return context.Query<ICake>(@"
                    SELECT
                        CakeId
                        ,CakeName
                        ,CakeDescription
                        ,CakePrice
                        ,ImageId
                        ,AddedDate
                    FROM
                        Cakes").ToList();
            }
        }

        public bool InsertCake(ICake cake)
        {
            cake.CakeId = GetIdForNextCake();
            cake.AddedDate = DateTime.Now;

            using (var context = Bakery.Sql())
            {
                return context.Execute(@"
                    INSERT
                        Cakes (CakeId, CakeName, CakeDescription, CakePrice, ImageId, AddedDate)
                    VALUES
                        (@cakeid, @cakename, @cakedescription, @cakeprice, @imageid, @addeddate)
                ", new
                {
                    cakeid = cake.CakeId,
                    cakename = cake.CakeName,
                    cakedescription = cake.CakeDescription,
                    cakeprice = cake.CakePrice,
                    imageid = cake.ImageId,
                    addeddate = cake.AddedDate
                }) != 0;
            }
        }

        public bool UpdateCake(ICake updateCake)
        {
            using (var context = Bakery.Sql())
            {
                return context.Execute(@"
                    UPDATE 
                        Cakes
                    SET
                        CakeName         = @cakename
                        ,CakeDescription = @cakedescription
                        ,CakePrice       = @cakeprice
                        ,ImageId         = @imageid
                        ,AddedDate       = @addeddate
                    WHERE
                        CakeId = @cakeid
                ", new
                {
                    cakeid = updateCake.CakeId,
                    cakename = updateCake.CakeName,
                    cakedescription = updateCake.CakeDescription,
                    cakeprice = updateCake.CakePrice,
                    imageid = updateCake.ImageId,
                    addeddate = updateCake.AddedDate
                }) != 0;
            }
        }

        public int GetCountRows()
        {
            using (var context = Bakery.Sql())
            {
                return context.ExecuteScalar<int>(@"
                    SELECT COUNT(CakeId)       
                    FROM 
                        Cakes");
            }
        }

        private int GetIdForNextCake()
        {
            var cakeID = GetCountRows();

            while (IsExists(cakeID))
            {
                cakeID++;
            }
            return cakeID;
        }

        public bool IsExists(int cakeid)
        {
            using (var context = Bakery.Sql())
            {
                return context.ExecuteScalar<int>(@"
                SELECT COUNT(CakeId)
                FROM
                    Cakes
                WHERE
                    CakeId = @cakeid
                ", new
                {
                    cakeid = cakeid
                }) != 0;
            }
        }
    }
}