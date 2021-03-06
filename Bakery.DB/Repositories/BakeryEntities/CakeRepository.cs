﻿using Bakery.DB.Interfaces;
using System;
using System.Collections.Generic;
using Dapper;
using System.Linq;
using System.Text;

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
                        CakeId = @cakeid
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
                return context.Query<Cake>(@"
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
                }).FirstOrDefault();
            }
        }

        public IList<Cake> GetCakes()
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

        public int InsertCake(ICake cake)
        {
            cake.CakeId = GetIdForNextCake();
            cake.AddedDate = DateTime.Now;

            if (cake.CakeId == 0)
            {
                cake.CakeId++;
            }

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
                }) != 0 ? cake.CakeId : 0;
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

        private string CreateQuery(ICake cake)
        {
            var query = new StringBuilder();

            if (cake.CakeId != 0)
            {
                query.Append($"WHERE CakeId={cake.CakeId}");
            }

            if (cake.CakeName != null && !cake.CakeName.Equals(string.Empty))
            {
                if (query.Length == 0)
                {
                    query.Append("WHERE ");
                }
                else
                {
                    query.Append(" AND ");
                }

                query.Append($"CakeName LIKE N'%{cake.CakeName}%'");
            }

            if (cake.CakePrice != 0)
            {
                if (query.Length == 0)
                {
                    query.Append("WHERE ");
                }
                else
                {
                    query.Append(" AND ");
                }

                query.Append($"CakePrice={cake.CakePrice}");
            }

            return query.ToString();
        }

        public IList<Cake> GetCakes(int from, int to, ICake searchCake)
        {
            var query = string.Empty;
            if (searchCake != null)
            {
                query = CreateQuery(searchCake);
            }

            to = to - from;

            using (var context = Bakery.Sql())
            {
                return context.Query<Cake>($@"
                   SELECT
                        CakeId
                        ,CakeName
                        ,CakeDescription
                        ,CakePrice
                        ,ImageId
                        ,AddedDate
                    FROM
                        Cakes
                    {query}
                    ORDER BY CakeId DESC
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

        public int GetCountRows(ICake searchCake)
        {
            string query = string.Empty;

            if (searchCake != null)
            {
                query = CreateQuery(searchCake);
            }

            using (var context = Bakery.Sql())
            {
                return context.ExecuteScalar<int>(@"
                    SELECT COUNT(CakeId)       
                    FROM 
                        Cakes
                    " + query);
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