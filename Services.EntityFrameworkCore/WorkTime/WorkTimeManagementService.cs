using Microsoft.EntityFrameworkCore;
using Services.EntityFrameworkCore.Context;
using Services.EntityFrameworkCore.Entities;
using Services.Models;
using Services.WorkTime;
using Task = Services.EntityFrameworkCore.Entities.Task;

namespace Services.EntityFrameworkCore.WorkTime
{
    /// <summary>
    /// Provides a management service for work time using Entity Framework Core.
    /// </summary>
    public class WorkTimeManagementService : IWorkTimeDataManagementService
    {
        private readonly TasksManagementContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkTimeManagementService"/> class.
        /// </summary>
        /// <param name="context">A database context.</param>
        public WorkTimeManagementService(TasksManagementContext context)
        {
            this.context = context;
        }

        /// <inheritdoc/>
        public async Task<IList<WorkTimeDataModel>> GetWorkTimeDataAsync()
        {
            return await this.context.WorkTimeData.Select(data => GetWorkTimeDataModel(data)).ToListAsync().ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public bool TryShowWorkTimeData(int id, out WorkTimeDataModel? data)
        {
            var entity = this.context.Find<WorkTimeData>(id);
            if (entity is null)
            {
                data = null;
                return false;
            }

            data = GetWorkTimeDataModel(entity);
            return true;
        }

        /// <inheritdoc/>
        public async Task<int> CreateWorkTimeDataAsync(WorkTimeDataModel data)
        {
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            var entry = this.context.Add(GetWorkTimeDataEntity(data));
            await this.context.SaveChangesAsync().ConfigureAwait(false);
            return entry.Entity.Id;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateWorkTimeDataAsync(int id, WorkTimeDataModel data)
        {
            var entity = await this.context.FindAsync<WorkTimeData>(id).ConfigureAwait(false);
            if (entity is null)
            {
                return false;
            }

            entity.TaskId = data.TaskId;
            entity.EmployeeId = data.EmployeeId;
            entity.WorkDate = data.WorkDate;
            entity.SpentTime = data.SpentTime;

            this.context.Update(entity);
            return await this.context.SaveChangesAsync().ConfigureAwait(false) > 0;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteWorkTimeDataAsync(int id)
        {
            var entity = await this.context.FindAsync<WorkTimeData>(id);
            if (entity is null)
            {
                return false;
            }

            this.context.Remove(entity);
            return await this.context.SaveChangesAsync().ConfigureAwait(false) > 0;
        }


        /// <inheritdoc/>
        public async Task<IList<EmployeeModel>> GetAllEmployeesWorkedOnTaskAsync(int taskId)
        {
            return await this.context.WorkTimeData.Where(d => d.TaskId == taskId)
                                                  .Select(d => GetEmployeeModel(d.Employee))
                                                  .ToListAsync()
                                                  .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IList<TaskModel>> GetAllEmployeeTasksAsync(int employeeId)
        {
            return await this.context.WorkTimeData.Where(d => d.EmployeeId == employeeId)
                                                  .Select(d => GetTaskModel(d.Task))
                                                  .ToListAsync()
                                                  .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IList<WorkTimeDataModel>> GetWorkTimeDataAsync(int employeeId)
        {
            return await this.context.WorkTimeData.Where(d => d.EmployeeId == employeeId)
                                                  .Select(d => GetWorkTimeDataModel(d))
                                                  .ToListAsync()
                                                  .ConfigureAwait(false);
        }

        private static WorkTimeDataModel GetWorkTimeDataModel(WorkTimeData data) =>
            new()
            {
                Id = data.Id,
                TaskId = data.TaskId,
                EmployeeId = data.EmployeeId,
                WorkDate = data.WorkDate,
                SpentTime = data.SpentTime,
            };

        private static WorkTimeData GetWorkTimeDataEntity(WorkTimeDataModel data) =>
            new()
            {
                Id = data.Id,
                TaskId = data.TaskId,
                EmployeeId = data.EmployeeId,
                WorkDate = data.WorkDate,
                SpentTime = data.SpentTime,
            };

        private static EmployeeModel GetEmployeeModel(Employee employee) =>
            new()
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                PhoneNumber = employee.PhoneNumber,
                Email = employee.Email,
            };

        private static TaskModel GetTaskModel(Task task) =>
            new()
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
            };
    }
}
