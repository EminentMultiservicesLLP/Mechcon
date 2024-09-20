using Microsoft.Practices.Unity;
using BISEPRService.Resolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Entity = BISERPBusinessLayer.Entities;
using BISERPBusinessLayer.Repositories.Master.Interfaces;
using BISERPBusinessLayer.Repositories.Master.Classes;
using BISERPBusinessLayer.Repositories.Purchase.Interfaces;
using BISERPBusinessLayer.Repositories.Purchase.Classes;
using BISERPBusinessLayer.Repositories.Store.Interfaces;
using BISERPBusinessLayer.Repositories.Store.Classes;
using BISERPBusinessLayer.Repositories.Branch.Interface;
using BISERPBusinessLayer.Repositories.Branch.Class;
using BISERPBusinessLayer.Repositories.Common;
using BISERPBusinessLayer.Repositories.UserManagement.Interface;
using BISERPBusinessLayer.Repositories.UserManagement.Class;
using BISERPService.Filters;
using BISERPBusinessLayer.Repositories.Transport.Interfaces;
using BISERPBusinessLayer.Repositories.Transport.Classes;
using BISERPBusinessLayer.Repositories.Asset.Interfaces;
using BISERPBusinessLayer.Repositories.Asset.Classes;
using BISERPBusinessLayer.Repositories.Billing;
using BISERPBusinessLayer.Repositories.Billing.Class;
using BISERPBusinessLayer.Repositories.Common.Class;
using BISERPBusinessLayer.Repositories.Common.Interface;
using BISERPBusinessLayer.Repositories.ScanDoc.Interfaces;
using BISERPBusinessLayer.Repositories.ScanDoc.Classes;
using BISERPBusinessLayer.Repositories.Training.Interfaces;
using BISERPBusinessLayer.Repositories.Training.Classes;
using BISERPBusinessLayer.Repositories.Billing.Interface;


using BISERPBusinessLayer.Repositories.AdminPanel.Interfaces;
using BISERPBusinessLayer.Repositories.AdminPanel.Classes;
using BISERPBusinessLayer.Repositories.DashBoard.Interfaces;
using BISERPBusinessLayer.Repositories.DashBoard.Classes;
using BISERPBusinessLayer.Repositories.Configuration.Interfaces;
using BISERPBusinessLayer.Repositories.Configuration.Classes;
using BISERPBusinessLayer.Repositories.SM.Interfaces;
using BISERPBusinessLayer.Repositories.SM.Classes;

namespace BISERPService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var container = new UnityContainer();
            container.RegisterType<IUnitMasterRepository, UnitMasterRepository>();

            //container.RegisterType<IPaymentTermsMasterRepository, PaymentTermsMasterRepository>();
            container.RegisterType<IOthersTermsMasterRepository, OthersTermsMasterRepository>();
            container.RegisterType<IItemMasterRepository, ItemMasterRepository>();
            container.RegisterType<IItemSupplierMappingRepository, ItemMasterRepository>();
            container.RegisterType<IItemPackSizeMasterRepository, ItemPackSizeMasterRepository>();
            container.RegisterType<IManufacturerMasterRepository, ManufacturerMasterRepository>();
            container.RegisterType<IMechconMasterRepository, MechconMasterRepository>();
            container.RegisterType<ICityStateCountryMasterRepository, CityStateCountryMasterRepository>();

            container.RegisterType<IItemTypeMasterRepository, ItemTypeMasterRepository>();

            container.RegisterType<ICityMasterRepository, CityMasterRepository>();
            container.RegisterType<IStateMasterRepository, StateMasterRepository>();
            container.RegisterType<IVillageMasterRepository, VillageMasterRepository>();
            container.RegisterType<ICountryMasterRepository, CountryMasterRepository>();

            container.RegisterType<IItemGroupMasterRepository, ItemGroupMasterRepository>();
            container.RegisterType<ISupplierMasterRepository, SupplierMasterRepository>();
            container.RegisterType<IStoreMasterRepository, StoreMasterRepository>();
            container.RegisterType<IClearanceNoteRepository, ClearanceNoteRepository>();
            container.RegisterType<IPackingListRepository, PackingListRepository>();
            container.RegisterType<IStoreTypeMasterRepository, StoreTypeMasterRepository>();
            container.RegisterType<IManufacturerMasterRepository, ManufacturerMasterRepository>();
            container.RegisterType<IItemMasterRepository, ItemMasterRepository>();
            container.RegisterType<IDeliveryTermsMasterRepository, DeliveryTermsMasterRepository>();
            container.RegisterType<IPaymentTermsMasterRepository, PaymentTermsMasterRepository>();
            container.RegisterType<IProjectTCMasterRepository, ProjectTCMasterRepository>();
            container.RegisterType<IOthersTermsMasterRepository, OthersTermsMasterRepository>();
            container.RegisterType<IMaterilaIndentRepository, MaterilaIndentRepository>();
            container.RegisterType<IMaterialIndentDetailsRepository, MaterialIndentDetailsRepository>();
            container.RegisterType<IMaterilaIndentCommonRepository, MaterilaIndentCommonRepository>();
            container.RegisterType<IMaterialIssueRepository, MaterialIssueRepository>();
            container.RegisterType<IMaterialIssueDetailRepository, MaterialIssueDetailRepository>();
            container.RegisterType<IMaterialIssueCommonRepository, MaterialIssueCommonRepository>();
            container.RegisterType<IPurchaseIndentDetailRepository, PurchaseIndentDetailRepository>();
            container.RegisterType<IPurchaseIndentRepository, PurchaseIndentRepository>();
            container.RegisterType<IPurchaseIndentCommonRepository, PurchaseIndentCommonRepository>();

            container.RegisterType<IOpeningBalanceRepository, OpeningBalanceRepository>();
            container.RegisterType<IStockDetailsRepository, StockDetailsRepository>();
            container.RegisterType<IStockRegisterRepository, StockRegisterRepository>();
            container.RegisterType<IBranchMasterRepository, BranchMasterRepository>();
            container.RegisterType<IPurchaseOrderDetailRepository, PurchaseOrderDetailRepository>();
            container.RegisterType<IPurchaseOrderRepository, PurchaseOrderRepository>();
            container.RegisterType<IPOCommonRepository, POCommonRepository>();
            container.RegisterType<IPODeliveryTerm, PODeliveryTerm>();
            container.RegisterType<IPOPaymentTerm, POPaymentTerm>();
            container.RegisterType<IPOOtherTerm, POOtherTerm>();
            container.RegisterType<IStoreItemTypeMappingRepository, StoreItemTypeMappingRepository>();
            container.RegisterType<IMaterialReturnRepository, MaterialReturnRepository>();
            container.RegisterType<IMaterialReturnDetailsRepository, MaterialReturnDetailsRepository>();
            container.RegisterType<IMaterialReturnCommonRepository, MaterialReturnCommonRepository>();
            container.RegisterType<IGRNRepository, GRNRepository>();
            container.RegisterType<IGRNDetailRepository, GRNDetailRepository>();
            container.RegisterType<IGRNCommonRepository, GRNCommonRepository>();
            container.RegisterType<ITaxMasterRepository, TaxMasterRepository>();
            container.RegisterType<IStoreConsumptionRepository, StoreConsumptionRepository>();
            container.RegisterType<IStoreConsumptionDetailsRepository, StoreConsumptionDetailsRepository>();
            container.RegisterType<IStoreConsumptionCommonRepository, StoreConsumptionCommonRepository>();
            container.RegisterType<ICancelPendingMaterialIndentRepository, CancelPendingMaterialIndentRepository>();
            container.RegisterType<IStoreUnitLinkingMasterRepository, StoreUnitLinkingMasterRepository>();
            container.RegisterType<IMaterialTransferRepository, MaterialTransferRepository>();
            container.RegisterType<IEmployeeRepository, EmployeeRepository>();
            container.RegisterType<IPurchaseReturnRepository, PurchaseReturnRepository>();
            container.RegisterType<IPurchaseReturnDetailsRepository, PurchaseReturnDetailsRepository>();
            container.RegisterType<IPurchaseReturnCommonRepository, PurchaseReturnCommanRepository>();

            container.RegisterType<IMaterialIssueGuardRepository, MaterialIssueGuardRepository>();
            container.RegisterType<IMaterialIssueGuardDtRepository, MaterialIssueGuardDtRepository>();
            container.RegisterType<IMaterialIssueGuardCommonRepository, MaterialIssueGuardCommonRepository>();
            container.RegisterType<IMaterialReturnGuardRepository, MaterialReturnGuardRepository>();
            container.RegisterType<IMaterialReturnGuardDtRepository, MaterialReturnGuardDtRepository>();

            container.RegisterType<IFlashDetailRepository, FlashDetailRepository>();
            container.RegisterType<IUserMenuRightsRepository, UserMenuRightsRepository>();
            container.RegisterType<IUserDeparment, UserDeparment>();

            container.RegisterType<IVendorMaterialIssueRepository, VendorMaterialIssueRepository>();
            container.RegisterType<IVendorMaterialIssueDtRepository, VendorMaterialIssueDtRepository>();
            container.RegisterType<IVendorMaterialIssueCommonRepository, VendorMaterialIssueCommonRepository>();

            container.RegisterType<IGRNVendorRepository, GRNVendorRepository>();
            container.RegisterType<IGRNVendorDetailRepository, GRNVendorDetailRepository>();
            container.RegisterType<IGRNVendorCommonRepository, GRNVendorCommonRepository>();
            container.RegisterType<IGRNVendorItemDetailRepository, GRNVendorItemDetailRepository>();

            container.RegisterType<IDiscrepancyRepository, DiscrepancyRepository>();
            container.RegisterType<IDiscrepancyDtRepository, DiscrepancyDtRepository>();
            container.RegisterType<IDiscrepancyCommonRepository, DiscrepancyCommonRepository>();

            container.RegisterType<IAdjustmentVoucherRepository, AdjustmentVoucherRepository>();
            container.RegisterType<IAdjustmentVoucherDtRepository, AdjustmentVoucherDtRepository>();
            container.RegisterType<IAdjustmentVoucherCommonRepository, AdjustmentVoucherCommonRepository>();

            container.RegisterType<IDivisionRepository, DivisionRepository>();
            container.RegisterType<ISubDivisionRepository, SubDivisionRepository>();

            container.RegisterType<IRoadTaxRepository, RoadTaxRepository>();

            container.RegisterType<IGreenTaxRepository, GreenTaxRepository>();
            container.RegisterType<IVehicleModelRepository, VehicleModelRepository>();
            container.RegisterType<IVehicleTypeRepository, VehicleTypeRepository>();
            container.RegisterType<IVehicleInfoRepository, VehicleInfoRepository>();
            container.RegisterType<IVehicleusageRepository, VehicleusageRepository>();
            container.RegisterType<IVehicleMakeRepository, VehicleMakeRepository>();

            container.RegisterType<IStockDisposeRepository, StockDiposeRepository>();
            container.RegisterType<IStockDisposeDetailsRepository, StockDisposeDetailsRepository>();
            container.RegisterType<IStockdisposeCommonRepository, StockDiposeCommonRepository>();
            container.RegisterType<IStoreQuantityLimitsRepository, StoreQuantityLimitsRepository>();
            container.RegisterType<IInsuranceRepository, InsuranceRepository>();
            container.RegisterType<IInsuranceCompanyRepository, InsuranceCompanyRepository>();
            container.RegisterType<IPermitDetailsRepository, PermitDetailsRepository>();
            container.RegisterType<IPUCDetailsRepository, PUCDetailsRepository>();
            container.RegisterType<IDriverRepository, DriverRepository>();
            container.RegisterType<IFuelDetailsRepository, FuelDetailsRepository>();
            container.RegisterType<IDriverScheduleRepository, DriverScheduleRepository>();
            container.RegisterType<IVehicleTransferRepository, VehicleTransferRepository>();
            container.RegisterType<ISupplierBillListRepository, SupplierBillListpository>();


            container.RegisterType<IBillCreationRepository, BillCreationRepository>();
            container.RegisterType<ISupplierBillPayment, SupplierBillPayment>();
            container.RegisterType<IDashBoardRepository, DashBoardRepository>();

            container.RegisterType<IRoomRepository, RoomRepository>();
            container.RegisterType<IFloorRespository, FloorRespository>();
            container.RegisterType<IAMCProviderRepository, AMCProviderRepository>();
            container.RegisterType<ITechnicianRepository, TechnicianRepository>();
            container.RegisterType<IAssetTypeRepository, AssetTypeRepository>();
            container.RegisterType<IAssetSubTypeRepository, AssetSubTypeRepository>();

            container.RegisterType<ICallTypeRepository, CallTypeRepository>();
            container.RegisterType<IMaintainanceTypeRepository, MaintainanceTypeRepository>();

            container.RegisterType<IAssetRepository, AssetRepository>();
            container.RegisterType<IAssetDetailRepository, AssetDetailRepository>();
            container.RegisterType<IAssetCommonRepository, AssetCommonRepository>();
            container.RegisterType<IAssetLocationRepository, AssetLocationRepository>();

            container.RegisterType<IAssetScheduleRepository, AssetScheduleRepository>();
            container.RegisterType<IAssetScheduleDetailRepository, AssetScheduleDetailRepository>();
            container.RegisterType<IAssetScheduleCommonRepository, AssetScheduleCommonRepository>();
            container.RegisterType<IRequestRegisterRepository, RequestRegisterRepository>();
            container.RegisterType<IScanDocRepository, ScanDocRepository>();
            container.RegisterType<IAssetDeletionRequestRepository, AssetDeletionRequestRepository>();

            container.RegisterType<IInHouseRepository, InHouseRepository>();
            container.RegisterType<IOutsideMaintenanceRepository, OutsideMaintenanceRepository>();
            container.RegisterType<IMaterialConsumptionRepository, MaterialConsumptionRepository>();
            container.RegisterType<ITechnicianEntryRepository, TechnicianEntryRepository>();
            container.RegisterType<IInHouseCommonRepository, InHouseCommonRepository>();
            container.RegisterType<ISparePartsOuthouseUtilRepository, SparePartsOuthouseUtilRepository>();
            container.RegisterType<IOutsideMaintenanceCommonRepository, OutsideMaintenanceCommonRepository>();

            container.RegisterType<IWarrantyMaintenanceRepository, WarrantyMaintenanceRepository>();
            container.RegisterType<IWarrantySparePartsRepository, WarrantySparePartsRepository>();
            container.RegisterType<IWarrantyMaintenanceCommonRepository, WarrantyMaintenanceCommonRepository>();

            container.RegisterType<IVendorRepository, VendorRepository>();
            container.RegisterType<ILocationRepository, LocationRepository>();
            container.RegisterType<IPurchaseMaterialIndentRepository, PurchaseMaterialIndentRepository>();
            container.RegisterType<IRequestStatusRepository, RequestStatusRepository>();
            container.RegisterType<IGuardDetailsRepository, GuardDetailsRepository>();
            container.RegisterType<ITrainingTypeRepository, TrainingTypeRepository>();
            container.RegisterType<IBatchTypeRepository, BatchTypeRepository>();
            container.RegisterType<ISubjectMasterRepository, SubjectMasterRepository>();
            container.RegisterType<ITrainingCentreRepository, TrainingCentreRepository>();
            container.RegisterType<ISubjectTopicRepository, SubjectTopicRepository>();
            container.RegisterType<IBatchRepository, BatchRepository>();
            container.RegisterType<ITrainerRepository, TrainerRepository>();
            container.RegisterType<IMomentOrderRepository, MomentOrderRepository>();
            container.RegisterType<IGradeRepository, GradeRepository>();
            container.RegisterType<IScheduleRepository, ScheduleRepository>();
            container.RegisterType<IRatingRepository, RatingRepository>();
            container.RegisterType<ISlotRepository, SlotRepository>();
            container.RegisterType<ITrainingCategoryRepository, TrainingCategoryRepository>();
            container.RegisterType<IBudgetHeadsRepository, BudgetHeadsRepository>();
            container.RegisterType<IGeneralBudgetProposalRepository, GeneralBudgetProposalRepository>();

            container.RegisterType<ITrainingTemplateRepository, TrainingTemplateRepository>();
            container.RegisterType<IPettyCashRepository, PettyCashRepository>();
            container.RegisterType<IMonthlyExpenditureRepository, MonthlyExpenditureRepository>();
            container.RegisterType<ITrainingDaillyUpdatesRepository, TrainingDaillyUpdatesRepository>();
            container.RegisterType<IEmployeeEnrollmentRepository, EmployeeEnrollmentRepository>();
            container.RegisterType<IRoleAccessRepository, RoleAccessRepository>();
            config.DependencyResolver = new UnityResolver(container);
            /***********billing Area*************/
            container.RegisterType<IClientBillingRepository, ClientBillingRepository>();
            container.RegisterType<IClientBillCancelRepository, ClientBillCancelRepository>();
            container.RegisterType<ISupplierBillingRepository, SupplierBillingRepository>();
            container.RegisterType<IStoreDashboardRepository, StoreDashboardRepository>();
            container.RegisterType<IPurchaseDashBoardRepository, PurchaseDashBoardRepository>();
            container.RegisterType<IPurchaseReportsRepository, PurchaseReportsRepository>();

            /***********Configuration Area*************/
            container.RegisterType<IProjectLogicRepository, ProjectLogicRepository>();
            container.RegisterType<ISeriesLogicRepository, SeriesLogicRepository>();
            container.RegisterType<IStockViewRepository, StockViewRepository>();

            //----------------------------RFQ----------------------------//
            container.RegisterType<IRequestForQuoteRepository, RequestForQuoteRepository>();
            container.RegisterType<IRequestForQuoteDetailRepository, RequestForQuoteDetailRepository>();
            container.RegisterType<IRequestForQuoteCommonRepository, RequestForQuoteCommonRepository>();

            //----------------------------WO----------------------------//
            container.RegisterType<BISERPBusinessLayer.Repositories.Purchase.Interfaces.IWorkOrderRepository, BISERPBusinessLayer.Repositories.Purchase.Classes.WorkOrderRepository>();
            container.RegisterType<IWorkOrderDetailRepository, WorkOrderDetailRepository>();
            container.RegisterType<IWorkOrderCommonRepository, WorkOrderCommonRepository>();


            //----------------------------Marketing----------------------------//
            container.RegisterType<IEnquiryRegisterRepository, EnquiryRegisterRepository>();
            container.RegisterType<IEnquiryAllocationRepository, EnquiryAllocationRepository>();
            container.RegisterType<IOfferRegisterRepository, OfferRegisterRepository>();
            container.RegisterType<IOrderBookRepository, OrderBookRepository>();
            container.RegisterType<ISM_WorkOrderRepository, SM_WorkOrderRepository>();

            //This line wil apply below attribute to all controllers at global level
            config.Filters.Add(new GZipCompressionAttribute());


            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "WithActionApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
            "DefaultApi",
            "api/{controller}/{id}",
            new { action = "DefaultAction", id = System.Web.Http.RouteParameter.Optional }
            );

        }
    }
}
