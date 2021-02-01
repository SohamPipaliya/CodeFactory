using Microsoft.EntityFrameworkCore;
using CodeFactoryAPI.Models;
using System.Security.Cryptography.X509Certificates;

namespace CodeFactoryAPI.DAL
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> optionsBuilder) : base(optionsBuilder)
        { }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        //    optionsBuilder.UseSqlServer(Configuration["ConnectionStrings:CodeFactory"]);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsersTags>().HasKey(x => new { x.Question_ID, x.Tag_ID });
            //base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Reply> Replies { get; set; }

        public DbSet<UsersTags> UsersTags { get; set; }
    }
}