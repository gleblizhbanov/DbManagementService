using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Services.EntityFrameworkCore.Entities
{
    /// <summary>
    /// Represents a task.
    /// </summary>
    [Table("Tasks")]
    public class Task
    {
        /// <summary>
        /// Gets or sets the task identifier.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the task name.
        /// </summary>
        [Required]
        [StringLength(40)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the task description.
        /// </summary>
        [Column(TypeName = "ntext")]
        public string? Description { get; set; }
    }
}
