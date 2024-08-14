using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Asset
{
    public class AssetLocationEntity
    {
        public int AssetLocationId { get; set; }
        public int LocationId { get; set; }
        public string AssetCode { get; set; }
        public int AssetId { get; set; }
        public string ItemName { get; set; }
        public string SerialNo { get; set; }
        public string ModelNo { get; set; }
        public string SupplierName { get; set; }
        public string Manufacturer { get; set; }
        public int AssetTypeId { get; set; }
        public string AssetType { get; set; }
        public int AssetSubTypeId { get; set; }
        public string AssetSubType { get; set; }
        public string Description { get; set; }
        public int DepartmentId { get; set; }
        public int BranchId { get; set; }
        public string Branch { get; set; }
        public int FloorId { get; set; }
        public string Floor { get; set; }
        public int RoomId { get; set; }
        public string Room { get; set; }
        public string LifeTime { get; set; }
        public bool IsWarranty { get; set; }
        public Nullable<DateTime> WarrantyStart { get; set; }
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
        public string LegacyPONo { get; set; }
        public string FreeService { get; set; }
        public bool IsFreeService { get; set; }
        public Nullable<DateTime> FreeServiceStartDate { get; set; }
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
        public bool PermanentDeactive { get; set; }
        public string LocationName { get; set; }
        public string AcquiredDate { get; set; }
        public string ItemCode { get; set; }
        public string Remarks { get; set; }
        public int ItemId { get; set; }

    }
}
