using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using DayCareApp.Web.Entities;
using DayCareApp.Web.Models;
using System.Data.Entity.Migrations;

namespace DayCareApp.Web.DataContext
{
    public class DayCareAppDB : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<Child> Child { get; set; }
        public DbSet<DayRegistration> DayRegistration { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Institution> Institution { get; set; }
        public DbSet<InstitutionAdmin> InstitutionAdmin { get; set; }
        public DbSet<Parent> Parent { get; set; }
        public DbSet<Addresses> Adresses { get; set; }
        public DbSet<Admin> Admin { get; set; }

        public DayCareAppDB()
            : base("dayCareAPPDBEntities5")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public static DayCareAppDB Create()
        {
            return new DayCareAppDB();
        }

       



    }
}