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
    public class TraningMaterialMasterController : Controller
    {
        private HRMSEntities db = new HRMSEntities();

        // GET: HRMS_TRAINING_MATERIAL_MS
        public ActionResult Index()
        {
            return View(db.HRMS_TRAINING_MATERIAL_MS.ToList());
        }

        // GET: HRMS_TRAINING_MATERIAL_MS/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_TRAINING_MATERIAL_MS hRMS_TRAINING_MATERIAL_MS = db.HRMS_TRAINING_MATERIAL_MS.Find(id);
            if (hRMS_TRAINING_MATERIAL_MS == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_TRAINING_MATERIAL_MS);
        }

        // GET: HRMS_TRAINING_MATERIAL_MS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HRMS_TRAINING_MATERIAL_MS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Material_ID,Material_Name,Material_Rate,Material_Remarks,Material_IsActive")] HRMS_TRAINING_MATERIAL_MS hRMS_TRAINING_MATERIAL_MS)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Status = "";
                var existData = db.HRMS_TRAINING_MATERIAL_MS.FirstOrDefault(rec => rec.Material_Name == hRMS_TRAINING_MATERIAL_MS.Material_Name);
                if (existData == null)
                {


                    db.HRMS_TRAINING_MATERIAL_MS.Add(hRMS_TRAINING_MATERIAL_MS);
                    db.SaveChanges();
                    ModelState.Clear();
                    ViewBag.Status = "New Material added successfully.";
                    return View();


                }
                ViewBag.Status = "this Material is already exist !";
                return View();

            }

            return View(hRMS_TRAINING_MATERIAL_MS);
        }

        // GET: HRMS_TRAINING_MATERIAL_MS/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_TRAINING_MATERIAL_MS hRMS_TRAINING_MATERIAL_MS = db.HRMS_TRAINING_MATERIAL_MS.Find(id);
            if (hRMS_TRAINING_MATERIAL_MS == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_TRAINING_MATERIAL_MS);
        }

        // POST: HRMS_TRAINING_MATERIAL_MS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Material_ID,Material_Name,Material_Rate,Material_Remarks,Material_IsActive")] HRMS_TRAINING_MATERIAL_MS hRMS_TRAINING_MATERIAL_MS)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Status = "";
                var existData = db.HRMS_TRAINING_MATERIAL_MS.FirstOrDefault(rec => rec.Material_Name == hRMS_TRAINING_MATERIAL_MS.Material_Name && rec.Material_Rate == hRMS_TRAINING_MATERIAL_MS.Material_Rate && rec.Material_Remarks == hRMS_TRAINING_MATERIAL_MS.Material_Remarks && rec.Material_IsActive == hRMS_TRAINING_MATERIAL_MS.Material_IsActive && rec.Material_ID != hRMS_TRAINING_MATERIAL_MS.Material_ID);
                if (existData == null)
                {

                    db.Entry(hRMS_TRAINING_MATERIAL_MS).State = EntityState.Modified;
                    db.SaveChanges();
                    //ModelState.Clear();
                    ViewBag.Status = "Material updated successfully.";
                    return View();


                }
                ViewBag.Status = "this Material is already exist !";
                return View();




            }
            return View(hRMS_TRAINING_MATERIAL_MS);
        }

        // GET: HRMS_TRAINING_MATERIAL_MS/Delete/5
        public bool Delete(long id)
        {
            HRMS_TRAINING_MATERIAL_MS employee_Personal_Detail = db.HRMS_TRAINING_MATERIAL_MS.Find(id);
            if (employee_Personal_Detail != null)
            {
                db.HRMS_TRAINING_MATERIAL_MS.Remove(employee_Personal_Detail);
                db.SaveChanges();
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
