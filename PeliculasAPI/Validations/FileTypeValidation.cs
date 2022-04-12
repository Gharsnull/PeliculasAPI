using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Validations
{
    public class FileTypeValidation: ValidationAttribute
    {
        private readonly string[] validTypes;

        public FileTypeValidation(string[] validTypes)
        {
            this.validTypes = validTypes;
        }

        public FileTypeValidation(GrupoTipoArchivo grupoTipoArchivo)
        {
            if(grupoTipoArchivo == GrupoTipoArchivo.Imagen)
            {
                validTypes = new string[] { "image/jpeg", "image/png", "image/gif" };
            }
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;
            IFormFile formfile = value as IFormFile;
            Console.WriteLine(formfile.ContentType);

            if (formfile == null) return ValidationResult.Success;

            if (!validTypes.Contains(formfile.ContentType)) 
                return new ValidationResult($"El tipo del archivo debe ser uno de los siguientes: {string.Join(", ", validTypes)}");

            return ValidationResult.Success;
        }
    }
}
