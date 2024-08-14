using BISERPBusinessLayer.Entities.Common;
using BISERPBusinessLayer.QueryCollection.Common;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Common
{

    enum FlashQueriesType
    {
        StoreMaterialIndent = 0,
        StoreMaterialissue =1,
        StorePurchaseIndent =2,
        StoreGRN = 3,
        StorePurchaseReturn = 4,
        StoreMatertialreturn = 5,
        StoreExpiry = 6
    }

    public class FlashDetailRepository: IFlashDetailRepository
    {
        public IEnumerable<FlashEntity> GetFlashDetails(int typeId, int userId)
        {
            var sql = default(string);
            switch(typeId)
            {
                case 1:
                    sql = FlashDetailsQueries.GetFlashMatertialIndentDetail;
                    break;
                default:
                    break;
            }

            List<FlashEntity> FlashDetails = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("UserId", userId, DbType.Int32));

                DataTable dtFlash = dbHelper.ExecuteDataTable(sql, paramCollection, CommandType.StoredProcedure);
                FlashDetails = dtFlash.AsEnumerable()
                            .Select(row => new FlashEntity
                            {
                                title = row.Field<string>("TITLE"),
                                body = row.Field<string>("BODY")
                            }).ToList();
                if (FlashDetails == null || !FlashDetails.Any())
                    FlashDetails.Add(new FlashEntity
                    {
                        title = "",
                        body = ""
                    });
            }
            return FlashDetails;
        }
    }
}
