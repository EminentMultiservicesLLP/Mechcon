using BISERPBusinessLayer.Entities.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Purchase.Interfaces
{
    public interface IPurchaseIndentCommonRepository
    {
        PurchaseIndentEntities CreatePurchaseIndent(PurchaseIndentEntities entity);
        bool UpdatePurchaseIndent(PurchaseIndentEntities entity);
        bool AuthCancelPurchaseIndent(PurchaseIndentEntities entity);
        PurchaseIndentEntities SaveIndentTemplate(PurchaseIndentEntities entity);
    }
}
