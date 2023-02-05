using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using System.Net.NetworkInformation;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIPhotos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {

        //[HttpGet("{nroTramite}")]
        [HttpGet("tramite/{ntramite}")]
        public IActionResult Get(string ntramite)
        {
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "Images", ntramite + ".png");
                var image = System.IO.File.OpenRead(path);
                return File(image, "image/png");
            }
            catch (Exception)
            {
                return NotFound($"No existe foto para el tramite: {ntramite}.");
            }
        }


        [HttpPost]
        public async Task<IActionResult> PostImage([FromForm] Photo photo)
        {
            if (photo.Imagen == null || photo.Imagen.Length == 0)
            {
                return BadRequest("No se ha enviado una imagen válida");
            }

            var extension = Path.GetExtension(photo.Imagen.FileName);


            var filename = $"{photo.Tramite}{extension}";

            var path = Path.Combine(Directory.GetCurrentDirectory(), "Images", filename);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await photo.Imagen.CopyToAsync(stream);
            }

            return Ok($"Imagen [{filename}] guardada correctamente");
        }

        [HttpDelete]
        public void Delete()
        {
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Images");

            DirectoryInfo directory = new DirectoryInfo(folderPath);
            FileInfo[] files = directory.GetFiles();

            // Eliminar cada archivo
            foreach (FileInfo file in files)
            {
                file.Delete();
            }
        }
    }
}
