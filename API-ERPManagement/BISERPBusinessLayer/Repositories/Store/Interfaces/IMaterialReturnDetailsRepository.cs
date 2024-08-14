using BISERPBusinessLayer.Entities.Store;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Store.Interfaces
{
  public  interface IMaterialReturnDetailsRepository
  {
      List<MaterialReturnDetailsEntities> MaterialReturnDetailsById(int ReturnID);
      IEnumerable<MaterialReturnDetailsEntities> GetDetailByIssueID(int IssueId);
      int CreateMaterialReturntDetails( int ReturnID,MaterialReturnDetailsEntities entity,DBHelper dbHelper);
    }
}
