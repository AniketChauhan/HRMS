using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace HRMS.Controllers
{
    [Authorize(Roles ="admin")]
    public class EmpReportController : Controller
    {
        // GET: EmpReport
        public ActionResult Index()
        {
           ReportViewer rptViewer = new ReportViewer();
            rptViewer.ProcessingMode = ProcessingMode.Remote;
            rptViewer.SizeToReportContent = true;
            rptViewer.ZoomMode = ZoomMode.PageWidth;
            rptViewer.Width = Unit.Percentage(100);
            rptViewer.Height = Unit.Percentage(100);
            rptViewer.AsyncRendering = true;
            rptViewer.ServerReport.ReportServerUrl = new Uri("http://DESKTOP-5FDC8M5:80/ReportServer");
            rptViewer.ServerReport.ReportPath = "/Report Project1/EmployeeReport";
            ViewBag.ReportViewer = rptViewer;

            return View();
        }
        public ActionResult TravelReporttt()
        {
             ReportViewer rptViewer = new ReportViewer();
            rptViewer.ProcessingMode = ProcessingMode.Remote;
            rptViewer.SizeToReportContent = true;
            rptViewer.ZoomMode = ZoomMode.PageWidth;
            rptViewer.Width = Unit.Percentage(100);
            rptViewer.Height = Unit.Percentage(100);
            rptViewer.AsyncRendering = true;
            rptViewer.ServerReport.ReportServerUrl = new Uri("http://DESKTOP-5FDC8M5:80/ReportServer");
            rptViewer.ServerReport.ReportPath = "/Report Project1/TravelReport";
            ViewBag.ReportViewer = rptViewer;

            return View();
        }
        public ActionResult TravelExpanseReporttt()
        {
            ReportViewer rptViewer = new ReportViewer();
            rptViewer.ProcessingMode = ProcessingMode.Remote;
            rptViewer.SizeToReportContent = true;
            rptViewer.ZoomMode = ZoomMode.PageWidth;
            rptViewer.Width = Unit.Percentage(100);
            rptViewer.Height = Unit.Percentage(100);
            rptViewer.AsyncRendering = true;
            rptViewer.ServerReport.ReportServerUrl = new Uri("http://DESKTOP-5FDC8M5:80/ReportServer");
            rptViewer.ServerReport.ReportPath = "/Report Project1/TravelExpanseReport";
            ViewBag.ReportViewer = rptViewer;

            return View();
        }

        public ActionResult TrainingApprovalReporttt()
        {
            ReportViewer rptViewer = new ReportViewer();
            rptViewer.ProcessingMode = ProcessingMode.Remote;
            rptViewer.SizeToReportContent = true;
            rptViewer.ZoomMode = ZoomMode.PageWidth;
            rptViewer.Width = Unit.Percentage(100);
            rptViewer.Height = Unit.Percentage(100);
            rptViewer.AsyncRendering = true;
            rptViewer.ServerReport.ReportServerUrl = new Uri("http://DESKTOP-5FDC8M5:80/ReportServer");
            rptViewer.ServerReport.ReportPath = "/Report Project1/TrainingApprovalaReport";
            ViewBag.ReportViewer = rptViewer;

            return View();
        }

        public ActionResult TrainingProgramReporttt()
        {
            ReportViewer rptViewer = new ReportViewer();
            rptViewer.ProcessingMode = ProcessingMode.Remote;
            rptViewer.SizeToReportContent = true;
            rptViewer.ZoomMode = ZoomMode.PageWidth;
            rptViewer.Width = Unit.Percentage(100);
            rptViewer.Height = Unit.Percentage(100);
            rptViewer.AsyncRendering = true;
            rptViewer.ServerReport.ReportServerUrl = new Uri("http://DESKTOP-5FDC8M5:80/ReportServer");
            rptViewer.ServerReport.ReportPath = "/Report Project1/TrainingProgramReport";
            ViewBag.ReportViewer = rptViewer;

            return View();
        }

        public ActionResult TrainingAttendanceReporttt()
        {
            ReportViewer rptViewer = new ReportViewer();
            rptViewer.ProcessingMode = ProcessingMode.Remote;
            rptViewer.SizeToReportContent = true;
            rptViewer.ZoomMode = ZoomMode.PageWidth;
            rptViewer.Width = Unit.Percentage(100);
            rptViewer.Height = Unit.Percentage(100);
            rptViewer.AsyncRendering = true;
            rptViewer.ServerReport.ReportServerUrl = new Uri("http://DESKTOP-5FDC8M5:80/ReportServer");
            rptViewer.ServerReport.ReportPath = "/Report Project1/TrainingAttendanceReport";
            ViewBag.ReportViewer = rptViewer;

            return View();
        }


    }

}