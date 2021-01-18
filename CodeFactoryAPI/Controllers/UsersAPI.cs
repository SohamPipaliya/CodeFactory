using CodeFactory.DAL;
using CodeFactoryAPI.Extra;
using Extra;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using static CodeFactoryAPI.Extra.Addons;

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
        public IEnumerable<User> Get() =>
            unit.GetUser.GetAll();


        [HttpGet("{id}")]
        public async Task<User> Get(Guid id) =>
            await unit.GetUser.FindAsync(id).ConfigureAwait(false);


        [HttpPost]
        public async Task<IActionResult> Post([FromForm][ModelBinder(BinderType = typeof(FormDataModelBinder))] User user, IFormFile? file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var exist = await unit.GetUser.AnyAsync(x => x.UserName == user.UserName);
                    if (exist)
                        return StatusCode(208, "Username is already taken");
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
                                using (var fs = new FileStream(ImagePath(user.Image), FileMode.Create))
                                {
                                    await file.CopyToAsync(fs).ConfigureAwait(false);
                                    await fs.FlushAsync().ConfigureAwait(false);
                                }
                            }
                            else
                                return StatusCode(406, "Image size must be between 50 KB to 1 MB");
                        }
                        else
                            return StatusCode(415, "Select valid Image");
                    }
                    unit.GetUser.Add(user);
                    if (await unit.SaveAsync() > 0)
                        return StatusCode(201);
                    else
                        return StatusCode(500);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                await ex.LogAsync().ConfigureAwait(false);
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] User user)
        {
            if (id != user.User_ID)
                return BadRequest();
            try
            {
                if (ModelState.IsValid)
                {
                    user.Password = await user.Password.EncryptAsync().ConfigureAwait(false);
                    unit.GetUser.context.Attach(user);
                    unit.GetUser.context.Entry(user)
                        .SetUpdatedColumns("UserName", "Password", "Email");
                    if (await unit.SaveAsync() > 0)
                        return Ok();
                    else
                        return StatusCode(500);
                }
                return BadRequest();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var exist = await unit.GetUser.AnyAsync(x => x.UserName == user.UserName);
                if (exist)
                {
                    return StatusCode(208, "Username is already taken");
                }
                exist = await unit.GetUser.AnyAsync(x => x.User_ID == id).ConfigureAwait(false);
                if (!exist)
                {
                    return NotFound();
                }
                await ex.LogAsync().ConfigureAwait(false);
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await unit.GetUser.RemoveAsync(id);
                if (await unit.SaveAsync() > 0)
                    return Ok();
            }
            catch (Exception ex)
            {
                var exist = await unit.GetUser
                   .AnyAsync(x => x.User_ID == id).ConfigureAwait(false);
                if (!exist)
                    return NotFound();
                await ex.LogAsync().ConfigureAwait(false);
            }
            return StatusCode(500);
        }

        #region Dispose
        private bool disposed = false;

        [NonAction]
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        [NonAction]
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
