using Microsoft.EntityFrameworkCore;
using TaskManager.Models;

namespace TaskManager.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // DbSet for TaskItem entity
        public DbSet<TaskItem> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuration for TaskItem entity
            modelBuilder.Entity<TaskItem>(entity =>
            {
                entity.ToTable("tasks", t => t.HasCheckConstraint("CK_tasks_status",
                    "status IN ('pending', 'in_progress', 'completed')"));

                entity.HasIndex(e => e.Status)
                      .HasDatabaseName("idx_tasks_status");

                entity.Property(e => e.CreateDate)
                      .HasDefaultValueSql("GETUTCDATE()");

                entity.Property(e => e.Status)
                      .HasDefaultValue("pending");
            });
        }
    }
}
