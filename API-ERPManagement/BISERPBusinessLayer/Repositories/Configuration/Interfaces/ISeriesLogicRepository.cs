using BISERPBusinessLayer.Entities.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Configuration.Interfaces
{
    public interface ISeriesLogicRepository
    {
        IEnumerable<SeriesLogicEntity> GetSeriesLogic();
    }
}
