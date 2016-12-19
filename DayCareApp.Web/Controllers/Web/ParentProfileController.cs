using DayCareApp.Web.DataContext;
using DayCareApp.Web.DataContext.Persistence;
using DayCareApp.Web.DataContext.Repositories;
using DayCareApp.Web.Entities;
using DayCareApp.Web.Helpers;
using DayCareApp.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DayCareApp.Web.Controllers.Web
{
    public class ParentProfileController : Controller
    {
        public readonly IParentRepository _ParentRepository;
        public readonly IInstitutionRepository _InstitutionRepository;
        public readonly IChildRepository _ChildRepository;
        public readonly IDepartmentRepository _DepartmentRepository;
        public readonly UnitOfWork _unitOfWork;

        public ParentProfileController()
        {
            this._unitOfWork = new UnitOfWork(DayCareAppDB.Create());
        }

        public ParentProfileController(IParentRepository ParentRepository, IInstitutionRepository InstitutionRepository, IChildRepository ChildRepository, IDepartmentRepository DepartmentRepository, IUnitOfWork unitOfWork)
        {
            _ParentRepository = unitOfWork.Parents;
            _InstitutionRepository = unitOfWork.Institutions;
            _ChildRepository = ChildRepository;
            _DepartmentRepository = DepartmentRepository;
        }


        // GET: ParentProfile
        public ActionResult Index()
        {
            return View();
        }

        //Get profile
        public ActionResult ParentProfile()
        {
            ParentViewModel ParentVM = new ParentViewModel();
            ParentVM.Parent = _ParentRepository.SingleOrDefault(x => x.ApplicationUserId == User.Identity.GetUserId());
            ParentVM.InstitutionList = new SelectList(_InstitutionRepository.GetAll().ToList(), "InstitutionId", "InstitutionName", ParentVM.Parent.ParentId);
            return View(ParentVM);
        }

        //Edit Profile
        public ActionResult EditProfile(int? id)
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
            return View(ParentViewModel); ;
        }

        [HttpPost]
        public ActionResult EditProfile(ParentViewModel model)
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
                return RedirectToAction("ParentProfile");
            }

            model.InstitutionList = new SelectList(_InstitutionRepository.GetAll().ToList(), "InstitutionId", "InstitutionName", model.Parent.ParentId);
            return View(model);
        }


        //Get Child
        public ActionResult GetChildren()
        {
            var userId = User.Identity.GetUserId();
            Parent parent = _ParentRepository.SingleOrDefault(p => p.ApplicationUserId == userId);
            var Children = _ChildRepository.Find(c => c.Parent1 == parent|| c.Parent2 == parent);

            return View(Children.ToList());
        }

        //Edit Child
        public ActionResult EditChild(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Child child = _ChildRepository.Get(id);
            if (child == null)
            {
                return HttpNotFound();
            }
            ChildViewModel ChildViewModel = new ChildViewModel();
            ChildViewModel.Child = child;
            ChildViewModel.InstitutionList = new SelectList(_InstitutionRepository.GetAll().ToList(), "InstitutionId", "InstitutionName", child.InstitutionId);
            ChildViewModel.DepartmentList = new SelectList(_DepartmentRepository.GetAll().ToList(), "DepartmentId", "DepartmentName", child.DepartmentId);
            return View(ChildViewModel); ;
        }

        [HttpPost]
        public ActionResult EditChild(ChildViewModel model)
        {
            return View();
        }


        //Get DayRegistration
        public ActionResult GetDayregistration(int? id)
        {
            return View();
        }

        //Edit Dayregistration
        public ActionResult EditDayRegistration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EditDayRegistration(DayRegistrationViewModel model)
        {
            return View();
        }

    }
}