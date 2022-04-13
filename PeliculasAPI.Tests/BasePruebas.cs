using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using PeliculasAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeliculasAPI.Tests
{
    public class BasePruebas
    {
        protected ApplicationDbContext ConstruirContext(string NombreDB)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(NombreDB)
                .Options;

            var context = new ApplicationDbContext(options);
            //context.Database.EnsureCreated();

            return context;
        }

        protected IMapper ConfigurarAutoMapper()
        {
            var config = new MapperConfiguration(options =>
            {
                var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
                options.AddProfile(new AutoMapperProfiles(geometryFactory));
            });

            return config.CreateMapper();
        }
    }
}
