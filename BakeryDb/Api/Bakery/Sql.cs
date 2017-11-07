using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryDb
{
    public static partial class Barery
    {
        public static SqlConnection Sql()
        {
            return new SqlConnection(Settings.Bakery.Api.Sql.ConnectionStrings.SqlReporting);
        }

        public static SqlConnection Sql(string connectionStrings)
        {
            return new SqlConnection(connectionStrings);
        }
    }
}
