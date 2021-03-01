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
    public class TagsAPI : ControllerBase, IDisposable
    {
        private UnitOfWork unit;

        public TagsAPI(Context context) =>
            unit = context;

        [HttpGet]
        public async Task<IActionResult> GetTags() =>
            await this.ToActionResult<IEnumerable<Tag>>(() => unit.GetTag.Model.OrderBy(tag => tag.Name)).ConfigureAwait(false);

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetTag(Guid id) =>
            await this.ToActionResult<Tag>(() => unit.GetTag.FindAsync(tag => tag.Tag_ID == id)).ConfigureAwait(false);

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
                    await unit.SaveAsync().ConfigureAwait(false);
                    return Ok();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var exist = await unit.GetTag.AnyAsync(tag => tag.Tag_ID == id).ConfigureAwait(false);
                    if (!exist)
                        return NotFound();
                    await ex.LogAsync().ConfigureAwait(false);
                    return StatusCode((int)HttpStatusCode.InternalServerError);
                }
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
        public async Task<IActionResult> DeleteTag(Guid id)
        {
            try
            {
                var tag = await unit.GetTag.FindAsync(tag => tag.Tag_ID == id).ConfigureAwait(false);
                if (tag is null)
                    return NotFound();

                unit.GetTag.Remove(tag);

                await unit.SaveAsync().ConfigureAwait(false);
                return Ok();
            }
            catch (Exception ex)
            {
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
                    unit.Dispose();
                }
                unit = null;
                disposed = true;
            }
        }
        #endregion
    }
}
