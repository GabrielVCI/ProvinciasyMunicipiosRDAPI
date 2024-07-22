namespace ProvinciasyMunicipiosRDAPI.Models
{
    public class Municipios
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Provincias Provincia { get; set; }
    }
}
