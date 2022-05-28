using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Services.EntityFrameworkCore.Entities
{
    /// <summary>
    /// Represent a work time.
    /// </summary>
    [Table("WorkTimeData")]
    public class WorkTimeData
    {
        /// <summary>
        /// Gets the identifier of the work time object.
        /// </summary>
        [Key]
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
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime WorkDate { get; set; }

        /// <summary>
        /// Gets or sets the time when employee started working on the task.
        /// </summary>
        [Required]
        [Column(TypeName = "time")]
        public TimeSpan StartTime { get; set; }

        /// <summary>
        /// Gets or sets the time when employee stopped working on the task.
        /// </summary>
        [Required]
        [Column(TypeName = "time")]
        public TimeSpan StopTime { get; set; }

        [ForeignKey(nameof(TaskId))]
        public Task Task { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public Employee Employee { get; set; }
    }
}
