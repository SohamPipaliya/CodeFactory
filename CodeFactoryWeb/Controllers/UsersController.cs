using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CodeFactoryAPI.Models;
using CodeFactoryWeb.Extra;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CodeFactoryWeb.Controllers
{
    public class UsersController : Controller
    {
        private HttpClient client;

        public UsersController() =>
            client = new() { BaseAddress = Addons.HostUrl };

        public async Task<ActionResult> Index() =>
             View(await client.GetDataAsync<IEnumerable<User>>(APIName.UsersAPI));

        public async Task<IActionResult> Details(Guid? id) =>
             View(await client.GetDataAsync<User>(APIName.UsersAPI, id));

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                Stream stream = null;
                StreamContent content = null;
                try
                {
                    using var data = new MultipartFormDataContent();
                    using var stringContent = await user.ParseToStringContentAsync();
                    data.Add(stringContent, "user");

                    if (file is not null)
                    {
                        stream = file.OpenReadStream();
                        content = new StreamContent(stream);
                        data.Add(content, "file", file.FileName);
                    }

                    using var response = await client.PostAsync("UsersAPI", data);
                    if (response.StatusCode == HttpStatusCode.Created)
                        return RedirectToAction(nameof(Index));
                    else
                    {
                        using var responseMessage = response.Content;

                        if (response.StatusCode == HttpStatusCode.AlreadyReported)
                            ModelState.AddModelError(nameof(user.UserName), await responseMessage.ReadAsStringAsync());
                        else if (response.StatusCode == HttpStatusCode.NotAcceptable)
                            ModelState.AddModelError(nameof(user.Image), await responseMessage.ReadAsStringAsync());
                        else if (response.StatusCode == HttpStatusCode.UnsupportedMediaType)
                            ModelState.AddModelError(nameof(user.Image), await responseMessage.ReadAsStringAsync());
                        else ModelState.AddModelError("", "Something went wrong");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Something went wrong");
                    await ex.LogAsync().ConfigureAwait(false);
                }
                finally
                {
                    stream?.Dispose();
                    content?.Dispose();
                }
            }
            return View(user);
        }

        public async Task<IActionResult> Edit(Guid? id) =>
            View(await client.GetDataAsync<User>(APIName.UsersAPI, id));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, User user, [FromForm] IFormFile? file)
        {
            if (id != user.User_ID)
                ModelState.AddModelError("", "userid is changed");
            else if (ModelState.IsValid)
            {
                Stream stream = null;
                StreamContent content = null;
                try
                {
                    using var data = new MultipartFormDataContent();
                    using var stringContent = await user.ParseToStringContentAsync();
                    data.Add(stringContent, "user");

                    if (file is not null)
                    {
                        stream = file.OpenReadStream();
                        content = new StreamContent(stream);
                        data.Add(content, "file", file.FileName);
                    }

                    using var response = await client.PutAsync("UsersAPI/" + id, data);
                    if (response.StatusCode == HttpStatusCode.Created)
                        return RedirectToAction(nameof(Index));
                    else
                    {
                        using var responseContent = response.Content;
                        if (response.StatusCode == HttpStatusCode.AlreadyReported)
                            ModelState.AddModelError(nameof(user.UserName), await responseContent.ReadAsStringAsync());
                        else if (response.StatusCode == HttpStatusCode.NotAcceptable)
                            ModelState.AddModelError(nameof(user.Image), await responseContent.ReadAsStringAsync());
                        else if (response.StatusCode == HttpStatusCode.UnsupportedMediaType)
                            ModelState.AddModelError(nameof(user.Image), await responseContent.ReadAsStringAsync());
                        else if (response.StatusCode == HttpStatusCode.NotFound)
                            ModelState.AddModelError("", await responseContent.ReadAsStringAsync());
                        else ModelState.AddModelError("", "Something went wrong");
                    }
                }
                catch (Exception ex)
                {
                    await ex.LogAsync().ConfigureAwait(false);
                }
                finally
                {
                    stream?.Dispose();
                    content?.Dispose();
                }
            }
            return View(user);
        }

        public async Task<IActionResult> Delete(Guid id) =>
            View(await client.GetDataAsync<User>(APIName.UsersAPI, id));

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteFinal(Guid id)
        {
            try
            {
                using (var response = await client.DeleteAsync("UsersAPI/" + id))
                {
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));
                }
            }
            catch { }
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                client?.Dispose();
            }
            client = null;
            base.Dispose(disposing);
        }
    }
}
