using Services.Models;

namespace Services.WorkTime
{
    /// <summary>
    /// Represents a management service for work time data.
    /// </summary>
    public interface IWorkTimeDataManagementService
    {
        /// <summary>
        /// Returns the list with all work time data.
        /// </summary>
        /// <returns>A list with all work time data.</returns>
        Task<IList<WorkTimeDataModel>> GetWorkTimeDataAsync();

        /// <summary>
        /// Tries to get work time data with a specific identifier.
        /// </summary>
        /// <param name="id">Work time data identifier.</param>
        /// <param name="data">Found data.</param>
        /// <returns>True if data was returned, false otherwise.</returns>
        bool TryShowWorkTimeData(int id, out WorkTimeDataModel? data);

        /// <summary>
        /// Adds work time data to storage.
        /// </summary>
        /// <param name="data">Work time data to add.</param>
        /// <returns>The identifier of the added data.</returns>
        Task<int> CreateWorkTimeDataAsync(WorkTimeDataModel data);

        /// <summary>
        /// Updated work time data with specific identifier.
        /// </summary>
        /// <param name="id">Work time data identifier.</param>
        /// <param name="data">New work time data.</param>
        /// <returns>True if work time data was updated successfully, false otherwise.</returns>
        Task<bool> UpdateWorkTimeDataAsync(int id, WorkTimeDataModel data);

        /// <summary>
        /// Deletes work time data with specific identifier.
        /// </summary>
        /// <param name="id">Work time data identifier.</param>
        /// <returns>True if work time data was deleted successfully, false otherwise.</returns>
        Task<bool> DeleteWorkTimeDataAsync(int id);

        /// <summary>
        /// Returns a list of all employees who worked on given task.
        /// </summary>
        /// <param name="taskId">A task identifier.</param>
        /// <returns>A list of employees.</returns>
        Task<IList<EmployeeModel>> GetAllEmployeesWorkedOnTaskAsync(int taskId);

        /// <summary>
        /// Returns a list of all employees who worked on given task.
        /// </summary>
        /// <param name="employeeId">A task identifier.</param>
        /// <returns>A list of employees.</returns>
        Task<IList<TaskModel>> GetAllEmployeeTasksAsync(int employeeId);

        /// <summary>
        /// Returns a list of all work time data of a given employee.
        /// </summary>
        /// <param name="employeeId">An employee identifier.</param>
        /// <returns>A list of work time data.</returns>
        Task<IList<WorkTimeDataModel>> GetWorkTimeDataAsync(int employeeId);

        /// <summary>
        /// Returns an employee from given work time data.
        /// </summary>
        /// <param name="workTimeDataId">Work time data identifier.</param>
        /// <returns>Employee from work time data.</returns>
        Task<EmployeeModel> GetEmployeeAsync(int workTimeDataId);

        /// <summary>
        /// Returns a task from given work time data.
        /// </summary>
        /// <param name="workTimeDataId">Work time data identifier.</param>
        /// <returns>Task from work time data.</returns>
        Task<TaskModel> GetTaskAsync(int workTimeDataId);
    }
}
