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
    public class QuestionsController : Controller
    {
        private HttpClient client;

        public QuestionsController() =>
            client = new() { BaseAddress = Addons.HostUrl };

        public async Task<IActionResult> Index() =>
              View(await client.GetDataAsync<IEnumerable<Question>>(APIName.QuestionsAPI).ConfigureAwait(false));

        public async Task<IActionResult> Details(Guid? id) =>
             View(await client.GetDataAsync<Question>(APIName.QuestionsAPI, id).ConfigureAwait(false));

        public async Task<IActionResult> Create()
        {
            await SetViewBag().ConfigureAwait(false);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Question question, IFormFile[]? files)
        {
            if (ModelState.IsValid)
            {
                List<Stream>? streams = null;
                List<StreamContent>? streamContents = null;
                try
                {
                    using var data = new MultipartFormDataContent();
                    using var stringContent = await question.ParseToStringContentAsync().ConfigureAwait(false);
                    data.Add(stringContent, "question");

                    if (files is not null && files.Length > 0)
                    {
                        streams = new();
                        streamContents = new();

                        for (int i = 0; i < files.Length; i++)
                        {
                            streams.Add(files[i].OpenReadStream());
                            streamContents.Add(new(streams[i]));
                            data.Add(streamContents[i], "files", files[i].FileName);
                        }
                    }

                    using var response = await client.PostAsync("QuestionsAPI", data).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));
                    else
                    {
                        using var responseContent = response.Content;

                        if (response.StatusCode == HttpStatusCode.NotAcceptable)
                            ModelState.AddModelError(nameof(question.Image1), await responseContent.ReadAsStringAsync().ConfigureAwait(false));
                        else if (response.StatusCode == HttpStatusCode.UnsupportedMediaType)
                            ModelState.AddModelError(nameof(question.Image1), await responseContent.ReadAsStringAsync().ConfigureAwait(false));
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
                    streams?.ForEachDispose();
                    streamContents?.ForEachDispose();
                }
            }
            await SetViewBag().ConfigureAwait(false);
            return View(question);
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
        public async Task<IActionResult> Edit(Guid id, Question question, IFormFile[]? files)
        {
            if (id != question.Question_ID)
                return NotFound();
            if (ModelState.IsValid)
            {
                List<Stream>? streams = null;
                List<StreamContent>? streamContents = null;
                try
                {
                    using var data = new MultipartFormDataContent();
                    using var stringContent = await question.ParseToStringContentAsync().ConfigureAwait(false);
                    data.Add(stringContent, "question");

                    if (files is not null && files.Length > 0)
                    {
                        streams = new();
                        streamContents = new();

                        for (int i = 0; i < files.Length; i++)
                        {
                            streams.Add(files[i].OpenReadStream());
                            streamContents.Add(new(streams[i]));
                            data.Add(streamContents[i], "files", files[i].FileName);
                        }
                    }

                    using var response = await client.PutAsync("QuestionsAPI/" + id, data).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));
                    else
                    {
                        using var responsecontent = response.Content;

                        if (response.StatusCode == HttpStatusCode.NotAcceptable)
                            ModelState.AddModelError(nameof(question.Image1), await responsecontent.ReadAsStringAsync().ConfigureAwait(false));
                        else if (response.StatusCode == HttpStatusCode.NotAcceptable)
                            ModelState.AddModelError(nameof(question.Image1), await responsecontent.ReadAsStringAsync().ConfigureAwait(false));
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
                    streams?.ForEachDispose();
                    streamContents?.ForEachDispose();
                }
            }
            await SetViewBag().ConfigureAwait(false);
            return View(question);
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
            ViewData["Tag_ID"] = new SelectList(await client.GetDataAsync<IEnumerable<Tag>>(APIName.TagsAPI).ConfigureAwait(false), "Tag_ID", "Name");
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
