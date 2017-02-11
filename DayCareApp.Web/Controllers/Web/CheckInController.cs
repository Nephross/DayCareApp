using DayCareApp.Web.Entities;
using DayCareApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DayCareApp.Web.DataContext;
using DayCareApp.Web.DataContext.Persistence;
using DayCareApp.Web.DataContext.Repositories;
using DayCareApp.Web.Entities;
using Microsoft.AspNet.Identity;

namespace DayCareApp.Web.Controllers.Web
{
    public class CheckInController : Controller
    {
        public readonly IChildRepository _childRepository;
        public readonly IEmployeeRepository _employeeRepository;
        public readonly UnitOfWork _unitOfWork;
        public readonly IInstitutionRepository _institutionRepository;
        public readonly IParentRepository _parentReposity;
        public readonly IDepartmentRepository _departmentReposity;
        public readonly IDayRegistrationRepository _dayRegistrationRepository;

        public CheckInController()
        {
            this._unitOfWork = new UnitOfWork(DayCareAppDB.Create());
            _childRepository = this._unitOfWork.Children;
            _parentReposity = this._unitOfWork.Parents;
            _institutionRepository = this._unitOfWork.Institutions;
            _departmentReposity = this._unitOfWork.Departments;
            _dayRegistrationRepository = this._unitOfWork.DayRegistrations;
        }

        public CheckInController(IUnitOfWork unitOfWork)
        {
            _childRepository = unitOfWork.Children;
            _employeeRepository = unitOfWork.Employees;
            _institutionRepository = unitOfWork.Institutions;
            _parentReposity = unitOfWork.Parents;
            _departmentReposity = unitOfWork.Departments;
            _dayRegistrationRepository = unitOfWork.DayRegistrations;
        }

        public ActionResult Index()
        {
           
            if (User.IsInRole("Employee"))
            {
                var userId = User.Identity.GetUserId();

                    int institutionId = _employeeRepository.SingleOrDefault(x => x.ApplicationUserId == userId).FK_InstitutionId;
                    var model = _childRepository.GetAll();
                    var modelEmpl =
                    from r in model
                    where r.FK_InstitutionId == institutionId 
                
                    select r;

                return View(modelEmpl);
            }
            if (User.IsInRole("Admin"))
            {
                var model = _childRepository.GetAll().ToList();
                return View(model);
            }

            return View();

        }

        public ActionResult IndexPictures()
        {

            var model = _childRepository.GetAll();
            return View(model);

        }

        // GET: Reviews/Details/5
        public ActionResult Details(int id)
        {
            
                 Child child = _childRepository.SingleOrDefault((r => r.ChildId == id));
                
                return View(child);

        }
      
        // GET: Reviews/Edit/5
        public ActionResult CreateDayRegistration(int id)
        {
            if (User.IsInRole("Employee"))
            {
                Child child = _childRepository.SingleOrDefault(r => r.ChildId == id);
                return View(child);
            }

            return View();
        }
/*
        [HttpPost]
        public ActionResult CreateDayRegistration(int id, FormCollection collection)
        {
            if (User.IsInRole("Employee"))
            {
                Child child = _childRepository.SingleOrDefault(r => r.ChildId == id);
                Parent parent = child.Parent.First(); 

                DayRegistration dayRegistration = new DayRegistration();
                dayRegistration.DayRegistrationId = _dayRegistrationRepository.GetAll().Count() +1 ;
                dayRegistration.CheckInTime = collection.Get(3);

                dayRegistration.CheckOutTime = collection.Get(4);
                dayRegistration.ExpectedCheckOutTime = collection.Get(5);  
                dayRegistration.Child = child;
                dayRegistration.FK_InstitutionId = child.Institution.InstitutionId;
                dayRegistration.FK_ExpectedCheckOutParentId = collection.Get(6);
                dayRegistration.FK_CheckInParentId = collection.Get(7);
                dayRegistration.FK_CheckInEmployeeId = 0;
                dayRegistration.FK_CheckOutParentId =  0;
                dayRegistration.FK_CheckOutEmployeeId = 0; 
              

                return View(child);

             }

            return View();
        }

    */
        public ActionResult Edit(int id)
        {
            Child child = _childRepository.SingleOrDefault((r => r.ChildId == id));
            return View(child);
        }

        // POST: Reviews/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (User.IsInRole("Employee") || User.IsInRole("Admin") || User.IsInRole("InstitutionAdmin"))
            {
                Child child = _childRepository.SingleOrDefault((r => r.ChildId == id));
                if (child != null)
                {

                    child.FirstName = collection.Get(2);
                     
                    child.Birthday = Convert.ToDateTime(collection.Get(4));
               
                  
                }

                _unitOfWork.Complete();
                return RedirectToAction("Index");

            }
                

            //add view for not saved action
            return View("Index");
        }
        /*
        public ActionResult EditDayRegistration(int id)
        {
            DayRegistration dayReg = _dayRegistrationRepository.SingleOrDefault((r => r.dayRegId == id));
            return View(dayReg);
        }

        // POST: Reviews/Edit/5
        [HttpPost]
        public ActionResult EditDayRegistration(int id, FormCollection collection)
        {
            if (User.IsInRole("Employee") || User.IsInRole("Admin") || User.IsInRole("InstitutionAdmin"))
            {
                DayRegistration dayReg = _dayRegistrationRepository.SingleOrDefault((r => r.dayRegId == id));
                if (dayReg != null)
                {


                    dayReg.checkInTime = collection.Get(3);
                    dayReg.checkOutTime = collection.Get(4);
                    dayReg.expectedCheckOutTime = collection.Get(5);
                    dayReg.expectectCheckOutParent = collection.Get(6);
                    dayReg.checkInParentId = parent.parentChildId;
                    dayReg.checkInEmployeeId = 0;
                    dayReg.checkOutParentId = 0;
                    dayReg.checkOutEmployeeId = 0;
                }

                _unitOfWork.Complete();
                return RedirectToAction("Index");

            }


            //add view for not saved action
            return View("Index");
        }



    */



    }
}