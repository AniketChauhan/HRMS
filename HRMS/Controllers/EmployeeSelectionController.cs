using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HRMS.Models;
using HRMS.ViewModel;

namespace HRMS.Controllers
{
    public class EmployeeSelectionController : Controller
    {
        private HRMSEntities db = new HRMSEntities();


        public ActionResult Index()
        {
            return View(db.HRMS_ProgramDetail.Where(rec=>rec.Flag == 1).ToList());
        }
    }
}