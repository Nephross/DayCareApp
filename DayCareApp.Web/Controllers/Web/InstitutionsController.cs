using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DayCareApp.Web.DataContext;
using DayCareApp.Web.Entities;
using DayCareApp.Web.DataContext.Repositories;
using DayCareApp.Web.DataContext.Persistence;

namespace DayCareApp.Web.Controllers.Web
{
    public class InstitutionsController : Controller
    {

        public  IInstitutionRepository _InstitutionRepository;
        public  UnitOfWork _unitOfWork;

        public InstitutionsController()
        {
            this._unitOfWork = new UnitOfWork(DayCareAppDB.Create());
        }

        public InstitutionsController(IInstitutionRepository InstitutionRepository, IUnitOfWork unitOfWork)
        {
            _InstitutionRepository = unitOfWork.Institutions;
        }

        // GET: Institutions
        public ActionResult Index()
        {
            return View(_InstitutionRepository.GetAll().ToList());
        }

        // GET: Institutions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Institution institution = _InstitutionRepository.Get(id);
            if (institution == null)
            {
                return HttpNotFound();
            }
            return View(institution);
        }

        // GET: Institutions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Institutions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InstitutionId,InstitutionName,FirstName,LastName,Address,AreaCode,City,Phonenumber,MobilePhone,Email")] Institution institution)
        {
            if (ModelState.IsValid)
            {
                _InstitutionRepository.Add(institution);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }

            return View(institution);
        }

        // GET: Institutions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Institution institution = _InstitutionRepository.Get(id);
            if (institution == null)
            {
                return HttpNotFound();
            }
            return View(institution);
        }

        // POST: Institutions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InstitutionId,InstitutionName,FirstName,LastName,Address,AreaCode,City,Phonenumber,MobilePhone,Email")] Institution institution)
        {
            if (ModelState.IsValid)
            {
                _InstitutionRepository.Edit(institution);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            return View(institution);
        }

        // GET: Institutions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Institution institution = _InstitutionRepository.Get(id);
            if (institution == null)
            {
                return HttpNotFound();
            }
            return View(institution);
        }

        // POST: Institutions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Institution institution = _InstitutionRepository.Get(id);
            _InstitutionRepository.Remove(institution);
            _unitOfWork.Complete();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
