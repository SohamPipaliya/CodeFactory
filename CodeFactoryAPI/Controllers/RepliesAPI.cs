using CodeFactoryAPI.DAL;
using CodeFactoryAPI.Extra;
using CodeFactoryAPI.Models;
using Microsoft.AspNetCore.Http;
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
    public class RepliesAPI : ControllerBase, IDisposable
    {
        private UnitOfWork unit;

        public RepliesAPI(Context context) =>
            unit = context;

        [HttpGet]
        public async Task<IActionResult> GetReplies() =>
            await this.ToActionResult(SerializeToJson<IEnumerable<Reply>>(await unit.GetReply.Model
                                                                     .Include(reply => reply.User)
                                                                     .OrderBy(reply => reply.Message)
                                                                     .SetMetaDataAsync(reply => reply.User.SetUserState())
                                                                     .ConfigureAwait(false))).ConfigureAwait(false);

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetReply(Guid id) =>
            await this.ToActionResult(SerializeToJson<Reply>(await
                                                        (await unit.GetReply.Model
                                                        .Include(reply => reply.User)
                                                        .FirstAsync(reply => reply.Reply_ID == id)
                                                        .ConfigureAwait(false))
                                                        .SetMetaDataAsync(reply => reply.User.SetUserState())
                                                        .ConfigureAwait(false))).ConfigureAwait(false);

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> PutReply(Guid id, [FromForm][ModelBinder(typeof(FormDataModelBinder))] Reply reply, IFormFile[]? files)
        {
            if (id != reply.Reply_ID)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    if (files is not null && files.Length > 0)
                    {
                        if (files.Length > 5)
                            return StatusCode((int)HttpStatusCode.NotAcceptable, "Only 5 Images are allowed");

                        foreach (var file in files)
                        {
                            var (IsImage, ActionResult) = this.IsValidImage(file.FileName, file.Length);

                            if (!IsImage) return ActionResult;
                        }

                        await reply.SetColumnsWithImages(files).ConfigureAwait(false);
                    }

                    reply.Message = await reply.Message.FilterStringAsync().ConfigureAwait(false);

                    var oldReply = await unit.GetReply.Model
                                   .AsNoTracking()
                                   .FirstAsync(rpl => rpl.Reply_ID == id)
                                   .ConfigureAwait(false);

                    unit.GetReply.Context.Attach(reply);
                    unit.GetReply.Context.Entry(reply)
                                 .SetUpdatedColumns(nameof(reply.Message), nameof(reply.Code), nameof(reply.Image1), nameof(reply.Image2),
                                                    nameof(reply.Image3), nameof(reply.Image4), nameof(reply.Image5));

                    if (await unit.SaveAsync().ConfigureAwait(false) > 0)
                    {
                        oldReply.DeleteImages();
                        return Ok();
                    }
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var exist = await unit.GetReply.AnyAsync(rpl => rpl.Reply_ID == id).ConfigureAwait(false);
                    if (!exist)
                        return NotFound();
                    await ex.LogAsync().ConfigureAwait(false);
                }
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            else return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> PostReply([FromForm][ModelBinder(typeof(FormDataModelBinder))] Reply reply, IFormFile[]? files)
        {
            try
            {
                reply.Reply_ID = Guid.NewGuid();
                reply.RepliedDate = DateTime.Now;

                if (files is not null && files.Length > 0)
                {
                    if (files.Length > 5)
                        return StatusCode((int)HttpStatusCode.NotAcceptable, "Only 5 Images are allowed");

                    foreach (var file in files)
                    {
                        var (IsImage, ActionResult) = this.IsValidImage(file.FileName, file.Length);

                        if (!IsImage) return ActionResult;
                    }

                    await reply.SetColumnsWithImages(files).ConfigureAwait(false);
                }

                reply.Message = await reply.Message.FilterStringAsync().ConfigureAwait(false);

                unit.GetReply.Add(reply);
                if (await unit.SaveAsync().ConfigureAwait(false) > 0)
                    return Ok();
            }
            catch (Exception ex)
            {
                await ex.LogAsync().ConfigureAwait(false);
            }
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteReply(Guid id)
        {
            try
            {
                var reply = await unit.GetReply.FindAsync(rpl => rpl.Reply_ID == id).ConfigureAwait(false);

                if (reply is null)
                    return NotFound();

                unit.GetReply.Remove(reply);
                if (await unit.SaveAsync().ConfigureAwait(false) > 0)
                {
                    reply.DeleteImages();
                    return Ok();
                }
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
