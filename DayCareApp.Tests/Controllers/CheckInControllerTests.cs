using NUnit.Framework;
using DayCareApp.Web.Controllers.Web;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using DayCareApp.Web.DataContext;
using DayCareApp.Web.DataContext.Persistence;
using DayCareApp.Web.DataContext.Repositories;
using DayCareApp.Web.Entities;
using Moq;

namespace DayCareApp.Web.Controllers.Web.Tests
{
    [TestFixture()]
    public class CheckInControllerTests
    {
        private DayCareAppDB db;

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
        public void IndexTest_Admin_Should_Return_Non_Null_View()
        {

            // Arrange:
            var checkInController = new CheckInController();
    
            Mock<ControllerContext> controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.Setup(
                x => x.HttpContext.User.IsInRole(It.Is<string>(s => s.Equals("Admin")))
                ).Returns(true);
            checkInController.ControllerContext = controllerContextMock.Object;
     
       
            // Act:
            ActionResult index = checkInController.Index();

            // Assert:
            Assert.IsNotNull(index);
           
            controllerContextMock.Verify(
                x => x.HttpContext.User.IsInRole(It.Is<string>(s => s.Equals("Admin"))),
                Times.Exactly(1),
                "Must check if user is in role 'Admin'");

        }

        [Test()]
        public void IndexPicturesTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void DetailsTest()
        {
          
           
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