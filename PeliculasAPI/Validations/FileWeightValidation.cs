using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Validations
{
    public class FileWeightValidation : ValidationAttribute
    {
        private readonly int pesoMaximoEnMegaBytes;

        public FileWeightValidation(int PesoMaximoEnMegaBytes)
        {
            pesoMaximoEnMegaBytes = PesoMaximoEnMegaBytes;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value == null) return ValidationResult.Success;

            IFormFile formfile =  value as IFormFile;

            if (formfile == null) return ValidationResult.Success;

            if(formfile.Length > pesoMaximoEnMegaBytes * 1024 * 1024)
            {
                return new ValidationResult($"El peso del archivo no debe ser mayor a {pesoMaximoEnMegaBytes}MB");
            }

            return ValidationResult.Success;

        }
    }
}
