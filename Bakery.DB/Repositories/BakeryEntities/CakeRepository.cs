﻿using Bakery.DB.Interfaces;
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

        public Cake GetCake(int cakeid)
        {
            using (var context = Bakery.Sql())
            {
                return context.ExecuteScalar<Cake>(@"
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

        public List<Cake> GetCakes()
        {
            using (var context = Bakery.Sql())
            {
                return context.Query<Cake>(@"
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
            using (var context = Bakery.Sql())
            {
                return context.Execute(@"
                    INSERT
                        Cakes (CakeName, CakeDescription, CakePrice, ImageId, AddedDate)
                    VALUES
                        (@cakename, @cakedescription, @cakeprice, @imageid, @addeddate)
                ", new
                {
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
    }
}