using Microsoft.AspNetCore.Mvc;
using Services.EntityFrameworkCore.Context;

namespace WebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private TasksManagementContext context;

        public EmployeesController(TasksManagementContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return this.Ok();
        }
    }
}
