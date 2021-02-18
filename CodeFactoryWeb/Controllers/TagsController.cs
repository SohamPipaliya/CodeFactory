using CodeFactoryAPI.Extra;
using CodeFactoryAPI.Models;
using CodeFactoryWeb.Extra;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CodeFactoryWeb.Controllers
{
    public class TagsController : Controller
    {
        private HttpClient client;

        public TagsController() =>
            client = new() { BaseAddress = Extra.Addons.HostUrl };

        public async Task<IActionResult> Index() =>
             await this.ToActionResult(await client.GetDataAsync<IEnumerable<Tag>>
                                      (APIName.TagsAPI).ConfigureAwait(false))
                                      .ConfigureAwait(false);

        public async Task<IActionResult> Details(Guid id) =>
             await this.ToActionResult(await client.GetDataAsync<Tag>
                                      (APIName.TagsAPI, id).ConfigureAwait(false))
                                      .ConfigureAwait(false);

        public async Task<IActionResult> Edit(Guid id) =>
            await Details(id).ConfigureAwait(false);

        public async Task<IActionResult> Delete(Guid id) =>
            await Details(id).ConfigureAwait(false);

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tag tag)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using var response = await client.PostAsJsonAsync(APIName.TagsAPI.ToString(), tag).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    await ex.LogAsync().ConfigureAwait(false);
                    return RedirectToAction("Error", "Error");
                }
            }
            return View(tag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Tag tag)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using var response = await client.PutAsJsonAsync(APIName.TagsAPI.ToString() + '/' + id, tag).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    await ex.LogAsync().ConfigureAwait(false);
                    return RedirectToAction("Error", "Error");
                }
            }
            return View(tag);
        }

        [HttpPost, ActionName(nameof(Delete))]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTag(Guid id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using var response = await client.DeleteAsync(APIName.TagsAPI.ToString() + '/' + id).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    await ex.LogAsync().ConfigureAwait(false);
                }
            }
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
