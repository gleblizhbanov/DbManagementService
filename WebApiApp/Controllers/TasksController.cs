using Microsoft.AspNetCore.Mvc;
using Services.Models;
using Services.Tasks;

namespace WebApiApp.Controllers
{
    /// <summary>
    /// Provides a task API controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskManagementsService taskManagementsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TasksController"/> class.
        /// </summary>
        /// <param name="taskManagementsService">A task management service.</param>
        public TasksController(ITaskManagementsService taskManagementsService)
        {
            this.taskManagementsService = taskManagementsService;
        }

        /// <inheritdoc cref="ITaskManagementsService.CreateTaskAsync"/>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTaskAsync(TaskModel task)
        {
            int id = await this.taskManagementsService.CreateTaskAsync(task).ConfigureAwait(false);
            if (id > 0)
            {
                return CreatedAtAction("CreateTask", new { id });
            }

            return BadRequest();
        }

        /// <returns>An action result with list of tasks.</returns>
        /// <inheritdoc cref="ITaskManagementsService.GetTasksAsync"/>>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<TaskModel>>> GetTasksAsync()
        {
            if (await this.taskManagementsService.GetTasksAsync().ConfigureAwait(false) is not List<TaskModel> tasks)
            {
                return BadRequest();
            }

            return tasks;
        }

        /// <summary>
        /// Gets a task with specific identifier.
        /// </summary>
        /// <param name="id">A task identifier.</param>
        /// <returns>An action result with task if such one found, of an empty action result otherwise.</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TaskModel> GetTask(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            if (this.taskManagementsService.TryShowTask(id, out var task))
            {
                return task!;
            }

            return NotFound();
        }

        /// <returns>An action result.</returns>
        /// <inheritdoc cref="ITaskManagementsService.UpdateTaskAsync"/>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTaskAsync(int id, TaskModel task)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            if (await this.taskManagementsService.UpdateTaskAsync(id, task).ConfigureAwait(false))
            {
                return NoContent();
            }

            return BadRequest();
        }

        /// <returns>An action result.</returns>
        /// <inheritdoc cref="ITaskManagementsService.DeleteTaskAsync"/>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteTaskAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            if (await this.taskManagementsService.DeleteTaskAsync(id))
            {
                return NoContent();
            }

            return BadRequest();
        }
    }
}
