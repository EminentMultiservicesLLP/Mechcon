using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using BISERPBusinessLayer.Entities.Common;

namespace BISERPBusinessLayer.Repositories.Common
{
    public interface IFlashDetailRepository
    {
        IEnumerable<FlashEntity> GetFlashDetails(int typeId, int userid);
    }
}
