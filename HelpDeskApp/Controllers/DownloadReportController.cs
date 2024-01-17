using HelpDeskApp.Data;
using HelpDeskApp.Models.ComplaintViewModel;
using HelpDeskApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace HelpDeskApp.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class DownloadReportController : Controller
    {
        private readonly string footer = "--footer-center \"Printed on: " + DateTime.Now.Date.ToString("MM/dd/yyyy") + "  Page: [page]/[toPage]\"" + " --footer-line --footer-font-size \"10\" --footer-spacing 6 --footer-font-name \"calibri light\"";
        private readonly ApplicationDbContext _context;
        private readonly ICommon _iCommon;

        public DownloadReportController(ApplicationDbContext context, ICommon iCommon)
        {
            _context = context;
            _iCommon = iCommon;
        }

        [Authorize(Roles = Pages.MainMenu.UserProfile.RoleName)]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DownloadComplaintReportPDF(long? id)
        {
            ComplaintManageViewModel _ComplaintManageViewModel = new ComplaintManageViewModel();
            _ComplaintManageViewModel.ComplaintGridViewModel = _iCommon.GetComplaintViewItem().Where(x => x.Id == id).SingleOrDefault();
            _ComplaintManageViewModel.CommentList = _context.Comment.Where(x => x.ComplaintId == id && x.Cancelled == false).ToList();
            _ComplaintManageViewModel.AttachmentFileList = _context.AttachmentFile.Where(x => x.ComplaintId == id && x.Cancelled == false).ToList();
            _ComplaintManageViewModel.CompanyInfoCRUDViewModel = _iCommon.GetCompanyInfo();
            var rpt = new ViewAsPdf();
            rpt.PageOrientation = Orientation.Portrait;
            rpt.CustomSwitches = footer;

            rpt.FileName = "Complaint_Report_" + id + ".pdf";
            rpt.ViewName = "DownloadComplaintReportPDF";
            rpt.Model = _ComplaintManageViewModel;
            return rpt;
        }

    }
}
