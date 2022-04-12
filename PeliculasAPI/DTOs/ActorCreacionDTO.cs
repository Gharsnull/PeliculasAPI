using PeliculasAPI.Validations;
using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.DTOs
{
    public class ActorCreacionDTO:ActorPatchDTO
    {
       
        [FileWeightValidation(4)]
        [FileTypeValidation(grupoTipoArchivo:GrupoTipoArchivo.Imagen)]
        public IFormFile Foto { get; set; }
    }
}
