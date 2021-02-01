using CodeFactoryAPI.Models;
using System;
using System.Threading.Tasks;

namespace CodeFactoryAPI.DAL
{
    public class UnitOfWork : IDisposable
    {
        #region Fields
        private Repository<User> userRepo;
        private Repository<Question> questRepo;
        private Repository<Reply> replyRepo;
        private Repository<Tag> tagRepo;
        private Context context;
        private bool disposed = false;
        #endregion

        #region Properties
        public UnitOfWork(Context context) =>
            this.context = context;

        public Repository<User> GetUser =>
            userRepo ??= new(context);

        public Repository<Question> GetQuestion =>
            questRepo ??= new(context);

        public Repository<Reply> GetReply =>
            replyRepo ??= new(context);

        public Repository<Tag> GetTag =>
            tagRepo ??= new(context);
        #endregion

        public int Save() =>
            context.SaveChanges();

        public async Task<int> SaveAsync() =>
            await context.SaveChangesAsync();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context?.Dispose();
                    userRepo?.Dispose();
                    questRepo?.Dispose();
                    replyRepo?.Dispose();
                    tagRepo?.Dispose();
                }
                context = null;
                userRepo = null;
                questRepo = null;
                replyRepo = null;
                tagRepo = null;
                disposed = true;
            }
        }
    }
}
