using BISERPBusinessLayer.Entities.Store;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Store.Interfaces
{
    public interface IMaterialIssueRepository
    {
        MaterialIndentEntities GetDetailByID(int Id);
        IEnumerable<MaterialIssueEntity> GetAllList(int UserId);
        IEnumerable<MaterialIssueEntity> GetAllMaterialIssueFileDownload(int UserId);
        IEnumerable<MaterialIssueEntity> GetAllmirptList(int StoreId,DateTime fromdate,DateTime todate,int UserId);
        IEnumerable<MaterialIssueEntity> GetMaterialIssue(int StoreId,DateTime fromdate,DateTime todate);
        IEnumerable<MaterialIssueEntity> GetUnAcceptedAuthorized();
        IEnumerable<MaterialIndentEntities> GetActiveList(int userId, int LOCId);
        MaterialIssueEntity CreateNewEntry(MaterialIssueEntity entity, DBHelper dbhelper);
        bool UpdateEntry(MaterialIssueEntity entity);
        bool AuthCancelMaterialIssue(MaterialIssueEntity entity, DBHelper dbhelper);
        bool AcceptMaterialIssue(MaterialIssueEntity entity, DBHelper dbhelper);
        bool DeleteEntry(MatrialIssueEntity entity);
        IEnumerable<MaterialIssueEntity> GetIssuenoforReturn(int userId, int LOCId, string Type);
    }
}
