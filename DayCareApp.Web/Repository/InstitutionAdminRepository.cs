using DayCareApp.Web.DataContext;
using DayCareApp.Web.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DayCareApp.Web.Repository
{
    public class InstitutionAdminRepository
    {

        private DayCareAppDB dbContext = new DayCareAppDB();

        //Method for getting all InstitutionAdmins
        public IQueryable<InstitutionAdmin> AllInstitutionAdmins()
        {
            return dbContext.InstitutionAdmins.Include(i => i.Institution);
        }

        //Getting a specific InstitutionAdmin
        public InstitutionAdmin FindInstitutionAdmin(int? id)
        {
            return dbContext.InstitutionAdmins.Find(id);
        }

        //Create or modify InstitutionAdmin
        public void InsertOrUpdateInstitutionAdmin(InstitutionAdmin inputInstitutionAdmin)
        {
            //Note that when creating an institutionAdmin, the id needs to be set to 0
            if (inputInstitutionAdmin.InstitutionAdminId == 0)
            {
                dbContext.InstitutionAdmins.Add(inputInstitutionAdmin);
            }
            else
            {
                dbContext.Entry(inputInstitutionAdmin).State = EntityState.Modified;
            }
        }

        //Method for deleting institutionAdmins
        public void DeleteInstitutionAdmin(int? id)
        {
            InstitutionAdmin InstitutionAdmin = FindInstitutionAdmin(id);
            dbContext.InstitutionAdmins.Remove(InstitutionAdmin);
        }


        public void Save()
        {
            dbContext.SaveChanges();
        }
    }
}