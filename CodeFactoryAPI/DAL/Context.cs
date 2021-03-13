using CodeFactoryAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodeFactoryAPI.DAL
{
    public class Context : IdentityDbContext<User>
    {
        public Context(DbContextOptions<Context> optionsBuilder) : base(optionsBuilder)
        { }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Reply> Replies { get; set; }

        public DbSet<Message> Message { get; set; }
    }
}