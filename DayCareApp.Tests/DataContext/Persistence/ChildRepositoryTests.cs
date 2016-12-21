using NUnit.Framework;
using DayCareApp.Web.DataContext.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DayCareApp.Web.Entities;

namespace DayCareApp.Web.DataContext.Persistence.Tests
{
    [TestFixture()]
    public class ChildRepositoryTests
    {
        [Test()]
        public void ChildRepositoryTest()
        {
            DayCareAppDB testDb = new DayCareAppDB();
            ChildRepository cRepo = new ChildRepository(testDb);
            var c1 = new Child();
            cRepo.Add(c1);



            Assert.Fail();
        }

        [Test()]
        public void GetAllChildrenTest()
        {
            Assert.Fail();
        }
    }
}