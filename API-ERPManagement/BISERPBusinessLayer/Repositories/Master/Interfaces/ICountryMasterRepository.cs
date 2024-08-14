using BISERPBusinessLayer.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Master.Interfaces
{
    public interface ICountryMasterRepository
    {
        CountryMasterEntities GetCountryById(int Id);
        IEnumerable<CountryMasterEntities> GetAllCountrys();
        bool CreateCountry(CountryMasterEntities entity);
        bool UpdateCountry(CountryMasterEntities entity);
        bool DeleteCountry(CountryMasterEntities entity);
    }
}
