using MvcClient.Models;

namespace MvcClient.ViewModels
{
    public class EmployeeViewModel
    {
        public EmployeeModel Employee { get; set; }

        public int TasksCount { get; set; }
    }
}
