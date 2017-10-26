namespace BakeryDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFields : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CakeSupplements",
                c => new
                    {
                        CakeSupplementId = c.Int(nullable: false, identity: true),
                        CakeSupplementName = c.Int(nullable: false),
                        Cake_CakeId = c.Int(),
                    })
                .PrimaryKey(t => t.CakeSupplementId)
                .ForeignKey("dbo.Cakes", t => t.Cake_CakeId)
                .Index(t => t.Cake_CakeId);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ImageId = c.Int(nullable: false, identity: true),
                        ImageName = c.String(),
                        ImagePath = c.String(),
                        Cake_CakeId = c.Int(),
                    })
                .PrimaryKey(t => t.ImageId)
                .ForeignKey("dbo.Cakes", t => t.Cake_CakeId)
                .Index(t => t.Cake_CakeId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        Cake_CakeId = c.Int(),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Cakes", t => t.Cake_CakeId)
                .Index(t => t.Cake_CakeId);
            
            AddColumn("dbo.Cakes", "PreviewImage_ImageId", c => c.Int());
            CreateIndex("dbo.Cakes", "PreviewImage_ImageId");
            AddForeignKey("dbo.Cakes", "PreviewImage_ImageId", "dbo.Images", "ImageId");
            DropColumn("dbo.Cakes", "CakeSupplements");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cakes", "CakeSupplements", c => c.String());
            DropForeignKey("dbo.Orders", "Cake_CakeId", "dbo.Cakes");
            DropForeignKey("dbo.Cakes", "PreviewImage_ImageId", "dbo.Images");
            DropForeignKey("dbo.Images", "Cake_CakeId", "dbo.Cakes");
            DropForeignKey("dbo.CakeSupplements", "Cake_CakeId", "dbo.Cakes");
            DropIndex("dbo.Orders", new[] { "Cake_CakeId" });
            DropIndex("dbo.Images", new[] { "Cake_CakeId" });
            DropIndex("dbo.CakeSupplements", new[] { "Cake_CakeId" });
            DropIndex("dbo.Cakes", new[] { "PreviewImage_ImageId" });
            DropColumn("dbo.Cakes", "PreviewImage_ImageId");
            DropTable("dbo.Orders");
            DropTable("dbo.Images");
            DropTable("dbo.CakeSupplements");
        }
    }
}
