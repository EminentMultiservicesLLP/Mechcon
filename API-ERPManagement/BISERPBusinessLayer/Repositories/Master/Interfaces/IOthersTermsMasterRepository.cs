using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPBusinessLayer.Entities.Masters;
using BISERPBusinessLayer.Entities.Purchase;

namespace BISERPBusinessLayer.Repositories.Master.Interfaces
{
   public  interface IOthersTermsMasterRepository
    {
       IEnumerable<OthersTermsMasterEntities> GetAllOtherTerms();
       IEnumerable<OthersTermsMasterEntities> GetActiveTerms();
       OthersTermsMasterEntities GetOtherTermsById(int termid);
       int CreateOtherTerms(OthersTermsMasterEntities entity);
       bool UpdateOtherTerms(OthersTermsMasterEntities entity);
       bool CheckDuplicateItem(string code);
       bool CheckDuplicateupdate(string code, int ID);
       IEnumerable<POPriceBasisEntity> AllBasisTerm();
       IEnumerable<POInspectionModel> AllInspectionTerm();
     int CreateInspectionmatser(InspectionMasterEntity entity);
       bool UpdateInspectionmatser(InspectionMasterEntity entity);
       bool CheckDuplicateInspectionItem(string inspectionCode);
       bool CheckDuplicateInspectionupdate(string inspectionCode, int inspectionId);
       int CreateBasisMaster(BasisMasterEntity entity);
       bool UpdateBasisMaster(BasisMasterEntity entity);
       bool CheckDuplicateBasisItem(string basisCode);
       bool CheckDuplicateBasisMasterupdate(string basisCode, int basisId);
    }
}
