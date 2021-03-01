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
            await this.ToActionResult<IEnumerable<Question>>(() => unit.GetQuestion.Model
                                           .OrderByDescending(question => question.AskedDate)
                                           .SetMetaDataAsync(question => question.Tag1 = unit.GetTag(question.Tag1_ID),
                                                             question => question.Tag2 = unit.GetTag(question.Tag2_ID),
                                                             question => question.Tag3 = unit.GetTag(question.Tag3_ID),
                                                             question => question.Tag4 = unit.GetTag(question.Tag4_ID),
                                                             question => question.Tag5 = unit.GetTag(question.Tag5_ID),
                                                             question => question.User = unit.GetUser(question.User_ID),
                                                             question => question.Replies = unit.GetReply.Model
                                                                               .Where(reply => reply.Question_ID == question.Question_ID)
                                                                               .Select(reply => new Reply()
                                                                               {
                                                                                   Reply_ID = reply.Reply_ID,
                                                                                   RepliedDate = reply.RepliedDate,
                                                                                   Image1 = reply.Image1,
                                                                                   Image2 = reply.Image2,
                                                                                   Image3 = reply.Image3,
                                                                                   Image4 = reply.Image4,
                                                                                   Image5 = reply.Image5,
                                                                                   User_ID = reply.User_ID,
                                                                                   Message = reply.Message,
                                                                                   Code = reply.Code,
                                                                                   User = unit.GetUser(reply.User_ID)
                                                                               }),
                                                             question => question.Messages = unit.GetMessage.Model
                                                                               .Where(message => message.Question_ID == question.Question_ID)
                                                                               .Select(message => new Message()
                                                                               {
                                                                                   Message_ID = message.Message_ID,
                                                                                   Messages = message.Messages,
                                                                                   MessageDate = message.MessageDate,
                                                                                   Messeger_ID = message.Messeger_ID,
                                                                                   Messeger = unit.GetUser(message.Messeger_ID),
                                                                                   Receiver_ID = message.Receiver_ID,
                                                                                   Receiver = unit.GetUser(message.Receiver_ID)
                                                                               })));

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetQuestion(Guid id)
        {
            var question = await unit.GetQuestion
                                 .FindAsync(question => question.Question_ID == id)
                                 .ConfigureAwait(false);
            if (question is null)
                return NotFound();

            return await this.ToActionResult<Question>(() => question
                                           .SetMetaDataAsync(question => question.Tag1 = unit.GetTag(question.Tag1_ID),
                                                             question => question.Tag2 = unit.GetTag(question.Tag2_ID),
                                                             question => question.Tag3 = unit.GetTag(question.Tag3_ID),
                                                             question => question.Tag4 = unit.GetTag(question.Tag4_ID),
                                                             question => question.Tag5 = unit.GetTag(question.Tag5_ID),
                                                             question => question.User = unit.GetUser(question.User_ID),
                                                             question => question.Replies = unit.GetReply.Model
                                                                               .Where(reply => reply.Question_ID == question.Question_ID)
                                                                               .Select(reply => new Reply()
                                                                               {
                                                                                   Reply_ID = reply.Reply_ID,
                                                                                   RepliedDate = reply.RepliedDate,
                                                                                   Image1 = reply.Image1,
                                                                                   Image2 = reply.Image2,
                                                                                   Image3 = reply.Image3,
                                                                                   Image4 = reply.Image4,
                                                                                   Image5 = reply.Image5,
                                                                                   User_ID = reply.User_ID,
                                                                                   Message = reply.Message,
                                                                                   Code = reply.Code,
                                                                                   User = unit.GetUser(reply.User_ID)
                                                                               }),
                                                             question => question.Messages = unit.GetMessage.Model
                                                                              .Where(message => message.Question_ID == question.Question_ID)
                                                                              .Select(message => new Message()
                                                                              {
                                                                                  Message_ID = message.Message_ID,
                                                                                  Messages = message.Messages,
                                                                                  MessageDate = message.MessageDate,
                                                                                  Messeger_ID = message.Messeger_ID,
                                                                                  Messeger = unit.GetUser(message.Messeger_ID),
                                                                                  Receiver_ID = message.Receiver_ID,
                                                                                  Receiver = unit.GetUser(message.Receiver_ID)
                                                                              })));
        }

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

                    await unit.SaveAsync().ConfigureAwait(false);
                    oldQuestion.DeleteImages();
                    return Ok();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var exist = await unit.GetQuestion.AnyAsync(x => x.Question_ID == id)
                                                      .ConfigureAwait(false);
                    if (!exist)
                        return NotFound();
                    else
                        await ex.LogAsync().ConfigureAwait(false);
                    return StatusCode((int)HttpStatusCode.InternalServerError);
                }
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
        public async Task<IActionResult> DeleteQuestion(Guid id)
        {
            try
            {
                var question = await unit.GetQuestion.FindAsync(question => question.Question_ID == id)
                                                     .ConfigureAwait(false);

                if (question is null)
                    return NotFound();

                unit.GetQuestion.Remove(question);

                await unit.SaveAsync().ConfigureAwait(false);
                question.DeleteImages();
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
                    unit?.Dispose();
                }
                unit = null;
                disposed = true;
            }
        }
        #endregion
    }
}
