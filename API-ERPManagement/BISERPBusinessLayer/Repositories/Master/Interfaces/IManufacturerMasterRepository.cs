using BISERPBusinessLayer.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Master.Interfaces
{
    public interface IManufacturerMasterRepository
    {
        ManufacturerMasterEntities GetManufacturerById(int ManufacturerId);
        IEnumerable<ManufacturerMasterEntities> GetAllManufacturers();
        IEnumerable<ManufacturerMasterEntities> GetActiveManufacturers();
        int CreateManufacturer(ManufacturerMasterEntities ManufacturerEntity);
        bool UpdateManufacturer(ManufacturerMasterEntities ManufacturerEntity);
        bool DeleteManufacturer(ManufacturerMasterEntities ManufacturerEntity);
        bool CheckDuplicateItem(string code);
        bool CheckDuplicateupdate(string code, int ID);
    }
}
