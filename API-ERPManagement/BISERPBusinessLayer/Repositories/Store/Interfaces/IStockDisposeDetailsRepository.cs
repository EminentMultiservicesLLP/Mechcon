using BISERPBusinessLayer.Entities.Store;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BISERPBusinessLayer.Repositories.Store.Interfaces
{
    public  interface IStockDisposeDetailsRepository
    {
        int StockDisposeDetails(StockDisposeEntity mainentity, StockDisposeDtEntity entity, DBHelper dbHelper);
     
    }
}
