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
    public class TravelOtherDetailsController : Controller
    {
        private HRMSEntities db = new HRMSEntities();

        // GET: HRMS_TRAVEL_OTHER_DETAILS_MS
        public ActionResult Index()
        {
            return View(db.HRMS_TRAVEL_OTHER_DETAILS_MS.ToList());
        }

        // GET: HRMS_TRAVEL_OTHER_DETAILS_MS/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_TRAVEL_OTHER_DETAILS_MS hRMS_TRAVEL_OTHER_DETAILS_MS = db.HRMS_TRAVEL_OTHER_DETAILS_MS.Find(id);
            if (hRMS_TRAVEL_OTHER_DETAILS_MS == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_TRAVEL_OTHER_DETAILS_MS);
        }

        // GET: HRMS_TRAVEL_OTHER_DETAILS_MS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HRMS_TRAVEL_OTHER_DETAILS_MS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Travel_Other_Details_ID,Travel_Other_Details_NM")] HRMS_TRAVEL_OTHER_DETAILS_MS hRMS_TRAVEL_OTHER_DETAILS_MS)
        {
            if (ModelState.IsValid)
            {
                db.HRMS_TRAVEL_OTHER_DETAILS_MS.Add(hRMS_TRAVEL_OTHER_DETAILS_MS);
                db.SaveChanges(); ModelState.Clear();

                return RedirectToAction("Index");
            }

            return View(hRMS_TRAVEL_OTHER_DETAILS_MS);
        }

        // GET: HRMS_TRAVEL_OTHER_DETAILS_MS/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_TRAVEL_OTHER_DETAILS_MS hRMS_TRAVEL_OTHER_DETAILS_MS = db.HRMS_TRAVEL_OTHER_DETAILS_MS.Find(id);
            if (hRMS_TRAVEL_OTHER_DETAILS_MS == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_TRAVEL_OTHER_DETAILS_MS);
        }

        // POST: HRMS_TRAVEL_OTHER_DETAILS_MS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Travel_Other_Details_ID,Travel_Other_Details_NM")] HRMS_TRAVEL_OTHER_DETAILS_MS hRMS_TRAVEL_OTHER_DETAILS_MS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hRMS_TRAVEL_OTHER_DETAILS_MS).State = EntityState.Modified;
                db.SaveChanges(); ModelState.Clear();

                return RedirectToAction("Index");
            }
            return View(hRMS_TRAVEL_OTHER_DETAILS_MS);
        }

        
        public bool delete(long id)
        {
            HRMS_TRAVEL_OTHER_DETAILS_MS hRMS_TRAVEL_OTHER_DETAILS_MS = db.HRMS_TRAVEL_OTHER_DETAILS_MS.Find(id);
            if (hRMS_TRAVEL_OTHER_DETAILS_MS != null)
            {
                db.HRMS_TRAVEL_OTHER_DETAILS_MS.Remove(hRMS_TRAVEL_OTHER_DETAILS_MS);
                db.SaveChanges(); ModelState.Clear();

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
