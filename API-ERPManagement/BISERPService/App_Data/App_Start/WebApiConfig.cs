using Microsoft.Practices.Unity;
using BISEPRService.Resolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Entity = BISERPBusinessLayer.Entities;
using BISERPBusinessLayer.Repositories.Master.Interfaces;
using BISERPBusinessLayer.Repositories.Master.Classes;
using BISERPBusinessLayer.Repositories.Store;
using BISERPBusinessLayer.Repositories.Purchase.Interfaces;
using BISERPBusinessLayer.Repositories.Purchase.Classes;
using BISERPBusinessLayer.Repositories.Store.Interfaces;
using BISERPBusinessLayer.Repositories.Store.Classes;

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


            container.RegisterType<IItemTypeMasterRepository, ItemTypeMasterRepository>();

            container.RegisterType<ICityMasterRepository, CityMasterRepository>();
            container.RegisterType<IStateMasterRepository, StateMasterRepository>();
            container.RegisterType<IVillageMasterRepository, VillageMasterRepository>();
            container.RegisterType<ICountryMasterRepository, CountryMasterRepository>();

            container.RegisterType<IItemGroupMasterRepository, ItemGroupMasterRepository>();
            container.RegisterType<ISupplierMasterRepository, SupplierMasterRepository>();
            container.RegisterType<IStoreMasterRepository, StoreMasterRepository>();
            container.RegisterType<IStoreTypeMasterRepository, StoreTypeMasterRepository>();
            container.RegisterType<IManufacturerMasterRepository, ManufacturerMasterRepository>();
            container.RegisterType<IItemMasterRepository, ItemMasterRepository>();
            container.RegisterType<IDeliveryTermsMasterRepository, DeliveryTermsMasterRepository>();
            container.RegisterType<IPaymentTermsMasterRepository, PaymentTermsMasterRepository>();
            container.RegisterType<IOthersTermsMasterRepository, OthersTermsMasterRepository>();
            container.RegisterType<IMaterilaIndentRepository, MaterilaIndentRepository>();
            container.RegisterType<IMaterialIndentDetailsRepository, MaterialIndentDetailsRepository>();

            container.RegisterType<IPurchaseIndentDetailRepository, PurchaseIndentDetailRepository>();
            container.RegisterType<IPurchaseIndentRepository, PurchaseIndentRepository>();
            container.RegisterType<IMaterialIssueRepository, MaterialIssueRepository>();

            container.RegisterType<IBranchMasterRepository, BranchMasterRepository>();

            container.RegisterType<IStockRegisterRepository, StockRegisterRepository>();
            container.RegisterType<ITaxMasterRepository, TaxMasterRepository>();
            config.DependencyResolver = new UnityResolver(container);

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
