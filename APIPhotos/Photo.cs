using APIPhotos.Validations;

namespace APIPhotos
{
    public class Photo
    {
        public string Tramite { get; set; }

        [ImageWeightValidation(maxWeightInMB: 4)]
        [FileTypeValidation(grupoTipoArchivos:GrupoTipoArchivos.Imagen)]
        public IFormFile Imagen { get; set; }

        
    }
}
