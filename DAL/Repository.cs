using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CodeFactory.DAL
{
    public class Repository<T> : IDisposable where T : class
    {
        private DbSet<T> model;

        public Context context { get; }

        public DbSet<T> Model => model;

        public Repository(Context context) =>
            (this.context, model) = (context, context.Set<T>());

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

        public Task<List<T>> GetAllAsync() =>
             model.ToListAsync();

        public async Task<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<T>> RemoveAsync(object id) =>
            model.Remove(await FindAsync(id));

        public void Remove(T entity) =>
            model.Remove(entity);

        public int Save() =>
            context.SaveChanges();

        public Task<int> SaveAsync() =>
             context.SaveChangesAsync();

        public void Update(T entity) =>
            context.Entry(entity).State = EntityState.Modified;

        public void Dispose()
        {
            model = null;
            GC.SuppressFinalize(this);
        }
    }
}
