using DayCareApp.Web.DataContext;
using DayCareApp.Web.DataContext.Repositories;
using DayCareApp.Web.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DayCareApp.Web.DataContext.Persistence
{
    public class InstitutionAdminRepository : Repository<InstitutionAdmin>, IInstitutionAdminRepository
    {
        public InstitutionAdminRepository(DayCareAppDB context)
        : base(context)
        {
        }

        public DayCareAppDB DayCareAppDbContext
        {
            get { return DayCareAppDbContext as DayCareAppDB; }
        }

    }
}
}