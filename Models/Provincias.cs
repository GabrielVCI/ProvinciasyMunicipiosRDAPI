namespace ProvinciasyMunicipiosRDAPI.Models
{
    public class Provincias
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Municipios> Municipios { get; set; } 
    }
}
