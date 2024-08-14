using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPBusinessLayer.Entities.Masters;
using BISERPDataLayer.DataAccess;
using System.Data;
using BISERPBusinessLayer.Repositories.Master.Interfaces;
using BISERPBusinessLayer.QueryCollection.Masters;

namespace BISERPBusinessLayer.Repositories.Master.Classes
{
    public class BranchMasterRepository:IBranchMasterRepository
    {
      public IEnumerable<BranchMasterEntities> GetAllBranches()
      {
          List<BranchMasterEntities> branches = null;
          using (DBHelper dbHelper = new DBHelper())
          {
              DataTable dtBranches = dbHelper.ExecuteDataTable(MasterQueries.GetAllBranches, CommandType.StoredProcedure);
              branches = dtBranches.AsEnumerable()
                          .Select(row => new BranchMasterEntities
                          {
                              BranchID = row.Field<int>("BranchID"),
                              BranchName = row.Field<string>("BranchName"),
                              BranchCode = row.Field<string>("BranchCode"),
                              Address = row.Field<string>("Address"),
                              Pin = row.Field<string>("Pin"),
                              ContactPerson1 = row.Field<string>("ContactPerson1"),
                              ContactPerson2 = row.Field<string>("ContactPerson2"),
                              ContactPerson3 = row.Field<string>("ContactPerson3"),
                              CityID = row.Field<int?>("CityID"),
                              Phone1 = row.Field<string>("Phone1"),
                              Phone2 = row.Field<string>("Phone2"),
                              Fax = row.Field<string>("Fax"),
                              Email = row.Field<string>("Email"),
                              Website = row.Field<string>("Website"),
                              Society = row.Field<string>("Society"),
                              Landmark = row.Field<string>("Landmark"),
                              StateID = row.Field<int?>("StateID"),
                              CountryId = row.Field<int?>("CountryId"),
                              VillageId = row.Field<int?>("VillageId"),
                              Deactive = row.Field<bool>("Deactive")
                          }).ToList();
              if (branches == null || branches.Count == 0)
                  branches.Add(new BranchMasterEntities
                  {
                      BranchID = 0,
                      BranchName = "",
                      BranchCode = "",
                      Deactive = false
                  });
          }
          return branches;
      }
      public int CreateBranch(BranchMasterEntities Entity)
      {
          int iResult = 0;
          using (DBHelper dbHelper = new DBHelper())
          {
              DBParameterCollection paramCollection = new DBParameterCollection();
              paramCollection.Add(new DBParameter("BranchID", Entity.BranchID, DbType.Int32, ParameterDirection.Output));
              paramCollection.Add(new DBParameter("BranchCode", Entity.BranchCode, DbType.String));
              paramCollection.Add(new DBParameter("BranchName", Entity.BranchName, DbType.String));
              paramCollection.Add(new DBParameter("Address", Entity.Address, DbType.String));
              paramCollection.Add(new DBParameter("InsertedBy", Entity.InsertedBy, DbType.Int32));
              paramCollection.Add(new DBParameter("InsertedMacName", Entity.InsertedMacName, DbType.String));
              paramCollection.Add(new DBParameter("InsertedIPAddress", Entity.InsertedIPAddress, DbType.String));
              paramCollection.Add(new DBParameter("InsertedON", Entity.InsertedON, DbType.DateTime));
              paramCollection.Add(new DBParameter("InsertedMacID", Entity.InsertedMacID, DbType.String));
              paramCollection.Add(new DBParameter("CityId", Entity.CityID, DbType.Int32));
              paramCollection.Add(new DBParameter("Pin", Entity.Pin, DbType.String));
              paramCollection.Add(new DBParameter("ContactPerson1", Entity.ContactPerson1, DbType.String));
              paramCollection.Add(new DBParameter("ContactPerson2", Entity.ContactPerson2, DbType.String));
              paramCollection.Add(new DBParameter("ContactPerson3", Entity.ContactPerson3, DbType.String));
              paramCollection.Add(new DBParameter("Phone1", Entity.Phone1, DbType.String));
              paramCollection.Add(new DBParameter("Phone2", Entity.Phone2, DbType.String));
              paramCollection.Add(new DBParameter("Fax", Entity.Fax, DbType.String));
              paramCollection.Add(new DBParameter("Email", Entity.Email, DbType.String));
              paramCollection.Add(new DBParameter("Website", Entity.Website, DbType.String));
              paramCollection.Add(new DBParameter("Society", Entity.Society, DbType.String));
              paramCollection.Add(new DBParameter("Landmark", Entity.Landmark, DbType.String));
              paramCollection.Add(new DBParameter("StateId", Entity.StateID, DbType.Int32));
              paramCollection.Add(new DBParameter("CountryId", Entity.CountryId, DbType.Int32));
              paramCollection.Add(new DBParameter("VillageId", Entity.VillageId, DbType.Int32));           
              paramCollection.Add(new DBParameter("Deactive", Entity.Deactive, DbType.Boolean));
              iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(MasterQueries.Ins_MST_Branch, paramCollection, CommandType.StoredProcedure, "BranchID");

          }
          return iResult;
      }
      public bool UpdateBranch(BranchMasterEntities Entity)
      {
          int iResult = 0;
          using (DBHelper dbHelper = new DBHelper())
          {

              DBParameterCollection paramCollection = new DBParameterCollection();
              paramCollection.Add(new DBParameter("BranchID", Entity.BranchID, DbType.Int32));
              paramCollection.Add(new DBParameter("BranchCode", Entity.BranchCode, DbType.String));
              paramCollection.Add(new DBParameter("BranchName", Entity.BranchName, DbType.String));
              paramCollection.Add(new DBParameter("Address", Entity.Address, DbType.String));
              paramCollection.Add(new DBParameter("UpdatedBy", Entity.UpdatedBy, DbType.Int32));
              paramCollection.Add(new DBParameter("UpdatedMacName", Entity.UpdatedMacName, DbType.String));
              paramCollection.Add(new DBParameter("UpdatedIPAddress", Entity.UpdatedIPAddress, DbType.String));
              paramCollection.Add(new DBParameter("UpdatedOn", Entity.UpdatedOn, DbType.DateTime));
              paramCollection.Add(new DBParameter("CityId", Entity.CityID, DbType.Int32));
              paramCollection.Add(new DBParameter("Pin", Entity.Pin, DbType.String));
              paramCollection.Add(new DBParameter("ContactPerson1", Entity.ContactPerson1, DbType.String));
              paramCollection.Add(new DBParameter("ContactPerson2", Entity.ContactPerson2, DbType.String));
              paramCollection.Add(new DBParameter("ContactPerson3", Entity.ContactPerson3, DbType.String));
              paramCollection.Add(new DBParameter("Phone1", Entity.Phone1, DbType.String));
              paramCollection.Add(new DBParameter("Phone2", Entity.Phone2, DbType.String));
              paramCollection.Add(new DBParameter("Fax", Entity.Fax, DbType.String));
              paramCollection.Add(new DBParameter("Email", Entity.Email, DbType.String));
              paramCollection.Add(new DBParameter("Website", Entity.Website, DbType.String));
              paramCollection.Add(new DBParameter("Society", Entity.Society, DbType.String));
              paramCollection.Add(new DBParameter("Landmark", Entity.Landmark, DbType.String));
              paramCollection.Add(new DBParameter("StateId", Entity.StateID, DbType.Int32));
              paramCollection.Add(new DBParameter("CountryId", Entity.CountryId, DbType.Int32));
              paramCollection.Add(new DBParameter("VillageId", Entity.VillageId, DbType.Int32));
              paramCollection.Add(new DBParameter("Deactive", Entity.Deactive, DbType.Boolean));
              iResult = dbHelper.ExecuteNonQuery(MasterQueries.Ins_MST_Branch, paramCollection, CommandType.StoredProcedure);

          }
          if (iResult > 0)
              return true;
          else
              return false;
      }
      public IEnumerable<BranchMasterEntities> GetActiveBranches(int? UserId)
      {
          List<BranchMasterEntities> branches = null;
          using (DBHelper dbHelper = new DBHelper())
          {
              DBParameterCollection paramCollection = new DBParameterCollection();
              paramCollection.Add(new DBParameter("UserId", UserId, DbType.Int32));
              DataTable dtManufacturer = dbHelper.ExecuteDataTable(MasterQueries.GetUserBranches, paramCollection, CommandType.StoredProcedure);
              branches = dtManufacturer.AsEnumerable()
                          .Select(row => new BranchMasterEntities
                          {
                              BranchID = row.Field<int>("BranchID"),
                              BranchName = row.Field<string>("BranchName"),
                              BranchCode = row.Field<string>("BranchCode"),
                              Address = row.Field<string>("Address"),
                              Pin = row.Field<string>("Pin"),
                              ContactPerson1 = row.Field<string>("ContactPerson1"),
                              ContactPerson2 = row.Field<string>("ContactPerson2"),
                              ContactPerson3 = row.Field<string>("ContactPerson3"),
                              CityID = row.Field<int?>("CityID"),
                              Phone1 = row.Field<string>("Phone1"),
                              Phone2 = row.Field<string>("Phone2"),
                              Fax = row.Field<string>("Fax"),
                              Email = row.Field<string>("Email"),
                              Website = row.Field<string>("Website"),
                              Society = row.Field<string>("Society"),
                              Landmark = row.Field<string>("Landmark"),
                              StateID = row.Field<int?>("StateID"),
                              CountryId = row.Field<int?>("CountryId"),
                              VillageId = row.Field<int?>("VillageId"),
                              Deactive = row.Field<bool>("Deactive")
                          }).ToList();
              if (branches == null || branches.Count == 0)
                  branches.Add(new BranchMasterEntities
                  {
                      BranchID = 0,
                      BranchName = "",
                      BranchCode = "",
                      Deactive = false
                  });
          }
          return branches;
      }
      public bool CheckDuplicateItem(string BranchCode)
      {
          bool bResult = false;
          using (DBHelper dbHelper = new DBHelper())
          {
              DBParameterCollection paramCollection = new DBParameterCollection();
              paramCollection.Add(new DBParameter("Type", "Branch", DbType.String));
              paramCollection.Add(new DBParameter("Code", BranchCode, DbType.String));
              paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));

              bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(MasterQueries.CheckDuplicateItem, paramCollection, CommandType.StoredProcedure, "IsExist");
          }
          return bResult;
      }
      public bool CheckDuplicateupdate(string Code, int Id)
      {
          bool bResult = false;
          using (DBHelper dbHelper = new DBHelper())
          {
              DBParameterCollection paramCollection = new DBParameterCollection();
              paramCollection.Add(new DBParameter("Type", "Branch", DbType.String));
              paramCollection.Add(new DBParameter("ID", Id, DbType.Int32));
              paramCollection.Add(new DBParameter("Code", Code, DbType.String));
              paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));

              bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(MasterQueries.CheckDuplicateupdate, paramCollection, CommandType.StoredProcedure, "IsExist");
          }
          return bResult;
      }
    }
}
