namespace BISERPBusinessLayer.QueryCollection.Asset
{
    public class AssetQueries
    {
        public const string GetAllFloor = "dbsp_ASSET_MST_GetAllFloor";
        public const string AstGetActiveFloor = "dbsp_AST_GetActiveFloor";
        public const string AstGetBranchFloor = "dbsp_AST_GetBranchFloor";
        public const string InsUpdAstMstFloor = "dbsp_InsUpd_AST_MST_Floor";
        public const string GetAllAmcProvider = "dbsp_AST_GetAllAMCProvider";
        public const string InsertAmcProvider = "dbsp_AST_InsAMCProvider";
        public const string UpdateAmcProvider = "dbsp_AST_UpdAMCProvider";

        public const string GetActiveRoom = "dbsp_AST_GetActiveRoom";
        public const string GetFloorRoom = "dbsp_AST_GetActiveFloorWiseRoom";
        public const string GetAllRoom = "dbsp_ASSET_MST_GetAllRoom";
        public const string InsUpdAstMstRoom = "dbsp_InsUpd_AST_MST_Room";

        public const string GetAllTechnician = "dbsp_AST_MST_GetAllTechnician";
        public const string InsUpdAstMstTechnician = "dbsp_InsUpd_AST_MST_Technician";

        public const string GetAllAssetType = "dbsp_AST_MST_GetAllAssetType";
        public const string InsUpdAstMstAssetType = "dbsp_InsUpd_AST_MST_AssetType";

        public const string GetAllAssetSubType = "dbsp_AST_MST_GetAllAssetSubType";
        public const string GetAssetTypewiseSubType = "dbsp_AST_MST_GetAssetTypewiseSubType";
        public const string InsUpdAstMstAssetSubType = "dbsp_InsUpd_AST_MST_AssetSubType";

        public const string GetAllCallType = "dbsp_ASSET_MST_GetAllCallType";
        public const string GetAllMaintainanceType = "dbsp_ASSET_MST_GetAllMaintainanceType";

        public const string InsUpdAstMstCallType = "dbsp_InsUpd_AST_MST_CallType";
        public const string InsUpdAstMstMaintainanceType = "dbsp_InsUpd_AST_MST_MaintainanceType";

        public const string GetBranchAsset = "dbsp_AST_GetBranchAssets";
        public const string InsUpdAstAsset = "dbsp_AST_InsUpd_Asset";
        public const string InsUpdAstAssetDetail = "dbsp_AST_InsUpd_AssetDetail";

        public const string GetAssetLocation = "dbsp_AST_GetAssetLocationInfo";
        public const string AssetDetailReport = "dbsp_rptAssetDetails";
        public const string AssetScheduleReport = "dbsp_rptAssetScheduling";
        public const string InsUpdAstAssetLocation = "dbsp_InsUpd_AST_AssetLocation";
        public const string rptAssetLocation = "dbsp_rptAssetLocation";

        public const string InsUpdAssetSchedule = "dbsp_AST_InsUpdAssetSchedule";
        public const string InsUpdAssetScheduleDetails = "dbsp_AST_InsUpdAssetScheduleDetails";

        public const string InsUpdRequestRegister = "dbsp_AST_InsUpdRequestRegister";
        public const string UpdateRequestRegister = "dbsp_AST_UpdRequestRegister";
        public const string GetRequistionNo = "dbsp_AST_GetRequistionNo";
        public const string GetRequistionDetail = "dbsp_AST_GetRequestDetail";
        public const string UpdRequestAcceptance = "dbsp_AST_UpdRequestAcceptance";
        public const string GetRequestNoDeletion = "dbsp_AST_GetRequestNoDeletion";

        public const string GetAssetSchedule = "dbsp_AST_GetAssetSchedule";
        public const string GetAssetScheduledt = "dbsp_AST_GetAssetScheduledt";
        public const string InsAssetScheduleCompletion = "dbsp_AST_InsAssetScheduleCompletion";
        public const string InsAssetDeletionRequest = "dbsp_AST_InsAssetDeletionRequest";

        public const string InsUpdInHouseMaintenance = "dbsp_AST_InsUpdInhouseMaintenance";
        public const string InsUpdTechnicianEntry = "dbsp_AST_InsUpdTechnicianEntry";
        public const string InsUpdMaterialConsumption = "dbsp_AST_InsUpdMaterialConsumption";

        public const string InsOutsideMaintenance = "dbsp_AST_InsOutsideMaintenance";
        public const string GetRequestRegister = "dbsp_AST_GetRRForMaintenance";
        public const string GetAllRequestRegister = "dbsp_AST_GetAllRequestRegister";
        public const string RequestRegisterReport = "dbsp_rptRequestRegister";
        public const string InsUpdSpareOuthouse = "dbsp_AST_InsUpdSpareOUTHOUSE";

        public const string GetTechnicianEntryDetail = "dbsp_AST_GetTechnicianEntryDetail";
        public const string GetMaterialConsumption = "dbsp_AST_GetMaterialConsumption";

        public const string GetOutsideMaintenance = "dbsp_AST_GetOutsideMaintenance";
        public const string GetOutsideMaintenancedt = "dbsp_AST_GetOutsideMaintenancedt";

        public const string InsUpdWarrantyMaintenance = "dbsp_AST_InsUpdWarrantyMaintenance";
        public const string InsUpdWarrantySparePart = "dbsp_AST_InsUpdWarrantySparePart";

        public const string GetOutsideMaintenancedtitem = "dbsp_AST_GetOutsideMaintenancedtitem";
        public const string GetPoNoAssetLo = "dbsp_AST_GetPoNoAssetLo";
        public const string GetSupplierNameAssetdt = "dbsp_AST_GetSupplierNameAssetdt";

        //public const string GetSupplierNameAssetdt = "dbsp_AST_GetSupplierNameAssetdt";
        //public const string GetPoNoAssetLo = "dbsp_AST_GetPoNoAssetLo";
        public const string GetBranchAssetsforschedule = "dbsp_AST_GetBranchAssetsforschedule";
        public const string AlertAmc = "dbsp_AST_AlertAMC";
        public const string AlertCmc = "dbsp_AST_AlertCMC";
        public const string AlertOtherNotification = "dbsp_AST_AlertOtherNotification";
        public const string AlertRequestNotification = "dbsp_AST_AlertRequestNotification";

        public const string GetAllLocation = "dbsp_AST_GetAllLocation";
        public const string InsLocation = "dbsp_AST_InsLocation";
        public const string UpdLocation = "dbsp_AST_UpdLocation";
        public const string AstGetActiveLocation = "dbsp_AST_GetActiveLocation";
        public const string GetBranchLocation = "dbsp_AST_GetBranchLocation";
        public const string GetActiveLocationWiseFloor = "dbsp_AST_GetActiveLocationWiseFloor";
        public const string RptAssetdeactivation = "dbsp_rptAssetdeactivation";


        public const string InsertVendor = "dbsp_InsUpd_AST_MST_Vendor";
        public const string GetAllVendor = "dbsp_INV_AST_MST_GetAllVendor";

        public const string GetActiveVendor = "dbsp_AST_GetActiveVendor";
        public const string GetBranchLocationFloor = "dbsp_AST_GetBranchLocationFloor";

        public const string AssetDetailsItemWise = "dbsp_rptAssetDetailsItemWise";
        public const string InsertClient = "dbsp_InsUpd_AST_MST_Client";
        public const string GetAllClient = "dbsp_INV_AST_MST_GetAllClient";


        public const string CreateConsignee = "dbsp_CreateConsignee";
        public const string UpdateConsignee = "dbsp_UpdateConsignee";
        public const string GetClientConsignee = "dbsp_GetClientConsignee";
    }
}
