using BISERPBusinessLayer.Entities.Masters;
using BISERPBusinessLayer.QueryCollection.Masters;
using BISERPBusinessLayer.Repositories.Master.Interfaces;
using BISERPCommon;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Master.Classes
{
    public class MechconMasterRepository : IMechconMasterRepository
    {
        private static readonly ILogger _loggger = Logger.Register(typeof(MechconMasterRepository));

        public MechconMasterEntity GeMechconData()
        {
            MechconMasterEntity data = new MechconMasterEntity();
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtManufacturer = dbHelper.ExecuteDataTable(MasterQueries.GetMechconData, CommandType.StoredProcedure);

                data.Id = dtManufacturer.Rows[0].Field<int>("ID");
                data.Name = dtManufacturer.Rows[0].Field<string>("Name");
                data.Address = dtManufacturer.Rows[0].Field<string>("Address");
                data.CINNumber = dtManufacturer.Rows[0].Field<string>("CIN");
                data.emailID = dtManufacturer.Rows[0].Field<string>("emailID");               
                data.GSTNumber = dtManufacturer.Rows[0].Field<string>("GST");                             
                            
            }
            return data;
        }

        public int SaveData(MechconMasterEntity model)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ID", model.Id, DbType.Int32));
                paramCollection.Add(new DBParameter("Name", model.Name, DbType.String));
                paramCollection.Add(new DBParameter("Address", model.Address, DbType.String));
                paramCollection.Add(new DBParameter("CIN", model.CINNumber, DbType.String));
                paramCollection.Add(new DBParameter("emailId", model.emailID, DbType.String));
                paramCollection.Add(new DBParameter("GST", model.GSTNumber, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", model.Deactive, DbType.Boolean));
                  
                iResult = dbHelper.ExecuteNonQuery(MasterQueries.InsertMechconData, paramCollection, CommandType.StoredProcedure);
            }
            return iResult;
        }
    }
}