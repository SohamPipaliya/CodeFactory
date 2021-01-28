using Extra;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Model;
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
              View(await client.GetDataAsync<IEnumerable<Question>>(APIName.QuestionsAPI));

        public async Task<IActionResult> Details(Guid? id) =>
             View(await client.GetDataAsync<Question>(APIName.QuestionsAPI, id));

        public async Task<IActionResult> Create()
        {
            ViewData["User_ID"] = new SelectList(await client.GetDataAsync<IEnumerable<User>>(APIName.UsersAPI), "User_ID", "Email");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Question question, IFormFile[]? files)
        {
            if (ModelState.IsValid)
            {
                List<Stream> streams = null;
                List<StreamContent> streamContents = null;
                try
                {
                    using var data = new MultipartFormDataContent();
                    using var stringContent = await question.ParseToStringContentAsync();
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

                    using var response = await client.PostAsync("QuestionsAPI", data);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));
                    else
                    {
                        var msg = response.ToString();
                        using var responseContent = response.Content;

                        if (response.StatusCode == HttpStatusCode.NotAcceptable)
                            ModelState.AddModelError(nameof(question.Image), await responseContent.ReadAsStringAsync());
                        else if (response.StatusCode == HttpStatusCode.UnsupportedMediaType)
                            ModelState.AddModelError(nameof(question.Image), await responseContent.ReadAsStringAsync());
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

            ViewData["User_ID"] = new SelectList(await client.GetDataAsync<IEnumerable<User>>(APIName.UsersAPI), "User_ID", "Email");

            return View(question);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            var question = await client.GetDataAsync<Question>(APIName.QuestionsAPI, id);

            if (question is null)
                return NotFound();

            ViewData["User_ID"] = new SelectList(await client.GetDataAsync<IEnumerable<User>>(APIName.UsersAPI), "User_ID", "Email", question.User_ID);

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
                List<Stream> streams = null;
                List<StreamContent> streamContents = null;
                try
                {
                    using var data = new MultipartFormDataContent();
                    using var stringContent = await question.ParseToStringContentAsync();
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

                    using var response = await client.PutAsync("QuestionsAPI/" + id, data);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));
                    else
                    {
                        using var responsecontent = response.Content;

                        if (response.StatusCode == HttpStatusCode.NotAcceptable)
                            ModelState.AddModelError(nameof(question.Image), await responsecontent.ReadAsStringAsync());
                        else if (response.StatusCode == HttpStatusCode.NotAcceptable)
                            ModelState.AddModelError(nameof(question.Image), await responsecontent.ReadAsStringAsync());
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
                return View(question);
            }

            ViewData["User_ID"] = new SelectList(await client.GetDataAsync<IEnumerable<User>>(APIName.UsersAPI), "User_ID", "Email", question.User_ID);

            return View(question);
        }

        public async Task<IActionResult> Delete(Guid? id) =>
             View(await client.GetDataAsync<Question>(APIName.QuestionsAPI, id));

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                using var response = await client.DeleteAsync("QuestionsAPI/" + id);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Something went wrong");
            }
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
