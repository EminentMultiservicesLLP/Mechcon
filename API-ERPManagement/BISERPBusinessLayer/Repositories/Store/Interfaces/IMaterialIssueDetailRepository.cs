using BISERPBusinessLayer.Entities.Store;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Store.Interfaces
{
    public interface IMaterialIssueDetailRepository
    {
        int CreateMIDetails(MaterialIssueEntity mientity, MaterialIssueDetailsEntity entity, DBHelper dbhelper);

        IEnumerable<MaterialIssueDetailsEntity> GetDetailByIssueID(int IssueId);
        bool UpdateMaterialIssueAuthQty(MaterialIssueEntity mientity, MaterialIssueDetailsEntity entity, DBHelper dbhelper);
    }
}
