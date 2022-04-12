using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entidades;

namespace PeliculasAPI.Controllers
{
    [ApiController]
    [Route("api/SalasDeCine")]
    public class SalasDeCineController: CustomBaseController
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly GeometryFactory geometryFactory;

        public SalasDeCineController(ApplicationDbContext context, IMapper mapper, GeometryFactory geometryFactory): base(context, mapper)
        {
            this.context = context;
            this.mapper = mapper;
            this.geometryFactory = geometryFactory;
        }

        [HttpGet]
        public async Task<ActionResult<List<SalaDeCineDTO>>> Get()
        {
            return await Get<SalaDeCine, SalaDeCineDTO>();
        }

        [HttpGet("{id:int}", Name = "obtenerSalaDecine")]
        public async Task<ActionResult<SalaDeCineDTO>> Get(int id)
        {
            return await Get<SalaDeCine, SalaDeCineDTO>(id);
        }

        [HttpGet("Cercanos")]
        public async Task<ActionResult<List<SalaDeCineCercanoDTO>>> Cercanos([FromQuery] SalaDeCineCercanoFiltroDTO filtro)
        {
            var ubicacionUsuario = geometryFactory.CreatePoint(new Coordinate(filtro.Longitud, filtro.Latitud));

            var salasDeCine = await context.SalasDeCine
                .OrderBy(x => x.Ubicacion.Distance(ubicacionUsuario))
                .Where(x => x.Ubicacion.IsWithinDistance(ubicacionUsuario, filtro.DistanciaEnkms * 1000))
                .Select(x => new SalaDeCineCercanoDTO
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    Latitud = x.Ubicacion.Y,
                    Longitud = x.Ubicacion.X,
                    DistanciaEnMetros = Math.Round(x.Ubicacion.Distance(ubicacionUsuario))
                }).ToListAsync();
            return salasDeCine;
        }

        [HttpPost]
        public async Task<ActionResult<SalaDeCineCreacionDTO>> Post([FromBody] SalaDeCineCreacionDTO dto)
        {
            return await Post<SalaDeCineCreacionDTO, SalaDeCine, SalaDeCineDTO>(dto, "obtenerSalaDecine");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] SalaDeCineCreacionDTO salaCreacionDTO)
        {
            return await Put<SalaDeCineCreacionDTO, SalaDeCine>(id, salaCreacionDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await Delete<SalaDeCine>(id);
        }
    }
}
