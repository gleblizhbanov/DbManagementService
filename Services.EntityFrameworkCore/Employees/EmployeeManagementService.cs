using Microsoft.EntityFrameworkCore;
using Services.Employees;
using Services.EntityFrameworkCore.Context;
using Services.EntityFrameworkCore.Entities;
using Services.Models;

namespace Services.EntityFrameworkCore.Employees
{
    /// <summary>
    /// Provides a management service for employees using Entity Framework Core.
    /// </summary>
    public class EmployeeManagementService : IEmployeeManagementService
    {
        private readonly TasksManagementContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeManagementService"/> class.
        /// </summary>
        /// <param name="context">A database context.</param>
        public EmployeeManagementService(TasksManagementContext context)
        {
            this.context = context;
        }

        /// <inheritdoc/>
        public async Task<IList<EmployeeModel>> GetEmployeesAsync()
        {
            return await this.context.Employees.Select(e => GetEmployeeModel(e)).ToListAsync().ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public bool TryShowEmployee(int employeeId, out EmployeeModel? employee)
        {
            var entity = this.context.Find<Employee>(employeeId);
            if (entity is null)
            {
                employee = null;
                return false;
            }

            employee = GetEmployeeModel(entity);
            return true;
        }

        /// <inheritdoc/>
        public async Task<int> CreateEmployeeAsync(EmployeeModel employee)
        {
            if (employee is null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            var entry = this.context.Add(GetEmployeeEntity(employee));
            await this.context.SaveChangesAsync();
            return entry.Entity.Id;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateEmployeeAsync(int employeeId, EmployeeModel employee)
        {
            var entity = await this.context.FindAsync<Employee>(employeeId).ConfigureAwait(false);
            if (entity is null)
            {
                return false;
            }
            
            entity.FirstName = employee.FirstName;
            entity.LastName = employee.LastName;
            entity.PhoneNumber = employee.PhoneNumber;
            entity.Email = employee.Email;

            this.context.Update(entity);
            return await this.context.SaveChangesAsync().ConfigureAwait(false) > 0;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteEmployeeAsync(int employeeId)
        {
            var entity = await this.context.FindAsync<Employee>(employeeId).ConfigureAwait(false);
            if (entity is null)
            {
                return false;
            }

            this.context.Remove(entity);
            return await this.context.SaveChangesAsync().ConfigureAwait(false) > 0;
        }

        private static EmployeeModel GetEmployeeModel(Employee employee) =>
            new()
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                PhoneNumber = employee.PhoneNumber,
                Email = employee.Email,
            };

        private static Employee GetEmployeeEntity(EmployeeModel employee) =>
            new()
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                PhoneNumber = employee.PhoneNumber,
                Email = employee.Email,
            };
    }
}
