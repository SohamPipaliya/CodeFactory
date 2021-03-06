﻿using CodeFactoryAPI.DAL;
using CodeFactoryAPI.Extra;
using CodeFactoryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static CodeFactoryAPI.Extra.Addons;

namespace CodeFactoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesAPI : ControllerBase, IDisposable
    {
        private UnitOfWork unit;

        public MessagesAPI(Context context) =>
            unit = context;

        [HttpGet]
        public async Task<IActionResult> GetMessages() =>
             await this.ToActionResult<IEnumerable<Message>>(() => unit.GetMessage.Model
                                                 .OrderBy(message => message.Messages)
                                                 .SetMetaDataAsync(message => message.Messeger = unit.GetUser(message.Messeger_ID),
                                                                   message => message.Receiver = unit.GetUser(message.Receiver_ID))).ConfigureAwait(false);

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetMessage(Guid id)
        {
            var message = await unit.GetMessage
                                    .FindAsync(message => message.Message_ID == id).ConfigureAwait(false);
            if (message is null)
                return NotFound();

            return await this.ToActionResult<Message>(() => message.SetMetaDataAsync(
                message => message.Messeger = unit.GetUser(message.Messeger_ID),
                message => message.Receiver = unit.GetUser(message.Receiver_ID)));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> PutMessage(Guid id, Message message)
        {
            if (id != message.Message_ID)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    message.Messages = await message.Messages.FilterStringAsync().ConfigureAwait(false);

                    unit.GetMessage.Context.Attach(message);
                    unit.GetUser.Context.Entry(message)
                        .SetUpdatedColumns(nameof(message.Messages));

                    await unit.SaveAsync().ConfigureAwait(false);
                    return Ok();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!await unit.GetMessage.AnyAsync(message => message.Message_ID == id).ConfigureAwait(false))
                        return NotFound();
                    await ex.LogAsync().ConfigureAwait(false);
                    return StatusCode((int)HttpStatusCode.InternalServerError);
                }
            }
            else return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> PostMessage(Message message)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    message.Messages = await message.Messages.FilterStringAsync().ConfigureAwait(false);
                    message.MessageDate = DateTime.Now;

                    unit.GetMessage.Add(message);
                    await unit.SaveAsync().ConfigureAwait(false);
                    return Ok();
                }
                catch (Exception ex)
                {
                    await ex.LogAsync().ConfigureAwait(false);
                    return StatusCode((int)HttpStatusCode.InternalServerError);
                }
            }
            else return BadRequest();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteMessage(Guid id)
        {
            try
            {
                var message = await unit.GetMessage.FindAsync(msg => msg.Message_ID == id).ConfigureAwait(false);

                if (message is null)
                    return NotFound();

                unit.GetMessage.Remove(message);

                await unit.SaveAsync().ConfigureAwait(false);
                return Ok();
            }
            catch (Exception ex)
            {
                if (!await unit.GetMessage.AnyAsync(message => message.Message_ID == id).ConfigureAwait(false))
                    return NotFound();
                await ex.LogAsync().ConfigureAwait(false);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        #region Dispose
        private bool disposed = false;

        [NonAction]
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    unit?.Dispose();
                }
                unit = null;
                disposed = true;
            }
        }
        #endregion
    }
}
