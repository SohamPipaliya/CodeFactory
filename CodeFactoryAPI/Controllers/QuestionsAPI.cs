using CodeFactoryAPI.DAL;
using CodeFactoryAPI.Extra;
using CodeFactoryAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static Utf8Json.JsonSerializer;

namespace CodeFactoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsAPI : ControllerBase, IDisposable
    {
        private UnitOfWork unit;

        public QuestionsAPI(Context context) =>
            unit = context;

        [HttpGet]
        public async Task<IActionResult> Getquestions()
        {
            try
            {
                return Ok(ToJsonString(await unit.GetQuestion.Model
                    .Include(question => question.User)
                    .Include(question => question.Tag1)
                    .Include(question => question.Tag2)
                    .Include(question => question.Tag3)
                    .Include(question => question.Tag4)
                    .Include(question => question.Tag5)
                    .Include(question => question.Messages)
                    .AsParallel()
                    .SetMetaDataAsync(question => question.User.SetUserState(),
                                      question => question.Replies = unit.GetReply.Model
                                                       .Include(reply => reply.User)
                                                       .AsNoTracking()
                                                       .Where(reply => reply.Question_ID == question.Question_ID)
                                                       .ToArray()
                                                       .SetMetaData(reply => reply.User.SetUserState()),
                                      question => question.Messages = unit.GetMessage.Model
                                                       .Include(message => message.User)
                                                       .AsNoTracking()
                                                       .Where(message => message.Question_ID == question.Question_ID)
                                                       .ToArray()
                                                       .SetMetaData(message => message.User.SetUserState()))
                    .ConfigureAwait(false)));
            }
            catch (Exception ex)
            {
                await ex.LogAsync().ConfigureAwait(false);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetQuestion(Guid id)
        {
            try
            {
                return Ok(ToJsonString(await (await unit.GetQuestion.Model
                    .Include(question => question.User)
                    .Include(question => question.Tag1)
                    .Include(question => question.Tag2)
                    .Include(question => question.Tag3)
                    .Include(question => question.Tag4)
                    .Include(question => question.Tag5)
                    .FirstAsync(question => question.Question_ID == id)
                    .ConfigureAwait(false))
                    .SetMetaDataAsync(question => question.User.SetUserState(),
                                      question => question.Replies = unit.GetReply.Model
                                                       .Include(reply => reply.User)
                                                       .AsNoTracking()
                                                       .Where(reply => reply.Question_ID == question.Question_ID)
                                                       .ToArray()
                                                       .SetMetaData(reply => reply.User.SetUserState()),
                                      question => question.Messages = unit.GetMessage.Model
                                                       .Include(message => message.User)
                                                       .AsNoTracking()
                                                       .Where(message => message.Question_ID == question.Question_ID)
                                                       .ToArray()
                                                       .SetMetaData(message => message.User.SetUserState()))
                    .ConfigureAwait(false)));
            }
            catch (Exception ex)
            {
                await ex.LogAsync().ConfigureAwait(false);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestion(Guid id, [FromForm][ModelBinder(binderType: typeof(FormDataModelBinder))] Question question, IFormFile[]? files)
        {
            if (id != question.Question_ID)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    if (files is not null && files.Length > 0)
                    {
                        if (files.Length > 5)
                            return StatusCode((int)HttpStatusCode.NotAcceptable, "Only 5 Images is allowed");

                        foreach (var file in files)
                        {
                            var extension = file.FileName.Split('.')[^1].ToUpper();
                            var IsImage = extension is "JPG" || extension is "PNG" || extension is "JPEG";

                            if (IsImage)
                            {
                                IsImage = file.Length > 51200 && file.Length < 1073741825;

                                if (!IsImage) return StatusCode((int)HttpStatusCode.NotAcceptable, "Image size must be between 50 KB to 1 MB");
                            }
                            else return StatusCode((int)HttpStatusCode.UnsupportedMediaType, "Select valid Image");
                        }

                        await question.SetColumnsWithImages(files);
                    }

                    (await unit.GetQuestion.Model
                                       .AsNoTracking()
                                       .FirstAsync(question => question.Question_ID == id)
                                       .ConfigureAwait(false))
                                       .DeleteImages();

                    unit.GetQuestion.Context.Attach(question);
                    unit.GetQuestion.Context.Entry(question)
                       .SetUpdatedColumns(nameof(question.Title), nameof(question.Code), nameof(question.Image1), nameof(question.Image2),
                                          nameof(question.Image3), nameof(question.Image4), nameof(question.Image5), nameof(question.Description),
                                          nameof(question.Tag1_ID), nameof(question.Tag2_ID), nameof(question.Tag3_ID), nameof(question.Tag4_ID),
                                          nameof(question.Tag5_ID));

                    if (await unit.SaveAsync().ConfigureAwait(false) > 0)
                        return NoContent();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var exist = await unit.GetQuestion
                        .AnyAsync(x => x.Question_ID == id)
                        .ConfigureAwait(false);

                    if (!exist)
                        return NotFound();
                    else
                        await ex.LogAsync().ConfigureAwait(false);
                }
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            else return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> PostQuestion([FromForm][ModelBinder(binderType: typeof(FormDataModelBinder))] Question question, IFormFile[]? files)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    question.Question_ID = Guid.NewGuid();
                    question.AskedDate = DateTime.Now;

                    if (files is not null && files.Length > 0)
                    {
                        if (files.Length > 5)
                            return StatusCode((int)HttpStatusCode.NotAcceptable, "Only 5 Images is allowed");

                        foreach (var file in files)
                        {
                            var extension = file.FileName.Split('.')[^1].ToUpper();
                            var IsImage = extension is "JPG" || extension is "PNG" || extension is "JPEG";

                            if (IsImage)
                            {
                                IsImage = file.Length > 51200 && file.Length < 1073741825;

                                if (!IsImage) return StatusCode((int)HttpStatusCode.NotAcceptable, "Image size must be between 50 KB to 1 MB");
                            }
                            else return StatusCode((int)HttpStatusCode.UnsupportedMediaType, "Select valid Image");
                        }

                        await question.SetColumnsWithImages(files);
                    }

                    unit.GetQuestion.Add(question);
                    if (await unit.SaveAsync().ConfigureAwait(false) > 0)
                        return NoContent();
                }
                else return BadRequest();
            }
            catch (Exception ex)
            {
                await ex.LogAsync().ConfigureAwait(false);
            }
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(Guid id)
        {
            try
            {
                var question = await unit.GetQuestion.FindAsync(id).ConfigureAwait(false);
                if (question == null)
                    return NotFound();
                unit.GetQuestion.Remove(question);
                question.DeleteImages();
                if (await unit.SaveAsync().ConfigureAwait(false) > 0)
                    return NoContent();
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
            }
        }
        #endregion
    }
}
