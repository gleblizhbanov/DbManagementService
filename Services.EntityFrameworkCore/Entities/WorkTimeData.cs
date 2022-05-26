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
        /// Gets or sets the time spent on the task.
        /// </summary>
        [Required]
        [Column(TypeName = "time")]
        public TimeSpan SpentTime { get; set; }

        [ForeignKey(nameof(TaskId))]
        public virtual Task Task { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public virtual Employee Employee { get; set; }
    }
}
