using DayCareApp.Web.Entities;
using DayCareApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DayCareApp.Web.Controllers.Web
{
    public class CheckInController : Controller
    {
        public ActionResult Index()
        {
            var model =
            from r in _children
            orderby r.Institution
            select r;

            return View(model);
        }

        // GET: Reviews/Details/5
        public ActionResult Details(int id)
        {
            var child = _children.Single(r => r.ChildId == id);
            return View(child);
        }

        // GET: Reviews/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reviews/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here, for saving

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Reviews/Edit/5
        public ActionResult Edit(int id)
        {
            var child = _children.Single(r => r.ChildId == id);
            return View(child);
        }

        // POST: Reviews/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var child = _children.Single(r => r.ChildId == id);
            if (TryUpdateModel(child))
            {
                //save to db here
                return RedirectToAction("Index");
            }
            return View(child);
        }

        // GET: Reviews/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Reviews/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // Method to set currentlycheckin back to norm - We should have a date for the last check to compare with 
        public void ResetCheckIns(DateTime LastCheck)
        {
            DateTime CurrentDate = DateTime.Today;

            if (CurrentDate.Date < LastCheck.Date)
            {
                for (int i = 0; i < _children.Count; i++)
                {
                    _children[i].CurrentlyCheckedIn = false;
                }

            } 
        }

        // here we should get the data that is needed in the view
        // currently mock data

        static List<Child> _children = new List<Child>
        {
        };

    }
}