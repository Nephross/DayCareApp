using NUnit.Framework;
using DayCareApp.Web.Controllers.Web;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DayCareApp.Web.DataContext;
using DayCareApp.Web.DataContext.Persistence;
using DayCareApp.Web.DataContext.Repositories;
using DayCareApp.Web.Entities;
using DayCareApp.Web.Models;
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
        public void IndexTest_Check_Login()
        {
            // Arrange:
            var checkInController = new CheckInController();
            var userMock = new Mock<IPrincipal>();
            userMock.Setup(p => p.IsInRole("InstitutionAdmin")).Returns(true);
            
            var contextMock = new Mock<HttpContextBase>();
            contextMock.SetupGet(ctx => ctx.User)
                       .Returns(userMock.Object);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(con => con.HttpContext)
                                 .Returns(contextMock.Object);

            checkInController.ControllerContext = controllerContextMock.Object;

            // Act:
            var result = checkInController.Index();

            // Assert:
            userMock.Verify(p => p.IsInRole("InstitutionAdmin"));
            Assert.AreEqual(((ViewResult)result).ViewName, "Index");







            

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