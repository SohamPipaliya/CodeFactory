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
    public class QuestionsAPI : ControllerBase, IDisposable
    {
        private UnitOfWork unit;

        public QuestionsAPI(Context context) =>
            unit = context;

        [HttpGet]
        public async Task<IActionResult> Getquestions() =>
            await this.ToActionResult(SerializeToJson<IEnumerable<Question>>
                                            (await unit.GetQuestion.Model
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
                                                                                .Include(message => message.Messeger)
                                                                                .Include(message => message.Receiver)
                                                                                .AsNoTracking()
                                                                                .Where(message => message.Question_ID == question.Question_ID)
                                                                                .ToArray()
                                                                                .SetMetaData(message => message.Receiver.SetUserState(),
                                                                                             message => message.Messeger.SetUserState()))
                                            .ConfigureAwait(false))).ConfigureAwait(false);

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetQuestion(Guid id) =>
            await this.ToActionResult(SerializeToJson<Question>(await
                                      (await unit.GetQuestion.Model
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
                                                                                .Include(message => message.Messeger)
                                                                                .Include(message => message.Receiver)
                                                                                .AsNoTracking()
                                                                                .Where(message => message.Question_ID == question.Question_ID)
                                                                                .ToArray()
                                                                                .SetMetaData(message => message.Messeger.SetUserState(),
                                                                                             message => message.Receiver.SetUserState()))
                                            .ConfigureAwait(false))).ConfigureAwait(false);

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> PutQuestion(Guid id, [FromForm][ModelBinder(typeof(FormDataModelBinder))] Question question, IFormFile[]? files)
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
                            return StatusCode((int)HttpStatusCode.NotAcceptable, "Only 5 Images are allowed");

                        foreach (var file in files)
                        {
                            var (IsImage, ActionResult) = this.IsValidImage(file.FileName, file.Length);

                            if (!IsImage) return ActionResult;
                        }

                        await question.SetColumnsWithImages(files).ConfigureAwait(false);
                    }

                    var oldQuestion = await unit.GetQuestion.Model
                                                .AsNoTracking()
                                                .FirstAsync(question => question.Question_ID == id)
                                                .ConfigureAwait(false);

                    question.Description = await question.Description.FilterStringAsync().ConfigureAwait(false);
                    question.Title = await question.Title.FilterStringAsync().ConfigureAwait(false);

                    unit.GetQuestion.Context.Attach(question);
                    unit.GetQuestion.Context.Entry(question)
                        .SetUpdatedColumns(nameof(question.Title), nameof(question.Code), nameof(question.Image1), nameof(question.Image2),
                                           nameof(question.Image3), nameof(question.Image4), nameof(question.Image5), nameof(question.Description),
                                           nameof(question.Tag1_ID), nameof(question.Tag2_ID), nameof(question.Tag3_ID), nameof(question.Tag4_ID),
                                           nameof(question.Tag5_ID));

                    if (await unit.SaveAsync().ConfigureAwait(false) > 0)
                    {
                        oldQuestion.DeleteImages();
                        return Ok();
                    }
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var exist = await unit.GetQuestion.AnyAsync(x => x.Question_ID == id)
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
        public async Task<IActionResult> PostQuestion([FromForm][ModelBinder(typeof(FormDataModelBinder))] Question question, IFormFile[]? files)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    question.Question_ID = Guid.NewGuid();
                    question.AskedDate = DateTime.Now;

                    if (files is not null && files.Length > 0)
                    {
                        if (files.Length > 5)
                            return StatusCode((int)HttpStatusCode.NotAcceptable, "Only 5 Images are allowed");

                        foreach (var file in files)
                        {
                            var (IsImage, ActionResult) = this.IsValidImage(file.FileName, file.Length);

                            if (!IsImage) return ActionResult;
                        }

                        await question.SetColumnsWithImages(files).ConfigureAwait(false);
                    }

                    question.Description = await question.Description.FilterStringAsync().ConfigureAwait(false);
                    question.Title = await question.Title.FilterStringAsync().ConfigureAwait(false);

                    unit.GetQuestion.Add(question);
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
        public async Task<IActionResult> DeleteQuestion(Guid id)
        {
            try
            {
                var question = await unit.GetQuestion.FindAsync(question => question.Question_ID == id)
                                                     .ConfigureAwait(false);

                if (question == null)
                    return NotFound();

                unit.GetQuestion.Remove(question);

                if (await unit.SaveAsync().ConfigureAwait(false) > 0)
                {
                    question.DeleteImages();
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
