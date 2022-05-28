using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MvcClient.Models;

namespace MvcClient.ViewModels
{
    /// <summary>
    /// Represent a work time.
    /// </summary>
    public class WorkDataViewModel
    {
        /// <summary>
        /// Gets or sets the identifier of the work time object.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the task.
        /// </summary>
        public TaskModel Task { get; set; }

        /// <summary>
        /// Gets or sets the Employee.
        /// </summary>
        public EmployeeModel Employee { get; set; }

        /// <summary>
        /// Gets or sets the date when the employee worked on the task.
        /// </summary>
        [DisplayName("Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime WorkDate { get; set; }

        /// <summary>
        /// Gets or sets the duration of work.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:F1}")]
        public double Duration { get; set; }
    }
}
