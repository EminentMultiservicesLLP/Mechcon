using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.QueryCollection.Training
{
    public class TrainingQueries
    {
        public const string GetAllTrainingType = "dbsp_TC_MST_GetAllTrainingType";
        public const string InsUpdTrainingType = "dbsp_InsUpd_TC_MST_TrainingType";

        public const string GetAllBatchType = "dbsp_TC_MST_GetAllBatchType";
        public const string InsUpdBatchType = "dbsp_InsUpd_TC_MST_BatchType";

        public const string GetAllSubjects = "dbsp_TC_MST_GetAllSubjects";
        public const string InsUpdSubject = "dbsp_InsUpd_TC_MST_Subject";

        public const string GetAllTrainingCentre = "dbsp_TC_MST_TrainingCentre";
        public const string GetAllSubjectsTopic = "dbsp_TC_MST_GetAllSubjectTopic";
        public const string GetAllBatch = "dbsp_TC_MST_Batch";
        public const string InsUpdSubjectTopic = "dbsp_InsUpd_TC_MST_SubjectTopic";

        public const string GetAllTrainers = "dbsp_TC_MST_GetAllTrainers";
        public const string InsUpdTrainers = "dbsp_InsUpd_TC_MST_Trainer";

        public const string GetAllGrades = "dbsp_TC_MST_GetAllGrades";

        public const string InsUpdBatch = "dbsp_InsUpd_TC_MST_Batch";

        public const string GetAllSchedule = "dbsp_TC_MST_GetAllSchedule";
        public const string InsUpdSchedule = "dbsp_InsUpd_TC_MST_Schedule";

        public const string GetAllRating = "dbsp_TC_MST_GetAllRating";
        public const string InsUpdRating = "dbsp_InsUpd_TC_MST_Rating";

        public const string InsUpdTrainingCategory = "dbsp_InsUpd_TC_MST_TrainingCategory";
        public const string GetAllTrainingCategory = "dbsp_TC_MST_GetAllTrainingCategory";

        public const string InsUpdSlot = "dbsp_InsUpd_TC_MST_Slot";
        public const string GetAllSlot = "dbsp_TC_MST_GetAllSlot";
        public const string GetAllDayWiseSlot = "dbsp_TC_MST_GetAllDayWiseSlot";

        public const string InsUpdBudgetHeads = "dbsp_InsUpd_TC_MST_BudgetHeads";
        public const string GetAllBudgetHeads = "dbsp_TC_MST_GetAllBudgetHeads";

        public const string GetSelectedSlot = "dbsp_TC_MST_GetSelectedSlot";
        public const string InsUpdTrainingTemplate = "dbsp_InsUpd_TC_MST_TrainingTemplate";

        public const string GetTraniningWiseSlot = "dbsp_TC_MST_GetTraniningWiseSlot";
        //*********
        public const string InsUpdTrainingTemplateHdr = "dbsp_InsUpd_TrainingTemplateHdr";
        public const string InsUpdTrainingTemplateDtl = "dbsp_InsUpd_TrainingTemplateDtl";
        public const string GetTrainingTemplateHdr = "dbsp_TC_GetTrainingTemplateHdr";
        public const string GetTrainingTemplateDtl = "dbsp_TC_GetTrainingTemplateDtl";
        

        public const string InsUpdMonthlyExpenditure = "dbsp_InsUpd_TC_MonthlyExpenditureDtl";
        public const string InsUpdMonthlyExpenditureTotal = "dbsp_InsUpd_TC_MonthlyExpenditureTotal";
        public const string GetExpenditureMonthWise = "dbsp_TC_GetMonthWiseExpenditureDtl";

        public const string InsUpdGeneralBudgetProposalTotal = "dbsp_InsUpd_TC_GeneralBudgetProposalTotal";
        public const string InsUpdGeneralBudgetProposalDtl = "dbsp_InsUpd_TC_GeneralBudgetProposalDtl";
        public const string GetbudgetProposalByMonth = "dbsp_TC_GetGeneralBudgetProposalMonthWise";
        public const string GetActualExpence = "dbsp_TC_GetActualExpence";
        public const string CheckDuplicateBudgetHead = "sp_Check_BudgetHead";
        public const string GetAllGeneralBudgetProposalTotal = "dbsp_TC_GetAllGeneralBudgetProposalTotal";
        public const string GetAllGeneralBudgetProposalDtl = "dbsp_TC_GetAllGeneralBudgetProposalDtl";
        public const string UpdGeneralBudgetProposalAuth = "dbsp_UpdGeneralBudgetProposalAuth";

        public const string InsUpdTrainingDaillyUpdatesHdr = "dbsp_InsUpd_TC_TrainingDaillyUpdatesHdr";
        public const string InsUpdTrainingDaillyUpdatesDtl = "dbsp_InsUpd_TC_TrainingDaillyUpdatesDtl";
        public const string InsUpdTrainingDailyUpdatesRating = "dbsp_InsUpd_TC_TrainingDailyUpdatesRating";
        public const string GetGuardTrainee = "TC_GetGuardTrainee";
        public const string GetTraniningDay = "dbsp_TC_GetTraniningDay";
        public const string GetTraniningTemplateSlot = "dbsp_TC_GetTraniningSlotDayWise";
        public const string GetTraniningTemplatePeriod = "dbsp_TC_GetTraniningPeriodDayWise";
        public const string GetTrainingTemplate = "dbsp_TC_GetTrainingTemplate";

        public const string GetTraniningTemplatePeriodRecord = "dbsp_TC_GetTraniningTemplatePeriodRecord";

        public const string GetAllDayWiseBatchSlot = "dbsp_TC_MST_GetAllDayWiseBatchSlot";
        public const string GetdailyupdateTrainingType = "dbsp_TC_MST_GetdailyupdateTrainingType";

        public const string CheckDuplicateBatch = "dbsp_Check_DuplicateBatch";

    }
}
