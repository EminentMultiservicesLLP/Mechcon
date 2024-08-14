using BISERPBusinessLayer.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Master.Interfaces
{
    public interface IStateMasterRepository
    {
        StateMasterEntities GetStateById(int Id);
        IEnumerable<StateMasterEntities> GetAllStates();
        bool CreateState(StateMasterEntities entity);
        bool UpdateState(StateMasterEntities entity);
        bool DeleteState(StateMasterEntities entity);
    }
}
