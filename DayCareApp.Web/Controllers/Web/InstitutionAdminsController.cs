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
    public class InstitutionAdminsController : Controller
    {
        private InstitutionAdminRepository _InstitutionAdminRepo = new InstitutionAdminRepository();
        private InstitutionRepository _InstitutionRepo = new InstitutionRepository();
        private ApplicationUserManager _userManager;

        public InstitutionAdminsController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public InstitutionAdminsController()
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
        // GET: InstitutionAdmins
        public ActionResult Index()
        {
            var institutionAdmins = _InstitutionAdminRepo.AllInstitutionAdmins();
            return View(institutionAdmins.ToList());
        }

        // GET: InstitutionAdmins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstitutionAdmin institutionAdmin = _InstitutionAdminRepo.FindInstitutionAdmin(id);
            if (institutionAdmin == null)
            {
                return HttpNotFound();
            }
            return View(institutionAdmin);
        }

        // GET: InstitutionAdmins/Create
        public ActionResult Create()
        {
            RegisterInstitutionAdminViewModel RegInstAdminVM = new RegisterInstitutionAdminViewModel();
            RegInstAdminVM.Institutions = new SelectList(_InstitutionRepo.AllInstitutions.ToList(), "InstitutionId", "InstitutionName");
            return View(RegInstAdminVM);
        }

        // POST: InstitutionAdmins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RegisterInstitutionAdminViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, "InstitutionAdmin");

                    model.InstitutionAdmin.ApplicationUserId = user.Id;
                    _InstitutionAdminRepo.InsertOrUpdateInstitutionAdmin(model.InstitutionAdmin);
                    _InstitutionAdminRepo.Save();

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
            model.Institutions = new SelectList(_InstitutionRepo.AllInstitutions.ToList(), "InstitutionId", "InstitutionName", model.InstitutionAdmin.InstitutionId);
            return View(model);
        }

        // GET: InstitutionAdmins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstitutionAdmin institutionAdmin = _InstitutionAdminRepo.FindInstitutionAdmin(id);
            if (institutionAdmin == null)
            {
                return HttpNotFound();
            }

            InstitutionAdminViewModel InstAdminVM = new InstitutionAdminViewModel();
            InstAdminVM.InstitutionList = new SelectList(_InstitutionRepo.AllInstitutions.ToList(), "InstitutionId", "InstitutionName", institutionAdmin.InstitutionId);
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
                _InstitutionAdminRepo.InsertOrUpdateInstitutionAdmin(model.institutionAdmin);
                _InstitutionAdminRepo.Save();
                return RedirectToAction("Index");
            }
            model.InstitutionList= new SelectList(_InstitutionRepo.AllInstitutions.ToList(), "InstitutionId", "InstitutionName", model.institutionAdmin.InstitutionId);
            return View(model);
        }

        // GET: InstitutionAdmins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstitutionAdmin institutionAdmin = _InstitutionAdminRepo.FindInstitutionAdmin(id);
            if (institutionAdmin == null)
            {
                return HttpNotFound();
            }
            return View(institutionAdmin);
        }

        // POST: InstitutionAdmins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _InstitutionAdminRepo.DeleteInstitutionAdmin(id);
            
            _InstitutionAdminRepo.Save();
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
