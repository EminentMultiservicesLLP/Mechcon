using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPBusinessLayer.Entities.Masters;

namespace BISERPBusinessLayer.Repositories.Master.Interfaces
{
   public interface IDeliveryTermsMasterRepository
    {
       IEnumerable<DeliveryTermsMasterEntities> GetAllDeliveryTerms();
       IEnumerable<DeliveryTermsMasterEntities> GetActiveTerms();
       
       DeliveryTermsMasterEntities GetDeliveryTermById(int termid);
       int CreateDeliveryTerm(DeliveryTermsMasterEntities entity);
       bool UpdateDeliveryTerm(DeliveryTermsMasterEntities entity);
       bool CheckDuplicateItem(string code);
       bool CheckDuplicateupdate(string code, int ID);
    }
}
