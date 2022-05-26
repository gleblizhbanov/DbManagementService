using Microsoft.AspNetCore.Mvc;
using Services.Models;
using Services.WorkTime;

namespace WebApiApp.Controllers
{
    /// <summary>
    /// Provides a work time data API controller.
    /// </summary>
    [Route("api/data")]
    [ApiController]
    public class WorkTimeDataController : ControllerBase
    {
        private readonly IWorkTimeDataManagementService workTimeDataManagementService;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkTimeDataController"/> class.
        /// </summary>
        /// <param name="workTimeDataManagementService">Work time data management service.</param>
        public WorkTimeDataController(IWorkTimeDataManagementService workTimeDataManagementService)
        {
            this.workTimeDataManagementService = workTimeDataManagementService;
        }

        /// <inheritdoc cref="IWorkTimeDataManagementService.CreateWorkTimeDataAsync"/>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateWorkTimeDataAsync(WorkTimeDataModel data)
        {
            int id = await this.workTimeDataManagementService.CreateWorkTimeDataAsync(data).ConfigureAwait(false);
            if (id > 0)
            {
                return CreatedAtAction("CreateWorkTimeData", new { id });
            }

            return BadRequest();
        }

        /// <returns>An action result with list of work time data.</returns>
        /// <inheritdoc cref="IWorkTimeDataManagementService.GetWorkTimeDataAsync"/>>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<WorkTimeDataModel>>> GetAllWorkTimeDataAsync()
        {
            if (await this.workTimeDataManagementService.GetWorkTimeDataAsync().ConfigureAwait(false) is not List<WorkTimeDataModel> data)
            {
                return BadRequest();
            }

            return data;
        }

        /// <summary>
        /// Gets work time data with specific identifier.
        /// </summary>
        /// <param name="id">Work time data identifier.</param>
        /// <returns>An action result with work time data if such one found, of an empty action result otherwise.</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<WorkTimeDataModel> GetWorkTimeData(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            if (this.workTimeDataManagementService.TryShowWorkTimeData(id, out var data))
            {
                return data!;
            }

            return NotFound();
        }

        /// <returns>An action result.</returns>
        /// <inheritdoc cref="IWorkTimeDataManagementService.UpdateWorkTimeDataAsync"/>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateWorkTimeDataAsync(int id, WorkTimeDataModel data)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            if (await this.workTimeDataManagementService.UpdateWorkTimeDataAsync(id, data).ConfigureAwait(false))
            {
                return NoContent();
            }

            return BadRequest();
        }

        /// <returns>An action result.</returns>
        /// <inheritdoc cref="IWorkTimeDataManagementService.DeleteWorkTimeDataAsync"/>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteWorkTimeDataAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            if (await this.workTimeDataManagementService.DeleteWorkTimeDataAsync(id))
            {
                return NoContent();
            }

            return BadRequest();
        }
    }
}
