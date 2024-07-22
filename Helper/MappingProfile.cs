using AutoMapper;
using ProvinciasyMunicipiosRDAPI.Data.DTO;
using ProvinciasyMunicipiosRDAPI.Models;

namespace ProvinciasyMunicipiosRDAPI.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
             CreateMap<Provincias, ProvinciaDTO>().ReverseMap(); 
             CreateMap<Municipios, MunicipioDTO>().ReverseMap(); 
        }
    }
}
