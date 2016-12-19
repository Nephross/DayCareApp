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
            var parentController = new ParentsController();
            var institutionController = new InstitutionsController();
           

            Mock<ControllerContext> controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.Setup(
                x => x.HttpContext.User.IsInRole(It.Is<string>(s => s.Equals("Admin")))
                ).Returns(true);
            checkInController.ControllerContext = controllerContextMock.Object;
            var i = new Institution();
            {
                i.InstitutionId = 1;
                i.FirstName = "1";
                i.LastName = "1";
                i.Address = "1";
                i.AreaCode = "1";
                i.City = "1";
                i.Phonenumber = "20202020";
                i.MobilePhone = "20202020";
            }
           
                
            
            institutionController._InstitutionRepository.Add(i);
            var p1 = new Parent();
            {
                p1.ParentId = 1;
                p1.ApplicationUserId = "1";
                p1.InstitutionId = i.InstitutionId;
                p1.FirstName = "test";
                p1.LastName = "testerson";
                p1.Address = "test";
                p1.AreaCode = "1";
                p1.City = "a";
                p1.MobilePhone = "20202020";
                p1.Email = "test@test.dk";
                p1.ImagePath = "test";

            }
            Child child = new Child();
            {
                child.ChildId = 1;
                child.Name = "Test Testerson";
                child.Country = "Denmark";
                child.Birthdate = DateTime.Today;
                child.SpecialNeeds = "none";
            }
            parentController._ParentRepository.Add(p1);
            Console.WriteLine("number of parents " + parentController._ParentRepository.GetAll().Count());
            //checkInController._childRepository.Add(child);
            //checkInController._unitOfWork.Complete();

       
            // Act:
            ActionResult index = checkInController.Index();

            // Assert:
            Assert.IsNotNull(index);
           
            controllerContextMock.Verify(
                x => x.HttpContext.User.IsInRole(It.Is<string>(s => s.Equals("Admin"))),
                Times.Exactly(1),
                "Must check if user is in role 'Admin'");
            Assert.Equals(checkInController._childRepository.GetAll().Count(), 1);



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