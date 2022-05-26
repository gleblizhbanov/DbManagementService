namespace Services.Models
{
    /// <summary>
    /// Represent a work time.
    /// </summary>
    public class WorkTimeDataModel
    {
        /// <summary>
        /// Gets the identifier of the work time object.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets the identifier of the task.
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// Gets the identifier of the 
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the date when the employee worked on the task.
        /// </summary>
        public DateTime WorkDate { get; set; }

        /// <summary>
        /// Gets or sets the time spent on the task.
        /// </summary>
        public TimeSpan SpentTime { get; set; }
    }
}
