﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DayCareApp.Web
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class dayCareAppDBEntities : DbContext
    {
        public dayCareAppDBEntities()
            : base("name=dayCareAppDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Child> Childs { get; set; }
        public virtual DbSet<DayRegistration> DayRegistrations { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<InstitutionAdmin> InstitutionAdmins { get; set; }
        public virtual DbSet<Institution> Institutions { get; set; }
        public virtual DbSet<Parent> Parents { get; set; }
    }
}
