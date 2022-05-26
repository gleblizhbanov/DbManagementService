using Services.Models;

namespace Services.Tasks
{
    /// <summary>
    /// Represents a management service for tasks.
    /// </summary>
    public interface ITaskManagementsService
    {
        /// <summary>
        /// Returns the list of all tasks.
        /// </summary>
        /// <returns>A list of tasks.</returns>
        Task<IList<TaskModel>> GetTasksAsync();

        /// <summary>
        /// Tries to get a task with a specific identifier.
        /// </summary>
        /// <param name="taskId">A task identifier.</param>
        /// <param name="task">A found task.</param>
        /// <returns>True if task was returned, false otherwise.</returns>
        bool TryShowTask(int taskId, out TaskModel? task);

        /// <summary>
        /// Adds a new task to storage.
        /// </summary>
        /// <param name="task">A task to add.</param>
        /// <returns>The identifier of the added task.</returns>
        Task<int> CreateTaskAsync(TaskModel task);

        /// <summary>
        /// Updated a task with specific identifier.
        /// </summary>
        /// <param name="taskId">A task identifier.</param>
        /// <param name="task">New task data.</param>
        /// <returns>True if task data was updated successfully, false otherwise.</returns>
        Task<bool> UpdateTaskAsync(int taskId, TaskModel task);

        /// <summary>
        /// Deletes a task with specific identifier.
        /// </summary>
        /// <param name="taskId">A task identifier.</param>
        /// <returns>True if task data was deleted successfully, false otherwise.</returns>
        Task<bool> DeleteTaskAsync(int taskId);
    }
}
