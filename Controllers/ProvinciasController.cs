using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProvinciasyMunicipiosRDAPI.Data.DTO;
using ProvinciasyMunicipiosRDAPI.Interfaces;
using ProvinciasyMunicipiosRDAPI.Models;

namespace ProvinciasyMunicipiosRDAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvinciasController : Controller
    {
        private readonly IProvinciasRepository provinciasRepository;
        private readonly IMapper mapper;

        public ProvinciasController(IProvinciasRepository provinciasRepository, IMapper mapper)
        {
            this.provinciasRepository = provinciasRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Provincias>))]
        public IActionResult GetProvincias()
        {
            var provincias = mapper.Map<List<ProvinciaDTO>>(provinciasRepository.GetProvincias());

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(provincias);
        }

        [HttpGet("{provinciaId}")]
        [ProducesResponseType(200, Type = typeof(Provincias))]
        [ProducesResponseType(400)]
        public IActionResult GetCategory(int provinciaId)
        {
            if (!provinciasRepository.ProvinciaExists(provinciaId))
            {
                return NotFound();
            }

            var provincia = mapper.Map<ProvinciaDTO>(provinciasRepository.GetProvincia(provinciaId));

            if (!ModelState.IsValid)
            {
                return BadRequest(provincia);
            }

            return Ok(provincia);
        }

        [HttpGet("{provinciaId}/municipios")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Municipios>))]
        [ProducesResponseType(400)]
        public IActionResult GetMunicipiosFromAProvincia(int provinciaId)
        {
            if (!provinciasRepository.ProvinciaExists(provinciaId))
            {
                return NotFound(ModelState);
            }
            var municipios = mapper.Map<List<MunicipioDTO>>(provinciasRepository.GetMunicipiosFromAProvincia(provinciaId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(municipios);

        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateProvincia([FromBody] ProvinciaDTO provincia)
        {
            if (provincia is null)
            {
                return BadRequest(ModelState);
            }

            var provinciaExists = provinciasRepository.GetProvincias()
                .Where(c => c.Name.Trim().ToUpper() == provincia.Name.Trim().ToUpper()).FirstOrDefault();

            if (provinciaExists is not null)
            {
                ModelState.AddModelError(string.Empty, "Provincia ya existente");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var provinciaMapped = mapper.Map<Provincias>(provincia);
            if (!provinciasRepository.CreateProvincia(provinciaMapped))
            {
                ModelState.AddModelError(string.Empty, "Something went wrong");
                return StatusCode(500, ModelState);
            }

            return Ok("Provincia creada exitosamente!");
        }
    }
}
