using ProvinciasyMunicipiosRDAPI.Models;

namespace ProvinciasyMunicipiosRDAPI.Interfaces
{
    public interface IMunicipioRepository
    {
        ICollection<Municipios> GetMunicipios(); 
        Municipios GetMunicipio(int municipioId); 
        bool CreateMunicipio(Municipios municipios); 
        bool Save(); 
        bool MunicipioExists(int municipioId);

    }
}
