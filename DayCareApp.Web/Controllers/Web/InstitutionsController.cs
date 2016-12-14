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

namespace DayCareApp.Web.Controllers.Web
{
    public class InstitutionsController : Controller
    {
        private InstitutionRepository _InstitutionRepo = new InstitutionRepository();

        // GET: Institutions
        public ActionResult Index()
        {
            return View(_InstitutionRepo.AllInstitutions.ToList());
        }

        // GET: Institutions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Institution institution = _InstitutionRepo.FindInstitution(id);
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
                _InstitutionRepo.InsertOrUpdateInstitution(institution);
                _InstitutionRepo.Save();
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
            Institution institution = _InstitutionRepo.FindInstitution(id);
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
                _InstitutionRepo.InsertOrUpdateInstitution(institution);
                _InstitutionRepo.Save();
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
            Institution institution = _InstitutionRepo.FindInstitution(id);
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
            _InstitutionRepo.DeleteInstitution(id);
            _InstitutionRepo.Save();
            return RedirectToAction("Index");
        }

    }
}
