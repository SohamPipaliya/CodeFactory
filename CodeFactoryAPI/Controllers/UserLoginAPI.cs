using CodeFactoryAPI.DAL;
using CodeFactoryAPI.Extra;
using CodeFactoryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CodeFactoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginAPI : ControllerBase, IDisposable
    {
        private UnitOfWork unit;

        public UserLoginAPI(Context context) =>
            unit = context;

        [HttpPost]
        public async Task<IActionResult> Post(UserLogin user)
        {
            try
            {
                user.Password = await user.Password.EncryptAsync().ConfigureAwait(false);

                bool exist = await unit.GetUser.AnyAsync(usr => usr.UserName == user.UserName &&
                                                         usr.Password == user.Password).ConfigureAwait(false);

                return exist ? Ok() : NotFound();
            }
            catch (Exception ex)
            {
                await ex.LogAsync().ConfigureAwait(false);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("{name:alpha:length(5,25)}")]
        public async Task<bool> Exist(string? name) =>
            await unit.GetUser.AnyAsync(user => user.UserName == name).ConfigureAwait(false);

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
