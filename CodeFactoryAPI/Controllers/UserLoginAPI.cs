using CodeFactory.DAL;
using CodeFactoryAPI.Extra;
using Extra;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using System;
using System.Threading.Tasks;

namespace CodeFactoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginAPI : ControllerBase, IDisposable
    {
        private UnitOfWork unit;

        public UserLoginAPI(Context context) =>
            unit = new(context);

        [HttpPost]
        public async Task<IActionResult> Post(UserLogin user)
        {
            try
            {
                user.Password = await user.Password.EncryptAsync().ConfigureAwait(false);

                bool exist = await unit.GetUser
                                       .AnyAsync(x => x.UserName == user.UserName && x.Password == user.Password)
                                       .ConfigureAwait(false);

                return exist ? Ok() : NotFound();
            }
            catch (Exception ex)
            {
                await ex.LogAsync().ConfigureAwait(false);
                return StatusCode(500);
            }
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
