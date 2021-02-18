using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CodeFactoryAPI.DAL
{
    public class Repository<T> : IDisposable where T : class
    {
        private DbSet<T> model;

        public Context Context { get; }

        public DbSet<T> Model => model;

        public Repository(Context context) =>
            (Context, model) = (context, context.Set<T>());

        public void Add(T entity) =>
            model.Add(entity);

        public ValueTask<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<T>> AddAsync(T entity) =>
            model.AddAsync(entity);

        public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate) =>
            model.AnyAsync(predicate);

        public bool Any(Expression<Func<T, bool>> predicate) =>
            model.Any(predicate);

        public T Find(object PKey) =>
            model.Find(PKey);

        public T Find(Expression<Func<T, bool>> predicate) =>
            model.Find(predicate);

        public ValueTask<T> FindAsync(object PKey) =>
            model.FindAsync(PKey);

        public Task<T> FindAsync(Expression<Func<T, bool>> predicate) =>
            model.FirstAsync(predicate);

        public IEnumerable<T> GetAll() => model;

        public Task<T[]> GetAllAsync() =>
             model.ToArrayAsync();

        public async Task<T?> RemoveAsync(object id) =>
            model.Remove(await FindAsync(id).ConfigureAwait(false)).Entity;

        public T? Remove(T entity) =>
            model.Remove(entity).Entity;

        public int Save() =>
            Context.SaveChanges();

        public Task<int> SaveAsync() =>
             Context.SaveChangesAsync();

        public void Update(T entity) =>
            Context.Entry(entity).State = EntityState.Modified;

        public void Dispose()
        {
            model = null;
            GC.SuppressFinalize(this);
        }

        public static implicit operator Repository<T>(Context context) => new(context);
    }
}
