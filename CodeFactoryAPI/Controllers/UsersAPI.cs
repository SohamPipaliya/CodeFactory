using CodeFactoryAPI.DAL;
using CodeFactoryAPI.Extra;
using CodeFactoryAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using static CodeFactoryAPI.Extra.Addons;
using static System.IO.File;
using static Utf8Json.JsonSerializer;

namespace CodeFactoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersAPI : ControllerBase, IDisposable
    {
        private UnitOfWork unit;

        public UsersAPI(Context context) =>
            unit = new(context);

        [HttpGet]
        public IActionResult Get() =>
           Ok(ToJsonString(unit.GetUser.GetAll()));

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var user = await unit.GetUser.FindAsync(id).ConfigureAwait(false);
                return user == null ? NotFound() : Ok(ToJsonString(user));
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm][ModelBinder(BinderType = typeof(FormDataModelBinder))] User user, IFormFile? file)
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
                        return StatusCode((int)HttpStatusCode.Created);
                }
                catch (Exception ex)
                {
                    await ex.LogAsync().ConfigureAwait(false);
                }
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            return BadRequest();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromForm][ModelBinder(BinderType = typeof(FormDataModelBinder))] User user, IFormFile? file)
        {
            if (id != user.User_ID) return BadRequest();
            else if (ModelState.IsValid)
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

                        var model = await unit.GetUser.Model
                                              .AsNoTracking()
                                              .FirstAsync(x => x.User_ID == id)
                                              .ConfigureAwait(false);

                        if (model?.Image is not null)
                        {
                            var path = ImagePath(model.Image);
                            if (Exists(path))
                                System.IO.File.Delete(path);
                        }

                        unit.GetUser.Context.Entry(user)
                            .SetUpdatedColumns("UserName", "Password", "Email", "Image");
                    }
                    else
                        unit.GetUser.Context.Entry(user)
                            .SetUpdatedColumns("UserName", "Password", "Email");

                    if (await unit.SaveAsync().ConfigureAwait(false) > 0)
                        return Ok();
                }
                catch (Exception ex)
                {
                    var model = await unit.GetUser
                                        .FindAsync(id)
                                        .ConfigureAwait(false);
                    if (model is null)
                        return NotFound("No user found");
                    else if (user.UserName == model.UserName)
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
                var model = await unit.GetUser.RemoveAsync(id);

                if (model?.Entity.Image is not null)
                {
                    var path = ImagePath(model.Entity.Image);
                    if (Exists(path))
                        System.IO.File.Delete(path);
                }

                if (await unit.SaveAsync().ConfigureAwait(false) > 0)
                    return Ok();
            }
            catch (Exception ex)
            {
                var exist = await unit.GetUser
                                    .FindAsync(id)
                                    .ConfigureAwait(false);

                if (exist == null) return NotFound();

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
                    unit?.Dispose();
                }
                unit = null;
            }
        }
        #endregion
    }
}
