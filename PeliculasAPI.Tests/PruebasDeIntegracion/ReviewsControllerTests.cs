using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using PeliculasAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PeliculasAPI.Tests.PruebasDeIntegracion
{
    [TestClass]
    public class ReviewsControllerTests : BasePruebas
    {
        private static readonly string url = "/api/peliculas/1/reviews";

        [TestMethod]
        public async Task ObtenerReviewPeliculaInexistente()
        {
            var nombreDB = Guid.NewGuid().ToString();
            var factory = ConstruirWebApplicationFactory(nombreDB);

            var cliente = factory.CreateClient();
            var respuesta = await cliente.GetAsync(url);

            Assert.AreEqual(404, (int)respuesta.StatusCode);

        }

        [TestMethod]
        public async Task ObtenerReviewListadoVacio()
        {
            var nombreDB = Guid.NewGuid().ToString();
            var factory = ConstruirWebApplicationFactory(nombreDB);
            var context = ConstruirContext(nombreDB);

            context.Peliculas.Add(new Entidades.Pelicula() { Titulo = "Pelicula1" });
            await context.SaveChangesAsync();

            var cliente = factory.CreateClient();
            var respuesta = await cliente.GetAsync(url);
            respuesta.EnsureSuccessStatusCode();

            var reviews = JsonConvert.DeserializeObject<List<ReviewDTO>>(await respuesta.Content.ReadAsStringAsync());
            Assert.AreEqual(0, reviews.Count);

        }

    }
}
