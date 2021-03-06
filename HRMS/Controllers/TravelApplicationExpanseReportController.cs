﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRMS.Models;

namespace HRMS.Controllers
{
    [Authorize(Roles = "admin")]
    public class TravelApplicationExpanseReportController : Controller
    {
        private HRMSEntities db = new HRMSEntities();

        // GET: TravelApplicationExpanseReport
        public ActionResult Index()
        {
            ViewBag.Designation = db.HRMS_DESG_MS.Where(rec => rec.IsActive == true);
            ViewBag.Grade = db.HRMS_CATEGORY_GRADE;
            ViewBag.dataTravelPurpose = new SelectList(db.HRMS_Travel_Purpose, "ID", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            string Designationn = collection["Designation"];
            string Gradee = collection["Grade"];
            string Travel_Purposee = collection["Travel_Purpose"];
            string fromdatee = collection["FromDate"];
            string todatee = collection["ToDate"];


            long? Designation = null;
            long? Grade = null;
            long? Travel_Purpose = null;
            DateTime? FromDate = null;
            DateTime? ToDate = null;
            if (Designationn != "")
            {
                Designation = Convert.ToInt64(collection["Designation"]);
            }
            if (Gradee != "")
            {
                Grade = Convert.ToInt64(collection["Grade"]);
            }
            if (Travel_Purposee != "")
            {
                Travel_Purpose = Convert.ToInt32(collection["Travel_Purpose"]);
            }
            if (fromdatee != "")
            {
                FromDate = Convert.ToDateTime(collection["FromDate"]);
            }
            if (todatee != "")
            {
                ToDate = Convert.ToDateTime(collection["ToDate"]);
            }

            if (FromDate != null && ToDate == null)
            {
                ViewBag.EmployeeReportStatus = "please enter ToDate!";
                ViewBag.Designation = db.HRMS_DESG_MS.Where(rec => rec.IsActive == true);
                ViewBag.Grade = db.HRMS_CATEGORY_GRADE;
                ViewBag.dataTravelPurpose = new SelectList(db.HRMS_Travel_Purpose, "ID", "Name");
                return View();
            }
            if (FromDate == null && ToDate != null)
            {
                ViewBag.EmployeeReportStatus = "please enter FromDate!";
                ViewBag.Designation = db.HRMS_DESG_MS.Where(rec => rec.IsActive == true);
                ViewBag.Grade = db.HRMS_CATEGORY_GRADE;
                ViewBag.dataTravelPurpose = new SelectList(db.HRMS_Travel_Purpose, "ID", "Name");
                return View();
            }
            if (Designation == null && Grade == null && Travel_Purpose == null && FromDate == null && ToDate == null)
            {
                ViewBag.EmployeeReportStatus = "Please enter anyone option!";
                ViewBag.Designation = db.HRMS_DESG_MS.Where(rec => rec.IsActive == true);
                ViewBag.Grade = db.HRMS_CATEGORY_GRADE;
                ViewBag.dataTravelPurpose = new SelectList(db.HRMS_Travel_Purpose, "ID", "Name");
                return View();
            }

            var modeldata = db.TravelApplicationExpanseReport(FromDate, ToDate,Travel_Purpose, Grade, Designation).ToList();
            TempData["Data"] = modeldata;
            ViewBag.Designation = db.HRMS_DESG_MS.Where(rec => rec.IsActive == true);
            ViewBag.Grade = db.HRMS_CATEGORY_GRADE;
            ViewBag.dataTravelPurpose = new SelectList(db.HRMS_Travel_Purpose, "ID", "Name");
            return View(modeldata);

        }
        public JsonResult Download()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var modeldata = TempData["Data"];

            var gv = new GridView();
            //gv.DataSource = db.Student.ToList();
            gv.DataSource = modeldata;
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=DemoExcel.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            gv.RenderControl(objHtmlTextWriter);
            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();
            return Json(true, JsonRequestBehavior.AllowGet);
            //return Content("Index");
        }

    }
}
 