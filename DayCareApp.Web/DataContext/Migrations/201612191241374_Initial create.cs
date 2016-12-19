namespace DayCareApp.Web.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialcreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChatMessages",
                c => new
                    {
                        ChatMessageId = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        ApplicationUserId = c.String(),
                        DayRegistrationId = c.Int(nullable: false),
                        TimeStamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ChatMessageId)
                .ForeignKey("dbo.DayRegistrations", t => t.DayRegistrationId, cascadeDelete: true)
                .Index(t => t.DayRegistrationId);
            
            CreateTable(
                "dbo.DayRegistrations",
                c => new
                    {
                        DayRegistrationId = c.Int(nullable: false, identity: true),
                        ChildId = c.Int(nullable: false),
                        Other = c.String(),
                        ChangedDiaper = c.Boolean(nullable: false),
                        ExpectedPickupParentId = c.Int(nullable: false),
                        ExpectedPickupTime = c.DateTime(nullable: false),
                        DeliveryParentId = c.Int(nullable: false),
                        PickUpParentId = c.Int(nullable: false),
                        ArrivalEmployeeId = c.Int(nullable: false),
                        DepartureEmployeeId = c.Int(nullable: false),
                        CheckInTime = c.DateTime(nullable: false),
                        CheckOutTime = c.DateTime(nullable: false),
                        ArrivalEmployee_EmployeeId = c.Int(),
                        DeliveryParent_ParentId = c.Int(nullable: false),
                        DepartureEmployee_EmployeeId = c.Int(),
                        ExpectedPickupParent_ParentId = c.Int(nullable: false),
                        PickUpParent_ParentId = c.Int(),
                    })
                .PrimaryKey(t => t.DayRegistrationId)
                .ForeignKey("dbo.Employees", t => t.ArrivalEmployee_EmployeeId)
                .ForeignKey("dbo.Children", t => t.ChildId, cascadeDelete: false)
                .ForeignKey("dbo.Parents", t => t.DeliveryParent_ParentId, cascadeDelete: false)
                .ForeignKey("dbo.Employees", t => t.DepartureEmployee_EmployeeId)
                .ForeignKey("dbo.Parents", t => t.ExpectedPickupParent_ParentId, cascadeDelete: false)
                .ForeignKey("dbo.Parents", t => t.PickUpParent_ParentId)
                .Index(t => t.ChildId)
                .Index(t => t.ArrivalEmployee_EmployeeId)
                .Index(t => t.DeliveryParent_ParentId)
                .Index(t => t.DepartureEmployee_EmployeeId)
                .Index(t => t.ExpectedPickupParent_ParentId)
                .Index(t => t.PickUpParent_ParentId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.String(),
                        Name = c.String(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        InstitutionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: false)
                .ForeignKey("dbo.Institutions", t => t.InstitutionId, cascadeDelete: false)
                .Index(t => t.DepartmentId)
                .Index(t => t.InstitutionId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentId = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(nullable: false),
                        InstitutionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DepartmentId)
                .ForeignKey("dbo.Institutions", t => t.InstitutionId, cascadeDelete: true)
                .Index(t => t.InstitutionId);
            
            CreateTable(
                "dbo.Institutions",
                c => new
                    {
                        InstitutionId = c.Int(nullable: false, identity: true),
                        InstitutionName = c.String(),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        AreaCode = c.String(nullable: false, maxLength: 4),
                        City = c.String(nullable: false),
                        Phonenumber = c.String(nullable: false, maxLength: 8),
                        MobilePhone = c.String(nullable: false, maxLength: 8),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.InstitutionId);
            
            CreateTable(
                "dbo.Children",
                c => new
                    {
                        ChildId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Parent1Id = c.Int(nullable: false),
                        Parent2Id = c.Int(nullable: false),
                        InstitutionId = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        Country = c.String(nullable: false),
                        Birthdate = c.DateTime(nullable: false),
                        CurrentlyCheckedIn = c.Boolean(nullable: false),
                        SpecialNeeds = c.String(nullable: false),
                        ImagePath = c.String(),
                        Parent_ParentId = c.Int(),
                        Parent1_ParentId = c.Int(nullable: false),
                        Parent2_ParentId = c.Int(),
                    })
                .PrimaryKey(t => t.ChildId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: false)
                .ForeignKey("dbo.Institutions", t => t.InstitutionId, cascadeDelete: false)
                .ForeignKey("dbo.Parents", t => t.Parent_ParentId)
                .ForeignKey("dbo.Parents", t => t.Parent1_ParentId, cascadeDelete: false)
                .ForeignKey("dbo.Parents", t => t.Parent2_ParentId)
                .Index(t => t.InstitutionId)
                .Index(t => t.DepartmentId)
                .Index(t => t.Parent_ParentId)
                .Index(t => t.Parent1_ParentId)
                .Index(t => t.Parent2_ParentId);
            
            CreateTable(
                "dbo.Parents",
                c => new
                    {
                        ParentId = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.String(),
                        InstitutionId = c.Int(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Address = c.String(),
                        AreaCode = c.String(maxLength: 4),
                        City = c.String(),
                        MobilePhone = c.String(nullable: false, maxLength: 8),
                        Email = c.String(),
                        ImagePath = c.String(),
                    })
                .PrimaryKey(t => t.ParentId)
                .ForeignKey("dbo.Institutions", t => t.InstitutionId, cascadeDelete: false)
                .Index(t => t.InstitutionId);
            
            CreateTable(
                "dbo.InstitutionAdmins",
                c => new
                    {
                        InstitutionAdminId = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.String(),
                        Name = c.String(nullable: false),
                        InstitutionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InstitutionAdminId)
                .ForeignKey("dbo.Institutions", t => t.InstitutionId, cascadeDelete: false)
                .Index(t => t.InstitutionId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.InstitutionAdmins", "InstitutionId", "dbo.Institutions");
            DropForeignKey("dbo.DayRegistrations", "PickUpParent_ParentId", "dbo.Parents");
            DropForeignKey("dbo.DayRegistrations", "ExpectedPickupParent_ParentId", "dbo.Parents");
            DropForeignKey("dbo.DayRegistrations", "DepartureEmployee_EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.DayRegistrations", "DeliveryParent_ParentId", "dbo.Parents");
            DropForeignKey("dbo.DayRegistrations", "ChildId", "dbo.Children");
            DropForeignKey("dbo.Children", "Parent2_ParentId", "dbo.Parents");
            DropForeignKey("dbo.Children", "Parent1_ParentId", "dbo.Parents");
            DropForeignKey("dbo.Parents", "InstitutionId", "dbo.Institutions");
            DropForeignKey("dbo.Children", "Parent_ParentId", "dbo.Parents");
            DropForeignKey("dbo.Children", "InstitutionId", "dbo.Institutions");
            DropForeignKey("dbo.Children", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.ChatMessages", "DayRegistrationId", "dbo.DayRegistrations");
            DropForeignKey("dbo.DayRegistrations", "ArrivalEmployee_EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Employees", "InstitutionId", "dbo.Institutions");
            DropForeignKey("dbo.Employees", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Departments", "InstitutionId", "dbo.Institutions");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.InstitutionAdmins", new[] { "InstitutionId" });
            DropIndex("dbo.Parents", new[] { "InstitutionId" });
            DropIndex("dbo.Children", new[] { "Parent2_ParentId" });
            DropIndex("dbo.Children", new[] { "Parent1_ParentId" });
            DropIndex("dbo.Children", new[] { "Parent_ParentId" });
            DropIndex("dbo.Children", new[] { "DepartmentId" });
            DropIndex("dbo.Children", new[] { "InstitutionId" });
            DropIndex("dbo.Departments", new[] { "InstitutionId" });
            DropIndex("dbo.Employees", new[] { "InstitutionId" });
            DropIndex("dbo.Employees", new[] { "DepartmentId" });
            DropIndex("dbo.DayRegistrations", new[] { "PickUpParent_ParentId" });
            DropIndex("dbo.DayRegistrations", new[] { "ExpectedPickupParent_ParentId" });
            DropIndex("dbo.DayRegistrations", new[] { "DepartureEmployee_EmployeeId" });
            DropIndex("dbo.DayRegistrations", new[] { "DeliveryParent_ParentId" });
            DropIndex("dbo.DayRegistrations", new[] { "ArrivalEmployee_EmployeeId" });
            DropIndex("dbo.DayRegistrations", new[] { "ChildId" });
            DropIndex("dbo.ChatMessages", new[] { "DayRegistrationId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.InstitutionAdmins");
            DropTable("dbo.Parents");
            DropTable("dbo.Children");
            DropTable("dbo.Institutions");
            DropTable("dbo.Departments");
            DropTable("dbo.Employees");
            DropTable("dbo.DayRegistrations");
            DropTable("dbo.ChatMessages");
        }
    }
}
