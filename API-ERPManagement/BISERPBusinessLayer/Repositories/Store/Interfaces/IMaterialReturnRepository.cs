using BISERPBusinessLayer.Entities.Store;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Store.Interfaces
{
    public interface IMaterialReturnRepository
    {
        MaterialReturnEntity GetDetailByID(int id);
        IEnumerable<MaterialReturnEntity> GetAllMaterialIssue();
        IEnumerable<MaterialReturnEntity> GetAllMaterialReturn(int Authorized);
        MaterialReturnEntity CreateMaterialReturn(MaterialReturnEntity entity, DBHelper dbHelper);
        bool UpdateMaterialReturnAuth(MaterialReturnEntity entity,MaterialReturnDetailsEntities entity1);
    }
}
