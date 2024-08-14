using System;
using System.Collections.Generic;

namespace BISERPBusinessLayer.Entities.Training
{
    public class GeneralBudgetProposalEntity
    {
        public int BudgetId { get; set; }
        public int BudgetMonth { get; set; }
        public int BudgetYear { get; set; }
        public double ExpenseTotal { get; set; }
        public double ProposedTotal { get; set; }
        public List<GeneralBudgetProposalDtEntity> BudgetDtModel { get; set; }
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
        public Nullable<bool> Authorized { get; set; }
        public Nullable<int> AuthorizedBy { get; set; }
        public Nullable<System.DateTime> AuthDate { get; set; }
        public Nullable<bool> cancelled { get; set; }
        public Nullable<int> cancelledBy { get; set; }
        public Nullable<System.DateTime> cancelledOn { get; set; }

        //display

        public string MonthYear { get; set; }

    }

    public class GeneralBudgetProposalDtEntity
    {
        public int BudgetDtlId { get; set; }
        public int BudgetId { get; set; }
        public string BudgetHeads { get; set; }
        public double ActualExpense { get; set; }
        public double ProposalBudget { get; set; }
        public double AuthorizedBudget { get; set; }
        public double ExpenseTotal { get; set; }
        public double ProposedTotal { get; set; }
        public double BalanceAmt { get; set; }


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

    }
}
