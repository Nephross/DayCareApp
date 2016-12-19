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
    public class InstitutionAdminsController : Controller
    {
        public readonly IInstitutionAdminRepository _InstitutionAdminRepository;
        public readonly IInstitutionRepository _InstitutionRepository;
        public readonly UnitOfWork _unitOfWork;
        private ApplicationUserManager _userManager;

        public InstitutionAdminsController()
        {
            this._unitOfWork = new UnitOfWork(DayCareAppDB.Create());
            _InstitutionAdminRepository = this._unitOfWork.InstitutionAdmins;
            _InstitutionRepository = this._unitOfWork.Institutions;
        }

        public InstitutionAdminsController(ApplicationUserManager userManager)
        {
           
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

        // GET: InstitutionAdmins
        public ActionResult Index()
        {
            var institutionAdmins = _InstitutionAdminRepository.GetAll().ToList();
            return View(institutionAdmins.ToList());
        }

        // GET: InstitutionAdmins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstitutionAdmin institutionAdmin = _InstitutionAdminRepository.Get(id);
            if (institutionAdmin == null)
            {
                return HttpNotFound();
            }
            return View(institutionAdmin);
        }

        // GET: InstitutionAdmins/Create
        [AllowAnonymous]
        public ActionResult Create()
        {
            RegisterInstitutionAdminViewModel RegInstAdminVM = new RegisterInstitutionAdminViewModel();
            RegInstAdminVM.InstitutionList = new SelectList(_InstitutionRepository.GetAll().ToList(), "InstitutionId", "InstitutionName");
            return View(RegInstAdminVM);
        }

        // POST: InstitutionAdmins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RegisterInstitutionAdminViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {

                    var userStore = new UserStore<ApplicationUser>(new DayCareAppDB());
                    var userManager = new UserManager<ApplicationUser>(userStore);
                    await UserManager.AddToRoleAsync(user.Id, "InstitutionAdmin");

                    model.InstitutionAdmin.ApplicationUserId = user.Id;
                    _InstitutionAdminRepository.Add(model.InstitutionAdmin);
                    _unitOfWork.Complete();

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "InstitutionAdmins");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay for
            model.InstitutionList = new SelectList(_InstitutionRepository.GetAll().ToList(), "InstitutionId", "InstitutionName", model.InstitutionAdmin.InstitutionId);
            return View(model);
        }

        // GET: InstitutionAdmins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstitutionAdmin institutionAdmin = _InstitutionAdminRepository.Get(id);
            if (institutionAdmin == null)
            {
                return HttpNotFound();
            }

            InstitutionAdminViewModel InstAdminVM = new InstitutionAdminViewModel();
            InstAdminVM.InstitutionList = new SelectList(_InstitutionRepository.GetAll().ToList(), "InstitutionId", "InstitutionName", institutionAdmin.InstitutionId);
            InstAdminVM.institutionAdmin = institutionAdmin;
            return View(InstAdminVM);
        }

        // POST: InstitutionAdmins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(InstitutionAdminViewModel model)
        {
            if (ModelState.IsValid)
            {
                _InstitutionAdminRepository.Edit(model.institutionAdmin);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            model.InstitutionList= new SelectList(_InstitutionRepository.GetAll().ToList(), "InstitutionId", "InstitutionName", model.institutionAdmin.InstitutionId);
            return View(model);
        }

        // GET: InstitutionAdmins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstitutionAdmin institutionAdmin = _InstitutionAdminRepository.Get(id);
            if (institutionAdmin == null)
            {
                return HttpNotFound();
            }
            return View(institutionAdmin);
        }

        // POST: InstitutionAdmins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            InstitutionAdmin institutionAdmin = _InstitutionAdminRepository.Get(id);

            if (ModelState.IsValid)
            {
                if (institutionAdmin.ApplicationUserId == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var user = await _userManager.FindByIdAsync(institutionAdmin.ApplicationUserId);
                var logins = user.Logins;
                var rolesForUser = await _userManager.GetRolesAsync(institutionAdmin.ApplicationUserId);

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

            _InstitutionAdminRepository.Remove(institutionAdmin);
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
