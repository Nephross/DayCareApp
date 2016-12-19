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
using DayCareApp.Web.Models;
using DayCareApp.Web.DataContext.Persistence;
using DayCareApp.Web.DataContext.Repositories;

namespace DayCareApp.Web.Controllers.Web
{
    public class DepartmentsController : Controller
    {
        public readonly IDepartmentRepository _DepartmentRepository;
        public readonly IInstitutionRepository _InstitutionRepository;
        public readonly UnitOfWork _unitOfWork;

        public DepartmentsController()
        {
            this._unitOfWork = new UnitOfWork(DayCareAppDB.Create());
            _DepartmentRepository = this._unitOfWork.Departments;
            _InstitutionRepository = this._unitOfWork.Institutions;
        }

        public DepartmentsController(IDepartmentRepository DepartmentRepository, IInstitutionRepository InstitutionRepository,IUnitOfWork unitOfWork)
        {
            _DepartmentRepository = unitOfWork.Departments;
            _InstitutionRepository = unitOfWork.Institutions;
        }

        // GET: Departments
        public ActionResult Index()
        {
            var departments = _DepartmentRepository.GetAll().ToList() ;
            return View(departments);
        }

        // GET: Departments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = _DepartmentRepository.Get(id);
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
            DpVM.InstitutionList = new SelectList(_InstitutionRepository.GetAll().ToList(), "InstitutionId", "InstitutionName");
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
                _DepartmentRepository.Add(model.Department);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }

            model.InstitutionList = new SelectList(_InstitutionRepository.GetAll().ToList(), "InstitutionId", "InstitutionName", model.Department.InstitutionId);
            return View(model);
        }

        // GET: Departments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = _DepartmentRepository.Get(id);
            if (department == null)
            {
                return HttpNotFound();
            }

            DepartmentViewModel DpVM = new DepartmentViewModel();
            DpVM.InstitutionList = new SelectList(_InstitutionRepository.GetAll().ToList(), "InstitutionId", "InstitutionName", department.InstitutionId);
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
                _DepartmentRepository.Edit(model.Department);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }

            model.InstitutionList = new SelectList(_InstitutionRepository.GetAll().ToList(), "InstitutionId", "InstitutionName", model.Department.InstitutionId);
            return View(model);
        }

        // GET: Departments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = _DepartmentRepository.Get(id);
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
            Department department = _DepartmentRepository.Get(id);
            
            _DepartmentRepository.Remove(department);
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
