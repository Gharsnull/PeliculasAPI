using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeliculasAPI.Controllers;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeliculasAPI.Tests.PruebasUnitarias
{
    [TestClass]
    public class GenerosControllerTests: BasePruebas
    {
        [TestMethod]
        public async Task ObtenerTodosLosGeneros()
        {
            //preparacion
            var nombreDB = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreDB);
            var mapper = ConfigurarAutoMapper();

            contexto.Generos.Add(new Genero() { Nombre = "Accion" });
            contexto.Generos.Add(new Genero() { Nombre = "Terror" });
            await contexto.SaveChangesAsync();

            var contexto2 = ConstruirContext(nombreDB);

            //prueba
            var controller = new GenerosController(contexto2, mapper);
            var respuesta = await controller.Get();

            //verificacion
            var generos = respuesta.Value;

            Assert.AreEqual(2, generos.Count);
        }

        [TestMethod]
        public async Task ObtenerGeneroPorIdNoExistente()
        {
            //preparacion
            var nombreDB = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreDB);
            var mapper = ConfigurarAutoMapper();

            var controller = new GenerosController(contexto, mapper);

            var respuesta = await controller.Get(1);

            var resultado = respuesta.Result as StatusCodeResult;
            Assert.AreEqual(404, resultado.StatusCode);

        }

        [TestMethod]
        public async Task ObtenerGeneroPorIdExistente()
        {
            //preparacion
            var nombreDB = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreDB);
            var mapper = ConfigurarAutoMapper();

            contexto.Generos.Add(new Genero() { Nombre = "Accion" });
            contexto.Generos.Add(new Genero() { Nombre = "Terror" });
            await contexto.SaveChangesAsync();

            var contexto2 = ConstruirContext(nombreDB);

            //prueba
            var id = 1;
            var controller = new GenerosController(contexto2, mapper);
            var respuesta = await controller.Get(id);

            //verificacion
            var genero = respuesta.Value;
            Assert.AreEqual(id, genero.Id);
        }

        [TestMethod]
        public async Task CrearGenero()
        {
            //preparacion
            var nombreDB = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreDB);
            var mapper = ConfigurarAutoMapper();

            var controller = new GenerosController(contexto, mapper);

            var nuevoGenero = new GeneroCreacionDTO()
            {
                Nombre = "Accion"
            };

            //prueba
            var respuesta = await controller.Post(nuevoGenero);

            //verificacion
            var resultado = respuesta as CreatedAtRouteResult;
            Assert.IsNotNull(resultado);

            var contexto2 = ConstruirContext(nombreDB);
            var cantidad = await contexto2.Generos.CountAsync();
            Assert.AreEqual(1, cantidad);
        }


        [TestMethod]
        public async Task ActualizarGenero()
        {
            var nombreDB = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreDB);
            var mapper = ConfigurarAutoMapper();

            contexto.Generos.Add(new Genero() { Nombre = "Accion" });
            await contexto.SaveChangesAsync();

            var contexto2 = ConstruirContext(nombreDB);
            var controller = new GenerosController(contexto2, mapper);

            var generoCreacionDTO = new GeneroCreacionDTO()
            {
                Nombre = "Accion 2"
            };

            var id = 1;
            var respuesta = await controller.Put(id, generoCreacionDTO);

            var resultado = respuesta as NoContentResult;
            Assert.IsNotNull(resultado);

            var contexto3 = ConstruirContext(nombreDB);
            var genero = await contexto3.Generos.FirstOrDefaultAsync(x => x.Id == id);
            Assert.AreEqual("Accion 2", genero.Nombre);
        }

        [TestMethod]
        public async Task EliminarGeneroNoExistente()
        {
            var nombreDB = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreDB);
            var mapper = ConfigurarAutoMapper();

            var controller = new GenerosController(contexto, mapper);

            var respuesta = await controller.Delete(1);
            var resultado = respuesta as NotFoundResult;
        }

        [TestMethod]
        public async Task EliminarGeneroExistente()
        {
            var nombreDB = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreDB);
            var mapper = ConfigurarAutoMapper();

            contexto.Generos.Add(new Genero() { Nombre = "Accion" });
            await contexto.SaveChangesAsync();

            var contexto2 = ConstruirContext(nombreDB);
            var controller = new GenerosController(contexto2, mapper);

            var respuesta = await controller.Delete(1);
            var resultado = respuesta as NoContentResult;
            Assert.IsNotNull(resultado);

            var contexto3 = ConstruirContext(nombreDB);
            var cantidad = await contexto3.Generos.CountAsync();
            Assert.AreEqual(0, cantidad);
        }

    }
}
