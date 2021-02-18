using CodeFactoryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeFactoryAPI.DAL
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> optionsBuilder) : base(optionsBuilder)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Reply> Replies { get; set; }

        public DbSet<Message> Message { get; set; }
    }
}