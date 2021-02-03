using CodeFactoryAPI.DAL;
using CodeFactoryAPI.Extra;
using CodeFactoryAPI.Models;
using Microsoft.AspNetCore.Http;
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
    public class QuestionsAPI : ControllerBase, IDisposable
    {
        private UnitOfWork unit;

        public QuestionsAPI(Context context) =>
            unit = new(context);

        [HttpGet]
        public async Task<IActionResult> Getquestions() =>
                Ok(ToJsonString(await unit.GetQuestion.Model
                    .Include(x => x.User)
                    .Include(x => x.Tag1)
                    .Include(x => x.Tag2)
                    .Include(x => x.Tag3)
                    .Include(x => x.Tag4)
                    .Include(x => x.Tag5)
                    .SetMetaDataAsync(x => x.User.Password = null,
                                      x => x.User.RegistrationDate = null)
                    .ConfigureAwait(false)));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestion(Guid id)
        {
            try
            {
                var question = await unit.GetQuestion.Model
                                         .FirstOneAsync(x => x.Question_ID == id)
                                         .ConfigureAwait(false);

                if (question is null)
                    return NotFound();

                question.User = await (await unit.GetUser.Model
                                         .FirstOneAsync(x => x.User_ID == question.User_ID)
                                         .ConfigureAwait(false))
                                         .SetMetaDataAsync(x => x.Password = null,
                                                           x => x.RegistrationDate = null)
                                         .ConfigureAwait(false);

                question.Tag1 = await unit.GetTag.Model
                                         .FirstOneAsync(x => x.Tag_ID == question.Tag1_ID)
                                         .ConfigureAwait(false);

                question.Tag2 = await unit.GetTag.Model
                                         .FirstOneAsync(x => x.Tag_ID == question.Tag2_ID)
                                         .ConfigureAwait(false);

                if (question.Tag3_ID is not null)
                    question.Tag3 = await unit.GetTag.Model
                                             .FirstOneAsync(x => x.Tag_ID == question.Tag3_ID)
                                             .ConfigureAwait(false);

                if (question.Tag4_ID is not null)
                    question.Tag4 = await unit.GetTag.Model
                                         .FirstOneAsync(x => x.Tag_ID == question.Tag4_ID)
                                         .ConfigureAwait(false);

                if (question.Tag5_ID is not null)
                    question.Tag5 = await unit.GetTag.Model
                                             .FirstOneAsync(x => x.Tag_ID == question.Tag5_ID)
                                             .ConfigureAwait(false);

                return Ok(ToJsonString(question));
            }
            catch (Exception ex)
            {
                await ex.LogAsync().ConfigureAwait(false);
                return NotFound();
                //return StatusCode((int)HttpStatusCode.InternalServerError);
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
                                       .FirstOneAsync(x => x.Question_ID == id)
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
