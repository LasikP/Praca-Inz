using HelpDeskApp.Models.ComplaintViewModel;
using System.Collections.Generic;

namespace HelpDeskApp.Models.DashboardViewModel
{
    public class DashboardDataViewModel
    {
        public DashboardSummaryViewModel DashboardSummaryViewModel { get; set; }
        public List<ComplaintGridViewModel> ComplaintList { get; set; }
    }
}
