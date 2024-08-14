using BISERPBusinessLayer.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Master.Interfaces
{
    public interface ITaxMasterRepository
    {
        TaxMasterEntity GetTaxById(int Id);
        IEnumerable<TaxMasterEntity> GetAllTaxes();
        int CreateTax(TaxMasterEntity entity);
        bool UpdateTax(TaxMasterEntity entity);
        bool DeleteTax(TaxMasterEntity entity);
    }
}
