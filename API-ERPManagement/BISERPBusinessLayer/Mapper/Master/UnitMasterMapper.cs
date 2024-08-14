using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BISERPBusinessLayer.Entities.Masters;
using System.Data;


namespace BISERPBusinessLayer.Mapper.Master
{
    public class UnitMasterMapper
    {
        public UnitMasterEntities Map(IDataReader reader, UnitMasterEntities unitMaster)
        {
            UnitMasterEntities unit = new UnitMasterEntities();

            //AutoMapper.Mapper.CreateMap<IDataReader, UnitMasterEntities>()
            //            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => (int)src["ID"]))
            //            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src["Code"]))
            //            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src["Name"]));
            return unit;
        }
    }
}
