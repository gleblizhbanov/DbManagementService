using Microsoft.AspNetCore.Mvc;
using MvcClient.Models;
using MvcClient.ViewModels;
using Newtonsoft.Json;

namespace MvcClient.Controllers
{
    [Route("[controller]")]
    public class TasksController : Controller
    {
        private readonly HttpClient client;

        public TasksController()
        {
            this.client = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:7088/api/"),
            };
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var tasksJson = await this.client.GetStringAsync("Tasks");
            var tasks = await Task.WhenAll(JsonConvert.DeserializeObject<IList<TaskModel>>(tasksJson)!
                                               .Select(async task => new TaskViewModel()
                                               {
                                                   Task = task,
                                                   EmployeesCount = JsonConvert.DeserializeObject<IList<EmployeeModel>>(await this.client.GetStringAsync($"Tasks/{task.Id}/employees").ConfigureAwait(false))?.Count ?? 0,
                                               }));

            ViewData["Title"] = "Tasks";
            return View(tasks);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            ViewData["Title"] = "Create task";
            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(TaskModel task)
        {
            await this.client.PostAsJsonAsync("Tasks", task);
            return RedirectToAction("Index");
        }

        [HttpGet("{id:int}/edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var taskJson = await this.client.GetStringAsync($"Tasks/{id}").ConfigureAwait(false);
            var task = JsonConvert.DeserializeObject<TaskModel>(taskJson);
            ViewData["Title"] = "Edit task";
            return View(task);
        }

        [HttpPost("{id:int}/edit")]
        public async Task<IActionResult> Edit(int id, TaskModel task)
        {
            await this.client.PutAsJsonAsync($"Tasks/{id}", task);
            return RedirectToAction("Index");
        }

        [HttpGet("{id:int}/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var taskJson = await this.client.GetStringAsync($"Tasks/{id}").ConfigureAwait(false);
            var task = JsonConvert.DeserializeObject<TaskModel>(taskJson);
            ViewData["Title"] = "Delete task";
            return View(task);
        }

        [HttpPost("{id:int}/delete")]
        public async Task<IActionResult> Delete(TaskModel task)
        {
            await this.client.DeleteAsync($"Tasks/{task.Id}");
            return RedirectToAction("Index");
        }
    }
}
