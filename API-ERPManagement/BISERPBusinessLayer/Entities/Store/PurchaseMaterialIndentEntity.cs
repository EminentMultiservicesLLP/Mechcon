using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Store
{
    public class PurchaseMaterialIndentEntity
    {
        public int Id { get; set; }
        public int IndentId { get; set; }
        public int IndentDetailId { get; set; }
        public int PIndentId { get; set; }
    }
}
