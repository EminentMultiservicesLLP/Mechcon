using BISERPBusinessLayer.Entities.Masters;
using BISERPBusinessLayer.QueryCollection.Masters;
using BISERPBusinessLayer.Repositories.Master.Interfaces;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Master.Classes
{
    public class TaxMasterRepository : ITaxMasterRepository
    {

        public TaxMasterEntity GetTaxById(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TaxMasterEntity> GetAllTaxes()
        {
            List<TaxMasterEntity> taxes = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtTaxes = dbHelper.ExecuteDataTable(MasterQueries.GetAllTaxMaster, CommandType.StoredProcedure);
                taxes = dtTaxes.AsEnumerable()
                            .Select(row => new TaxMasterEntity
                            {
                                Taxid = row.Field<int>("Taxid"),
                                Tax_Code = row.Field<string>("Tax_Code"),
                                Tax_name = row.Field<string>("Tax_name"),
                                Tax_Type = row.Field<int>("Tax_Type"),
                                Tax_percentage = row.Field<double?>("Tax_percentage"),
                                Formula = row.Field<string>("Formula"),
                                Tax_EncExc = row.Field<bool?>("Tax_EncExc"),
                                Taxes = row.Field<string>("Taxes"),
                                Tax_Printname = row.Field<string>("Tax_Printname"),
                                TaxSub_Type = row.Field<string>("TaxSub_Type"),
                                FormC_Tax = row.Field<double?>("FormC_Tax"),
                                IsDependent = row.Field<bool?>("IsDependent"),
                                Total_TaxPer = row.Field<double?>("Total_TaxPer"),
                                Deactive = row.Field<bool?>("Deactive")
                            }).ToList();
            }
            return taxes;
        }

        public int CreateTax(TaxMasterEntity entity)
        {
            throw new NotImplementedException();
        }

        public bool UpdateTax(TaxMasterEntity entity)
        {
            throw new NotImplementedException();
        }

        public bool DeleteTax(TaxMasterEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
