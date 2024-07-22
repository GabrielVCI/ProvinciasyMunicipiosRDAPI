using ProvinciasyMunicipiosRDAPI.Data;
using ProvinciasyMunicipiosRDAPI.Interfaces;
using ProvinciasyMunicipiosRDAPI.Models;

namespace ProvinciasyMunicipiosRDAPI.Repository
{
    public class MunicipioRepository : IMunicipioRepository
    {
        private readonly ApplicationDbContext context;

        public MunicipioRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public bool CreateMunicipio(Municipios municipio)
        {
            context.Add(municipio);
            return Save();
        }

        public Municipios GetMunicipio(int municipioId)
        {
            return context.Municipios.Where(m => m.Id == municipioId).FirstOrDefault();
        }

        public ICollection<Municipios> GetMunicipios()
        {
            return context.Municipios.ToList();   
        }

        public bool MunicipioExists(int municipioId)
        {
            return context.Municipios.Any(m => m.Id == municipioId);
        }

        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
