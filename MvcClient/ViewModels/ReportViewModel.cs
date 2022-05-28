namespace MvcClient.ViewModels
{
    public class ReportViewModel
    {
        public IEnumerable<WorkDataViewModel> Data { get; set; }

        public DateTime Month { get; set; } = DateTime.Today;
    }
}
