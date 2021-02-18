using CodeFactoryAPI.DAL;
using CodeFactoryAPI.Extra;
using CodeFactoryAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using static CodeFactoryAPI.Extra.Addons;
using static System.IO.File;

namespace CodeFactoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersAPI : ControllerBase, IDisposable
    {
        private UnitOfWork unit;

        public UsersAPI(Context context) =>
            unit = context;

        [HttpGet]
        public async Task<IActionResult> GetUsers() =>
            await this.ToActionResult(SerializeToJson<IEnumerable<User>>(unit.GetUser.Model)).ConfigureAwait(false);

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUser(Guid id) =>
            await this.ToActionResult(SerializeToJson<User>(await unit.GetUser
                                                        .FindAsync(user => user.User_ID == id)
                                                        .ConfigureAwait(false))).ConfigureAwait(false);

        [HttpPost]
        public async Task<IActionResult> PostUser([FromForm][ModelBinder(typeof(FormDataModelBinder))] User user, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var exist = await unit.GetUser
                                        .AnyAsync(x => x.UserName == user.UserName)
                                        .ConfigureAwait(false);
                    if (exist)
                        return StatusCode((int)HttpStatusCode.AlreadyReported, "Username is already taken");

                    user.User_ID = Guid.NewGuid();
                    user.RegistrationDate = DateTime.Now;
                    user.Password = await user.Password.EncryptAsync().ConfigureAwait(false);

                    if (file is not null)
                    {
                        var extension = Path.GetExtension(file.FileName).ToUpper();
                        if (extension is ".JPG" || extension is ".PNG" || extension is ".JPEG")
                        {
                            if (file.Length > 51200 && file.Length < 1073741825)
                            {
                                user.Image = Guid.NewGuid() + " - " + user.UserName + extension;
                                using var fs = new FileStream(ImagePath(user.Image), FileMode.Create);

                                await file.CopyToAsync(fs).ConfigureAwait(false);
                                await fs.FlushAsync().ConfigureAwait(false);
                                await fs.DisposeAsync().ConfigureAwait(false);
                            }
                            else return StatusCode((int)HttpStatusCode.NotAcceptable, "Image size must be between 50 KB to 1 MB");
                        }
                        else return StatusCode((int)HttpStatusCode.UnsupportedMediaType, "Select valid Image");
                    }

                    unit.GetUser.Add(user);
                    if (await unit.SaveAsync().ConfigureAwait(false) > 0)
                        return Ok();
                }
                catch (Exception ex)
                {
                    await ex.LogAsync().ConfigureAwait(false);
                }
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            else return BadRequest();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> PutUser(Guid id, [FromForm][ModelBinder(typeof(FormDataModelBinder))] User user, IFormFile? file)
        {
            if (id != user.User_ID)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    unit.GetUser.Context.Attach(user);
                    user.Password = await user.Password.EncryptAsync().ConfigureAwait(false);

                    if (file is not null)
                    {
                        var extension = Path.GetExtension(file.FileName).ToUpper();
                        if (extension is ".JPG" || extension is ".PNG" || extension is ".JPEG")
                        {
                            if (file.Length > 51200 && file.Length < 1073741825)
                            {
                                user.Image = Guid.NewGuid() + " - " + user.UserName + extension;
                                using var fs = new FileStream(ImagePath(user.Image), FileMode.Create);

                                await file.CopyToAsync(fs).ConfigureAwait(false);
                                await fs.FlushAsync().ConfigureAwait(false);
                                await fs.DisposeAsync().ConfigureAwait(false);
                            }
                            else return StatusCode((int)HttpStatusCode.NotAcceptable, "Image size must be between 50 KB to 1 MB");
                        }
                        else return StatusCode((int)HttpStatusCode.UnsupportedMediaType, "Select valid Image");

                        unit.GetUser.Context.Entry(user)
                            .SetUpdatedColumns(nameof(user.UserName), nameof(user.Password), nameof(user.Email), nameof(user.Image));
                    }
                    else
                        unit.GetUser.Context.Entry(user)
                            .SetUpdatedColumns(nameof(user.UserName), nameof(user.Password), nameof(user.Email));

                    if (await unit.SaveAsync().ConfigureAwait(false) > 0)
                    {
                        if (file is not null)
                        {
                            var model = await unit.GetUser.Model.AsNoTracking()
                                                  .FirstAsync(x => x.User_ID == id)
                                                  .ConfigureAwait(false);
                            var path = ImagePath(model.Image);
                            if (Exists(path))
                                System.IO.File.Delete(path);
                        }
                        return Ok();
                    }
                }
                catch (Exception ex)
                {
                    if (!await unit.GetUser.AnyAsync(user => user.User_ID == id).ConfigureAwait(false))
                        return NotFound();

                    if (await unit.GetUser.AnyAsync(us => us.UserName == user.UserName).ConfigureAwait(false))
                        return StatusCode((int)HttpStatusCode.AlreadyReported, "Username is already taken");

                    await ex.LogAsync().ConfigureAwait(false);
                }
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            else return BadRequest();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var user = await unit.GetUser.FindAsync(user => user.User_ID == id)
                                             .ConfigureAwait(false);
                if (user is null)
                    return NotFound();

                unit.GetUser.Remove(user);

                if (await unit.SaveAsync().ConfigureAwait(false) > 0)
                {
                    if (user.Image is not null)
                    {
                        var path = ImagePath(user.Image);
                        if (Exists(path))
                            System.IO.File.Delete(path);
                    }
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                await ex.LogAsync().ConfigureAwait(false);
            }
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        #region Dispose
        private bool disposed = false;

        [NonAction]
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    unit.Dispose();
                }
                unit = null;
                disposed = true;
            }
        }
        #endregion
    }
}
