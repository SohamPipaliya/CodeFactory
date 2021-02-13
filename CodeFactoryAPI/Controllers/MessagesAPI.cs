using CodeFactoryAPI.DAL;
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
        public async Task<IActionResult> GetMessages()
        {
            try
            {
                return Ok(SerializeToJson<IEnumerable<Message>>(/*await*/ unit.GetMessage.Model
                                                                   //.Include(message => message.Messeger)
                                                                   //.Include(message => message.Receiver)
                                                                   .OrderBy(message => message.Messages)));
                //.SetMetaDataAsync(message => message.Messeger.SetUserState(),
                //                  message => message.Receiver.SetUserState())
                //.ConfigureAwait(false)));
            }
            catch (Exception ex)
            {
                await ex.LogAsync().ConfigureAwait(false);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetMessage(Guid id)
        {
            try
            {
                return Ok(SerializeToJson<Message>(await /*(await */unit.GetMessage.Model
                                                                 //.Include(message => message.Messeger)
                                                                 //.Include(message => message.Receiver)
                                                                 .FirstAsync(message => message.Message_ID == id)
                                                                 .ConfigureAwait(false)));
                //.SetMetaDataAsync(message => message.Messeger.SetUserState(),
                //                  message => message.Receiver.SetUserState())
                //.ConfigureAwait(false)));
            }
            catch (Exception ex)
            {
                await ex.LogAsync().ConfigureAwait(false);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
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

                    if (await unit.SaveAsync().ConfigureAwait(false) > 0)
                        return Ok();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    bool exist = await unit.GetMessage.AnyAsync(message => message.Message_ID == id).ConfigureAwait(false);
                    if (!exist)
                        return NotFound();
                    await ex.LogAsync().ConfigureAwait(false);
                }
                return StatusCode((int)HttpStatusCode.InternalServerError);
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

                    unit.GetMessage.Add(message);
                    if (await unit.SaveAsync().ConfigureAwait(false) > 0)
                        return Ok();
                }
                catch (Exception ex)
                {
                    await ex.LogAsync().ConfigureAwait(false);
                }
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            else return BadRequest();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteMessage(Guid id)
        {
            try
            {
                await unit.GetMessage.RemoveAsync(id).ConfigureAwait(false);
                if (await unit.SaveAsync().ConfigureAwait(false) > 0)
                    return Ok();
            }
            catch (Exception ex)
            {
                await ex.LogAsync().ConfigureAwait(false);
            }
            return StatusCode((int)HttpStatusCode.InternalServerError);
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
