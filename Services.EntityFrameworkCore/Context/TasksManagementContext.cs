using Microsoft.EntityFrameworkCore;
using Services.EntityFrameworkCore.Entities;
using Task = Services.EntityFrameworkCore.Entities.Task;

namespace Services.EntityFrameworkCore.Context
{
    public class TasksManagementContext : DbContext
    {
        public TasksManagementContext(DbContextOptions<TasksManagementContext> options) : base(options)
        {   
        }

        public virtual DbSet<Employee> Employees { get; set; }

        public virtual DbSet<Task> Tasks { get; set; }

        public virtual DbSet<WorkTimeData> WorkTimeData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id)
                      .IsClustered();
                entity.ToView("Employees");
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.HasKey(t => t.Id)
                      .IsClustered();
                entity.ToView("Tasks");
            });

            modelBuilder.Entity<WorkTimeData>(entity =>
            {
                entity.HasOne(d => d.Task)
                      .WithMany()
                      .HasForeignKey(d => d.TaskId);

                entity.HasOne(d => d.Employee)
                      .WithMany()
                      .HasForeignKey(d => d.EmployeeId);
            });
        }
    }
}
