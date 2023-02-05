using System.ComponentModel.DataAnnotations;

namespace APIPhotos.Validations
{
    public class FileTypeValidation: ValidationAttribute
    {
        private readonly string[] tiposvalidos;

        public FileTypeValidation(string[] tiposvalidos)
        {
            this.tiposvalidos = tiposvalidos;
        }

        public FileTypeValidation(GrupoTipoArchivos grupoTipoArchivos)
        {
            if (grupoTipoArchivos==GrupoTipoArchivos.Imagen)
            {
                tiposvalidos = new string[] {"image/jpeg","image/png","image/gif" };
            }
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            IFormFile formFile = value as IFormFile;

            if (formFile == null)
            {
                return ValidationResult.Success;
            }

            if (!tiposvalidos.Contains(formFile.ContentType))
            {
                return new ValidationResult($"El tipo del archivo debe ser uno de los siguentes: {string.Join(", ",tiposvalidos)}");
            }

            return ValidationResult.Success;
        }
    }
}
