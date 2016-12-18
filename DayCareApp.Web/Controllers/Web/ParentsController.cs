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
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using DayCareApp.Web.Helpers;

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
        public async Task<ActionResult> Create(RegisterParentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, "Parent");

                    model.Parent.ApplicationUserId = user.Id;
                    model.Parent.Email = user.Email;

                    FileHandler fileHandler = new FileHandler();
                    string serverPath = Server == null ? "" : Server.MapPath("~");
                    try
                    {
                        model.Parent.ImagePath = fileHandler.SaveImage(model.FileUploadPacket, serverPath);
                    }
                    catch { }

                    _ParentRepository.Add(model.Parent);
                    _unitOfWork.Complete();

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Parents");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay for
            model.InstitutionList = new SelectList(_InstitutionRepository.GetAll().ToList(), "InstitutionId", "InstitutionName", model.Parent.ParentId);
            return View(model);
        }

        // GET: Parents/Edit/5
        public ActionResult Edit(int? id)
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
            ParentViewModel ParentViewModel = new ParentViewModel();
            ParentViewModel.Parent = parent;
            ParentViewModel.InstitutionList = new SelectList(_InstitutionRepository.GetAll().ToList(), "InstitutionId", "InstitutionName", parent.ParentId);
            return View(parent);
        }

        // POST: Parents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ParentViewModel model)
        {
            if (ModelState.IsValid)
            {
                FileHandler fileHandler = new FileHandler();
                if (model.FileUploadPacket != null)
                {
                    string serverPath = Server == null ? "" : Server.MapPath("~");
                    try
                    {
                        //Deleting the current profile picture.
                        fileHandler.deleteImage(serverPath, model.Parent.ImagePath);
                        //Saving the new profile picture.
                        model.Parent.ImagePath = fileHandler.SaveImage(model.FileUploadPacket, serverPath);
                    }
                    catch { }
                }
                _ParentRepository.Edit(model.Parent);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }

            model.InstitutionList = new SelectList(_InstitutionRepository.GetAll().ToList(), "InstitutionId", "InstitutionName", model.Parent.ParentId);
            return View(model);
        }

        // GET: Parents/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Parents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Parent parent = _ParentRepository.Get(id);
            FileHandler fileHandler = new FileHandler();
            
            string serverPath = Server == null ? "" : Server.MapPath("~");
            try
            {
                //Deleting the current profile picture.
                fileHandler.deleteImage(serverPath, parent.ImagePath);
                //Saving the new profile picture.
                   
            }
            catch { }
          
            _ParentRepository.Remove(parent);
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


        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}
