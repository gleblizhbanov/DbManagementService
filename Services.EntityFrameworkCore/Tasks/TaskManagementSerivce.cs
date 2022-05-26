using Microsoft.EntityFrameworkCore;
using Services.EntityFrameworkCore.Context;
using Services.Models;
using Services.Tasks;
using Task = Services.EntityFrameworkCore.Entities.Task;

namespace Services.EntityFrameworkCore.Tasks
{
    /// <summary>
    /// Provides a management service for tasks using Entity Framework Core.
    /// </summary>
    public class TaskManagementService : ITaskManagementsService
    {
        private readonly TasksManagementContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskManagementService"/> class.
        /// </summary>
        /// <param name="context">A database context.</param>
        public TaskManagementService(TasksManagementContext context)
        {
            this.context = context;
        }

        /// <inheritdoc/>
        public async Task<IList<TaskModel>> GetTasksAsync()
        {
            return await this.context.Tasks.Select(t => GetTaskModel(t)).ToListAsync().ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public bool TryShowTask(int taskId, out TaskModel? task)
        {
            var entity = this.context.Find<Task>(taskId);
            if (entity is null)
            {
                task = null;
                return false;
            }

            task = GetTaskModel(entity);
            return true;
        }

        /// <inheritdoc/>
        public async Task<int> CreateTaskAsync(TaskModel task)
        {
            if (task is null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            var entry = await this.context.AddAsync(GetTaskEntity(task)).ConfigureAwait(false);
            await this.context.SaveChangesAsync().ConfigureAwait(false);
            return entry.Entity.Id;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateTaskAsync(int taskId, TaskModel task)
        {
            var entity = await this.context.FindAsync<Task>(taskId);
            if (entity is null)
            {
                return false;
            }

            entity.Name = task.Name;
            entity.Description = task.Description;

            this.context.Update(entity);
            return await this.context.SaveChangesAsync().ConfigureAwait(false) > 0;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteTaskAsync(int taskId)
        {
            var entity = await this.context.FindAsync<Task>(taskId).ConfigureAwait(false);
            if (entity is null)
            {
                return false;
            }

            this.context.Remove(entity);
            return await this.context.SaveChangesAsync().ConfigureAwait(false) > 0;
        }

        private static TaskModel GetTaskModel(Task task) =>
            new()
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
            };

        private static Task GetTaskEntity(TaskModel task) =>
            new()
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
            };
    }
}
