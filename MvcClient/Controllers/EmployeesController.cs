using Microsoft.AspNetCore.Mvc;
using MvcClient.Models;
using MvcClient.ViewModels;
using Newtonsoft.Json;

namespace MvcClient.Controllers
{
    [Route("Employees")]
    public class EmployeesController : Controller
    {
        private readonly HttpClient client;

        public EmployeesController()
        {
            this.client = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:7088/api/"),
            };
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employeesJson = await this.client.GetStringAsync("Employees").ConfigureAwait(false);

            var data = await Task.WhenAll(JsonConvert.DeserializeObject<IList<EmployeeModel>>(employeesJson)!
                                                 .Select(async employee => new EmployeeViewModel()
                                                 {
                                                     Employee = employee,
                                                     TasksCount = JsonConvert.DeserializeObject<IList<TaskModel>>(await this.client.GetStringAsync($"Employees/{employee.Id}/tasks").ConfigureAwait(false))?.Count ?? 0,
                                                 })).ConfigureAwait(false);

            ViewData["Title"] = "Employees";
            return View(data);
        }

        [HttpGet("{id:int}/edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var employeeJson = await this.client.GetStringAsync($"Employees/{id}");
            var data = JsonConvert.DeserializeObject<EmployeeModel>(employeeJson);
            ViewData["Title"] = "Edit Employee";
            return View(data);
        }

        [HttpPost("{id:int}/edit")]
        public async Task<IActionResult> Edit(int id, EmployeeModel employee)
        {
            await this.client.PutAsJsonAsync($"Employees/{id}", employee);
            return RedirectToAction("Index");
        }
        
        [HttpGet("create")]
        public IActionResult Create()
        {
            ViewData["Title"] = "Create Employee";
            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(EmployeeModel employee)
        {
            await this.client.PostAsJsonAsync("Employees", employee);
            return RedirectToAction("Index");
        }

        [HttpGet("{id:int}/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            ViewData["Title"] = "Delete Employee";
            var employeeJson = await this.client.GetStringAsync($"Employees/{id}").ConfigureAwait(false);
            var employee = JsonConvert.DeserializeObject<EmployeeModel>(employeeJson);
            return View(employee);
        }

        [HttpPost("{id:int}/delete")]
        public async Task<IActionResult> Delete(EmployeeModel employee)
        {
            await this.client.DeleteAsync($"Employees/{employee.Id}");
            return RedirectToAction("Index");
        }
    }
}
