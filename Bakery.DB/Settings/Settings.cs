﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bakery.DB
{
    public static class Settings
    {
        /// <summary>
        /// Exigo-specific API credentials and configurations
        /// </summary>
        public static class Bakery
        {
            /// <summary>
            /// Web service, OData and SQL API credentials and configurations
            /// </summary>
            public static class Api
            {
                public static class Sql
                {
                    public static class ConnectionStrings

                    {
                        public static string SqlReporting

                        {
                            get
                            {
                                return "Data Source=mssql5.gear.host;Initial Catalog=bakerysanbox1;Persist Security Info=True;User ID=bakerysanbox1;Password=Sj3UJL-VO4e~;Pooling=True";
                            }
                        }

                    }
                }
            }
        }
    }
}