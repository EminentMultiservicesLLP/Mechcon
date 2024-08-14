using BISERPBusinessLayer.Entities.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Transport.Interfaces
{
    public interface IDivisionRepository
    {
      int CreateDivision(DivisionMaster Entity);
      bool UpdateDivision(DivisionMaster Entity);
      IEnumerable<DivisionMaster> GetActiveDivision(int UserId);
      IEnumerable<DivisionMaster> GetAllDivision();
  
    }
}
