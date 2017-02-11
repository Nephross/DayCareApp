using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DayCareApp.Web.DataContext.Repositories;
using DayCareApp.Web.Entities;

namespace DayCareApp.Web.DataContext.Persistence
{
    public class DayRegistrationRepository : Repository<DayRegistration>, IDayRegistrationRepository
    {

      
            public DayRegistrationRepository(DayCareAppDB context)
            : base(context)
            {
            }

            public DayCareAppDB DayCareAppDbContext
            {
                get { return DayCareAppDbContext as DayCareAppDB; }
            }

       
    }
}