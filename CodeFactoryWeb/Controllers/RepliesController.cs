using CodeFactoryAPI.Models;
using CodeFactoryWeb.Extra;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CodeFactoryWeb.Controllers
{
    public class RepliesController : Controller
    {
        private HttpClient client;

        public RepliesController() =>
            client = new() { BaseAddress = Addons.HostUrl };

        public async Task<IActionResult> Index()
        {
            try
            {
                var data = await client.GetDataAsync<IEnumerable<Reply>>(APIName.RepliesAPI).ConfigureAwait(false);
                return View(data);
            }
            catch (Exception ex)
            {
                await ex.LogAsync().ConfigureAwait(false);
                return View();
            }
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            try
            {
                var reply = await client.GetDataAsync<Reply>(APIName.RepliesAPI, id).ConfigureAwait(false);
                return View(reply);
            }
            catch (Exception ex)
            {
                await ex.LogAsync().ConfigureAwait(false);
                return View();
            }
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
                    using var response = await client.AsFormDataAsync
                                                       (APIName.RepliesAPI, MethodName.PostAsync, reply, files).ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));
                    else
                    {
                        using var responseContent = response.Content;

                        if (response.StatusCode == HttpStatusCode.NotAcceptable)
                            ModelState.AddModelError(nameof(reply.Image1), await responseContent.ReadAsStringAsync().ConfigureAwait(false));
                        else if (response.StatusCode == HttpStatusCode.UnsupportedMediaType)
                            ModelState.AddModelError(nameof(reply.Image1), await responseContent.ReadAsStringAsync().ConfigureAwait(false));
                        else ModelState.AddModelError("", "Something went wrong");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Something went wrong");
                    await ex.LogAsync().ConfigureAwait(false);
                }
            }
            await SetViewBag().ConfigureAwait(false);
            return View(reply);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            var question = await client.GetDataAsync<Question>(APIName.QuestionsAPI, id).ConfigureAwait(false);
            if (question is null) return NotFound();

            await SetViewBag().ConfigureAwait(false);
            return View(question);
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
                    using var response = await client.AsFormDataAsync
                                                        (APIName.RepliesAPI, MethodName.PostAsync, reply, files, id).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));
                    else
                    {
                        using var responsecontent = response.Content;

                        if (response.StatusCode == HttpStatusCode.NotAcceptable)
                            ModelState.AddModelError(nameof(reply.Image1), await responsecontent.ReadAsStringAsync().ConfigureAwait(false));
                        else if (response.StatusCode == HttpStatusCode.UnsupportedMediaType)
                            ModelState.AddModelError(nameof(reply.Image1), await responsecontent.ReadAsStringAsync().ConfigureAwait(false));
                        else ModelState.AddModelError("", "Something went wrong");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Something went wrong");
                    await ex.LogAsync().ConfigureAwait(false);
                }
            }
            await SetViewBag().ConfigureAwait(false);
            return View(reply);
        }

        public async Task<IActionResult> Delete(Guid? id) =>
             View(await client.GetDataAsync<Question>(APIName.QuestionsAPI, id).ConfigureAwait(false));

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
            return View();
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
