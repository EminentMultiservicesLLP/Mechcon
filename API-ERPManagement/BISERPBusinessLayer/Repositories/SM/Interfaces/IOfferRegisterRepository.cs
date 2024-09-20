using BISERPBusinessLayer.Entities.SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.SM.Interfaces
{
    public interface IOfferRegisterRepository
    {
        IEnumerable<EnquiryRegisterEntities> GetEnqForOffer(int UserID);
        OfferDetailEntities GetOfferID(int EnquiryID);
        OfferRegisterEntities SaveOffer(OfferRegisterEntities model);
        IEnumerable<OfferRegisterEntities> GetOffer(int UserID);
        IEnumerable<OfferDetailEntities> GetOfferDetails(int OfferRegisterID);
    }
}
