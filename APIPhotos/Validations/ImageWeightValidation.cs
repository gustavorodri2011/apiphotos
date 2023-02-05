using System.ComponentModel.DataAnnotations;

namespace APIPhotos.Validations
{
    public class ImageWeightValidation : ValidationAttribute
    {
        private readonly int maxWeightInMB;

        public ImageWeightValidation(int maxWeightInMB)
        {
            this.maxWeightInMB = maxWeightInMB;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value==null)
            {
                return ValidationResult.Success;
            }

            IFormFile formFile = value as IFormFile;

            if (formFile==null)
            {
                return ValidationResult.Success;
            }

            if (formFile.Length>maxWeightInMB * 1024 *1024)
            {
                return new ValidationResult($"El peso del archivo no debe ser mayor a {maxWeightInMB} MB");
            }
            return ValidationResult.Success;
        }
    }
}
