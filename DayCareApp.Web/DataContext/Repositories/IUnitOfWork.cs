using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayCareApp.Web.DataContext.Repositories
{
    public interface IUnitOfWork
    {
        IChildRepository Children { get; }
        IParentRepository Parents { get; }
        IInstitutionRepository Institutions { get; }
        IInstitutionAdminRepository InstitutionAdmins { get; }
        IDepartmentRepository Departments { get; }
        IEmployeeRepository Employees { get; }
        IDayRegistrationRepository DayRegistrations { get; }
       
        int Complete();
    }
}
