using HelpDeskApp.Models.CompanyInfoViewModel;
using System.Collections.Generic;

namespace HelpDeskApp.Models.ReportViewModel
{
    public class ManageReportViewModel
    {
        public List<AssignToSummaryViewModel> AssignToSummaryViewModel { get; set; }
        public ComplaintStatusSummaryViewModel ComplaintStatusSummaryViewModel { get; set; }
        public CompanyInfoCRUDViewModel CompanyInfoCRUDViewModel { get; set; }
    }
}
