using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeliculasAPI.Controllers;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entidades;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasAPI.Tests.PruebasUnitarias
{
    [TestClass]
    public class ReviewsController: BasePruebas
    {
        [TestMethod]
        public async Task UsuarioNoPuedreCrearDosReviewsParaMismaPelicula()
        {
            var nombreDB = Guid.NewGuid().ToString();
            var context = ConstruirContext(nombreDB);
            CrearPeliculas(nombreDB);

            var peliculaId = context.Peliculas.Select(x=> x.Id).FirstOrDefault();
            var review1 = new Review()
            {
                PeliculaId = peliculaId,
                UsuarioId = usuarioPorDefectoId,
                Puntuacion = 5
            };

            context.Add(review1);

            await context.SaveChangesAsync();

            var contexto2 = ConstruirContext(nombreDB);
            var mapper = ConfigurarAutoMapper();

            var controller = new ReviewController(contexto2, mapper);
            controller.ControllerContext = ConstruirControllerContext();

            var reviewCreacionDTO = new ReviewCreacionDTO { Puntuacion = 5 };
            var respuesta = await controller.Post(peliculaId, reviewCreacionDTO);

            var valor = respuesta as IStatusCodeActionResult;
            Assert.AreEqual(400, valor.StatusCode.Value);
        }

        [TestMethod]
        public async Task CrearReview()
        {
            var nombreDB = Guid.NewGuid().ToString();
            var context = ConstruirContext(nombreDB);
            CrearPeliculas(nombreDB);

            var peliculaId = context.Peliculas.Select(x => x.Id).FirstOrDefault();
            var contexto2 = ConstruirContext(nombreDB);
            var mapper = ConfigurarAutoMapper();

            var controller = new ReviewController(contexto2, mapper);
            controller.ControllerContext = ConstruirControllerContext();

            var reviewCreacionDTO = new ReviewCreacionDTO { Puntuacion = 5 };
            var respuesta = await controller.Post(peliculaId, reviewCreacionDTO);

            var valor = respuesta as NoContentResult;
            Assert.IsNotNull(valor);

            var contexto3 = ConstruirContext(nombreDB);
            var reviewDb = contexto3.Reviews.First();
            Assert.AreEqual(usuarioPorDefectoId, reviewDb.UsuarioId);
        }


        private void CrearPeliculas(string nombreDB)
        {
            var contexto = ConstruirContext(nombreDB);
            contexto.Peliculas.Add(new Pelicula() { Titulo = "Pelicula 1" });
            contexto.SaveChanges();
        }
    }
}
