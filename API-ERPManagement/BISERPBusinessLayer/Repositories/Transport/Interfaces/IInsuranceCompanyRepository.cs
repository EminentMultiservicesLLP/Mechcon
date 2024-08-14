using BISERPBusinessLayer.Entities.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Transport.Interfaces
{
    public interface IInsuranceCompanyRepository
    {
        IEnumerable<InsuranceCompanyEntity> GetAllCompany();
    }
}
