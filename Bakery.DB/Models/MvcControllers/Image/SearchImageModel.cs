using Bakery.DB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bakery.DB
{
    public class SearchImageModel : IImage, IPage
    {
        public int Rows { get; set; }

        public int Page { get; set; }

        public int ImageId { get; set; }

        public string ImageName { get; set; }

        public string ImagePath { get; set; }

        public bool IsImageNotNull()
        {
            if (ImageId < 0 && ImageName == null && ImagePath == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}