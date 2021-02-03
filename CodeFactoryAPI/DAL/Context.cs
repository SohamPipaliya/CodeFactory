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

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        //    optionsBuilder.UseSqlServer(Configuration["ConnectionStrings:CodeFactory"]);

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<UsersTags>().HasKey(x => new { x.Tag_ID, x.Question_ID });

        //    modelBuilder.Entity<UsersTags>()
        //        .HasOne(x => x.Tag)
        //        .WithMany(x => x.UsersTags)
        //        .HasForeignKey(x => x.Tag_ID);

        //    modelBuilder.Entity<UsersTags>()
        //        .HasOne(x => x.Question)
        //        .WithMany(x => x.UsersTags)
        //        .HasForeignKey(x => x.Question_ID);
        //    //base.OnModelCreating(modelBuilder);
        //}

        public DbSet<User> Users { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Reply> Replies { get; set; }

        //public DbSet<UsersTags> UsersTags { get; set; }
    }
}