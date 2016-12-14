using DayCareApp.Web.DataContext;
using DayCareApp.Web.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DayCareApp.Web.Repository
{
    public class InstitutionRepository
    {
        private DayCareAppDB dbContext = new DayCareAppDB();

        //Method for getting all Institutions
        public IQueryable<Institution> AllInstitutions
        {
            get { return dbContext.Institutions; }
        }

        //Getting a specific Institution
        public Institution FindInstitution(int? id)
        {
            return dbContext.Institutions.Find(id);
        }

        //Create or modify Institution
        public void InsertOrUpdateInstitution(Institution inputInstitution)
        {
            //Note that when creating an institution, the id needs to be set to 0
            if (inputInstitution.InstitutionId == 0)
            {
                dbContext.Institutions.Add(inputInstitution);
            }
            else
            {
                dbContext.Entry(inputInstitution).State = EntityState.Modified;
            }
        }

        //Method for deleting institutions
        public void DeleteInstitution(int? id)
        {
            Institution Institution = FindInstitution(id);
            dbContext.Institutions.Remove(Institution);
        }

        //Method for saving changes in the dbcontext to the db.
        public void Save()
        {
            dbContext.SaveChanges();
        }

    }
}