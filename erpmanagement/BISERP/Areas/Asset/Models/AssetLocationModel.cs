using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Asset.Models
{
    public class AssetLocationModel
    {
        public int AssetLocationId { get; set; }

        [Display(Name = "Location")]
        public int LocationId { get; set; }

        [Display(Name="Asset Code")]
        public string AssetCode { get; set; }
        public int AssetId { get; set; }

        [Display(Name = "Asset Name")]
        public string ItemName { get; set; }

        [Display(Name = "Serial No")]
        public string SerialNo { get; set; }

        [Display(Name = "Model")]
        public string ModelNo { get; set; }

        [Display(Name = "Supplier")]
        public string SupplierName { get; set; }

        [Display(Name = "Manufacturer")]
        public string Manufacturer { get; set; }

        [Display(Name = "Asset Type")]
        public int AssetTypeId { get; set; }
        public string AssetType { get; set; }

        [Display(Name = "Asset SubType")]
        public int AssetSubTypeId { get; set; }
        public string AssetSubType { get; set; }

        [Display(Name = "Product Description")]
        public string Description { get; set; }
        public int DepartmentId { get; set; }

        [Display(Name = "Branch")]
        public int BranchId { get; set; }
        public string Branch { get; set; }

        [Display(Name = "Floor")]
        public int FloorId { get; set; }
        public string Floor { get; set; }

        [Display(Name = "Room")]
        public int RoomId { get; set; }
        public string Room { get; set; }
        public string LifeTime { get; set; }

        [Display(Name = "Warranty")]
        public bool IsWarranty { get; set; }

        [Display(Name = "From ")]
        public Nullable<DateTime> WarrantyStart { get; set; }

        [Display(Name = "To")]
        public Nullable<DateTime> WarrantyExpire { get; set; }
        public string strWarrantyStart { get; set; }
        public string strWarrantyExpire { get; set; }
        public string Attachment { get; set; }
        public string FileName { get; set; }
        public Nullable<DateTime> DueDate { get; set; }
        public Nullable<DateTime> InstallationDate { get; set; }
        public string EquipmentCondition { get; set; }
        public string EquipmentStatus { get; set; }
        public string MaintenanceStatus { get; set; }
        public int Authorization { get; set; }

        [Display(Name = "PO No")]
        public string LegacyPONo { get; set; }
        public string FreeService { get; set; }

        [Display(Name = "Free Service")]
        public bool IsFreeService { get; set; }

        [Display(Name = "From")]
        public Nullable<DateTime> FreeServiceStartDate { get; set; }

        [Display(Name = "To")]
        public Nullable<DateTime> FreeServiceEndDate { get; set; }
        public string strFreeServiceStartDate { get; set; }
        public string strFreeServiceEndDate { get; set; }
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

        [Display(Name = "Deactive")]
        public bool PermanentDeactive { get; set; } 
        public string LocationName { get; set; }
    }
}