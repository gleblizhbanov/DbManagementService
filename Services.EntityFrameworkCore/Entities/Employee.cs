using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Services.EntityFrameworkCore.Entities
{
    /// <summary>
    /// Represents an employee.
    /// </summary>
    [Table("Employees")]
    public class Employee
    {
        /// <summary>
        /// Gets or sets the employee identifier.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the employee first name.
        /// </summary>
        [Required]
        [StringLength(10)]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the employee last name.
        /// </summary>
        [Required]
        [StringLength(20)]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the employee phone number.
        /// </summary>
        [Required]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the employee email address.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
    }
}
