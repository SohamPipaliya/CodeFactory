using CodeFactoryWeb.Extra;
using Microsoft.AspNetCore.Mvc;
using CodeFactoryAPI.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CodeFactoryWeb.Controllers
{
    public class UsersLoginController : Controller
    {
        private HttpClient client;

        public UsersLoginController() =>
            client = new() { BaseAddress = Extra.Addons.HostUrl };

        public IActionResult Login() =>
             View();

        [HttpPost]
        public async Task<IActionResult> Login(UserLogin userLogin)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using var content = await userLogin.ParseToStringContentAsync();
                    using var response = await client.PostAsync("UserLoginAPI", content);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction("Index", nameof(UsersController));
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                        ModelState.AddModelError("", "Invalid Username or password");
                }
            }
            catch
            {
                ModelState.AddModelError("", "Something went wrong");
            }
            return View(userLogin);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                client?.Dispose();
            client = null;
            GC.SuppressFinalize(this);
            base.Dispose(disposing);
        }
    }
}
