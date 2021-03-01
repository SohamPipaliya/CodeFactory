using CodeFactoryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CodeFactoryWeb.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("Error/{statuscode}")]
        public IActionResult StatusCodeResult(int statuscode)
        {
            if (statuscode is not 400)
                return View();
            return View("NotFound");
        }
    }
}
