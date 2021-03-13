using CodeFactoryAPI.Extra;
using CodeFactoryAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        private UserManager<User> userManager;

        public UsersAPI(UserManager<User> userManager) =>
            (this.userManager) = (userManager);

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery(Name = "UserName")] string? UserName, [FromQuery(Name = "SearchBy")] string? SearchBy = "Name") =>
            UserName is null
            ? await this.ToActionResult(() => userManager.Users.AsUserViewmodel()).ConfigureAwait(false)
            : SearchBy == "Name"
            ? await this.ToActionResult(() => userManager.Users.Where(user => user.UserName.Contains(UserName)).AsUserViewmodel()).ConfigureAwait(false)
            : await this.ToActionResult(() => userManager.Users.Where(user => user.Email.Contains(UserName)).AsUserViewmodel()).ConfigureAwait(false);

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id) =>
            await this.ToActionResult<UserViewModel>(() => new UserViewModel(userManager.Users.FirstOrDefault(user => user.Id == id))).ConfigureAwait(false);

        [HttpPost]
        public async Task<IActionResult> PostUser([FromForm][ModelBinder(typeof(FormDataModelBinder))] UserViewModel userView, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    User user = new(userView);
                    if (await userManager.Users.AnyAsync(us => us.Id == user.Id).ConfigureAwait(false))
                        return StatusCode((int)HttpStatusCode.AlreadyReported, "Username is already taken");

                    user.Id = Guid.NewGuid().ToString();
                    user.RegistrationDate = DateTime.Now;

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

                    var result = await userManager.CreateAsync(user, userView.Password).ConfigureAwait(false);
                    if (result.Succeeded)
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

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id, [FromForm][ModelBinder(typeof(FormDataModelBinder))] UserViewModel userView, IFormFile? file)
        {
            if (id != userView.User_ID)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    User user = await userManager.FindByIdAsync(id);

                    if (user is null)
                        return NotFound();

                    user.UserName = userView.UserName;
                    user.Email = userView.Email;

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

                        user.Image = Guid.NewGuid() + user.UserName + file.FileName;
                    }

                    var result = await userManager.UpdateAsync(user).ConfigureAwait(false);
                    if (result.Succeeded)
                    {
                        if (file is not null)
                        {
                            {
                                var model = await userManager.FindByIdAsync(id).ConfigureAwait(false);
                                var path = ImagePath(model.Image);
                                if (Exists(path))
                                    System.IO.File.Delete(path);
                            }
                            return Ok();
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (!await userManager.Users.AnyAsync(us => us.Id == id).ConfigureAwait(false))
                        return NotFound();

                    if (await userManager.Users.AnyAsync(us => us.UserName == userView.UserName).ConfigureAwait(false))
                        return StatusCode((int)HttpStatusCode.AlreadyReported, "Username is already taken");

                    await ex.LogAsync().ConfigureAwait(false);
                }
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            else return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var user = await userManager.FindByIdAsync(id).ConfigureAwait(false);
                if (user is null)
                    return NotFound();
                var result = await userManager.DeleteAsync(user);

                if (result.Succeeded)
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
                    userManager.Dispose();
                }
                userManager = null;
                disposed = true;
            }
        }
        #endregion
    }
}
