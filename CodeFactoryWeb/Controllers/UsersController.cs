using CodeFactoryAPI.Extra;
using CodeFactoryAPI.Models;
using CodeFactoryWeb.Extra;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            client = new() { BaseAddress = Extra.Addons.HostUrl };

        public async Task<IActionResult> Index(string? UserName, string SearchBy) =>
            await this.ToActionResult<IEnumerable<UserViewModel>>(() => client.GetUsersAsync(UserName, SearchBy));

        public async Task<IActionResult> Details(string? id) =>
             await this.ToActionResult<UserViewModel>(() => client.GetDataAsync<UserViewModel>
                                      (APIName.UsersAPI, id)).ConfigureAwait(false);

        public async Task<IActionResult> Edit(string? id) =>
            await Details(id).ConfigureAwait(false);

        public async Task<IActionResult> Delete(string? id) =>
            await Details(id).ConfigureAwait(false);

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserViewModel user, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                Stream stream = null;
                StreamContent content = null;
                try
                {
                    using var data = new MultipartFormDataContent();
                    using var stringContent = await user.ParseToStringContentAsync().ConfigureAwait(false);
                    data.Add(stringContent, "userView");

                    if (file is not null)
                    {
                        stream = file.OpenReadStream();
                        content = new StreamContent(stream);
                        data.Add(content, "file", file.FileName);
                    }

                    using var response = await client.PostAsync(APIName.UsersAPI.ToString(), data).ConfigureAwait(false);
                    if (response.StatusCode == HttpStatusCode.OK)
                        return RedirectToAction(nameof(Index));
                    else
                    {
                        if (response.StatusCode == HttpStatusCode.AlreadyReported)
                            ModelState.AddModelError(nameof(user.UserName), await response.Content.ReadAsStringAsync().ConfigureAwait(false));
                        else if (response.StatusCode == HttpStatusCode.NotAcceptable || response.StatusCode == HttpStatusCode.UnsupportedMediaType)
                            ModelState.AddModelError(nameof(user.Image), await response.Content.ReadAsStringAsync().ConfigureAwait(false));
                        else ModelState.AddModelError("", "Something went wrong");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Something went wrong");
                    await ex.LogAsync().ConfigureAwait(false);
                    return RedirectToAction("Error", "Error");
                }
                finally
                {
                    stream?.Dispose();
                    content?.Dispose();
                }
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, User user, [FromForm] IFormFile? file)
        {
            if (id != user.Id)
                ModelState.AddModelError("", "userid is changed");
            else if (ModelState.IsValid)
            {
                Stream stream = null;
                StreamContent content = null;
                try
                {
                    using var data = new MultipartFormDataContent();
                    using var stringContent = await user.ParseToStringContentAsync().ConfigureAwait(false);
                    data.Add(stringContent, "user");

                    if (file is not null)
                    {
                        stream = file.OpenReadStream();
                        content = new StreamContent(stream);
                        data.Add(content, "file", file.FileName);
                    }

                    using var response = await client.PutAsync(APIName.UsersAPI.ToString() + '/' + id, data).ConfigureAwait(false);
                    if (response.StatusCode == HttpStatusCode.OK)
                        return RedirectToAction(nameof(Index));
                    else
                    {
                        if (response.StatusCode == HttpStatusCode.AlreadyReported)
                            ModelState.AddModelError(nameof(user.UserName), await response.Content.ReadAsStringAsync().ConfigureAwait(false));
                        else if (response.StatusCode == HttpStatusCode.NotAcceptable || response.StatusCode == HttpStatusCode.UnsupportedMediaType)
                            ModelState.AddModelError(nameof(user.Image), await response.Content.ReadAsStringAsync().ConfigureAwait(false));
                        else if (response.StatusCode == HttpStatusCode.NotFound)
                            ModelState.AddModelError("", "No user Found");
                        else ModelState.AddModelError("", "Something went wrong");
                    }
                }
                catch (Exception ex)
                {
                    await ex.LogAsync().ConfigureAwait(false);
                    return RedirectToAction("Error", "Error");
                }
                finally
                {
                    stream?.Dispose();
                    content?.Dispose();
                }
            }
            return View(user);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteFinal(Guid id)
        {
            try
            {
                using var response = await client.DeleteAsync(APIName.UsersAPI.ToString() + '/' + id).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
            }
            catch { }
            return RedirectToAction("Error", "Error");
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
