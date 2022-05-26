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
    }
}
