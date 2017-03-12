using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using DayCareApp.Data.DAL;
using DayCareApp.Web.Models;
using System.Data.Entity.Migrations;

namespace DayCareApp.Web.DataContext
{
    public class DayCareAppDB : IdentityDbContext<ApplicationUser>
    {

        public DayCareAppDB()
            : base("RemoteConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public static DayCareAppDB Create()
        {
            return new DayCareAppDB();
        }

       



    }
}