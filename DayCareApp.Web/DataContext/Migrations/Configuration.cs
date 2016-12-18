namespace DayCareApp.Web.DataContext.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
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
        }
    }
}
