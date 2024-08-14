using BISERPBusinessLayer.Entities.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.UserManagement.Interface
{
    public interface IUserMenuRightsRepository
    {
        List<UserMenuRightsEntity> GetAllMenuRights(int UserId, int ParentMenuId);
        List<UserMenuRightsEntity> GetAllMenuRights();
        List<ReportListEntity> GetReportList(int UserId, int ReportGroupId);
        int saveManuSettings(UserMenuRightsEntity Items);
    }
}
