using CodeFactory.DAL;
using CodeFactoryAPI.Extra;
using Extra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeFactoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Questions : ControllerBase, IDisposable
    {
        private UnitOfWork uow;

        public Questions(Context context) =>
            uow = new(context);

        [HttpGet]
        public IEnumerable<Question> Getquestions() =>
           uow.GetQuestion.Model.Include(x => x.User);

        [HttpGet("{id}")]
        public async Task<ActionResult<Question>> GetQuestion(Guid id)
        {
            var question = await uow.GetQuestion.FindAsync(id).ConfigureAwait(false);
            if (question is null)
                return NotFound();
            question.User = await uow.GetUser.FindAsync(question.User_ID).ConfigureAwait(false);
            return question;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestion(Guid id, Question question)
        {
            if (id != question.Question_ID)
                return BadRequest();
            try
            {
                uow.GetQuestion.context.Attach(question);
                uow.GetQuestion.context.Entry(question)
                   .SetUpdatedColumns("Title", "Code", "Image", "Image2", "Image3", "Image4", "Image5", "Description");
                await uow.SaveAsync().ConfigureAwait(false);
                return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var exist = await uow.GetQuestion.AnyAsync(x => x.Question_ID == id).ConfigureAwait(false);

                if (!exist)
                    return NotFound();
                else
                {
                    await ex.LogAsync().ConfigureAwait(false);
                    return StatusCode(500);
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostQuestion(Question question)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    question.Question_ID = Guid.NewGuid();
                    question.AskedDate = DateTime.Now;
                    uow.GetQuestion.Add(question);
                    await uow.SaveAsync().ConfigureAwait(false);
                    return NoContent();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                await ex.LogAsync().ConfigureAwait(false);
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(Guid id)
        {
            try
            {
                var question = await uow.GetQuestion.FindAsync(id).ConfigureAwait(false);
                if (question == null)
                    return NotFound();
                uow.GetQuestion.Remove(question);
                await uow.SaveAsync().ConfigureAwait(false);
                return NoContent();
            }
            catch (Exception ex)
            {
                await ex.LogAsync().ConfigureAwait(false);
                return StatusCode(500);
            }
        }

        [NonAction]
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    uow?.Dispose();
                }
                uow = null;
                disposed = true;
            }
        }
    }
}
