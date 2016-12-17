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
using DayCareApp.Web.Repository;
using DayCareApp.Web.DataContext.Repositories;
using DayCareApp.Web.DataContext.Persistence;
using Microsoft.AspNet.Identity.Owin;
using DayCareApp.Web.Models;

namespace DayCareApp.Web.Controllers.Web
{
    public class ParentsController : Controller
    {
        public readonly IParentRepository _ParentRepository;
        public readonly IInstitutionRepository _InstitutionRepository;
        public readonly UnitOfWork _unitOfWork;
        private ApplicationUserManager _userManager;

        public ParentsController()
        {
            this._unitOfWork = new UnitOfWork(DayCareAppDB.Create());
        }

        public ParentsController(IParentRepository ParentRepository, IInstitutionRepository InstitutionRepository, IUnitOfWork unitOfWork, ApplicationUserManager userManager)
        {
            _ParentRepository = unitOfWork.Parents;
            _InstitutionRepository = unitOfWork.Institutions;
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        // GET: Parents
        public ActionResult Index()
        {
            var parents = _ParentRepository.GetAll();
            return View(parents.ToList());
        }

        // GET: Parents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parent parent = _ParentRepository.Get(id);
            if (parent == null)
            {
                return HttpNotFound();
            }
            return View(parent);
        }

        // GET: Parents/Create
        public ActionResult Create()
        {
            RegisterParentViewModel ParentVM = new RegisterParentViewModel();
            ParentVM.InstitutionList = new SelectList(_InstitutionRepository.GetAll().ToList(), "InstitutionId", "InstitutionName");
            return View(ParentVM);
        }

        // POST: Parents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ParentId,ApplicationUserId,InstitutionId,FirstName,LastName,Address,AreaCode,City,MobilePhone,Email,ImagePath")] Parent parent)
        {
            if (ModelState.IsValid)
            {
                db.Parents.Add(parent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InstitutionId = new SelectList(db.Institutions, "InstitutionId", "InstitutionName", parent.InstitutionId);
            return View(parent);
        }

        // GET: Parents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parent parent = db.Parents.Find(id);
            if (parent == null)
            {
                return HttpNotFound();
            }
            ViewBag.InstitutionId = new SelectList(db.Institutions, "InstitutionId", "InstitutionName", parent.InstitutionId);
            return View(parent);
        }

        // POST: Parents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ParentId,ApplicationUserId,InstitutionId,FirstName,LastName,Address,AreaCode,City,MobilePhone,Email,ImagePath")] Parent parent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(parent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InstitutionId = new SelectList(db.Institutions, "InstitutionId", "InstitutionName", parent.InstitutionId);
            return View(parent);
        }

        // GET: Parents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parent parent = db.Parents.Find(id);
            if (parent == null)
            {
                return HttpNotFound();
            }
            return View(parent);
        }

        // POST: Parents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Parent parent = db.Parents.Find(id);
            db.Parents.Remove(parent);
            db.SaveChanges();
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
