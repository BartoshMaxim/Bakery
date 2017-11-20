using Bakery.DB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace Bakery.DB.Repositories
{
    public class RoleTypeRepository : IRoleTypeRepository
    {
        public RoleType GetOrder(int roletypeid)
        {
            using (var context = Bakery.Sql())
            {
                return (RoleType) context.ExecuteScalar<int>(@"
                    SELECT
                        CustomerRoleId
                    FROM
                        CustomerRoles
                    WHERE
                        CustomerRoleId = @roletypeid
                ", new
                {
                    roletypeid = roletypeid
                });
            }
        }

        public List<RoleType> GetRoleTypes()
        {
            using (var context = Bakery.Sql())
            {
                return context.Query<RoleType>(@"
                    SELECT
                        CustomerRoleId
                    FROM
                        CustomerRoles
                ").ToList();
            }
        }
    }
}