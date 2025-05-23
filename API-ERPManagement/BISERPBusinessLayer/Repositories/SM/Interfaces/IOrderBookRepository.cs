﻿using BISERPBusinessLayer.Entities.SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.SM.Interfaces
{
    public interface IOrderBookRepository
    {
        IEnumerable<EnquiryRegisterEntities> GetEnqForOrderBook(int UserID);
        IEnumerable<ConsigneeEntities> GetConsignee(int? enquiryId);
        OfferDetailEntities GetFinalOffer(int EnquiryID);
        OrderBookEntities SaveOrderBook(OrderBookEntities model);
        IEnumerable<OrderBookEntities> GetOrderBook(int UserID);
        IEnumerable<OrderBookOtherDetail> GetOBOtherDetails(int OrderBookID);
        IEnumerable<ProjectTCDetails> GetOrderBookProjectTC(int orderBookID);
        IEnumerable<PaymentTermDetails> GetOrderBookPaymentTerms(int orderBookID);
        IEnumerable<DeliveryTermDetails> GetOrderBookDeliveryTerms(int orderBookID);
        IEnumerable<OtherTermDetails> GetOrderBookOtherTerms(int orderBookID);
        IEnumerable<BasisTermDetails> GetOrderBookBasisTerms(int orderBookID);
        IEnumerable<IncoTermEntities> GetIncoTerm();
        IEnumerable<OrderBookEntities> GetOrderBookForRpt(int UserID);
    }
}
