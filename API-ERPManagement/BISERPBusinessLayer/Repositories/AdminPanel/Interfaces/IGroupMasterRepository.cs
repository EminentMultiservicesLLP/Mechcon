using BISERPBusinessLayer.Entities.AdminPanel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.AdminPanel.Interfaces
{
    public interface IGroupMasterRepository
    {
        GroupMaster SaveGroupMaster(GroupMaster model);
        IEnumerable<GroupMaster> GetGroupMaster(int? statusID);
        IEnumerable<GroupUserMapping> GetGroupUserMapping(int? GroupID);
        IEnumerable<EmployeeEnrollmentEntity> GetUserByGroups(string GroupIDs);
    }
}
