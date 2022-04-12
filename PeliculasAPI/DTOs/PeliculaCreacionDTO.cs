using Microsoft.AspNetCore.Mvc;
using PeliculasAPI.Helpers;
using PeliculasAPI.Validations;
using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.DTOs
{
    public class PeliculaCreacionDTO: PeliculaPatchDTO
    {
        
        [FileWeightValidation(4)]
        [FileTypeValidation(GrupoTipoArchivo.Imagen)]
        public IFormFile Poster { get; set; }

        [ModelBinder(BinderType =typeof(TypeBinder<List<int>>))]
        public List<int> GenerosIds { get; set; }

        [ModelBinder(BinderType= typeof(TypeBinder<List<ActorPeliculasCreacionDTO>>))]
        public List<ActorPeliculasCreacionDTO> Actores { get; set; }
    }
}
