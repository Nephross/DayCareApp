using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using DayCareApp.Web.Entities;
using DayCareApp.Web.Models;

namespace DayCareApp.Web.DataContext
{
    public class DayCareAppDB : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<Child> Children { get; set; }
        public DbSet<DayRegistration> DayRegistrations { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Institution> Institutions { get; set; }
        public DbSet<InstitutionAdmin> InstitutionAdmins { get; set; }
        public DbSet<Parent> Parents { get; set; }

        public DayCareAppDB()
            : base("RemoteConnection") 
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        public static DayCareAppDB Create()
        {
            return new DayCareAppDB();
        }




    }
}