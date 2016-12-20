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
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using DayCareApp.Web.DataContext.Repositories;
using DayCareApp.Web.DataContext.Persistence;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DayCareApp.Web.Controllers.Web
{
    public class EmployeesController : Controller
    {
        public readonly IEmployeeRepository _EmployeeRepository;
        public readonly IDepartmentRepository _DepartmentRepository;
        public readonly IInstitutionRepository _InstitutionRepository;
        public readonly UnitOfWork _unitOfWork;
        private ApplicationUserManager _userManager;

        public EmployeesController()
        {
            this._unitOfWork = new UnitOfWork(DayCareAppDB.Create());
            _EmployeeRepository = this._unitOfWork.Employees;
            _DepartmentRepository = this._unitOfWork.Departments;
            _InstitutionRepository = this._unitOfWork.Institutions;
        }

        public EmployeesController(IEmployeeRepository EmployeeRepository, IDepartmentRepository DepartmentRepository, IInstitutionRepository InstitutionRepository, IUnitOfWork unitOfWork, ApplicationUserManager userManager)
        {
            _EmployeeRepository = unitOfWork.Employees;
            _DepartmentRepository = unitOfWork.Departments;
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
        // GET: Employees
        public ActionResult Index()
        {
            var employees = _EmployeeRepository.GetAll(e => e.Department, e => e.Institution).ToList() ;
            return View(employees);
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = _EmployeeRepository.SingleOrDefault(i => i.EmployeeId.Equals(id), i => i.Institution, i => i.Department);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            RegisterEmployeeViewModel RegEmployeeViewModel = new RegisterEmployeeViewModel();
            RegEmployeeViewModel.InstitutionList = new SelectList(_InstitutionRepository.GetAll().ToList(), "InstitutionId", "InstitutionName");
            RegEmployeeViewModel.DepartmentList = new SelectList(_DepartmentRepository.GetAll().ToList(), "DepartmentId", "DepartmentName");
            
            return View(RegEmployeeViewModel);
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RegisterEmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var userStore = new UserStore<ApplicationUser>(new DayCareAppDB());
                    var userManager = new UserManager<ApplicationUser>(userStore);
                    await userManager.AddToRoleAsync(user.Id, "Employee");

                    model.Employee.ApplicationUserId = user.Id;
                    _EmployeeRepository.Add(model.Employee);
                    _unitOfWork.Complete();

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Employees");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay for
            model.InstitutionList = new SelectList(_InstitutionRepository.GetAll().ToList(), "InstitutionId", "InstitutionName", model.Employee.InstitutionId);
            model.DepartmentList = new SelectList(_DepartmentRepository.GetAll().ToList(), "DepartmentId", "DepartmentName", model.Employee.DepartmentId);
            return View(model);
            
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = _EmployeeRepository.Get(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            EmployeeViewModel EmpVM = new EmployeeViewModel();
            EmpVM.Employee = employee;
            EmpVM.DepartmentList = new SelectList(_DepartmentRepository.GetAll().ToList(), "DepartmentId", "DepartmentName", employee.DepartmentId);
            EmpVM.InstitutionList = new SelectList(_InstitutionRepository.GetAll().ToList(), "InstitutionId", "InstitutionName", employee.InstitutionId);
            return View(EmpVM);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                _EmployeeRepository.Edit(model.Employee);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            model.DepartmentList = new SelectList(_DepartmentRepository.GetAll().ToList(), "DepartmentId", "DepartmentName", model.Employee.DepartmentId);
            model.InstitutionList = new SelectList(_InstitutionRepository.GetAll().ToList(), "InstitutionId", "InstitutionName", model.Employee.InstitutionId);
            return View(model);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = _EmployeeRepository.Get(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Employee employee = _EmployeeRepository.Get(id);

            if (ModelState.IsValid)
            {
                if (employee.ApplicationUserId == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var user = await _userManager.FindByIdAsync(employee.ApplicationUserId);
                var logins = user.Logins;
                var rolesForUser = await _userManager.GetRolesAsync(employee.ApplicationUserId);

                using (var transaction = _unitOfWork.getContext().Database.BeginTransaction())
                {
                    foreach (var login in logins.ToList())
                    {
                        await _userManager.RemoveLoginAsync(login.UserId, new UserLoginInfo(login.LoginProvider, login.ProviderKey));
                    }

                    if (rolesForUser.Count() > 0)
                    {
                        foreach (var item in rolesForUser.ToList())
                        {
                            // item should be the name of the role
                            var result = await _userManager.RemoveFromRoleAsync(user.Id, item);
                        }
                    }

                    await _userManager.DeleteAsync(user);
                    transaction.Commit();
                }
            }

            _EmployeeRepository.Remove(employee);
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

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        #endregion
    }
}
