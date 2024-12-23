using BISERPBusinessLayer.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Master.Interfaces
{
    public interface IProductMasterRepository
    {
        IEnumerable<ProductMasterEntities> GetAllProduct();
        int CreateProduct(ProductMasterEntities entity);
        bool UpdateProduct(ProductMasterEntities entity);
        bool CheckDuplicateItem(string code);
        bool CheckDuplicateupdate(string code, int ID);
    }
}
