using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace MvcClient.Models
{
    /// <summary>
    /// Represents an employee.
    /// </summary>
    public class EmployeeModel
    {
        /// <summary>
        /// Gets or sets the employee identifier.
        /// </summary>
        [DisplayName("ID")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the employee first name.
        /// </summary>
        [DisplayName("First name")]
        [StringLength(10)]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the employee last name.
        /// </summary>
        [DisplayName("Last name")]
        [StringLength(20)]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the employee phone number.
        /// </summary>
        [DisplayName("Phone number")]
        [StringLength(20, MinimumLength = 10)]
        [RegularExpression(@"^(\+\s?)?((?<!\+.*)\(\+?\d+([\s\-\.]?\d+)?\)|\d+)([\s\-\.]?(\(\d+([\s\-\.]?\d+)?\)|\d+))*(\s?(x|ext\.?)\s?\d+)?$", ErrorMessage = "The Phone number field is not a valid phone number")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the employee email address.
        /// </summary>
        [DisplayName("E-mail")]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }
    }
}
