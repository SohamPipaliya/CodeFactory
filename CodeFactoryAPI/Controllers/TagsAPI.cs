using CodeFactoryAPI.DAL;
using CodeFactoryAPI.Extra;
using CodeFactoryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Threading.Tasks;
using static Utf8Json.JsonSerializer;

namespace CodeFactoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsAPI : ControllerBase
    {
        private readonly UnitOfWork unit;

        public TagsAPI(Context context) =>
            unit = context;

        [HttpGet]
        public IActionResult GetTags() =>
            Ok(ToJsonString(unit.GetTag.GetAll()));

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetTag(Guid id)
        {
            var tag = await unit.GetTag.FindAsync(id);
            return tag is null ? NotFound() : Ok(ToJsonString(tag));
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
                    await unit.SaveAsync().ConfigureAwait(false);
                    return NoContent();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var exist = await unit.GetTag.AnyAsync(x => x.Tag_ID == id);
                    if (exist)
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
            try
            {
                unit.GetTag.Add(tag);
                await unit.SaveAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                await ex.LogAsync().ConfigureAwait(false);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteTag(Guid id)
        {
            try
            {
                await unit.GetTag.RemoveAsync(id);
                if (await unit.SaveAsync() > 0)
                    return NoContent();
            }
            catch (Exception ex)
            {
                var exist = await unit.GetTag.AnyAsync(x => x.Tag_ID == id);
                if (exist)
                    return NotFound();
                await ex.LogAsync().ConfigureAwait(false);
            }
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }
}
