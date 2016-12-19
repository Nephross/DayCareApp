using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DayCareApp.Web.Entities;

namespace DayCareApp.Web.DataContext.Repositories
{
    public interface IChildRepository : IRepository<Child>
    {
       
        IEnumerable<Child> GetAllChildren();

       
    }
}
