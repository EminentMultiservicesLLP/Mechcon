using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace BISERP.Areas.Training.Models
{
    public class MonthlyExpenditureModel : HttpResponseMessage
    {
        public int Id { get; set; }
        public int ExpMonth { get; set; }
        public int ExpYear { get; set; }
        public double RecieptTotal { get; set; }
        public double ExpTotal { get; set; }
        public double BalanceAmount { get; set; }
        public List<MonthlyExpenditureDtModel> ExpenditureModel { get; set; }
        public int BudgetId { get; set; }
        public string UpdatedMacName { get; set; }
        public string UpdatedMacID { get; set; }
        public string UpdatedIPAddress { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
        public Nullable<int> InsertedBy { get; set; }
        public Nullable<DateTime> InsertedON { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacID { get; set; }
        public string InsertedIPAddress { get; set; }
        public bool Deactive { get; set; }
        public string Message { get; set; }

    }

    public class MonthlyExpenditureDtModel : HttpResponseMessage
    {
        public int DtlId { get; set; }
        public int Id { get; set; }
        public string BudgetHeads { get; set; }
        public int BudgetId { get; set; }
        public string ExpDate { get; set; }
        public string Reciepts { get; set; }
        public double ReieptsAmt { get; set; }
        public double ExpenseAmt { get; set; }

        public string UpdatedMacName { get; set; }
        public string UpdatedMacID { get; set; }
        public string UpdatedIPAddress { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
        public Nullable<int> InsertedBy { get; set; }
        public Nullable<DateTime> InsertedON { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacID { get; set; }
        public string InsertedIPAddress { get; set; }

        //to display data
        public double RecieptTotal { get; set; }
        public double ExpTotal { get; set; }
        public string strExpDate { get; set; }

    }



}