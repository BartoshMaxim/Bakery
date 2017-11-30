using Bakery.DB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminDashboard.Models.Entities.Cake
{
    public class SearchCakeModel : IPage, ICake
    {
        public int CakeId { get; set; }

        public string CakeName { get; set; }

        public int CakePrice { get; set; }

        public int Rows { get; set; }

        public int Page { get; set; }

        public bool IsCakeNotNull()
        {
           if(CakeId == 0 && CakeName == null && CakePrice == 0)
            {
                return false;
            }
           else
            {
                return true;
            }
        }

        #region usepull fields
        public string CakeDescription { get; set; }
        public int ImageId { get; set; }
        public DateTime AddedDate { get; set; }
        #endregion
    }
}