using DayCareApp.Web.DataContext.Repositories;
using DayCareApp.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DayCareApp.Web.DataContext.Persistence
{
    public class ParentRepository : Repository<Parent>, IParentRepository
    {
        public ParentRepository(DayCareAppDB context)
        : base(context)
        {
        }

        public DayCareAppDB DayCareAppDbContext
        {
            get { return DayCareAppDbContext as DayCareAppDB; }
        }

    }
    {
    }
}