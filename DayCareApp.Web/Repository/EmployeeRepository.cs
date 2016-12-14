using DayCareApp.Web.DataContext;
using DayCareApp.Web.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DayCareApp.Web.Repository
{
    public class EmployeeRepository
    {
        private DayCareAppDB dbContext = new DayCareAppDB();

        //Method for getting all DEpartments
        public IQueryable<Employee> AllEmployees()
        {
            return dbContext.Employees.Include(i => i.Institution).Include(i => i.Department);
        }

        //Getting a specific Department
        public Employee FindEmployee(int? id)
        {
            return dbContext.Employees.Where(i => i.EmployeeId == id).Include(i => i.Institution).Include(i => i.Department).FirstOrDefault();
        }

        //Create or modify Department
        public void InsertOrUpdateEmployee(Employee inputEmployee)
        {
            //Note that when creating an institutionAdmin, the id needs to be set to 0
            if (inputEmployee.EmployeeId == 0)
            {
                dbContext.Employees.Add(inputEmployee);
            }
            else
            {
                dbContext.Entry(inputEmployee).State = EntityState.Modified;
            }
        }

        //Method for deleting Department
        public void DeleteEmployee(int? id)
        {
            Employee Employee = FindEmployee(id);
            dbContext.Employees.Remove(Employee);
        }


        public void Save()
        {
            dbContext.SaveChanges();
        }
    }
}