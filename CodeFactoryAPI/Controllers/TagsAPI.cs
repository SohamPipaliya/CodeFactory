using CodeFactoryAPI.DAL;
using CodeFactoryAPI.Extra;
using CodeFactoryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using static CodeFactoryAPI.Extra.Addons;

namespace CodeFactoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsAPI : ControllerBase, IDisposable
    {
        private UnitOfWork unit;

        public TagsAPI(Context context) =>
            unit = context;

        [HttpGet]
        public async Task<IActionResult> GetTags()
        {
            try
            {
                var json = SerializeToJson<IEnumerable<Tag>>(unit.GetTag.GetAll());
                return Ok(json);
            }
            catch (Exception ex)
            {
                await ex.LogAsync().ConfigureAwait(false);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetTag(Guid id)
        {
            try
            {
                var tag = await unit.GetTag.FindAsync(id).ConfigureAwait(false);
                return tag is null ? NotFound() : Ok(SerializeToJson(tag));
            }
            catch (Exception ex)
            {
                await ex.LogAsync().ConfigureAwait(false);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> PutTag(Guid id, Tag tag)
        {
            if (id != tag.Tag_ID)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    unit.GetTag.Update(tag);
                    if (await unit.SaveAsync().ConfigureAwait(false) > 0)
                        return Ok();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var exist = await unit.GetTag.AnyAsync(tag => tag.Tag_ID == id).ConfigureAwait(false);
                    if (!exist)
                        return NotFound();
                    await ex.LogAsync().ConfigureAwait(false);
                }
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            else return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> PostTag(Tag tag)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    tag.Tag_ID = Guid.NewGuid();
                    unit.GetTag.Add(tag);
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
        public async Task<IActionResult> DeleteTag(Guid id)
        {
            try
            {
                var tag = await unit.GetTag.FindAsync(tag => tag.Tag_ID == id).ConfigureAwait(false);
                if (tag is null)
                    return NotFound();

                unit.GetTag.Remove(tag);

                if (await unit.SaveAsync() > 0)
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
                    unit.Dispose();
                }
                unit = null;
                disposed = true;
            }
        }
        #endregion
    }
}
