using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPBusinessLayer.Entities.Masters;

namespace BISERPBusinessLayer.Repositories.Master.Interfaces
{
   public  interface IPaymentTermsMasterRepository
    {

        IEnumerable<PaymentTermsMasterEntities> GetAllPayment();
        IEnumerable<PaymentTermsMasterEntities> GetActiveTerms();
        PaymentTermsMasterEntities GetPaymentById(int termid);
        int CreatePayment(PaymentTermsMasterEntities paymentEntity);
        bool UpdatePaymentTerm(PaymentTermsMasterEntities unitEntity);
        bool CheckDuplicateItem(string code);
        bool CheckDuplicateupdate(string code, int ID);
    }
}
