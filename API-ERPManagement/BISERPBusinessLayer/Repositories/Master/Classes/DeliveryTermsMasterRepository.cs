using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPDataLayer.DataAccess;
using BISERPBusinessLayer.Entities.Masters;
using System.Data;
using BISERPBusinessLayer.QueryCollection.Masters;
using BISERPCommon.Extensions;
using BISERPBusinessLayer.Repositories.Master.Interfaces;

namespace BISERPBusinessLayer.Repositories.Master.Classes
{
    public  class DeliveryTermsMasterRepository:IDeliveryTermsMasterRepository
    {
        public IEnumerable<DeliveryTermsMasterEntities> GetAllDeliveryTerms()
        {
            List<DeliveryTermsMasterEntities> deliveries = new List<DeliveryTermsMasterEntities>();
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtDeliveries = dbHelper.ExecuteDataTable(MasterQueries.GetAllDeliveryMaster, CommandType.StoredProcedure);
                deliveries = dtDeliveries.AsEnumerable().Select(row => new DeliveryTermsMasterEntities
                {
                    TermID = row.Field<int>("TermID"),
                    DeliveryTermDesc = row.Field<string>("DeliveryTermDesc").NullToString(),
                    DeliveryTermCode = row.Field<string>("DeliveryTermCode"),
                    Deactive = row.Field<Boolean>("Deactive")   
                }).ToList();
                if (deliveries == null || deliveries.Count == 0)
                    deliveries.Add(new DeliveryTermsMasterEntities
                    {
                        TermID = 0,
                        DeliveryTermDesc = "",
                        DeliveryTermCode = "",
                        Deactive = false
                    }); 
            }
            return deliveries;
        }

        public IEnumerable<DeliveryTermsMasterEntities> GetActiveTerms()
        {
            List<DeliveryTermsMasterEntities> deliveries = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("Deactive", 0, DbType.Int32);
                DataTable dtDeliveries = dbHelper.ExecuteDataTable(MasterQueries.GetAllDeliveryMaster, param, CommandType.StoredProcedure);
                deliveries = dtDeliveries.AsEnumerable().Select(row => new DeliveryTermsMasterEntities
                {
                    TermID = row.Field<int>("TermID"),
                    DeliveryTermDesc = row.Field<string>("DeliveryTermDesc").NullToString(),
                    DeliveryTermCode = row.Field<string>("DeliveryTermCode"),
                    Deactive = row.Field<Boolean>("Deactive")
                }).ToList();
                return deliveries;
            }
        }

        public DeliveryTermsMasterEntities GetDeliveryTermById(int termid)
        {
            DeliveryTermsMasterEntities deliveries = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("Termid", termid, DbType.Int32);
                DataTable dtdeliveries = dbHelper.ExecuteDataTable(MasterQueries.GetDeliveryById, param, CommandType.StoredProcedure);

                deliveries = dtdeliveries.AsEnumerable()
                            .Select(row => new DeliveryTermsMasterEntities
                            {
                                TermID = row.Field<int>("TermID"),
                                DeliveryTermDesc = row.Field<string>("DeliveryTermDesc").NullToString(),
                                DeliveryTermCode = row.Field<string>("DeliveryTermCode"),
                                Deactive = row.Field<Boolean>("Deactive")
                            }).FirstOrDefault();
            }
            return deliveries;      
        }

        public int CreateDeliveryTerm(DeliveryTermsMasterEntities entity)
        {          
                int iResult = 0;
                using (DBHelper dbHelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("Termid", entity.TermID, DbType.Int32, ParameterDirection.Output));
                    paramCollection.Add(new DBParameter("DeliveryTermDesc", entity.DeliveryTermDesc, DbType.String));
                    paramCollection.Add(new DBParameter("DeliveryTermCode", entity.DeliveryTermCode, DbType.String));
                    paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                    paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
                    paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                    paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                    paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
                    paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));
                    iResult = dbHelper.ExecuteNonQuery(MasterQueries.InsertDeliveryTermMaster, paramCollection, CommandType.StoredProcedure);
                }
                return iResult;
        }

        public bool UpdateDeliveryTerm(DeliveryTermsMasterEntities entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("TermID", entity.TermID, DbType.Int32));
                paramCollection.Add(new DBParameter("DeliveryTermDesc", entity.DeliveryTermDesc, DbType.String));
                paramCollection.Add(new DBParameter("DeliveryTermCode", entity.DeliveryTermCode, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedBy", entity.UpdatedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("UpdatedOn", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("UpdatedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacID", entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQuery(MasterQueries.UpdateDeliveryTermMasterById, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }
        public bool CheckDuplicateItem(string DeliveryTermCode)
        {
            bool bResult = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Type", "Delivery", DbType.String));
                paramCollection.Add(new DBParameter("Code", DeliveryTermCode, DbType.String));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));

                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(MasterQueries.CheckDuplicateItem, paramCollection, CommandType.StoredProcedure, "IsExist");
            }
            return bResult;
        }

        public bool CheckDuplicateupdate(string code, int ID)
        {
            bool bResult = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Type", "Delivery", DbType.String));
                paramCollection.Add(new DBParameter("ID", ID, DbType.Int32));
                paramCollection.Add(new DBParameter("Code", code, DbType.String));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));

                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(MasterQueries.CheckDuplicateupdate, paramCollection, CommandType.StoredProcedure, "IsExist");
            }
            return bResult;
        }
    }
}
