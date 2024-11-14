using BISERPBusinessLayer.Entities.SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.SM.Interfaces
{
    public interface IEnquiryRegisterRepository
    {
        IEnumerable<SourceEntities> GetSources();
        IEnumerable<ProductEntities> GetProducts();
        IEnumerable<LocationEntities> GetLocations();
        IEnumerable<TypeEntities> GetTypes();
        IEnumerable<SectorEntities> GetSectors();
        IEnumerable<ZoneEntities> GetZones();
        IEnumerable<StatusEntities> GetEnqStatus();
        IEnumerable<StatusEntities> GetStatus();
        EnquiryRegisterEntities SaveEnquiry(EnquiryRegisterEntities model);
        IEnumerable<EnquiryRegisterEntities> GetEnquiry(int? statusID);
        EnquiryRegisterEntities GetEnquiryDetails(int enquiryId);
        IEnumerable<EnqRegFollowUpDetails> GetEnqRegFollowUp(int? enquiryId);
    }
}
