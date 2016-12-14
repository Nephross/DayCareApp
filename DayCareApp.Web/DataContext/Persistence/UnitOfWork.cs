using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DayCareApp.Web.DataContext.Repositories;

namespace DayCareApp.Web.DataContext.Persistence
{
    public class UnitOfWork 
    {
        private readonly DayCareAppDB _context;

        public UnitOfWork(DayCareAppDB context)
        {
            //repositories in the unit of work here
            _context = context;
            Children = new ChildRepository(_context);
           
        }

        // place repository interfaces here
        public IChildRepository Children { get; private set; }
       

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
