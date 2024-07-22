using ProvinciasyMunicipiosRDAPI.Data;
using ProvinciasyMunicipiosRDAPI.Interfaces;
using ProvinciasyMunicipiosRDAPI.Models;

namespace ProvinciasyMunicipiosRDAPI.Repository
{
    public class ProvinciasRepository : IProvinciasRepository
    {
        private readonly ApplicationDbContext context; 
        public ProvinciasRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public bool CreateProvincia(Provincias provincias)
        {
            context.Add(provincias);
            return Save();
        }

        public ICollection<Municipios> GetMunicipiosFromAProvincia(int provinciaId)
        {
            return context.Municipios.Where(m => m.Provincia.Id == provinciaId).ToList();
        }

        public Provincias GetProvincia(int provinciaId)
        {
            return context.Provincias.Where(p => p.Id == provinciaId).FirstOrDefault();
        }

        public ICollection<Provincias> GetProvincias()
        {
            return context.Provincias.ToList();
        }

        public bool ProvinciaExists(int provinciaId)
        {
            return context.Provincias.Any(p => p.Id == provinciaId);
        }

        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0 ? true : false;
        } 
        public bool UpdateProvincia(Provincias provincias)
        {
            context.Update(provincias);
            return Save();
        }
    }
}
