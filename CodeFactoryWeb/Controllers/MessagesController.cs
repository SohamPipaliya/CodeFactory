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
            client = new() { BaseAddress = Addons.HostUrl };

        public async Task<IActionResult> Index() =>
             View(await client.GetDataAsync<IEnumerable<Message>>(APIName.MessagesAPI));

        public async Task<IActionResult> Details(Guid id) =>
            View(await client.GetDataAsync<Message>(APIName.MessagesAPI, id));

        public async Task<IActionResult> Delete(Guid id) =>
            View(await client.GetDataAsync<Message>(APIName.MessagesAPI, id));

        public async Task<IActionResult> Edit(Guid id)
        {
            await SetViewBag();
            return View(await client.GetDataAsync<Message>(APIName.MessagesAPI, id));
        }
        public async Task<IActionResult> Create()
        {
            await SetViewBag();
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
                    using var response = await client.PostAsJsonAsync("MessagesAPI", message);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    await ex.LogAsync();
                }
            }
            await SetViewBag();
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
                    using var response = await client.PutAsJsonAsync("MessagesAPI/" + id, message);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    await ex.LogAsync();
                }
            }
            await SetViewBag();
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
                    using var response = await client.DeleteAsync("MessagesAPI/" + id);
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

        private async Task SetViewBag()
        {
            ViewData["User_ID"] = new SelectList(await client.GetDataAsync<IEnumerable<User>>(APIName.UsersAPI), "User_ID", "Email");
            ViewData["Question_ID"] = new SelectList(await client.GetDataAsync<IEnumerable<Question>>(APIName.QuestionsAPI), "Question_ID", "Title");
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
