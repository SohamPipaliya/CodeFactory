using CodeFactoryAPI.Extra;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CodeFactoryWeb.Controllers
{
    [Route("Error")]
    public class ErrorController : Controller
    {
        [Route("{statuscode}")]
        public async Task<IActionResult> StatusCodeResult(int statuscode)
        {
            if (statuscode is 404)
                return View("NotFound");
            if (statuscode is 500)
                return await Error().ConfigureAwait(false);
            return View();
        }

        [Route("Error")]
        public async Task<IActionResult> Error()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            if (exception is not null)
                await exception.Error.LogAsync().ConfigureAwait(false);
            return View("Error");
        }
    }
}
