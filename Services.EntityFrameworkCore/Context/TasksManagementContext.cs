using Microsoft.EntityFrameworkCore;
using Services.EntityFrameworkCore.Entities;
using Task = Services.EntityFrameworkCore.Entities.Task;

namespace Services.EntityFrameworkCore.Context
{
    /// <summary>
    /// Provides a database context class
    /// </summary>
    public class TasksManagementContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TasksManagementContext"/> class.
        /// </summary>
        /// <param name="options">Database context options.</param>
        public TasksManagementContext(DbContextOptions<TasksManagementContext> options) : base(options)
        {   
        }
        
        /// <summary>
        /// Gets or sets the list of employees.
        /// </summary>
        public virtual DbSet<Employee> Employees { get; set; }

        /// <summary>
        /// Gets or sets the list of tasks.
        /// </summary>
        public virtual DbSet<Task> Tasks { get; set; }

        /// <summary>
        /// Gets or sets the work time data.
        /// </summary>
        public virtual DbSet<WorkTimeData> WorkTimeData { get; set; }

        /// <inheritdoc/>
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
                entity.HasKey(d => d.Id)
                      .IsClustered();

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
