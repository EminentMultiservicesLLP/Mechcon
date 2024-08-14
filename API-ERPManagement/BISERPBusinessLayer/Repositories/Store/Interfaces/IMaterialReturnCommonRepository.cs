using BISERPBusinessLayer.Entities.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Store.Interfaces
{
    public interface IMaterialReturnCommonRepository 
    {
      MaterialReturnEntity SaveMREDetails(MaterialReturnEntity entity);
    //  bool AuthCancelGRN(MaterialReturnEntity entity);
    }
}
