using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.QueryCollection.Transport
{
    class TransportQuery
    {
        public const string GetAllRoadTax = "sp_TRA_GetAllRoadTax";
        public const string GetAllPUCDetails = "sp_GetAllPUCDetails";
        public const string GetAllGreenTax = "sp_TRA_GetAllGreenTax";
        public const string GetAllPermitDetails = "sp_GetAllPermitDetails";
        public const string GetAllInsurance = "sp_GetAllInsurance";
        public const string GetAllVehicleInfo = "sp_TRA_GetAllVehicleInfo";
        public const string GetAllVehicleNo = "sp_TRA_GetAllVehicleNO";
        public const string VehicleInfoReport = "sp_TRA_rptVehicleInformation";
        public const string GetAllVehicleInfoSchedule = "sp_TRA_GetAllVehicleInfoSchedule";
        public const string GetAllVehicleType = "sp_TRA_GetAllVehicleType";
        public const string InsUpd_TRA_MST_VehicleType = "sp_InsUpd_TRA_MST_VehicleType";
        public const string GetActiveVehicleType = "sp_GetActiveVehicleType";
        public const string GetAllVehicleModel = "sp_TRA_GetAllVehicleModel";
        public const string GetAllInsuranceCompany = "sp_TRA_GetAllInsuranceCompany";
        public const string InsUpd_TRA_MST_VehicleModel = "sp_InsUpd_TRA_MST_VehicleModel";
        public const string GetActiveVehicleModel = "sp_GetActiveVehicleModel";
        public const string GetAllVehicleListRpt = "sp_TRA_GetAllVehicleListRpt";
        public const string GetActiveVehicleUsage = "sp_GetActiveVehicleUsage";
        public const string InsUpd_TRA_MST_VehicleUsage = "sp_InsUpd_TRA_MST_VehicleUsage";
        public const string GetAllVehicleUsage = "sp_TRA_GetAllVehicleUsage";

        public const string GetAllVehicleMake = "sp_TRA_GetAllVehicleMake";
        public const string GetActiveVehicleMake = "sp_GetActiveVehicleMake";
        public const string InsUpd_TRA_MST_VehicleMake = "sp_InsUpd_TRA_MST_VehicleMake";

        public const string InsertRoadTax = "sp_TRA_InsRoadTax";
        public const string UpdateRoadTax = "sp_TRA_UpdRoadTax";
        public const string InsertVehicleInfo = "sp_TRA_InsVehicleInfo";
        public const string UpdateVehicleInfo = "sp_TRA_UpdVehicleInfo";
        public const string InsertGreenTax = "sp_TRA_InsGreenTax";
        public const string InsertPermitDetails = "sp_TRA_InsPermitDetails";
        public const string InsertPUCDetails = "sp_TRA_InsPUCDetails";
        public const string UpdateGreenTax = "sp_TRA_UpdGreenTax";
        public const string InsertInsurance = "sp_TRA_InsInsurance";
        public const string UpdateInsurance = "sp_TRA_UpdInsurance";
        public const string CheckDpVehicleTax = "sp_TRA_CheckDpVehicleTax";
        public const string CheckDpVehicle = "sp_TRA_CheckDpVehicle";

        public const string InsUpd_TRA_MST_Division = "sp_InsUpd_TRA_MST_Division";
        public const string TRA_MST_GetAllDivision = "sp_TRA_MST_GetAllDivision";
        public const string sp_GetActiveDivision = "sp_GetActiveDivision";

        public const string InsUpd_TRA_MST_SubDivision = "sp_InsUpd_TRA_MST_SubDivision";
        public const string TRA_MST_GetAllSubDivision = "sp_TRA_MST_GetAllSubDivision";
        public const string GetActiveSubDivision = "sp_GetActiveSubDivision";


        public const string GetVehicleRoadTax = "sp_TRA_GetVehicleRoadTax";
        public const string GetVehiclePUCDetails = "sp_GetVehiclePUCDetails";
        public const string GetVehicleGreenTax = "sp_TRA_GetVehicleGreenTax";
        public const string GetVehiclePermitDetails = "sp_GetVehiclePermitDetails";
        public const string GetVehicleInsurance = "sp_GetVehicleInsurance";
        public const string GetAllDriver = "sp_TRA_GetAllDriver";
        public const string GetAllDriverSchedule = "sp_TRA_GetAllDriverSchedule";

        public const string GetFuelDetail = "sp_TRA_GetFuelDetail";
        public const string GetDriverSchedule = "sp_TRA_GetDriverSchedule";
        public const string GetAllSchedule = "sp_TRA_GetAllSchedule";
        public const string InsertDriverSchedule = "sp_TRA_InsDriverSchedule";
        public const string InsertFuelDetails = "sp_TRA_InsFuelDetails";
        public const string UpdateFuelDetails = "sp_TRA_UpdFuelDetails";
        public const string GetAllSchedulefuel = "sp_TRA_GetAllSchedulefuel";
        public const string GetAllScheduleRpt = "sp_TRA_GetAllScheduleRpt";
        public const string Fuelconsumptionvehiclewiserpt = "sp_TRA_Fuelconsumptionvehiclewiserpt";

        public const string GetInsuranceNotification = "sp_TRA_AlertInsurance";
        public const string GetGreenTaxNotification = "sp_TRA_AlertGreenTax";
        public const string GetRoadTaxNotification = "sp_TRA_AlertRoadTax";
        public const string GetPermitDetailNotification = "sp_AlertPermitDetails";
        public const string GetPUCDetailNotification = "sp_AlertPUCDetails";

        public const string InsertVehicleTransfer = "sp_TRA_InsVehicleTransfer";
        public const string AuthorizeCancelTransfer = "sp_TRA_AuthorizeCancelTransfer";
        public const string GetVehicleTransfer = "sp_TRA_GetAllVehicleTransfer";
        public const string rptVehicleTransfer = "sp_TRA_rptVehicleTransfer";
        public const string rptVehicleSold = "sp_TRA_rptVehicleSold";
    }
}
