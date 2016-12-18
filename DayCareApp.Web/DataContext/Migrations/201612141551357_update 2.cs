namespace DayCareApp.Web.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Departments", "Institution_InstitutionId", "dbo.Institutions");
            DropIndex("dbo.Departments", new[] { "Institution_InstitutionId" });
            RenameColumn(table: "dbo.Departments", name: "Institution_InstitutionId", newName: "InstitutionId");
            AlterColumn("dbo.Departments", "InstitutionId", c => c.Int(nullable: false));
            CreateIndex("dbo.Departments", "InstitutionId");
            AddForeignKey("dbo.Departments", "InstitutionId", "dbo.Institutions", "InstitutionId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Departments", "InstitutionId", "dbo.Institutions");
            DropIndex("dbo.Departments", new[] { "InstitutionId" });
            AlterColumn("dbo.Departments", "InstitutionId", c => c.Int());
            RenameColumn(table: "dbo.Departments", name: "InstitutionId", newName: "Institution_InstitutionId");
            CreateIndex("dbo.Departments", "Institution_InstitutionId");
            AddForeignKey("dbo.Departments", "Institution_InstitutionId", "dbo.Institutions", "InstitutionId");
        }
    }
}
