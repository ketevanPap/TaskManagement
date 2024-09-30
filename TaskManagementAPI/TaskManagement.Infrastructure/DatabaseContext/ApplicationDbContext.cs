
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Entity.Models.ApplicationClasses;

namespace TaskManagement.Infrastructure.DatabaseContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }
        public virtual DbSet<TaskItem> TaskItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<TaskItem>().ToTable("TaskItems");
            builder.Entity<ApplicationUser>().ToTable("Users");

            builder.Entity<TaskItem>()
            .HasOne(t => t.User)
            .WithMany(u => u.TaskItems)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<TaskItem>()
                .HasIndex(t => t.UserId)
                .HasDatabaseName("IX_TaskItem_UserId");

             builder.Entity<TaskItem>()
                 .HasIndex(t => t.Status)
                 .HasDatabaseName("IX_TaskItem_Status");

             builder.Entity<TaskItem>()
                 .HasIndex(t => t.Priority)
                 .HasDatabaseName("IX_TaskItem_Priority");

            builder.Entity<TaskItem>()
                .Property(t => t.CreatedAt)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
