using Extra;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using static CodeFactoryAPI.Extra.Addons;
using static System.IO.File;

namespace CodeFactoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserImages : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> InsertImage([FromForm] IFormFile File)
        {
            //var files = Request.Form.Files; for multiple files or List<IFormFile> or IFormCollection
            try
            {
                if (File is not null)
                {
                    var extension = Path.GetExtension(File.FileName).ToUpper();
                    if (extension is ".JPG" || extension is ".PNG" || extension is ".JPEG")
                    {
                        if (File.Length > 50000 && File.Length < 1048500)
                        {
                            using (var stream = new FileStream(ImagePath(File.FileName), FileMode.Create))
                            {
                                await File.CopyToAsync(stream).ConfigureAwait(false);
                                await stream.FlushAsync().ConfigureAwait(false);
                            }
                            return NoContent();
                        }
                        return StatusCode(406);
                    }
                    return StatusCode(415);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                await ex.LogAsync().ConfigureAwait(false);
                return StatusCode(500);
            }
        }

        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteImage(string name)
        {
            try
            {
                var path = ImagePath(name);
                if (Exists(path))
                    Delete(path);
                return NoContent();
            }
            catch (Exception ex)
            {
                await ex.LogAsync().ConfigureAwait(false);
                return StatusCode(500);
            }
        }
    }
}
