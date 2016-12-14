using DayCareApp.Web.DataContext;
using DayCareApp.Web.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DayCareApp.Web.Repository
{
    public class DepartmentRepository
    {
        private DayCareAppDB dbContext = new DayCareAppDB();

        //Method for getting all DEpartments
        public IQueryable<Department> AllDepartments()
        {
            return dbContext.Departments.Include(i => i.Institution);
        }

        //Getting a specific Department
        public Department FindDepartment(int? id)
        {
            return dbContext.Departments.Where(i => i.DepartmentId == id).Include(i => i.Institution).FirstOrDefault();
        }

        //Create or modify Department
        public void InsertOrUpdateDepartment(Department inputDepartment)
        {
            //Note that when creating an institutionAdmin, the id needs to be set to 0
            if (inputDepartment.DepartmentId == 0)
            {
                dbContext.Departments.Add(inputDepartment);
            }
            else
            {
                dbContext.Entry(inputDepartment).State = EntityState.Modified;
            }
        }

        //Method for deleting Department
        public void DeleteDepartment(int? id)
        {
            Department Department = FindDepartment(id);
            dbContext.Departments.Remove(Department);
        }


        public void Save()
        {
            dbContext.SaveChanges();
        }

    }
}