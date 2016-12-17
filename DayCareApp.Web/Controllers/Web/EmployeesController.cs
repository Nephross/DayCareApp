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
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace DayCareApp.Web.Controllers.Web
{
    public class EmployeesController : Controller
    {
        private EmployeeRepository _EmployeeRepo = new EmployeeRepository();
        private InstitutionRepository _InstitutionRepo = new InstitutionRepository();
        private DepartmentRepository _DepartmentRepo = new DepartmentRepository();
        private ApplicationUserManager _userManager;

        public EmployeesController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public EmployeesController()
        {
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
            var employees = _EmployeeRepo.AllEmployees() ;
            return View(employees.ToList());
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = _EmployeeRepo.FindEmployee(id);
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
            RegEmployeeViewModel.DepartmentList = new SelectList(_DepartmentRepo.AllDepartments().ToList(), "DepartMentId", "DepartmentName");
            RegEmployeeViewModel.InstitutionList = new SelectList(_InstitutionRepo.AllInstitutions, "InstitutionId", "InstitutionName");
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
                    await UserManager.AddToRoleAsync(user.Id, "Employee");

                    model.Employee.ApplicationUserId = user.Id;
                    _EmployeeRepo.InsertOrUpdateEmployee(model.Employee);
                    _EmployeeRepo.Save();

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
            model.InstitutionList = new SelectList(_InstitutionRepo.AllInstitutions.ToList(), "InstitutionId", "InstitutionName", model.Employee.InstitutionId);
            model.DepartmentList = new SelectList(_DepartmentRepo.AllDepartments().ToList(), "DepartmentId", "DepartmentName", model.Employee.DepartmentId);
            return View(model);
            
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = _EmployeeRepo.FindEmployee(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            EmployeeViewModel EmpVM = new EmployeeViewModel();
            EmpVM.Employee = employee;
            EmpVM.DepartmentList = new SelectList(_DepartmentRepo.AllDepartments().ToList(), "DepartMentId", "DepartmentName", employee.DepartmentId);
            EmpVM.InstitutionList = new SelectList(_DepartmentRepo.AllInstitutions, "InstitutionId", "InstitutionName", employee.InstitutionId);
            return View(EmpVM);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeVievModel model)
        {
            if (ModelState.IsValid)
            {
                _EmployeeRepo.InsertOrUpdate(model.Employee);
                _EmployeeRepo.Save();
                return RedirectToAction("Index");
            }
            model.DepartmentList = new SelectList(_DepartmentRepo.AllDepartments().ToList(), "DepartMentId", "DepartmentName", model.Employee.DepartmentId);
            model.InstitutionList = new SelectList(_DepartmentRepo.AllInstitutions, "InstitutionId", "InstitutionName", model.Employee.InstitutionId);
            return View(model);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = _EmployeeRepo.FindEmployee(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _EmployeeRepo.Delete(id);
            _EmployeeRepo.Save();
            return RedirectToAction("Index");
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
