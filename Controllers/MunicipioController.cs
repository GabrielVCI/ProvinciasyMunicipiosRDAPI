using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProvinciasyMunicipiosRDAPI.Data;
using ProvinciasyMunicipiosRDAPI.Data.DTO;
using ProvinciasyMunicipiosRDAPI.Interfaces;
using ProvinciasyMunicipiosRDAPI.Models;
using System.Xml.Schema;

namespace ProvinciasyMunicipiosRDAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MunicipioController : Controller
    { 
        private readonly IMunicipioRepository municipioRepository;
        private readonly IMapper mapper;
        private readonly IProvinciasRepository provinciasRepository;

        public MunicipioController(IMunicipioRepository municipioRepository, IMapper mapper, IProvinciasRepository provinciasRepository)
        {
             
            this.municipioRepository = municipioRepository;
            this.mapper = mapper;
            this.provinciasRepository = provinciasRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Municipios>))]
        public IActionResult GetMunicipios()
        {
            var municipios = mapper.Map<List<MunicipioDTO>>(municipioRepository.GetMunicipios());

            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }  

            return Ok(municipios);
        }

        [HttpGet("{municipioId}")]
        [ProducesResponseType(200, Type = typeof(Municipios))]
        public IActionResult GetMunicipio(int municipioId)
        {
            if(!municipioRepository.MunicipioExists(municipioId))
            {
                return NotFound(ModelState);
            }

            var municipio = mapper.Map<MunicipioDTO>(municipioRepository.GetMunicipio(municipioId));

            if(!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            return Ok(municipio);

        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateMunicipio([FromQuery] int provinciaId, [FromBody] MunicipioDTO municipioCreate)
        {
            if(municipioCreate == null)
            {
                return BadRequest(ModelState);
            }

            if (!provinciasRepository.ProvinciaExists(provinciaId))
            {
                ModelState.AddModelError("", $"La provincia con el Id {provinciaId} no existe");
                return StatusCode(404, ModelState);
            }

            var municipioExists = municipioRepository.GetMunicipios()
                .Where(m => m.Name.Trim().ToUpper() == municipioCreate.Name.Trim().ToUpper()).FirstOrDefault();

            if(municipioExists is not null)
            {
                ModelState.AddModelError("", "Este municipio ya existe");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var provinciaMunicipio = provinciasRepository.GetProvincia(provinciaId);
            var municipioMapper = mapper.Map<Municipios>(municipioCreate);

            municipioMapper.Provincia = provinciaMunicipio;

            if (!municipioRepository.CreateMunicipio(municipioMapper))
            {
                ModelState.AddModelError("", "Error en el servidor");
                return StatusCode(500, ModelState);
            }

            return Ok("Municipio agregado correctamente");
        }
       
    }
}
