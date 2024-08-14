using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Masters
{
   public  class BranchMasterEntities
    {

        public int BranchID { get; set; }

        public string BranchCode { get; set; }

        public string BranchName { get; set; }
        public string Address { get; set; }
        public string Pin { get; set; }

        public string ContactPerson1 { get; set; }
        public string ContactPerson2 { get; set; }
        public string ContactPerson3 { get; set; }

        public Nullable<int> CityID { get; set; }
       
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Fax { get; set; }

        public string Email { get; set; }
        public string Website { get; set; }
        public bool Center { get; set; }

        public Nullable<int> ProjectID { get; set; }
        public string Society { get; set; }
        public string Landmark { get; set; }

        public Nullable<int> StateID { get; set; }
        public Nullable<int> CountryId { get; set; }
        public Nullable<int> VillageId { get; set; }
        public string Reportfooter { get; set; }

        // public  LogoRight { get; set; }
        public string UnitOf { get; set; }
        public string UpdatedMacName { get; set; }

        public string UpdatedMacID { get; set; }

        public string UpdatedIPAddress { get; set; }

        public Nullable<int> UpdatedBy { get; set; }

        public Nullable<System.DateTime> UpdatedOn { get; set; }

        public Nullable<int> InsertedBy { get; set; }

        public Nullable<System.DateTime> InsertedON { get; set; }

        public string InsertedMacName { get; set; }

        public string InsertedMacID { get; set; }

        public string InsertedIPAddress { get; set; }

      
        public bool Deactive { get; set; }
    }
}
