using BISERPBusinessLayer.Entities.SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.SM.Interfaces
{
    public interface IEnquiryAllocationRepository
    {
        IEnumerable<EnquiryRegisterEntities> GetEnqForAllocation(int? statusID);
        EnquiryAllocationEntities SaveAllocation(EnquiryAllocationEntities model);
        IEnumerable<EnquiryAllocationEntities> GetAllocation();
    }
}
