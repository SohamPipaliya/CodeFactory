using Microsoft.EntityFrameworkCore;
using Models.Model;

namespace CodeFactory.DAL
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> optionsBuilder) : base(optionsBuilder)
        { }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        //    optionsBuilder.UseSqlServer(Configuration["ConnectionStrings:CodeFactory"]);

        public DbSet<User> users { get; set; }

        public DbSet<Question> questions { get; set; }

        public DbSet<Tag> tags { get; set; }

        public DbSet<Reply> replies { get; set; }
    }
}