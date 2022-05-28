using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MvcClient.Models;

namespace MvcClient.ViewModels
{
    /// <summary>
    /// Represent a work time.
    /// </summary>
    public class WorkTimeDataViewModel
    {
        /// <summary>
        /// Gets the identifier of the work time object.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the task.
        /// </summary>
        public TaskModel Task { get; set; }

        /// <summary>
        /// Gets or sets the employee.
        /// </summary>
        public EmployeeModel Employee { get; set; }

        /// <summary>
        /// Gets or sets the date when the employee worked on the task.
        /// </summary>
        [DisplayName("Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime WorkDate { get; set; }

        /// <summary>
        /// Gets or sets the time when the employee started working on the task.
        /// </summary>
        [DisplayName("Start time")]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}")]
        [DataType(DataType.Time)]
        public TimeSpan StartTime { get; set; }

        /// <summary>
        /// Gets or sets the time when the employee stopped working on the task.
        /// </summary>
        [DisplayName("Stop time")]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}")]
        [DataType(DataType.Time)]
        public TimeSpan StopTime { get; set; }
    }
}
