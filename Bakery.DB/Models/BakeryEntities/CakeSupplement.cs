﻿using Bakery.DB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.DB
{
    public class CakeSupplement : ICakeSupplement
    {
        public int CakeSupplementId { get; set; }

        public int CakeId { get; set; }

        public int SupplementId { get; set; }
    }
}
