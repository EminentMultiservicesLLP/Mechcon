using BISERPBusinessLayer.Entities.Transport;
using BISERPBusinessLayer.QueryCollection.Transport;
using BISERPBusinessLayer.Repositories.Transport.Interfaces;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Transport.Classes
{
    public class InsuranceCompanyRepository : IInsuranceCompanyRepository
    {

        public IEnumerable<InsuranceCompanyEntity> GetAllCompany()
        {
            List<InsuranceCompanyEntity> tax = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtUnits = dbHelper.ExecuteDataTable(TransportQuery.GetAllInsuranceCompany, CommandType.StoredProcedure);
                tax = dtUnits.AsEnumerable()
                            .Select(row => new InsuranceCompanyEntity
                            {
                                CompanyId = row.Field<int>("CompanyId"),
                                CompanyName = row.Field<string>("CompanyName"),
                                Deactive = row.Field<Boolean>("Deactive")
                            }).ToList();
            }
            return tax;
        }
    }
}
