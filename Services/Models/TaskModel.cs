namespace Services.Models
{
    /// <summary>
    /// Represents a task.
    /// </summary>
    public class TaskModel
    {
        /// <summary>
        /// Gets or sets the task identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the task name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the task description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the employee which worked on the task.
        /// </summary>
        public int? EmployeeId { get; set; }
    }
}
