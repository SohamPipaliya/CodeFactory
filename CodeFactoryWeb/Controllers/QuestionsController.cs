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
    public class QuestionsController : Controller
    {
        private HttpClient client;

        public QuestionsController() =>
            client = new() { BaseAddress = Extra.Addons.HostUrl };

        public async Task<IActionResult> Index() =>
             await this.ToActionResult(await client.GetDataAsync<IEnumerable<Question>>
                                      (APIName.QuestionsAPI).ConfigureAwait(false))
                                      .ConfigureAwait(false);

        public async Task<IActionResult> Details(Guid? id) =>
             await this.ToActionResult(await client.GetDataAsync<Question>
                                      (APIName.QuestionsAPI, id).ConfigureAwait(false))
                                      .ConfigureAwait(false);

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
        public async Task<IActionResult> Create(Question question, IFormFile[]? files)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (files is not null && files.Length > 5)
                    {
                        ModelState.AddModelError(nameof(question.Image1), "Onlys Images are allowed");
                        return View(question);
                    }

                    using var response = await client.PostAsFormDataAsync(APIName.QuestionsAPI.ToString(), question, files).ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));
                    else
                    {
                        if (response.StatusCode == HttpStatusCode.NotAcceptable)
                            ModelState.AddModelError(nameof(question.Image1), await response.Content.ReadAsStringAsync().ConfigureAwait(false));
                        else if (response.StatusCode == HttpStatusCode.UnsupportedMediaType)
                            ModelState.AddModelError(nameof(question.Image1), await response.Content.ReadAsStringAsync().ConfigureAwait(false));
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
                try
                {
                    if (files is not null && files.Length > 5)
                    {
                        ModelState.AddModelError(nameof(question.Image1), "Onlys Images are allowed");
                        return View(question);
                    }

                    using var response = await client.PutAsFormDataAsync(APIName.QuestionsAPI.ToString() + '/' + id, question, files).ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));
                    else
                    {
                        if (response.StatusCode == HttpStatusCode.NotAcceptable)
                            ModelState.AddModelError(nameof(question.Image1), await response.Content.ReadAsStringAsync().ConfigureAwait(false));
                        else if (response.StatusCode == HttpStatusCode.UnsupportedMediaType)
                            ModelState.AddModelError(nameof(question.Image1), await response.Content.ReadAsStringAsync().ConfigureAwait(false));
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
            return View(question);
        }

        [HttpPost, ActionName(nameof(Delete))]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                using var response = await client.DeleteAsync(APIName.QuestionsAPI.ToString() + '/' + id).ConfigureAwait(false);
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
