namespace Services.Models
{
    /// <summary>
    /// Represents an employee.
    /// </summary>
    public class EmployeeModel
    {
        /// <summary>
        /// Gets or sets the employee identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the employee first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the employee last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the employee phone number.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the employee email address.
        /// </summary>
        public string Email { get; set; }
    }
}
