using CodeFactoryAPI.DAL;
using CodeFactoryAPI.Models;
using CodeFactoryWeb.Extra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.OpenApi.Writers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CodeFactoryWeb.Controllers
{
    public class TagsController : Controller
    {
        private HttpClient client;

        public TagsController() =>
            client = new() { BaseAddress = Addons.HostUrl };

        public async Task<IActionResult> Index() =>
            View(await client.GetDataAsync<IEnumerable<Tag>>(APIName.TagsAPI));

        public async Task<IActionResult> Details(Guid id) =>
            View(await client.GetDataAsync<Tag>(APIName.TagsAPI, id));

        public async Task<IActionResult> Edit(Guid id) =>
            View(await client.GetDataAsync<Tag>(APIName.TagsAPI, id));

        public async Task<IActionResult> Delete(Guid id) =>
            View(await client.GetDataAsync<Tag>(APIName.TagsAPI, id));

        public async Task<IActionResult> Create()
        {
            ViewData["Tag_ID"] = new SelectList(await client.GetDataAsync<IEnumerable<Tag>>(APIName.TagsAPI), "Tag_ID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tag tag)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using var response = await client.PostAsJsonAsync("TagsAPI", tag);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    await ex.LogAsync();
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
                    using var response = await client.PutAsJsonAsync("TagsAPI/" + id, tag);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    await ex.LogAsync();
                }
            }
            return View(tag);
        }

        [HttpPost, ActionName(nameof(Delete))]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteFinally(Guid id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using var response = await client.DeleteAsync("TagsAPI/" + id);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    await ex.LogAsync();
                }
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
