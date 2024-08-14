using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Store
{
    public class OpeningBalanceEntity
    {
       
        public int ID { get; set; }
        public int Storeid { get; set; }     
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string BatchName { get; set; }      
        public Nullable<double> OpeningBal { get; set; }      
        public Nullable<double> CurrentBal { get; set; }       
        public Nullable<System.DateTime> ExpiryDate { get; set; }      
        public Nullable<double> Rate { get; set; }      
        public Nullable<double> MRP { get; set; }
        public Nullable<double> PackSizeMrp { get; set; }
        public Nullable<double> PackLendingRate { get; set; }
        public Nullable<double> LendingRate { get; set; }
        public Nullable<double> ActualLendingRate { get; set; }      
        public string UpdatedMacName { get; set; }      
        public string UpdatedMacID { get; set; }        
        public string UpdatedIPAddress { get; set; }        
        public Nullable<int> UpdatedBy { get; set; }        
        public Nullable<System.DateTime> UpdatedOn { get; set; }      
        public Nullable<int> InsertedBy { get; set; }     
        public Nullable<System.DateTime> InsertedON { get; set; }      
        public string InsertedMacName { get; set; }      
        public string InsertedMacID { get; set; }            
        public string InsertedIPAddress { get; set; }
    }
}
