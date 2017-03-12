using DayCareApp.Web.DataContext;
using DayCareApp.Web.DataContext.Repositories;
using DayCareApp.Data.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DayCareApp.Web.DataContext.Persistence
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(DayCareAppDB context)
        : base(context)
        {
        }

        public DayCareAppDB DayCareAppDbContext
        {
            get { return DayCareAppDbContext as DayCareAppDB; }
        }
    }
}