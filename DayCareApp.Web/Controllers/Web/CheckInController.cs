using DayCareApp.Web.Entities;
using DayCareApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DayCareApp.Web.DataContext;
using DayCareApp.Web.DataContext.Persistence;
using DayCareApp.Web.DataContext.Repositories;

namespace DayCareApp.Web.Controllers.Web
{
    public class CheckInController : Controller
    {
        public readonly IChildRepository _childRepository;
        public readonly UnitOfWork _unitOfWork;

        public CheckInController()
        {
            this._unitOfWork = new UnitOfWork(DayCareAppDB.Create());
        }

        public CheckInController(IChildRepository childRepository, IUnitOfWork unitOfWork)
        {
            _childRepository = unitOfWork.Children;
        }

        public ActionResult Index()
        {

            var model = _childRepository.GetAll();       
            return View(model);
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

        // GET: Reviews/Create
       /* public ActionResult Create()
        {
            return View();
        }

        // POST: Reviews/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
               
                 Child child = new Child();
                         {
                             // add for the following attributes
                             child.ChildId = _childRepository.GetAll().Count() + 1;
                             child.Name = collection.Get(1);
                             child.Country = collection.Get(2);
                             child.Birthdate = Convert.ToDateTime(collection.Get(3));
                             child.CurrentlyCheckedIn = Convert.ToBoolean(collection.Get(4));
                             child.SpecialNeeds = collection.Get(4);

                }
                _childRepository.Add(child);
                _unitOfWork.Complete();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }*/

        // GET: Reviews/Edit/5
        public ActionResult Edit(int id)
        {
            Child child = _childRepository.SingleOrDefault((r => r.ChildId == id));
            return View(child);
        }

        // POST: Reviews/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {

                using (DayCareAppDB context = new DayCareAppDB())
                {

                Child child = _childRepository.SingleOrDefault((r => r.ChildId == id));
                    if (child != null)
                    {
                    
                    child.Name = collection.Get(2);
                    child.Country = collection.Get(3);
                    child.Birthdate = Convert.ToDateTime(collection.Get(4));
                    child.CurrentlyCheckedIn = Convert.ToBoolean(collection.Get(5));
                    child.SpecialNeeds = collection.Get(6);
                }

                _unitOfWork.Complete();
                return RedirectToAction("Index");

                 }
            

            //add view for not saved action
            return View("Index");
        }

        // GET: Reviews/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Reviews/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


    }
}