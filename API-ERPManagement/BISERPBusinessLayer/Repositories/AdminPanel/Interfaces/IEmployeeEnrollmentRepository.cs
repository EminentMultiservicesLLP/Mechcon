using BISERPBusinessLayer.Entities.AdminPanel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.AdminPanel.Interfaces
{
    public interface IEmployeeEnrollmentRepository
    {
        IEnumerable<EmployeeEnrollmentEntity> GetUserCode();
        IEnumerable<EmployeeEnrollmentEntity> GetUserDetails(int LoginId);
        int SaveUser(EmployeeEnrollmentEntity Items);
        int DeleteUser(EmployeeEnrollmentEntity Items);
        bool CheckDuplicateItem(string UserCode, int UserID);
        int ChangePassword(EmployeeEnrollmentEntity Items);
        IEnumerable<DepartmentModel> GetDepartments();
        IEnumerable<DesignationModel> GetDesignations();
    }
}
