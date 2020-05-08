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
    public class TravelPurposeController : Controller
    {
        private HRMSEntities db = new HRMSEntities();

        // GET: TravelPurpose
        public ActionResult Index()
        {
            return View(db.HRMS_Travel_Purpose.ToList());
        }

        // GET: TravelPurpose/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TravelPurpose/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] HRMS_Travel_Purpose hRMS_Travel_Purpose)
        {
            if (ModelState.IsValid)
            {
                HRMS_Travel_Purpose IsExist = db.HRMS_Travel_Purpose.Where(rec => rec.Name == hRMS_Travel_Purpose.Name).FirstOrDefault();
                if (IsExist == null)
                {

                    db.HRMS_Travel_Purpose.Add(hRMS_Travel_Purpose);
                    db.SaveChanges(); ModelState.Clear();

                    ViewBag.PurposeStatus = "Travel Purpose Added Successfully.";
                    return View();
                }
                else
                {
                    ViewBag.PurposeStatus = "Travel Purpose Is Already Exist!";
                    return View(hRMS_Travel_Purpose);
                }
            }

            return View(hRMS_Travel_Purpose);
        }

        // GET: TravelPurpose/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_Travel_Purpose hRMS_Travel_Purpose = db.HRMS_Travel_Purpose.Find(id);
            if (hRMS_Travel_Purpose == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_Travel_Purpose);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] HRMS_Travel_Purpose hRMS_Travel_Purpose)
        {
            if (ModelState.IsValid)
            {
                HRMS_Travel_Purpose IsExist = db.HRMS_Travel_Purpose.Where(rec => rec.Name == hRMS_Travel_Purpose.Name && rec.ID != hRMS_Travel_Purpose.ID).FirstOrDefault();
                if (IsExist == null)
                {

                    db.Entry(hRMS_Travel_Purpose).State = EntityState.Modified;
                    db.SaveChanges(); ModelState.Clear();

                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.PurposeStatus = "Travel Purpose Is Already Exist!";
                    return View(hRMS_Travel_Purpose);
                }
            }

            return View(hRMS_Travel_Purpose);
        }

        [HttpPost]
        public bool Delete(long id)
        {
            HRMS_Travel_Purpose hRMS_Travel_Purpose = db.HRMS_Travel_Purpose.Find(id);
            db.HRMS_Travel_Purpose.Remove(hRMS_Travel_Purpose);
            db.SaveChanges(); ModelState.Clear();

            return true;
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
