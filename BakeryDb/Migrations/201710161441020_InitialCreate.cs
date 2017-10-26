namespace BakeryDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cakes",
                c => new
                    {
                        CakeId = c.Int(nullable: false, identity: true),
                        CakeName = c.String(),
                        CakeDescription = c.String(),
                        CakeSupplements = c.String(),
                        CakePrice = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CakeId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Cakes");
        }
    }
}
