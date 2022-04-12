using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entidades;
using PeliculasAPI.Helpers;
using PeliculasAPI.Services;

namespace PeliculasAPI.Controllers
{
    [ApiController]
    [Route("api/actores")]
    public class ActoresController: CustomBaseController
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly string contenedor = "actores";

        public ActoresController(ApplicationDbContext context, IMapper mapper, IAlmacenadorArchivos almacenadorArchivos):base(context, mapper)
        {
            this.context = context;
            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
        }

        [HttpGet]
        public  async Task<ActionResult<List<ActorDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO)
        {
            return await Get<Actor, ActorDTO>(paginacionDTO);

        }


        [HttpGet("{id:int}", Name = "obtenerActor")]
        public async Task<ActionResult<ActorDTO>> Get(int id)
        {
            return await Get<Actor, ActorDTO>(id);
        }


        [HttpPost]
        public async Task<ActionResult<ActorCreacionDTO>> Post([FromForm] ActorCreacionDTO dto)
        {
            var entidad = mapper.Map<Actor>(dto);

            if(dto.Foto != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await dto.Foto.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(dto.Foto.FileName);
                    entidad.Foto = await almacenadorArchivos.GuardarArchivo(contenido, extension, contenedor, dto.Foto.ContentType);
                }
            }
            context.Add(entidad);
            await context.SaveChangesAsync();
            var actorDTO = mapper.Map<ActorDTO>(entidad);

            return new CreatedAtRouteResult("obtenerActor", new { id = actorDTO.Id }, actorDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromForm] ActorCreacionDTO actorCreacionDTO)
        {
            var actorDB = await context.Actores.FirstOrDefaultAsync(x => x.Id == id);

            if (actorDB == null) return NotFound();

            actorDB = mapper.Map(actorCreacionDTO, actorDB);

            if (actorCreacionDTO.Foto != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await actorCreacionDTO.Foto.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(actorCreacionDTO.Foto.FileName);
                    actorDB.Foto = await almacenadorArchivos.EditarArchivo(contenido, extension, contenedor,actorDB.Foto, actorCreacionDTO.Foto.ContentType);
                }
            }

            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<ActorPatchDTO> patchDocument)
        {
            return await Patch<Actor, ActorPatchDTO>(id,patchDocument);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await Delete<Actor>(id);
        }
    }
}
