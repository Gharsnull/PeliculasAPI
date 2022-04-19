using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetTopologySuite;
using NetTopologySuite.Geometries;
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
    public class SalasDeCineControlletTests : BasePruebas
    {
        [TestMethod]
        public async Task ObtenerSalasDecine5Km()
        {
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            //using (var context = LocalDbInitializer.GetDbContextLocalDb(false))
            //{
            //    var salasDeCine = new List<SalaDeCine>()
            //    {
            //         new SalaDeCine{Nombre = "Agora", Ubicacion = geometryFactory.CreatePoint(new Coordinate(-69.9388777, 18.4839233))}
            //    };

            //    context.AddRange(salasDeCine);
            //    await context.SaveChangesAsync();
            //}

            var filtro = new SalaDeCineCercanoFiltroDTO()
            {
                DistanciaEnkms = 5,
                Latitud = 18.481139,
                Longitud = -69.938950
            };

            using (var context = LocalDbInitializer.GetDbContextLocalDb(false))
            {
                var mapper = ConfigurarAutoMapper();
                var controller = new SalasDeCineController(context, mapper, geometryFactory);
                var respuesta = await controller.Cercanos(filtro);

                var valor = respuesta.Value;

                Assert.AreEqual(2, valor.Count);
            }
        }
    }
}
