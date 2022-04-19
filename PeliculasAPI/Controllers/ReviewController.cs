using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entidades;
using PeliculasAPI.Helpers;
using System.Security.Claims;

namespace PeliculasAPI.Controllers
{
    [Route("api/peliculas/{peliculaId:int}/reviews")]
    [ServiceFilter(typeof(PeliculaExisteAttribute))]
    
    public class ReviewController: CustomBaseController
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ReviewController(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ReviewDTO>>> Get(int peliculaId, [FromQuery] PaginacionDTO paginacionDTO)
        {
            var queryable = context.Reviews.Include(x => x.Usuario).AsQueryable();
            queryable = queryable.Where(x => x.PeliculaId == peliculaId);
            return await Get<Review, ReviewDTO>(paginacionDTO, queryable);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post(int peliculaId, [FromBody] ReviewCreacionDTO reviewCreacionDTO)
        {
            var usuarioId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

            var reviewExiste = await context.Reviews.AnyAsync(x => x.PeliculaId == peliculaId && x.UsuarioId == usuarioId);
            if (reviewExiste) return BadRequest("Ya has realizado una reseña para esta pelicula");

            var review = mapper.Map<Review>(reviewCreacionDTO);
            review.PeliculaId = peliculaId;
            review.UsuarioId = usuarioId;

            context.Add(review);
            var resultado = await context.SaveChangesAsync();
            if (resultado > 0)
            {
                return NoContent();
            }
            return BadRequest();
        }

        [HttpPut("{reviewId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Put(int peliculaId, int reviewId, [FromBody] ReviewCreacionDTO reviewCreacionDTO)
        {
            var review = await context.Reviews.FirstOrDefaultAsync(x => x.Id == reviewId);
            if (review == null) return NotFound();

            var usuarioId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            if (review.UsuarioId != usuarioId) return Forbid("No puedes modificar una reseña que no es tuya");

            review = mapper.Map(reviewCreacionDTO, review);
            var resultado = await context.SaveChangesAsync();
            if (resultado > 0)
            {
                return NoContent();
            }
            return BadRequest();
        }

        [HttpDelete("{reviewId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Delete(int reviewId)
        {
            var review = await context.Reviews.FirstOrDefaultAsync(x => x.Id == reviewId);
            if (review == null) return NotFound();

            var usuarioId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            if (review.UsuarioId != usuarioId) return Forbid("No puedes eliminar una reseña que no es tuya");

            context.Remove(review);
            var resultado = await context.SaveChangesAsync();
            if (resultado > 0)
            {
                return NoContent();
            }
            return BadRequest();
        }

    }
}
