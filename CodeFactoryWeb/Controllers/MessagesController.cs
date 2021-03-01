using CodeFactoryAPI.Extra;
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
    public class MessagesController : Controller
    {
        private HttpClient client;

        public MessagesController() =>
            client = new() { BaseAddress = Extra.Addons.HostUrl };

        public async Task<IActionResult> Index() =>
            await this.ToActionResult<IEnumerable<Message>>(() => client.GetDataAsync<IEnumerable<Message>>
                                     (APIName.MessagesAPI)).ConfigureAwait(false);

        public async Task<IActionResult> Details(Guid? id) =>
            await this.ToActionResult<Message>(() => client.GetDataAsync<Message>
                                     (APIName.MessagesAPI, id)).ConfigureAwait(false);

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
        public async Task<IActionResult> Create(Message message)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using var response = await client.PostAsJsonAsync(APIName.MessagesAPI.ToString(), message).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    await ex.LogAsync().ConfigureAwait(false);
                    return RedirectToAction("Error", "Error");
                }
            }
            await SetViewBag().ConfigureAwait(false);
            return View(message);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Message message)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using var response = await client.PutAsJsonAsync(APIName.MessagesAPI.ToString() + '/' + id, message).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    await ex.LogAsync().ConfigureAwait(false);
                    return RedirectToAction("Error", "Error");
                }
            }
            await SetViewBag().ConfigureAwait(false);
            return View(message);
        }

        [HttpPost, ActionName(nameof(Delete))]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteFinally(Guid id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using var response = await client.DeleteAsync(APIName.MessagesAPI.ToString() + '/' + id).ConfigureAwait(false);
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

        private async Task SetViewBag()
        {
            ViewData["User_ID"] = new SelectList(await client.GetDataAsync<IEnumerable<User>>(APIName.UsersAPI).ConfigureAwait(false), "User_ID", "Email");
            ViewData["Question_ID"] = new SelectList(await client.GetDataAsync<IEnumerable<Question>>(APIName.QuestionsAPI).ConfigureAwait(false), "Question_ID", "Title");
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
