using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Common
{
    public class ActionResponseStatus
    {
        public string ResponseMessage { get; set; }
        public string ResponseStatus { get; set; }
        public bool Sucess { get; set; }
        public bool Error { get; set; }
    }
}
