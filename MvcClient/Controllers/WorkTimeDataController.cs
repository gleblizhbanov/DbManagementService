using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcClient.Models;
using MvcClient.ViewModels;
using Newtonsoft.Json;

namespace MvcClient.Controllers
{
    [Route("work-data")]
    public class WorkTimeDataController : Controller
    {
        private readonly HttpClient client;

        public WorkTimeDataController()
        {
            this.client = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:7088/api/"),
            };
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var dataJson = await this.client.GetStringAsync("work-data").ConfigureAwait(false);
            var data = await Task.WhenAll(JsonConvert.DeserializeObject<IList<WorkTimeDataModel>>(dataJson)!.Select(GetViewModelAsync));
            ViewData["Title"] = "Work data";
            return View(data);
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            var employeesJson = await this.client.GetStringAsync("Employees").ConfigureAwait(false);
            var employees = JsonConvert.DeserializeObject<IList<EmployeeModel>>(employeesJson)!;
            ViewBag.Employees = employees.Select(e => new SelectListItem($"{e.FirstName} {e.LastName}", e.Id.ToString()));

            var tasksJson = await this.client.GetStringAsync("Tasks").ConfigureAwait(false);
            var tasks = JsonConvert.DeserializeObject<IList<TaskModel>>(tasksJson)!;
            ViewBag.Tasks = tasks.Select(t => new SelectListItem(t.Name, t.Id.ToString()));

            ViewData["Title"] = "Create work data";
            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(WorkTimeDataModel workTimeData)
        {
            await this.client.PostAsJsonAsync("work-data", workTimeData).ConfigureAwait(false);
            return RedirectToAction("Index");
        }

        [HttpGet("{id:int}/edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var dataJson = await this.client.GetStringAsync($"work-data/{id}").ConfigureAwait(false);
            var data = JsonConvert.DeserializeObject<WorkTimeDataModel>(dataJson);
            ViewData["Title"] = "Edit work data";

            var employeesJson = await this.client.GetStringAsync("Employees").ConfigureAwait(false);
            var employees = JsonConvert.DeserializeObject<IList<EmployeeModel>>(employeesJson)!;
            ViewBag.Employees = employees.Select(e => new SelectListItem($"{e.FirstName} {e.LastName}", e.Id.ToString()));

            var tasksJson = await this.client.GetStringAsync("Tasks").ConfigureAwait(false);
            var tasks = JsonConvert.DeserializeObject<IList<TaskModel>>(tasksJson)!;
            ViewBag.Tasks = tasks.Select(t => new SelectListItem(t.Name, t.Id.ToString()));
            return View(data);
        }

        [HttpPost("{id:int}/edit")]
        public async Task<IActionResult> Edit(int id, WorkTimeDataModel data)
        {
            await this.client.PutAsJsonAsync($"work-data/{id}", data).ConfigureAwait(false);
            return RedirectToAction("Index");
        }

        [HttpGet("{id:int}/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var dataJson = await this.client.GetStringAsync($"work-data/{id}").ConfigureAwait(false);
            var data = JsonConvert.DeserializeObject<WorkTimeDataModel>(dataJson)!;
            var viewModel = await GetViewModelAsync(data);
            ViewData["Title"] = "Delete work data";
            return View(GetWorkDataViewModel(viewModel));
        }

        [HttpPost("{id:int}/delete")]
        public async Task<IActionResult> Delete(WorkTimeDataModel data)
        {
            await this.client.DeleteAsync($"work-data/{data.Id}").ConfigureAwait(false);
            return RedirectToAction("Index");
        }

        //[HttpGet("report")]
        //public async Task<IActionResult> Report()
        //{
        //    var dataJson = await this.client.GetStringAsync("work-data").ConfigureAwait(false);
        //    var data = await Task.WhenAll(JsonConvert.DeserializeObject<IList<WorkTimeDataModel>>(dataJson)!.Select(GetViewModelAsync));
        //    var viewData = data.Select(GetWorkDataViewModel).OrderBy(x => x.Task.Name);
        //    ViewData["Title"] = "Report";
        //    return View(new ReportViewModel()
        //    {
        //        Data = viewData,
        //        Month = DateTime.Today,
        //    });
        //}

        [HttpGet("report")]
        public async Task<IActionResult> Report(string month)
        {
            if (!DateTime.TryParse(month, out var selectedMonth))
            {
                selectedMonth = DateTime.Today;
            }

            var dataJson = await this.client.GetStringAsync("work-data").ConfigureAwait(false);
            var data = await Task.WhenAll(JsonConvert.DeserializeObject<IList<WorkTimeDataModel>>(dataJson)!
                                                     .Where(d => d.WorkDate.Year == selectedMonth.Year && d.WorkDate.Month == selectedMonth.Month)
                                                     .Select(GetViewModelAsync));
            var viewData = data.Select(GetWorkDataViewModel)
                                                     .GroupBy(model => model.Task.Name)
                                                     .Select(grouping => grouping.GroupBy(x => x.Employee)
                                                                                 .Select(g => new WorkDataViewModel()
                                                                                                                                {
                                                                                                                                    Employee = g.First().Employee,
                                                                                                                                    Task = grouping.First().Task,
                                                                                                                                    Duration = g.Sum(x => x.Duration),
                                                                                                                                }).MaxBy(x => x.Duration));
            ViewData["Title"] = "Report";
            return View(new ReportViewModel()
            {
                Data = viewData!,
                Month = selectedMonth,
            });
        }

        private async Task<WorkTimeDataViewModel> GetViewModelAsync(WorkTimeDataModel model) =>
            new()
            {
                Id = model.Id,
                Task = JsonConvert.DeserializeObject<TaskModel>(await this.client.GetStringAsync($"work-data/{model.Id}/task").ConfigureAwait(false))!,
                Employee = JsonConvert.DeserializeObject<EmployeeModel>(await this.client.GetStringAsync($"work-data/{model.Id}/employee").ConfigureAwait(false))!,
                WorkDate = model.WorkDate,
                StartTime = model.StartTime,
                StopTime = model.StopTime,
            };

        private WorkDataViewModel GetWorkDataViewModel(WorkTimeDataViewModel model) =>
            new()
            {
                Id = model.Id,
                Employee = model.Employee,
                Task = model.Task,
                WorkDate = model.WorkDate,
                Duration = (model.StopTime - model.StartTime).TotalHours,
            };
    }
}
