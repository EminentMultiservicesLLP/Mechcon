using BISERPBusinessLayer.Entities.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Transport.Interfaces
{
    public interface ISubDivisionRepository
    {
        int CreateSubDivision(SubDivisionMasterEntity Entity);
        bool UpdateSubDivision(SubDivisionMasterEntity Entity);
        IEnumerable<SubDivisionMasterEntity> GetActiveSubDivision(int UserId);
        IEnumerable<SubDivisionMasterEntity> GetAllSubDivision();
    }
}
