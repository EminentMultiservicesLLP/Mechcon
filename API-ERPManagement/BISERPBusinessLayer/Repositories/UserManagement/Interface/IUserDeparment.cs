using BISERPBusinessLayer.Entities.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.UserManagement.Interface
{
    public interface IUserDeparment
    {
        List<UserDepartment> GetUserAccessBranch(int UserId);
        List<UserDepartment> GetUserAccessStore(int UserId);
        List<UserDepartment> GetUserAccessUnitStore(int UserId);
    }
}
