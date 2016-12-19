using NUnit.Framework;
using DayCareApp.Web.Controllers.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using DayCareApp.Web.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DayCareApp.Web.Controllers.Web.Tests
{
    [TestFixture()]
    public class CheckInControllerTests
    {
        [Test()]
        public void CheckInControllerTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void CheckInControllerTest1()
        {
            Assert.Fail();
        }

        [Test()]
        public void IndexTest()
        {
            // Arrange
            CheckInController controller = new CheckInController();
            ApplicationUser testUser = new ApplicationUser();
            testUser.GenerateUserIdentityAsync();
            testUser.
            

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test()]
        public void IndexPicturesTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void DetailsTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void CreateTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void CreateTest1()
        {
            Assert.Fail();
        }

        [Test()]
        public void EditTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void EditTest1()
        {
            Assert.Fail();
        }

        [Test()]
        public void DeleteTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void DeleteTest1()
        {
            Assert.Fail();
        }
    }
}