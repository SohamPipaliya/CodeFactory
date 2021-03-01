using CodeFactoryAPI.Extra;
using CodeFactoryAPI.Models;
using CodeFactoryWeb.Extra;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CodeFactoryWeb.Controllers
{
    public class RepliesController : Controller
    {
        private HttpClient client;

        public RepliesController() =>
            client = new() { BaseAddress = Extra.Addons.HostUrl };

        public async Task<IActionResult> Index() =>
            await this.ToActionResult<IEnumerable<Reply>>(() => client.GetDataAsync<IEnumerable<Reply>>
                                      (APIName.RepliesAPI)).ConfigureAwait(false);

        public async Task<IActionResult> Details(Guid? id) =>
            await this.ToActionResult<Reply>(() => client.GetDataAsync<Reply>
                                      (APIName.RepliesAPI, id)).ConfigureAwait(false);

        public async Task<IActionResult> Delete(Guid? id) =>
             await Details(id).ConfigureAwait(false);

        public async Task<IActionResult> Edit(Guid? id)
        {
            await SetViewBag().ConfigureAwait(false);
            return await Details(id).ConfigureAwait(false);
        }

        public async Task<IActionResult> Create()
        {
            await SetViewBag().ConfigureAwait(false);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reply reply, IFormFile[]? files)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (files is not null && files.Length > 5)
                    {
                        ModelState.AddModelError(nameof(reply.Image1), "Onlys Images are allowed");
                        return View(reply);
                    }

                    using var response = await client.PostAsFormDataAsync(APIName.RepliesAPI.ToString(), reply, files)
                                .ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        if (response.StatusCode == HttpStatusCode.NotAcceptable)
                            ModelState.AddModelError(nameof(reply.Image1), await response.Content.ReadAsStringAsync().ConfigureAwait(false));
                        else if (response.StatusCode == HttpStatusCode.UnsupportedMediaType)
                            ModelState.AddModelError(nameof(reply.Image1), await response.Content.ReadAsStringAsync().ConfigureAwait(false));
                        else ModelState.AddModelError("", "Something went wrong");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Something went wrong");
                    await ex.LogAsync().ConfigureAwait(false);
                    return RedirectToAction("Error", "Error");
                }
            }
            await SetViewBag().ConfigureAwait(false);
            return View(reply);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Reply reply, IFormFile[]? files)
        {
            if (id != reply.Reply_ID)
                return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    if (files is not null && files.Length > 5)
                    {
                        ModelState.AddModelError(nameof(reply.Image1), "Onlys Images are allowed");
                        return View(reply);
                    }

                    using var response = await client.PutAsFormDataAsync(APIName.RepliesAPI.ToString() + '/' + id, reply, files)
                                .ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        if (response.StatusCode == HttpStatusCode.NotAcceptable)
                            ModelState.AddModelError(nameof(reply.Image1), await response.Content.ReadAsStringAsync().ConfigureAwait(false));
                        else if (response.StatusCode == HttpStatusCode.UnsupportedMediaType)
                            ModelState.AddModelError(nameof(reply.Image1), await response.Content.ReadAsStringAsync().ConfigureAwait(false));
                        else ModelState.AddModelError("", "Something went wrong");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Something went wrong");
                    await ex.LogAsync().ConfigureAwait(false);
                    return RedirectToAction("Error", "Error");
                }
            }
            await SetViewBag().ConfigureAwait(false);
            return View(reply);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                using var response = await client.DeleteAsync("QuestionsAPI/" + id).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Something went wrong");
            }
            return RedirectToAction("Error", "Error");
        }

        private async Task SetViewBag()
        {
            ViewData["User_ID"] = new SelectList(await client.GetDataAsync<IEnumerable<User>>(APIName.UsersAPI).ConfigureAwait(false), "User_ID", "Email");
            ViewData["Question_ID"] = new SelectList(await client.GetDataAsync<IEnumerable<Question>>(APIName.QuestionsAPI).ConfigureAwait(false), "Question_ID", "Title");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                client.Dispose();
            }
            client = null;
            base.Dispose(disposing);
        }
    }
}
