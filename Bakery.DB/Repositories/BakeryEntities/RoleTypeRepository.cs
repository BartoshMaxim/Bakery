using Bakery.DB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace Bakery.DB.Repositories
{
    public class RoleTypeRepository : IRoleTypeRepository
    {
        public IList<Customer> GetCustomers(int roletypeid)
        {
            using (var context = Bakery.Sql())
            {
                return context.Query<Customer>(@"
                    SELECT
                        CustomerId
                        ,FirstName
                        ,LastName
                        ,CreatedDate
                        ,Email
                        ,CustomerPassword
                        ,CustomerPhone
                        ,CustomerRole = CustomerRoleId
                    
                        ,Address1
                        ,Address2
                        ,City
                        ,Country
                    FROM
                        Customers
                    WHERE
                        CustomerRoleId = @roletypeid
                ", new
                {
                    roletypeid = roletypeid
                }).ToList();
            }
        }

        public IList<RoleType> GetRoleTypes()
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