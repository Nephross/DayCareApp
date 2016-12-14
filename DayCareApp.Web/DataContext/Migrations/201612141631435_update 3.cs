namespace DayCareApp.Web.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "DepartmentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Employees", "DepartmentId");
            AddForeignKey("dbo.Employees", "DepartmentId", "dbo.Departments", "DepartMentId", cascadeDelete: true);
            DropColumn("dbo.Employees", "Department");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "Department", c => c.String(nullable: false));
            DropForeignKey("dbo.Employees", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.Employees", new[] { "DepartmentId" });
            DropColumn("dbo.Employees", "DepartmentId");
        }
    }
}
