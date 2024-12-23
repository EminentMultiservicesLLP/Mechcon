using BISERPBusinessLayer.Entities.Masters;
using BISERPBusinessLayer.QueryCollection.Masters;
using BISERPBusinessLayer.Repositories.Master.Interfaces;
using BISERPCommon.Extensions;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Master.Classes
{
    public class ProductMasterRepository : IProductMasterRepository
    {
        public IEnumerable<ProductMasterEntities> GetAllProduct()
        {
            List<ProductMasterEntities> projectTC = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtprojectTC = dbHelper.ExecuteDataTable(MasterQueries.GetAllProduct, CommandType.StoredProcedure);
                projectTC = dtprojectTC.AsEnumerable().Select(row => new ProductMasterEntities
                {
                    ProductID = row.Field<int>("ProductID"),
                    ProductDesc = row.Field<string>("ProductDesc").NullToString(),
                    ProductName = row.Field<string>("ProductName"),
                    Deactive = row.Field<Boolean>("Deactive")
                }).ToList();
                if (projectTC == null || projectTC.Count == 0)
                    projectTC.Add(new ProductMasterEntities
                    {
                        ProductID = 0,
                        ProductDesc = "",
                        ProductName = "",
                        Deactive = false
                    });
            }
            return projectTC;
        }

        public int CreateProduct(ProductMasterEntities entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ProductID", entity.ProductID, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("ProductName", entity.ProductName, DbType.String));
                paramCollection.Add(new DBParameter("ProductDesc", entity.ProductDesc, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(MasterQueries.InsertProductMaster, paramCollection, CommandType.StoredProcedure, "ProductID");
            }
            return iResult;
        }

        public bool UpdateProduct(ProductMasterEntities entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ProductID", entity.ProductID, DbType.Int32));
                paramCollection.Add(new DBParameter("ProductName", entity.ProductName, DbType.String));
                paramCollection.Add(new DBParameter("ProductDesc", entity.ProductDesc, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQuery(MasterQueries.UpdateProductMaster, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }

        public bool CheckDuplicateItem(string ProductName)
        {
            bool bResult = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Type", "Product", DbType.String));
                paramCollection.Add(new DBParameter("Code", ProductName, DbType.String));
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
                paramCollection.Add(new DBParameter("Type", "Product", DbType.String));
                paramCollection.Add(new DBParameter("ID", ID, DbType.Int32));
                paramCollection.Add(new DBParameter("Code", code, DbType.String));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));

                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(MasterQueries.CheckDuplicateupdate, paramCollection, CommandType.StoredProcedure, "IsExist");
            }
            return bResult;
        }
    }
}