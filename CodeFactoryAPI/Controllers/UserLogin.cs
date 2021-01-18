using CodeFactory.DAL;
using CodeFactoryAPI.Extra;
using Extra;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CodeFactoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLogin : ControllerBase, IDisposable
    {
        private UnitOfWork unit;

        public UserLogin(Context context)
        {
            unit = new(context);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Models.Model.UserLogin user)
        {
            try
            {
                user.Password = await user.Password.EncryptAsync().ConfigureAwait(false);
                bool exist = await unit.GetUser.AnyAsync(x => x.UserName == user.UserName && x.Password == user.Password).ConfigureAwait(false);
                if (exist)
                    return NoContent();
                return Unauthorized();
            }
            catch (Exception ex)
            {
                await ex.LogAsync().ConfigureAwait(false);
                return StatusCode(500);
            }
        }

        [NonAction]
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        [NonAction]
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                unit?.Dispose();
            }
            unit = null;
        }
    }
}
