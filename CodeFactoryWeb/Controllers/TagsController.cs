using CodeFactoryAPI.Models;
using CodeFactoryWeb.Extra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            client = new() { BaseAddress = Addons.HostUrl };

        public async Task<IActionResult> Index()
        {
            try
            {
                var data = await client.GetDataAsync<IEnumerable<Tag>>(APIName.TagsAPI).ConfigureAwait(false);
                return View(data);
            }
            catch (Exception ex)
            {
                await ex.LogAsync().ConfigureAwait(false);
                return View();
            }
        }

        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var tag = await client.GetDataAsync<Tag>(APIName.TagsAPI, id).ConfigureAwait(false);
                return View(tag);
            }
            catch (Exception ex)
            {
                await ex.LogAsync().ConfigureAwait(false);
                return View();
            }
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var tag = await client.GetDataAsync<Tag>(APIName.TagsAPI, id).ConfigureAwait(false);
                return View(tag);
            }
            catch (Exception ex)
            {
                await ex.LogAsync().ConfigureAwait(false);
                return View();
            }
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var tag = await client.GetDataAsync<Tag>(APIName.TagsAPI, id).ConfigureAwait(false);
                return View(tag);
            }
            catch (Exception ex)
            {
                await ex.LogAsync().ConfigureAwait(false);
                return View();
            }
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tag tag)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using var response = await client.PostAsJsonAsync("TagsAPI", tag).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    await ex.LogAsync().ConfigureAwait(false);
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
                    using var response = await client.PutAsJsonAsync("TagsAPI/" + id, tag).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    await ex.LogAsync().ConfigureAwait(false);
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
                    using var response = await client.DeleteAsync("TagsAPI/" + id).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    await ex.LogAsync().ConfigureAwait(false);
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
