using BISERPBusinessLayer.Entities.Branch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Branch.Interface
{
    public interface IEmployeeRepository
    {
        IEnumerable<EmployeeEntity> GetAllEmployee(int UserId);
        IEnumerable<EmployeeEntity> GetAllEmployeeForGuarantor(int UserId);
    }
}
