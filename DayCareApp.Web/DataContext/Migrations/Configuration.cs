namespace DayCareApp.Web.DataContext.Migrations
{
    using Entities;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DayCareApp.Web.DataContext.DayCareAppDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"DataContext\Migrations";
        }

        protected override void Seed(DayCareApp.Web.DataContext.DayCareAppDB context)
        {
            //  This method will be called after migrating to the latest version.

            var rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            rm.Create(new IdentityRole("Admin"));
            rm.Create(new IdentityRole("InstitutionAdmin"));
            rm.Create(new IdentityRole("Employee"));
            rm.Create(new IdentityRole("Parent"));

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            //create the ApplicationUser with a username
            var client1 = new ApplicationUser { UserName = "admin@daycareapp.dk" };

            //Client to usermngr with password
            var result1 = userManager.Create(client1, "password");

            //If the user already exist in the usrmngr we aquire it for later use.
            if (result1.Succeeded == false)
            {
                client1 = userManager.FindByName("admin@daycareapp.dk");
            }

            //Add the new user to admin role
            userManager.AddToRole(client1.Id, "Admin");



            var client2 = new ApplicationUser { UserName = "institutionAdmin@daycareapp.dk" };

            //Client to usermngr with password
            var result2 = userManager.Create(client2, "password");

            //If the user already exist in the usrmngr we aquire it for later use.
            if (result2.Succeeded == false)
            {
                client2 = userManager.FindByName("institutionAdmin@daycareapp.dk");
            }

            //Add the new user to admin role
            userManager.AddToRole(client2.Id, "InstitutionAdmin");


        

            var client3 = new ApplicationUser { UserName = "testEmployee@daycareapp.dk" };

            //Client to usermngr with password
            var result3 = userManager.Create(client3, "password");

            //If the user already exist in the usrmngr we aquire it for later use.
            if (result3.Succeeded == false)
            {
                client3 = userManager.FindByName("testEmployee@daycareapp.dk");
            }

            //Add the new user to admin role
            userManager.AddToRole(client3.Id, "Employee");



            var client4 = new ApplicationUser { UserName = "testParent@daycareapp.dk" };

            //Client to usermngr with password
            var result4 = userManager.Create(client4, "password");

            //If the user already exist in the usrmngr we aquire it for later use.
            if (result4.Succeeded == false)
            {
                client4 = userManager.FindByName("testParent@daycareapp.dk");
            }

            //Add the new user to admin role
            userManager.AddToRole(client4.Id, "Parent");

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            IList<Addresses> adresses = new List<Addresses>();
            IList<Child> children = new List<Child>();
            IList<Admin> admins = new List<Admin>();
            IList<Institution> institutions = new List<Institution>();
            IList<Department> departments = new List<Department>();

            adresses.Add(new Addresses()
            {
                AddressId = context.Adresses.Count() + 1,
                StreetName = "RandomStreet",
                PostCode = 2000,
                Country = "Denmark"

            });
            admins.Add(new Admin()
            {
                AdminId = context.Admin.Count() + 1,
                FirstName = "Admin",
                LastName = "Adminson",
                PhoneNumber = 20202020,
                Email = "admin@admin.com",
                ApplicationUserId = "06327f24-2ea0-443f-b6ad-8354a8cc3c4e",
                FK_AddressId = context.Adresses.SingleOrDefault(x => x.AddressId == 1).AddressId
            });

            institutions.Add(new Institution()
            {
                InstitutionId = context.Institution.Count() + 1,
                InstitutionName = "RandomInstitution",
                PhoneNumber = 30303030,
                Email = "institution@insti.com",
                FK_AddressId = context.Adresses.SingleOrDefault(x => x.AddressId == 1).AddressId
            });

            departments.Add(new Department()
            {
                DepartmentId = context.Department.Count() + 1,
                DepartmentName = "RandomDepartment",
                PhoneNumber = 40404040,
                FK_InstitutionId = context.Institution.SingleOrDefault(x => x.InstitutionId == 1).InstitutionId
            });

            children.Add(new Child()
            {
                ChildId = context.Child.Count() + 1,
                FirstName = "Steve",
                LastName = "Stevenson",
                Birthday = DateTime.Now,
                FK_DepartmentId = context.Department.SingleOrDefault(x => x.DepartmentId == 1).DepartmentId,
                FK_InstitutionId = context.Institution.SingleOrDefault(x => x.InstitutionId == 1).InstitutionId,
                FK_AddressId = context.Adresses.SingleOrDefault(x => x.AddressId == 1).AddressId
            });



            foreach (Addresses adress in adresses)
                context.Adresses.Add(adress);
            foreach (Admin admin in admins)
                context.Admin.Add(admin);
            foreach (Institution insti in institutions)
                context.Institution.Add(insti);
            foreach (Department dep in departments)
                context.Department.Add(dep);
            foreach (Child child in children)
                context.Child.Add(child);
            base.Seed(context);
        }
    }
}
