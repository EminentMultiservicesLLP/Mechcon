using BISERPBusinessLayer.Entities.Purchase;
using BISERPBusinessLayer.QueryCollection.Purchase;
using BISERPBusinessLayer.Repositories.Purchase.Interfaces;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Purchase.Classes
{
    public class RequestForQuoteDetailRepository: IRequestForQuoteDetailRepository
    {
        public int CreateRequestForQuoteDetails(int IndentId, RequestForQuoteDetailEntities entity, DBHelper dbhelper)
        {
            int iResult = 0;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("IndentDetailId", entity.IndentDetailId, DbType.Int32, ParameterDirection.Output));
            paramCollection.Add(new DBParameter("IndentId", IndentId, DbType.Int32));
            paramCollection.Add(new DBParameter("ItemId", entity.ItemId, DbType.Int32));
            paramCollection.Add(new DBParameter("ItemQty", entity.ItemQty, DbType.Double));
            paramCollection.Add(new DBParameter("ItemRate", entity.ItemRate, DbType.Double));
            paramCollection.Add(new DBParameter("EstimatedCost", entity.EstimatedCost, DbType.Double));
            paramCollection.Add(new DBParameter("SalesTax", entity.SalesTax, DbType.Double));
            paramCollection.Add(new DBParameter("ExciseTax", entity.ExciseTax, DbType.Double));
            paramCollection.Add(new DBParameter("Escalated", entity.Escalated, DbType.Double));
            paramCollection.Add(new DBParameter("LandingRate", entity.LandingRate, DbType.Double));
            paramCollection.Add(new DBParameter("DeliveryStartDate", entity.DeliveryStartDate, DbType.DateTime));
            paramCollection.Add(new DBParameter("DeliveryEnddate", entity.DeliveryEnddate, DbType.DateTime));
            paramCollection.Add(new DBParameter("AuthorisedQty", entity.AuthorisedQty, DbType.Double));
            paramCollection.Add(new DBParameter("packsizeid", entity.packsizeid, DbType.Int32));
            paramCollection.Add(new DBParameter("freeqty", entity.freeqty, DbType.Double));
            paramCollection.Add(new DBParameter("Discount", entity.Discount, DbType.Double));
            paramCollection.Add(new DBParameter("Tax", entity.Tax, DbType.Double));
            paramCollection.Add(new DBParameter("TaxAmount", entity.TaxAmount, DbType.Double));
            paramCollection.Add(new DBParameter("VATOn", entity.VATOn, DbType.String));
            paramCollection.Add(new DBParameter("VAT", entity.VAT, DbType.String));
            paramCollection.Add(new DBParameter("MRP", entity.MRP, DbType.Double));
            paramCollection.Add(new DBParameter("IssueQty", entity.IssueQty, DbType.Int32));
            paramCollection.Add(new DBParameter("Consumeqty", entity.Consumeqty, DbType.Int32));
            paramCollection.Add(new DBParameter("BrandID", entity.BrandID, DbType.Int32));
            paramCollection.Add(new DBParameter("POStatus", entity.POStatus, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
            paramCollection.Add(new DBParameter("DescriptiveName", entity.DescriptiveName, DbType.String));
            paramCollection.Add(new DBParameter("IndentRemark", entity.IndentRemark, DbType.String));
            paramCollection.Add(new DBParameter("RequiredDate", entity.ItemsRequiredDate, DbType.String));

            iResult = dbhelper.ExecuteNonQueryForOutParameter<int>(PurchaseQueries.RequestForQuoteIndentDtl, paramCollection, CommandType.StoredProcedure, "IndentDetailId");
            return iResult;
        }

        public List<RequestForQuoteDetailEntities> GetRequestForQuoteDetailById(int IndentId)
        {
            List<RequestForQuoteDetailEntities> RFQDetails = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("IndentId", IndentId, DbType.Int32);
                DataTable dtPIndentDetail = dbHelper.ExecuteDataTable(PurchaseQueries.GetRequestForQuoteDetailById, param, CommandType.StoredProcedure);
                RFQDetails = dtPIndentDetail.AsEnumerable()
                            .Select(row => new RequestForQuoteDetailEntities
                            {
                                IndentDetailId = row.Field<int>("IndentDetailId"),
                                ItemId = row.Field<int>("ItemId"),
                                ItemRate = row.Field<double?>("ItemRate"),
                                ItemQty = row.Field<double?>("ItemQty"),
                                EstimatedCost = row.Field<double?>("EstimatedCost"),
                                SalesTax = row.Field<double?>("SalesTax"),
                                ExciseTax = row.Field<double?>("ExciseTax"),
                                Escalated = row.Field<double?>("Escalated"),
                                LandingRate = row.Field<double?>("LandingRate"),
                                DeliveryStartDate = row.Field<DateTime?>("DeliveryStartDate"),
                                DeliveryEnddate = row.Field<DateTime?>("DeliveryEnddate"),
                                AuthorisedQty = row.Field<double?>("AuthorisedQty"),
                                packsizeid = row.Field<int>("packsizeid"),
                                freeqty = row.Field<double?>("freeqty"),
                                Discount = row.Field<double?>("Discount"),
                                Tax = row.Field<double?>("Tax"),
                                TaxAmount = row.Field<double?>("TaxAmount"),
                                VATOn = row.Field<string>("VATOn"),
                                VAT = row.Field<string>("VAT"),
                                MRP = row.Field<double?>("MRP"),
                                IssueQty = row.Field<double?>("IssueQty"),
                                Consumeqty = row.Field<double?>("Consumeqty"),
                                BrandID = row.Field<int>("BrandID"),
                                POStatus = row.Field<string>("POStatus"),
                                ItemCode = row.Field<string>("ItemCode"),
                                ItemName = row.Field<string>("ItemName"),
                                TaxRate = row.Field<double?>("TaxRate"),
                                UnitName = row.Field<string>("UnitName"),
                                PackSize = row.Field<string>("PackSize"),
                                PackSizeQuantity = row.Field<decimal?>("PackSizeQuantity"),
                                CurrentQty = row.Field<double?>("CurrentQty"),
                                DescriptiveName = row.Field<string>("DescriptiveName"),
                                IndentRemark = row.Field<string>("IndentRemark"),
                                HSNCode = row.Field<string>("HSNCode"),
                                ItemsRequiredDate = row.Field<string>("RequiredDate"),
                                PendingQty = row.Field<double?>("PendingQty"),
                                Make = row.Field<string>("Make"),
                                MaterialOfConstruct = row.Field<string>("MaterialOfConstruct"),
                            }).ToList();
            }
            return RFQDetails;
        }

        public bool UpdateRequestForQuoteAuthQty(RequestForQuoteEntities mainentity, RequestForQuoteDetailEntities entity, DBHelper dbhelper)
        {
            int iResult = 0;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("IndentDetailId", entity.IndentDetailId, DbType.Int32));
            paramCollection.Add(new DBParameter("IndentId", mainentity.IndentId, DbType.Int32));
            paramCollection.Add(new DBParameter("ItemId", entity.ItemId, DbType.Int32));
            paramCollection.Add(new DBParameter("ItemQty", entity.ItemQty, DbType.Double));
            paramCollection.Add(new DBParameter("ItemRate", entity.ItemRate, DbType.Double));
            paramCollection.Add(new DBParameter("EstimatedCost", entity.EstimatedCost, DbType.Double));
            paramCollection.Add(new DBParameter("SalesTax", entity.SalesTax, DbType.Double));
            paramCollection.Add(new DBParameter("ExciseTax", entity.ExciseTax, DbType.Double));
            paramCollection.Add(new DBParameter("Escalated", entity.Escalated, DbType.Double));
            paramCollection.Add(new DBParameter("LandingRate", entity.LandingRate, DbType.Double));
            paramCollection.Add(new DBParameter("DeliveryStartDate", entity.DeliveryStartDate, DbType.DateTime));
            paramCollection.Add(new DBParameter("DeliveryEnddate", entity.DeliveryEnddate, DbType.DateTime));
            paramCollection.Add(new DBParameter("AuthorisedQty", entity.AuthorisedQty, DbType.Double));
            paramCollection.Add(new DBParameter("packsizeid", entity.packsizeid, DbType.Int32));
            paramCollection.Add(new DBParameter("freeqty", entity.freeqty, DbType.Double));
            paramCollection.Add(new DBParameter("Discount", entity.Discount, DbType.Double));
            paramCollection.Add(new DBParameter("Tax", entity.Tax, DbType.Double));
            paramCollection.Add(new DBParameter("TaxAmount", entity.TaxAmount, DbType.Double));
            paramCollection.Add(new DBParameter("VATOn", entity.VATOn, DbType.String));
            paramCollection.Add(new DBParameter("VAT", entity.VAT, DbType.String));
            paramCollection.Add(new DBParameter("MRP", entity.MRP, DbType.Double));
            paramCollection.Add(new DBParameter("IssueQty", entity.IssueQty, DbType.Int32));
            paramCollection.Add(new DBParameter("Consumeqty", entity.Consumeqty, DbType.Int32));
            paramCollection.Add(new DBParameter("BrandID", entity.BrandID, DbType.Int32));
            paramCollection.Add(new DBParameter("POStatus", entity.POStatus, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
            paramCollection.Add(new DBParameter("AuthorisedBy", mainentity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("AuthorisedOn", mainentity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("DescriptiveName", entity.DescriptiveName, DbType.String));
            paramCollection.Add(new DBParameter("IndentRemark", entity.IndentRemark, DbType.String));
            paramCollection.Add(new DBParameter("RequiredDate", entity.ItemsRequiredDate, DbType.String));
            paramCollection.Add(new DBParameter("AuthorizationStatusId", mainentity.AuthorizationStatusId, DbType.Int32));

            iResult = dbhelper.ExecuteNonQuery(PurchaseQueries.UpdateRequestForQuoteAuthQty, paramCollection, CommandType.StoredProcedure);
            if (iResult > 0)
                return true;
            else
                return false;
        }

    }
}
