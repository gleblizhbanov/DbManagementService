using Microsoft.AspNetCore.Mvc;
using Services.Employees;
using Services.Models;
using Services.Tasks;
using Services.WorkTime;

namespace WebApiApp.Controllers
{
    /// <summary>
    /// Provides an employee API controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeManagementService employeeManagementService;
        private readonly ITaskManagementsService tasksManagementsService;
        private readonly IWorkTimeDataManagementService workTimeDataManagementService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeesController"/> class.
        /// </summary>
        /// <param name="employeeManagementService">An employee management service.</param>
        /// <param name="tasksManagementsService">Tasks management service.</param>
        /// <param name="workTimeDataManagementService">Work time data management service.</param>
        public EmployeesController(IEmployeeManagementService employeeManagementService, ITaskManagementsService tasksManagementsService, IWorkTimeDataManagementService workTimeDataManagementService)
        {
            this.employeeManagementService = employeeManagementService;
            this.tasksManagementsService = tasksManagementsService;
            this.workTimeDataManagementService = workTimeDataManagementService;
        }

        /// <inheritdoc cref="IEmployeeManagementService.CreateEmployeeAsync"/>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateEmployeeAsync(EmployeeModel employee)
        {
            int id = await this.employeeManagementService.CreateEmployeeAsync(employee).ConfigureAwait(false);
            if (id > 0)
            {
                return CreatedAtAction("CreateEmployee", new { id });
            }

            return BadRequest();
        }

        /// <returns>An action result with list of employees.</returns>
        /// <inheritdoc cref="IEmployeeManagementService.GetEmployeesAsync"/>>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<EmployeeModel>>> GetEmployeesAsync()
        {
            if (await this.employeeManagementService.GetEmployeesAsync().ConfigureAwait(false) is not List<EmployeeModel> employees)
            {
                return BadRequest();
            }

            return employees;
        }

        /// <summary>
        /// Gets an employee with specific identifier.
        /// </summary>
        /// <param name="id">An employee identifier.</param>
        /// <returns>An action result with employee if such one found, of an empty action result otherwise.</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<EmployeeModel> GetEmployee(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            if (this.employeeManagementService.TryShowEmployee(id, out var employee))
            {
                return employee!;
            }

            return NotFound();
        }

        /// <returns>An action result.</returns>
        /// <inheritdoc cref="IEmployeeManagementService.UpdateEmployeeAsync"/>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateEmployeeAsync(int id, EmployeeModel employee)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            if (await this.employeeManagementService.UpdateEmployeeAsync(id, employee).ConfigureAwait(false))
            {
                return NoContent();
            }

            return BadRequest();
        }

        /// <returns>An action result.</returns>
        /// <inheritdoc cref="IEmployeeManagementService.DeleteEmployeeAsync"/>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteEmployeeAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            if (await this.employeeManagementService.DeleteEmployeeAsync(id))
            {
                return NoContent();
            }

            return BadRequest();
        }

        /// <returns>An action result with list of tasks.</returns>
        /// <inheritdoc cref="IWorkTimeDataManagementService.GetAllEmployeeTasksAsync"/>
        [HttpGet("{id:int}/tasks")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IList<TaskModel>>> GetTasks(int employeeId)
        {
            if (employeeId <= 0)
            {
                return BadRequest();
            }

            if (!this.employeeManagementService.TryShowEmployee(employeeId, out _))
            {
                return NotFound();
            }

            if (await this.workTimeDataManagementService.GetAllEmployeeTasksAsync(employeeId).ConfigureAwait(false) is not List<TaskModel> tasks)
            {
                return BadRequest();
            }

            return tasks;
        }

        /// <returns>An action result with list of work time data.</returns>
        /// <inheritdoc cref="IWorkTimeDataManagementService.GetWorkTimeDataAsync(int)"/>
        [HttpGet("{employeeId:int}/data")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IList<WorkTimeDataModel>>> GetWorkTimeDataAsync(int employeeId)
        {
            if (employeeId <= 0)
            {
                return BadRequest();
            }

            if (!this.employeeManagementService.TryShowEmployee(employeeId, out _))
            {
                return NotFound();
            }

            if (await this.workTimeDataManagementService.GetWorkTimeDataAsync(employeeId).ConfigureAwait(false) is not List<WorkTimeDataModel> data)
            {
                return NotFound();
            }

            return data;
        }
    }
}
