using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HRMS.Models;

namespace HRMS.Controllers
{
    [Authorize(Roles = "admin")]
    public class CostCenterController : Controller
    {
        private HRMSEntities db = new HRMSEntities();

        public ActionResult Index()
        {
            return View(db.HRMS_COST_CENTER.ToList());
        }

        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_COST_CENTER hRMS_COST_CENTER = db.HRMS_COST_CENTER.Find(id);
            if (hRMS_COST_CENTER == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_COST_CENTER);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Cost_Cntr_Code,Cost_Cntr_Name,Parent_Cost_Cntr_Code,Ledger_Code")] HRMS_COST_CENTER hRMS_COST_CENTER)
        {
            if (ModelState.IsValid)
            {
                var existData = db.HRMS_COST_CENTER.FirstOrDefault(rec => rec.Cost_Cntr_Code == hRMS_COST_CENTER.Cost_Cntr_Code && rec.Cost_Cntr_Name == hRMS_COST_CENTER.Cost_Cntr_Name);
                if (existData == null)
                {
                    var existCode = db.HRMS_COST_CENTER.FirstOrDefault(rec => rec.Cost_Cntr_Code == hRMS_COST_CENTER.Cost_Cntr_Code);
                    if (existCode == null)
                    {
                        var ExistName = db.HRMS_COST_CENTER.FirstOrDefault(rec => rec.Cost_Cntr_Name == hRMS_COST_CENTER.Cost_Cntr_Name);
                        if (ExistName == null)
                        {

                            db.HRMS_COST_CENTER.Add(hRMS_COST_CENTER);
                            db.SaveChanges();
                            ModelState.Clear();
                            ViewBag.cost_Status = "New Cost added succcessfully.";
                            return View();
                        }
                        ViewBag.cost_Status = "This Cost Name is Already Exist ! ";
                        return View();
                    }
                    ViewBag.cost_Status = "This Cost code is already exist !";
                    return View();
                }
                ViewBag.cost_Status = "this cost Code and Name is already exist !";
                return View();
            }

            return View(hRMS_COST_CENTER);
        }

        // GET: CostCenter/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_COST_CENTER hRMS_COST_CENTER = db.HRMS_COST_CENTER.Find(id);
            if (hRMS_COST_CENTER == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_COST_CENTER);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Cost_Cntr_Code,Cost_Cntr_Name,Parent_Cost_Cntr_Code,Ledger_Code")] HRMS_COST_CENTER hRMS_COST_CENTER)
        {
            if (ModelState.IsValid)
            {
                var existData = db.HRMS_COST_CENTER.FirstOrDefault(rec => rec.Cost_Cntr_Code == hRMS_COST_CENTER.Cost_Cntr_Code && rec.Cost_Cntr_Name == hRMS_COST_CENTER.Cost_Cntr_Name && rec.ID != hRMS_COST_CENTER.ID);
                if (existData == null)
                {
                    var existCode = db.HRMS_COST_CENTER.FirstOrDefault(rec => rec.Cost_Cntr_Code == hRMS_COST_CENTER.Cost_Cntr_Code && rec.ID != hRMS_COST_CENTER.ID);
                    if (existCode == null)
                    {
                        var ExistName = db.HRMS_COST_CENTER.FirstOrDefault(rec => rec.Cost_Cntr_Name == hRMS_COST_CENTER.Cost_Cntr_Name && rec.ID != hRMS_COST_CENTER.ID);
                        if (ExistName == null)
                        {

                            db.Entry(hRMS_COST_CENTER).State = EntityState.Modified;
                            db.SaveChanges();
                            ModelState.Clear();

                            ViewBag.cost_Status = "Cost Updated succcessfully.";
                            return View();
                        }
                        ViewBag.cost_Status = "This Cost Name is Already Exist ! ";
                        return View();
                    }
                    ViewBag.cost_Status = "This Cost code is already exist !";
                    return View();
                }
                ViewBag.cost_Status = "this cost Code and Name is already exist !";
                return View();
            }

            return View(hRMS_COST_CENTER);
        }
    public bool delete(long id)
    {
        HRMS_COST_CENTER hRMS_COST_CENTER = db.HRMS_COST_CENTER.Find(id);
        if (hRMS_COST_CENTER != null)
        {
            db.HRMS_COST_CENTER.Remove(hRMS_COST_CENTER);
            db.SaveChanges();
                ModelState.Clear();

                return true;
        }
        else
        {
            return false;
        }

    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            db.Dispose();
        }
        base.Dispose(disposing);
    }
}
}
