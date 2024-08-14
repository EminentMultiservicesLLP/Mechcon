using BISERPBusinessLayer.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Master.Interfaces
{
    public interface ICityMasterRepository
    {
        CityMasterEntities GetCityById(int Id);
        IEnumerable<CityMasterEntities> GetAllCities();
        int CreateCity(CityMasterEntities entity);
        bool UpdateCity(CityMasterEntities entity);
        bool DeleteCity(CityMasterEntities entity);
    }
}
