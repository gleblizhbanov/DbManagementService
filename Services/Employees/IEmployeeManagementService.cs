using Services.Models;

namespace Services.Employees
{
    /// <summary>
    /// Represents a managements service for employees.
    /// </summary>
    public interface IEmployeeManagementService
    {
        /// <summary>
        /// Returns the list of all employees.
        /// </summary>
        /// <returns>A list of employees.</returns>
        Task<IList<EmployeeModel>> GetEmployeesAsync();

        /// <summary>
        /// Tries to get an employee with a specific identifier.
        /// </summary>
        /// <param name="employeeId">An employee identifier.</param>
        /// <param name="employee">A found employee.</param>
        /// <returns>True if employee was returned, false otherwise.</returns>
        bool TryShowEmployee(int employeeId, out EmployeeModel? employee);
        
        /// <summary>
        /// Adds a new employee to storage.
        /// </summary>
        /// <param name="employee">An employee to add.</param>
        /// <returns>The identifier of the added employee.</returns>
        Task<int> CreateEmployeeAsync(EmployeeModel employee);
        
        /// <summary>
        /// Updated an employee with specific identifier.
        /// </summary>
        /// <param name="employeeId">An employee identifier.</param>
        /// <param name="employee">New employee data.</param>
        /// <returns>True if employee data was updated successfully, false otherwise.</returns>
        Task<bool> UpdateEmployeeAsync(int employeeId, EmployeeModel employee);
        
        /// <summary>
        /// Deletes an employee with specific identifier.
        /// </summary>
        /// <param name="employeeId">An employee identifier.</param>
        /// <returns>True if employee data was deleted successfully, false otherwise.</returns>
        Task<bool> DeleteEmployeeAsync(int employeeId);
    }
}