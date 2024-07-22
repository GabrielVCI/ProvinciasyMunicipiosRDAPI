using ProvinciasyMunicipiosRDAPI.Models;

namespace ProvinciasyMunicipiosRDAPI.Interfaces
{
    public interface IProvinciasRepository
    {
        ICollection<Provincias> GetProvincias();
        ICollection<Municipios> GetMunicipiosFromAProvincia(int provinciaId);
        Provincias GetProvincia(int provinciaId);
        bool ProvinciaExists(int provinciaId);
        bool CreateProvincia(Provincias provincias);
        bool Save();

        bool UpdateProvincia(Provincias provincias);
    }
}
