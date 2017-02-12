using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DayCareApp.Web.DataContext.Repositories;
using DayCareApp.Web.Entities;

namespace DayCareApp.Web.DataContext.Persistence
{
    public class ChildRepository : Repository<Child>, IChildRepository
    {
        public ChildRepository(DayCareAppDB context)
        : base(context)
        {
        }

      
        public IEnumerable<Child> GetAllChildren()
        {
            return DayCareAppDB.Create().Child.OrderByDescending(c => c.FirstName).ToList();

        }

        
        public DayCareAppDB DayCareAppDbContext
        {
            get { return DayCareAppDbContext as DayCareAppDB; }
        }
    }
}