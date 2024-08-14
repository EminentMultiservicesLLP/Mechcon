using BISERPBusinessLayer.Entities.Masters;
using BISERPBusinessLayer.QueryCollection.Masters;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using BISERPBusinessLayer.Mapper.Master;
using AutoMapper;
using System.Data.SqlClient;
using BISERPBusinessLayer.Repositories.Master;
using BISERPBusinessLayer.Repositories.Master.Interfaces;

namespace BISERPBusinessLayer.Repositories.Master.Classes
{
   
  
       public class ItemGroupMasterRepository : IItemGroupMasterRepository
       {
           public ItemGroupMasterEntities GetItemGroupById(int itemgroupId)
           {
               ItemGroupMasterEntities itemgroup = null;
               using (DBHelper dbHelper = new DBHelper())
               {
                   DBParameter param = new DBParameter("itemGroup", itemgroupId, DbType.Int32);
                   DataTable dtitemgroup = dbHelper.ExecuteDataTable(MasterQueries.GetItemGroupMasterById, param, CommandType.StoredProcedure);

                   itemgroup = dtitemgroup.AsEnumerable()
                               .Select(row => new ItemGroupMasterEntities
                               {
                                   ID = row.Field<int>("ID"),
                                   Code = row.Field<string>("Code"),
                                   Name = row.Field<string>("Name"),
                                   //Quantity = row.Field<string>("Quantity")
                               }).FirstOrDefault();
               }
               //var unit = _unitOfWork.UnitMasterRepository.GetByID(unitId);
               //if (unit != null)
               //{
               //    Mapper.CreateMap<INV_MST_Unit, UnitMasterEntities>();
               //    var unitModel = Mapper.Map<INV_MST_Unit, UnitMasterEntities>(unit);
               //    return unitModel;
               //}
               return itemgroup;
           }

           public IEnumerable<ItemGroupMasterEntities> GetAllItemGroup()
           {
               List<ItemGroupMasterEntities> itemgroup = null;
               using (DBHelper dbHelper = new DBHelper())
               {
                   DataTable dtitemgroup = dbHelper.ExecuteDataTable(MasterQueries.GetAllItemGroupMaster, CommandType.StoredProcedure);
                   itemgroup = dtitemgroup.AsEnumerable()
                               .Select(row => new ItemGroupMasterEntities
                               {
                                   ID = row.Field<int>("ID"),
                                   Code = row.Field<string>("Code"),
                                   Name = row.Field<string>("Name"),
                                  // Quantity=row.Field<string>("Quantity")
                               }).ToList();

                   if (itemgroup == null || itemgroup.Count == 0)
                       itemgroup.Add(new ItemGroupMasterEntities
                       {
                           ID = 0,
                           Code = "",
                           Name = "",
                         //  Quantity=""
                       });
               }
              
               return itemgroup;
           }
           public IEnumerable<ItemGroupMasterEntities> GetActiveItemGroup()
           {
               List<ItemGroupMasterEntities> itemgroup = null;
               using (DBHelper dbHelper = new DBHelper())
               {
                   DBParameter param = new DBParameter("Deactive", 0, DbType.Int32);
                   DataTable dtManufacturer = dbHelper.ExecuteDataTable(MasterQueries.GetAllActiveItemGroup, param, CommandType.StoredProcedure);
                   itemgroup = dtManufacturer.AsEnumerable()
                               .Select(row => new ItemGroupMasterEntities
                               {
                                   ID = row.Field<int>("ID"),
                                   Code = row.Field<string>("Code"),
                                   Name = row.Field<string>("Name"),

                               }).ToList();
               }
               return itemgroup;
           }
           public bool CreateItemGroup(ItemGroupMasterEntities itemgroupEntity)
           {
               int iResult = 0;
               using (DBHelper dbHelper = new DBHelper())
               {
                   DBParameterCollection paramCollection = new DBParameterCollection();
                   paramCollection.Add(new DBParameter("ItemCode", itemgroupEntity.Code, DbType.String));
                   paramCollection.Add(new DBParameter("ItemName", itemgroupEntity.Name, DbType.String));
                   paramCollection.Add(new DBParameter("CreatedBy", itemgroupEntity.InsertedBy, DbType.String));
                   paramCollection.Add(new DBParameter("CreatedIP", itemgroupEntity.InsertedIPAddress, DbType.String));
                   paramCollection.Add(new DBParameter("CreatedMacName", itemgroupEntity.InsertedMacName, DbType.String));
                   paramCollection.Add(new DBParameter("CreatedMacId", itemgroupEntity.InsertedMacID, DbType.String));


                   iResult = dbHelper.ExecuteNonQuery(MasterQueries.InsertItemGroupMaster, paramCollection, CommandType.StoredProcedure);
               }
               if (iResult > 0)
                   return true;
               else
                   return false;
           }

           public bool UpdateItemGroup(ItemGroupMasterEntities itemgroupEntity)
           {
               int iResult = 0;
               using (DBHelper dbHelper = new DBHelper())
               {
                   DBParameterCollection paramCollection = new DBParameterCollection();
                   paramCollection.Add(new DBParameter("ItemID", itemgroupEntity.ID, DbType.Int32));
                   paramCollection.Add(new DBParameter("ItemCode", itemgroupEntity.Code, DbType.String));
                   paramCollection.Add(new DBParameter("ItemName", itemgroupEntity.Name, DbType.String));
                   paramCollection.Add(new DBParameter("UpdatedBy", itemgroupEntity.UpdatedBy, DbType.String));
                   paramCollection.Add(new DBParameter("UpdatedIP", itemgroupEntity.UpdatedIPAddress, DbType.String));


                   iResult = dbHelper.ExecuteNonQuery(MasterQueries.UpadateItemGroupMasterById, paramCollection, CommandType.StoredProcedure);
               }
               if (iResult > 0)
                   return true;
               else
                   return false;
           }

           public bool DeleteItemGroup(ItemGroupMasterEntities itemgroupEntity)
           {
               int iResult = 0;
               using (DBHelper dbHelper = new DBHelper())
               {
                   DBParameterCollection paramCollection = new DBParameterCollection();
                   paramCollection.Add(new DBParameter("ItemID", itemgroupEntity.ID, DbType.Int32));
                   paramCollection.Add(new DBParameter("UpdatedBy", itemgroupEntity.UpdatedBy, DbType.String));
                   paramCollection.Add(new DBParameter("UpdatedIP", itemgroupEntity.UpdatedIPAddress, DbType.String));

                   iResult = dbHelper.ExecuteNonQuery(MasterQueries.DeleteItemGroupMasterById, paramCollection, CommandType.StoredProcedure);
               }
               if (iResult > 0)
                   return true;
               else
                   return false;
           }
       }
    }

