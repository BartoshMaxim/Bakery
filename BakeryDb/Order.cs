﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryDb
{
    public class Order
    {
        public int OrderId { get; set; }  

        public virtual Cake Cake { get; set; }
    }
}
