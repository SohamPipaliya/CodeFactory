using CodeFactoryAPI.DAL;
using CodeFactoryAPI.Extra;
using CodeFactoryAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CodeFactoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginAPI : ControllerBase, IDisposable
    {

        private SignInManager<User> signInManager;

        public UserLoginAPI(SignInManager<User> signInManager)
        {
            this.signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserLogin userLogin)
        {
            try
            {
                var result = await signInManager.PasswordSignInAsync(userLogin.UserName, userLogin.Password, true, false);
                if (result.Succeeded)
                    return Ok();
                return BadRequest();
            }
            catch (Exception ex)
            {
                await ex.LogAsync().ConfigureAwait(false);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("{name:alpha:length(5,25)}")]
        public async Task<bool> Exist(string? name) =>
            await signInManager.UserManager.Users.AnyAsync(user => user.UserName == name).ConfigureAwait(false);

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
                }
                disposed = true;
            }
        }
        #endregion
    }
}
