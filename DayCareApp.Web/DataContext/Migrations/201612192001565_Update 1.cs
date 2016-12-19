namespace DayCareApp.Web.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DayRegistrations", "DeliveryParent_ParentId", "dbo.Parents");
            DropForeignKey("dbo.DayRegistrations", "ExpectedPickupParent_ParentId", "dbo.Parents");
            DropForeignKey("dbo.Children", "Parent1_ParentId", "dbo.Parents");
            DropIndex("dbo.DayRegistrations", new[] { "DeliveryParent_ParentId" });
            DropIndex("dbo.DayRegistrations", new[] { "ExpectedPickupParent_ParentId" });
            DropIndex("dbo.Children", new[] { "Parent1_ParentId" });
            AlterColumn("dbo.DayRegistrations", "DeliveryParent_ParentId", c => c.Int());
            AlterColumn("dbo.DayRegistrations", "ExpectedPickupParent_ParentId", c => c.Int());
            AlterColumn("dbo.Children", "Parent1_ParentId", c => c.Int());
            CreateIndex("dbo.DayRegistrations", "DeliveryParent_ParentId");
            CreateIndex("dbo.DayRegistrations", "ExpectedPickupParent_ParentId");
            CreateIndex("dbo.Children", "Parent1_ParentId");
            AddForeignKey("dbo.DayRegistrations", "DeliveryParent_ParentId", "dbo.Parents", "ParentId");
            AddForeignKey("dbo.DayRegistrations", "ExpectedPickupParent_ParentId", "dbo.Parents", "ParentId");
            AddForeignKey("dbo.Children", "Parent1_ParentId", "dbo.Parents", "ParentId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Children", "Parent1_ParentId", "dbo.Parents");
            DropForeignKey("dbo.DayRegistrations", "ExpectedPickupParent_ParentId", "dbo.Parents");
            DropForeignKey("dbo.DayRegistrations", "DeliveryParent_ParentId", "dbo.Parents");
            DropIndex("dbo.Children", new[] { "Parent1_ParentId" });
            DropIndex("dbo.DayRegistrations", new[] { "ExpectedPickupParent_ParentId" });
            DropIndex("dbo.DayRegistrations", new[] { "DeliveryParent_ParentId" });
            AlterColumn("dbo.Children", "Parent1_ParentId", c => c.Int(nullable: false));
            AlterColumn("dbo.DayRegistrations", "ExpectedPickupParent_ParentId", c => c.Int(nullable: false));
            AlterColumn("dbo.DayRegistrations", "DeliveryParent_ParentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Children", "Parent1_ParentId");
            CreateIndex("dbo.DayRegistrations", "ExpectedPickupParent_ParentId");
            CreateIndex("dbo.DayRegistrations", "DeliveryParent_ParentId");
            AddForeignKey("dbo.Children", "Parent1_ParentId", "dbo.Parents", "ParentId", cascadeDelete: false);
            AddForeignKey("dbo.DayRegistrations", "ExpectedPickupParent_ParentId", "dbo.Parents", "ParentId", cascadeDelete: false);
            AddForeignKey("dbo.DayRegistrations", "DeliveryParent_ParentId", "dbo.Parents", "ParentId", cascadeDelete: false);
        }
    }
}
