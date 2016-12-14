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
using DayCareApp.Web.Models;

namespace DayCareApp.Web.Controllers.Web
{
    public class DepartmentsController : Controller
    {
        private DepartmentRepository _DepartmentRepo = new DepartmentRepository();
        private InstitutionRepository _InstitutionRepo = new InstitutionRepository();

        // GET: Departments
        public ActionResult Index()
        {
            var departments = _DepartmentRepo.AllDepartments().ToList() ;
            return View(departments);
        }

        // GET: Departments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = _DepartmentRepo.FindDepartment(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: Departments/Create
        public ActionResult Create()
        {
            DepartmentViewModel DpVM = new DepartmentViewModel();
            DpVM.InstitutionList = new SelectList(_InstitutionRepo.AllInstitutions, "InstitutionId", "InstitutionName");
            return View(DpVM);
        }

        // POST: Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DepartmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                _DepartmentRepo.InsertOrUpdateDepartment(model.Department);
                _DepartmentRepo.Save();
                return RedirectToAction("Index");
            }

            model.InstitutionList = new SelectList(_InstitutionRepo.AllInstitutions.ToList(), "InstitutionId", "InstitutionName", model.Department.InstitutionId);
            return View(model);
        }

        // GET: Departments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = _DepartmentRepo.FindDepartment(id);
            if (department == null)
            {
                return HttpNotFound();
            }

            DepartmentViewModel DpVM = new DepartmentViewModel();
            DpVM.InstitutionList = new SelectList(_InstitutionRepo.AllInstitutions, "InstitutionId", "InstitutionName", department.InstitutionId);
            DpVM.Department = department;
            return View(DpVM);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DepartmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                _DepartmentRepo.InsertOrUpdateDepartment(model.Department);
                _DepartmentRepo.Save();
                return RedirectToAction("Index");
            }

            model.InstitutionList = new SelectList(_InstitutionRepo.AllInstitutions, "InstitutionId", "InstitutionName", model.Department.InstitutionId);
            return View(model);
        }

        // GET: Departments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = _DepartmentRepo.FindDepartment(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _DepartmentRepo.DeleteDepartment(id);
            _DepartmentRepo.Save();
            return RedirectToAction("Index");
        }

       
    }
}
