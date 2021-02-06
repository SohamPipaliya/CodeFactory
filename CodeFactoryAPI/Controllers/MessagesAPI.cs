using CodeFactoryAPI.DAL;
using CodeFactoryAPI.Extra;
using CodeFactoryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using static Utf8Json.JsonSerializer;

namespace CodeFactoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesAPI : ControllerBase
    {
        private UnitOfWork unit;

        public MessagesAPI(Context context) =>
            unit = context;

        [HttpGet]
        public async Task<IActionResult> GetMessage()
        {
            try
            {
                return Ok(ToJsonString(await unit.GetMessage.Model
                                                 .Include(message => message.User)
                                                 .OrderBy(message => message.Messages)
                                                 .SetMetaDataAsync(message => message.User.SetUserState())
                                                 .ConfigureAwait(false)));
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
                return Ok(ToJsonString(await
                                      (await unit.GetMessage.Model
                                                 .Include(message => message.User)
                                                 .FirstAsync(message => message.Message_ID == id)
                                                 .ConfigureAwait(false))
                                                 .SetMetaDataAsync(message => message.User.SetUserState())
                                                 .ConfigureAwait(false)));
            }
            catch (Exception ex)
            {
                await ex.LogAsync().ConfigureAwait(false);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMessage(Guid id, Message message)
        {
            if (id != message.Message_ID)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    unit.GetMessage.Context.Attach(message);
                    unit.GetUser.Context.Entry(message)
                        .SetUpdatedColumns(nameof(message.Messages));

                    if (await unit.SaveAsync().ConfigureAwait(false) > 0)
                        return NoContent();
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
                    unit.GetMessage.Add(message);
                    if (await unit.SaveAsync().ConfigureAwait(false) > 0)
                        return NoContent();
                }
                catch (Exception ex)
                {
                    await ex.LogAsync().ConfigureAwait(false);
                }
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            else return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(Guid id)
        {
            try
            {
                await unit.GetMessage.RemoveAsync(id);
                if (await unit.SaveAsync().ConfigureAwait(false) > 0)
                    return NoContent();
            }
            catch (Exception ex)
            {
                await ex.LogAsync().ConfigureAwait(false);
            }
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }
}
