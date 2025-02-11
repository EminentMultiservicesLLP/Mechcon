using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.QueryCollection.Masters
{
    public class MasterQueries
    {
        public const string GetAllBranches = "sp_INV_MST_GetAllBranches";
        public const string GetUserBranches = "sp_GetActiveBranches";

        public const string GetAllUnitMaster = "sp_INV_MST_GetAllUnits";
        public const string GetUnitMasterById = "sp_INV_MST_GetUnitById";
        public const string InsertUnitMaster = "sp_InsUpd_INV_MST_Unit";
        public const string DeleteUnitMasterById = "sp_INV_MST_DeleteUnitById";
        public const string UpdateUnitMasterById = "sp_InsUpd_INV_MST_Unit";

        public const string GetAllItemTypeMaster = "sp_INV_MST_GetAllItemType";
        public const string GetItemTypeMasterById = "sp_INV_MST_GetItemTypeById";

        public const string GetItemTypeByStoreId = "sp_GetStoreItemTypeList";
        
        public const string UpdateItemTypeMasterById = "sp_InsUpd_INV_MST_ItemType";
        public const string InsertItemTypeMaster = "sp_InsUpd_INV_MST_ItemType";
        public const string GetAllItemTypeMasterforStoreItemType = "sp_INV_MST_GetAllItemTypeSTP";

        public const string GetAllItemPackSizeMaster = "sp_INV_MST_GetAllItemPackSize";
        public const string GetItemPackSizeMasterById = "sp_INV_MST_GetItemPackSizeById";
        public const string UpdateItemPackSizeMasterById = "sp_INV_MST_ItemPackSize";
        public const string InsertItemPackSizeMaster = "sp_INV_MST_ItemPackSize";
        public const string DeleteItemPackSizeMasterById = "";

        public const string GetAllCountryMaster = "sp_GetAllCountry";
        public const string GetAllVillageMaster = "sp_GetAllVillages";
        public const string GetAllStateMaster = "sp_GetAllStates";
        public const string GetAllCityMaster = "sp_GetAllCities";

        public const string GetAllItemGroupMaster = "sp_INV_MST_GetAllItemGroup";
        public const string GetItemGroupMasterById = "sp_INV_MST_ItemGroupById";
        public const string InsertItemGroupMaster = "sp_INV_MST_InsertItemGroup";
        public const string DeleteItemGroupMasterById = "sp_INV_MST_DeleteItemGroupById";
        public const string UpadateItemGroupMasterById = "sp_INV_MST_UpdateItemGroupById";
        public const string GetAllActiveItemGroup = "sp_INV_MST_GetAllActiveItemGroup";

        public const string GetAllSupplier = "sp_INV_MST_GetAllSupplier";
        public const string GetSupplierById = "sp_INV_MST_GetSupplierById";
        public const string InsertSupplier = "sp_InsUpd_INV_MST_Supplier";
        public const string DeleteSupplierById = "sp_INV_MST_DeleteSupplierById";
        public const string UpadateSupplierById = "sp_INV_MST_UpdateSupplierById";

        public const string GetAllManufacturerMaster = "sp_INV_MST_GetAllManufacturer";
        public const string GetManufacturerMasterById = "sp_INV_MST_GetManufacturerById";
        public const string UpdateManufactureMasterById = "sp_InsUpd_INV_MST_Manufacturer";
        public const string InsertManufactureMaster = "sp_InsUpd_INV_MST_Manufacturer";

        public const string GetAllItemMaster = "sp_INV_MST_GetAllItem";
        public const string GetItems = "sp_INV_MST_GetItems";
        public const string GetNonVendorItems = "sp_GetNonVendorItems";
        public const string GetNonIndentItems = "sp_GetNonIndentItems";
        public const string GetAllStoreItemMaster = "sp_GetTypewisePreparedItems";
        public const string GetStoreItems = "sp_GetStoreItemsOPBal";
        public const string GetStoreItemStock = "sp_GetStoreItemsStock";
        public const string StoreVendorItems = "sp_GetStoreItems";
        public const string GetItemMasterById = "sp_INV_MST_GetItemId";
        public const string CheckDuplicateItem = "sp_Check_DuplicateCode";
        public const string UpdateItemMasterById = "sp_InsUpd_INV_MST_ITEM";
        public const string InsertItemMaster = "sp_InsUpd_INV_MST_ITEM";
        public const string InsertItemMasterSupplier = "sp_InsUpd_INV_MST_ITEMSupplier";
        public const string InsertVendorItems = "sp_InsVendorItems";
        public const string GetAllstorestockItems = "sp_GetAllstorestockItems";

        public const string GetAllStoreMaster = "sp_INV_MST_GetAllStores"; 
        public const string GetApprovedStores = "sp_INV_MST_GetApprovedStores";
        public const string GetAllMainStore = "sp_INV_MST_GetTypeWiseStores";
        public const string GetUnitStore = "sp_INV_MST_GetTypeWiseStores";
        public const string GetTypeWiseStores = "sp_INV_MST_GetTypeWiseStores";
        public const string GetIndentToStores = "sp_GetIndentToStores";
        public const string GetStoreMasterById = "sp_INV_MST_GetStoreId";
        public const string UpdateStoreMasterById = "sp_InsUpd_StoreMaster";
        public const string InsertStoreMaster = "sp_InsUpd_StoreMaster";
        public const string GetBranchStores = "sp_GetBranchStores";

        public const string GetAllStoreTypeMaster = "sp_INV_MST_GetAllStoreTypes";
        public const string GetStoreTypeById = "sp_INV_MST_GetStoreTypesById";
        public const string UpdateStoreTypeById = "sp_InsUpd_StoreType";
        public const string InsertStoreTypeMaster = "sp_InsUpd_StoreType";

        public const string GetAllStoreItemMapping = "sp_INV_MST_GetAllStoreItemMapping";
        public const string GetStoreItemTypeMappingByStoreId = "sp_GetstoreITMById";
        public const string UpdateStoreItemTypeByStoreId = "sp_InsUpd_StoreMaster";
        public const string InsertStoreItemTypeMapping = "sp_Ins_INV_MST_STP";

        public const string GetAllDeliveryMaster = "sp_INV_MST_GetAlldelivery";
        public const string GetDeliveryById = "sp_INV_MST_GetDeliveryById";
        public const string InsertDeliveryTermMaster = "sp_InsUpd_INV_MST_DeliveryTerms";
        public const string UpdateDeliveryTermMasterById = "sp_InsUpd_INV_MST_DeliveryTerms";

        public const string GetAllPayment = "sp_INV_MST_GetAllPayment";
        public const string GetPaymentById = "sp_INV_MST_GetPaymentById";
        public const string InsertPaymentMaster = "sp_InsUpd_INV_MST_PaymentTerms";
        public const string UpdatePaymentMasterById = "sp_InsUpd_INV_MST_PaymentTerms";

        public const string GetAllProjectTC = "sp_INV_MST_GetAllProjectTC"; 
        public const string InsertProjectTCMaster = "sp_InsUpd_INV_MST_ProjectTC";
        public const string UpdateProjectTCMaster = "sp_InsUpd_INV_MST_ProjectTC";

        public const string GetAllProduct = "sp_INV_MST_GetAllProduct";
        public const string InsertProductMaster = "sp_InsUpd_INV_MST_Product";
        public const string UpdateProductMaster = "sp_InsUpd_INV_MST_Product";

        public const string GetAllOthers = "sp_INV_MST_GetAllOthers";
        public const string GetOthersById = "sp_INV_MST_GetOthersById";
        public const string InsertOthersMaster = "sp_InsUpd_INV_MST_OtherTerms";
        public const string UpdateOthersMasterById = "sp_InsUpd_INV_MST_OtherTerms";

        public const string GetItemSupplierMappingByItemId = "sp_GetAllItemSupplier";
        public const string GetVendorItems = "sp_GetVendorItems";
        public const string GetVendorItemsByItemId = "sp_GetVendorItemsbyItemId";

        public const string GetAllMaterialIndents = "sp_GetAllMaterialIndents";
        public const string GetAllMaterialIndentsActive = "sp_GetAllMaterialIndentActive";
        public const string GetMaterialIndentsById = "sp_GetAllMaterialIndentsById";

        public const string GetAllTaxMaster = "sp_GetTaxesforPOGRN";
        public const string Ins_MST_Branch = "sp_Ins_MST_Branch";

        public const string AddDeleteStoreItemTypeMapping = "sp_AddDeleteStoreItemTypeMapping";

        public const string CheckDuplicatestore = "sp_Check_Duplicatestore";
        public const string CheckDuplicateupdate = "sp_Check_DuplicateCodeupdate";

        public const string GetAllStoreunit = "sp_INV_MST_GetAllUnitStore";
        public const string InsertStoreunit = "sp_InsUpd_Storeunit";
        public const string GetItemCode = "sp_Getitemcode";

        public const string GetItemsforclientbilling = "sp_GetItemsforclientbilling";
        public const string AllBasisTerm = "sp_INV_MST_GetAllBasis";
        public const string AllInspectionTerm = "sp_INV_MST_GetAllInspectionTerm";
        public const string GetItemDetails = "sp_GetItemDetails";

        public const string CreateInspectionmatser = "sp_InsUpd_INV_MST_InspectionMater";
        public const string UpdateInspectionmatser = "sp_InsUpd_INV_MST_InspectionMater";

        public const string CreateBasisMaster = "sp_InsUpd_INV_MST_BasisMater";
        public const string UpdateBasisMaster = "sp_InsUpd_INV_MST_BasisMater";
        public const string CreateDtlStore = "sp_InsUpd_CreateDtlStore";
        public const string GetStoredtl = "dbsp_getCreateDtlStore";


        public const string CreateProjectBudgetDtl = "sp_InsUpd_CreateProjectBudgetDtl";
        public const string CreateProjectTCDtl = "sp_InsUpd_CreateProjectTCDtl";
        public const string GetStoreBudgetdtl = "dbsp_GetStoreBudgetdtl";
        public const string GetProjectBudget = "dbsp_GetProjectBudget";
        public const string GetProjectBudgetStatus = "dbsp_GetProjectBudgetStatus";
        public const string GetBudgetUtilzedDetails = "dbsp_GetBudgetUtilzedDetails";
        public const string GetMasterCode = "dbsp_GetMasterCode";
        public const string GetStorewiseProjectTC = "sp_GetStorewiseProjectTC";

        public const string InsertMechconData = "dbsp_Mst_SaveMechconData";
        public const string GetMechconData = "dbsp_Mst_GetMechconData";

        public const string SaveCity = "sp_InsUpd_INV_City";
        public const string SaveState = "sp_InsUpd_INV_State"; 

        public const string SaveStoreMasterApproval = "sp_INV_StoreMasterApproval";
        public const string AuthCanSupplier = "sp_INV_MST_AuthCanSupplier";
        public const string GetRevisionDetails = "sp_INV_MST_GetRevisionDetails";
        public const string GetProjectTransactionRecord = "dbsp_GetProjectTransactionRecord";
        public const string GetDeliverablesDetail = "dbsp_GetDeliverablesDetail";
        public const string GetBudgetStatus = "dbsp_GetBudgetStatus";
        public const string GetEnqForProjectMaster = "sp_SM_GetEnqForProjectMaster";
        public const string GetPRDashboardCount = "sp_INV_GetPRDashboardCount";
        public const string GetPODashboardCount = "sp_INV_GetPODashboardCount";
        public const string GetGRNDashboardCount = "sp_INV_GetGRNDashboardCount";
        public const string GetMRDashboardCount = "sp_INV_GetMRDashboardCount";

        public const string SaveClearanceNote = "sp_InsUpd_SaveClearanceNote";
        public const string GetClearanceNote = "dbsp_GetClearanceNote";

        public const string SavePackingList = "sp_InsUpd_PackingList";
        public const string GetPackingList = "dbsp_GetPackingList";
        public const string GetPackingListDetail = "dbsp_GetPackingListDetail";
        public const string GetPackingListForRpt = "dbsp_GetPackingListforRpt";
        public const string GetPLById = "dbsp_GetPLById";

    }
}
