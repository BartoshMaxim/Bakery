using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryDb
{
    public class BakeryContext : DbContext
    {
        public BakeryContext() : base()
        {

        }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Cake> Cakes { get; set; }

        public DbSet<CakeSupplement> CakeSupplemnts { get; set; }
    }

    public class AppDbInitiatializer : DropCreateDatabaseAlways<BakeryContext>
    {
        protected override void Seed(BakeryContext context)
        {
            

            base.Seed(context);
        }

        
    }
}
