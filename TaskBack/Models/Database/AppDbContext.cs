using Microsoft.EntityFrameworkCore;
using TaskBack.Models.Entities;

namespace TaskBack.Models.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Reminder> Reminders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Department>()
                .HasOne(d => d.ParentDepartment)
                .WithMany(d => d.SubDepartments)
                .HasForeignKey(d => d.ParentDepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
