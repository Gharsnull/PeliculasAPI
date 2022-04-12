using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using PeliculasAPI.Helpers;
using PeliculasAPI.Services;
using System.Text;

namespace PeliculasAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container.
            services.AddAutoMapper(typeof(Startup));
            //save files to azure
            services.AddTransient<IAlmacenadorArchivos, AlmacenadorArchivosAzure>();

            //save files in local
            //services.AddTransient<IAlmacenadorArchivos, AlmacenadorArchivosLocal>();
            //services.AddHttpContextAccessor();

            services.AddSingleton<GeometryFactory>(NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326));
            services.AddSingleton(provider =>
            
            new MapperConfiguration(config =>
            {
                var geometryFactory = provider.GetRequiredService<GeometryFactory>();
                config.AddProfile(new AutoMapperProfiles(geometryFactory));
            }).CreateMapper()
            );

            services.AddScoped<PeliculaExisteAttribute>();

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
            sqlServerOptions=> sqlServerOptions.UseNetTopologySuite()));
            

            services.AddControllers().AddNewtonsoftJson();

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["jwt:key"])),
                ClockSkew = TimeSpan.Zero
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
            }

            app.UseHttpsRedirection();
            //this allows to load static files (the ones sotred in the solution)
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
    }
}
