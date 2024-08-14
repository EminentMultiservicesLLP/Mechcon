using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPDataLayer.DataAccess;
using BISERPBusinessLayer.Entities.Masters;
using System.Data;
using BISERPBusinessLayer.Entities.Purchase;
using BISERPBusinessLayer.QueryCollection.Masters;
using BISERPCommon.Extensions;
using BISERPBusinessLayer.Repositories.Master.Interfaces;


namespace BISERPBusinessLayer.Repositories.Master.Classes
{
    public class OthersTermsMasterRepository:IOthersTermsMasterRepository
    {
        public IEnumerable<OthersTermsMasterEntities> GetAllOtherTerms()
        {
            List<OthersTermsMasterEntities> othersTerms = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtothers = dbHelper.ExecuteDataTable(MasterQueries.GetAllOthers, CommandType.StoredProcedure);
                othersTerms = dtothers.AsEnumerable().Select(row => new OthersTermsMasterEntities
                {
                    TermID = row.Field<int>("TermID"),
                    OthersTermDesc = row.Field<string>("OthersTermDesc").NullToString(),
                    OthersTermCode = row.Field<string>("OthersTermCode"),
                    Deactive = row.Field<Boolean>("Deactive")
                }).ToList();
                if (othersTerms == null || othersTerms.Count == 0)
                    othersTerms.Add(new OthersTermsMasterEntities
                    {
                        TermID = 0,
                        OthersTermDesc = "",
                        OthersTermCode = "",
                        Deactive = false
                    }); 
            }
            return othersTerms;
        }

        public IEnumerable<OthersTermsMasterEntities> GetActiveTerms()
        {
            List<OthersTermsMasterEntities> othersTerms = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("Deactive", 0, DbType.Int32);
                DataTable dtothers = dbHelper.ExecuteDataTable(MasterQueries.GetAllOthers, param, CommandType.StoredProcedure);
                othersTerms = dtothers.AsEnumerable().Select(row => new OthersTermsMasterEntities
                {
                    TermID = row.Field<int>("TermID"),
                    OthersTermDesc = row.Field<string>("OthersTermDesc").NullToString(),
                    OthersTermCode = row.Field<string>("OthersTermCode"),
                    Deactive = row.Field<Boolean>("Deactive")
                }).ToList();
                return othersTerms;
            }
        }

        public OthersTermsMasterEntities GetOtherTermsById(int termid)
        {
            OthersTermsMasterEntities othersTerms = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("Termid", termid, DbType.Int32);
                DataTable dtothers = dbHelper.ExecuteDataTable(MasterQueries.GetOthersById, param, CommandType.StoredProcedure);

                othersTerms = dtothers.AsEnumerable()
                            .Select(row => new OthersTermsMasterEntities
                            {
                                TermID = row.Field<int>("TermID"),
                                OthersTermDesc = row.Field<string>("OthersTermDesc").NullToString(),
                                OthersTermCode = row.Field<string>("OthersTermCode"),
                                Deactive = row.Field<Boolean>("Deactive")
                            }).FirstOrDefault();
            }

            return othersTerms; 
        }

        public int CreateOtherTerms(OthersTermsMasterEntities entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("TermID", entity.TermID, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("OthersTermCode", entity.OthersTermCode, DbType.String));
                paramCollection.Add(new DBParameter("OthersTermDesc", entity.OthersTermDesc, DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));
                //iResult = dbHelper.ExecuteNonQuery(MasterQueries.InsertOthersMaster, paramCollection, CommandType.StoredProcedure);
                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(MasterQueries.InsertOthersMaster, paramCollection, CommandType.StoredProcedure, "TermID");
            }
            return iResult; 
        }

        public bool UpdateOtherTerms(OthersTermsMasterEntities entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Termid", entity.TermID, DbType.Int32));
                paramCollection.Add(new DBParameter("OthersTermCode", entity.OthersTermCode, DbType.String));
                paramCollection.Add(new DBParameter("OthersTermDesc", entity.OthersTermDesc, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedBy", entity.UpdatedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("UpdatedOn", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("UpdatedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacID", entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQuery(MasterQueries.UpdateOthersMasterById, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }
        public bool CheckDuplicateItem(string OthersTermCode)
        {
            bool bResult = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Type", "Other", DbType.String));
                paramCollection.Add(new DBParameter("Code", OthersTermCode, DbType.String));
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
                paramCollection.Add(new DBParameter("Type", "Other", DbType.String));
                paramCollection.Add(new DBParameter("ID", ID, DbType.Int32));
                paramCollection.Add(new DBParameter("Code", code, DbType.String));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));

                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(MasterQueries.CheckDuplicateupdate, paramCollection, CommandType.StoredProcedure, "IsExist");
            }
            return bResult;
        }

        public IEnumerable<POPriceBasisEntity> AllBasisTerm()
        {
            List<POPriceBasisEntity> othersTerms = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtothers = dbHelper.ExecuteDataTable(MasterQueries.AllBasisTerm, CommandType.StoredProcedure);
                othersTerms = dtothers.AsEnumerable().Select(row => new POPriceBasisEntity
                {
                    BasisId = row.Field<int>("BasisID"),
                    BasisDesc = row.Field<string>("BasisDesc").NullToString(),
                    BasisCode = row.Field<string>("BasisCode")
                   
                }).ToList();
               
            }
            return othersTerms;
        }
        public IEnumerable<POInspectionModel> AllInspectionTerm()
        {
            List<POInspectionModel> othersTerms = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtothers = dbHelper.ExecuteDataTable(MasterQueries.AllInspectionTerm, CommandType.StoredProcedure);
                othersTerms = dtothers.AsEnumerable().Select(row => new POInspectionModel
                {
                    InspectionId = row.Field<int>("InspectionId"),
                    InspectionCode = row.Field<string>("InspectionCode"),
                    InspectionDesc = row.Field<string>("InspectionDesc").NullToString()

                }).ToList();

            }
            return othersTerms;
        }

        public int CreateInspectionmatser(InspectionMasterEntity entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("InspectionId", entity.InspectionId, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("InspectionCode", entity.InspectionCode, DbType.String));
                paramCollection.Add(new DBParameter("InspectionDesc", entity.InspectionDesc, DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));
                //iResult = dbHelper.ExecuteNonQuery(MasterQueries.InsertOthersMaster, paramCollection, CommandType.StoredProcedure);
                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(MasterQueries.CreateInspectionmatser, paramCollection, CommandType.StoredProcedure, "InspectionId");
            }
            return iResult; 
        }

        public bool UpdateInspectionmatser(InspectionMasterEntity entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("InspectionId", entity.InspectionId, DbType.Int32));
                paramCollection.Add(new DBParameter("InspectionCode", entity.InspectionCode, DbType.String));
                paramCollection.Add(new DBParameter("InspectionDesc", entity.InspectionDesc, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedBy", entity.UpdatedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("UpdatedOn", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("UpdatedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacID", entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQuery(MasterQueries.UpdateInspectionmatser, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            return false;
        }

        public bool CheckDuplicateInspectionItem(string inspectionCode)
        {
            bool bResult = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Type", "inspection", DbType.String));
                paramCollection.Add(new DBParameter("Code", inspectionCode, DbType.String));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));

                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(MasterQueries.CheckDuplicateItem, paramCollection, CommandType.StoredProcedure, "IsExist");
            }
            return bResult;
        }

        public bool CheckDuplicateInspectionupdate(string inspectionCode, int inspectionId)
        {
            bool bResult = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Type", "inspection", DbType.String));
                paramCollection.Add(new DBParameter("ID", inspectionId, DbType.Int32));
                paramCollection.Add(new DBParameter("Code", inspectionCode, DbType.String));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));

                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(MasterQueries.CheckDuplicateupdate, paramCollection, CommandType.StoredProcedure, "IsExist");
            }
            return bResult;
        }

        public int CreateBasisMaster(BasisMasterEntity entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("BasisID", entity.BasisID, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("BasisCode", entity.BasisCode, DbType.String));
                paramCollection.Add(new DBParameter("BasisDesc", entity.BasisDesc, DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(MasterQueries.CreateBasisMaster, paramCollection, CommandType.StoredProcedure, "BasisID");
            }
            return iResult; 
        }

        public bool UpdateBasisMaster(BasisMasterEntity entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("BasisID", entity.BasisID, DbType.Int32));
                paramCollection.Add(new DBParameter("BasisCode", entity.BasisCode, DbType.String));
                paramCollection.Add(new DBParameter("BasisDesc", entity.BasisDesc, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedBy", entity.UpdatedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("UpdatedOn", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("UpdatedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacID", entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQuery(MasterQueries.UpdateBasisMaster, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            return false;
        }

        public bool CheckDuplicateBasisItem(string basisCode)
        {
            bool bResult = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Type", "Basis", DbType.String));
                paramCollection.Add(new DBParameter("Code", basisCode, DbType.String));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));

                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(MasterQueries.CheckDuplicateItem, paramCollection, CommandType.StoredProcedure, "IsExist");
            }
            return bResult;
        }

        public bool CheckDuplicateBasisMasterupdate(string basisCode, int basisId)
        {
            bool bResult = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Type", "Basis", DbType.String));
                paramCollection.Add(new DBParameter("ID", basisId, DbType.Int32));
                paramCollection.Add(new DBParameter("Code", basisCode, DbType.String));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));

                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(MasterQueries.CheckDuplicateupdate, paramCollection, CommandType.StoredProcedure, "IsExist");
            }
            return bResult;
        }
    }
}
