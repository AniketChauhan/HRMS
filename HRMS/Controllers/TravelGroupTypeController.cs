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
    public class TravelGroupTypeController : Controller
    {
        private HRMSEntities db = new HRMSEntities();

        // GET: TravelGroupType
        public ActionResult Index()
        {
            var hRMS_TravelGroupType_MS = db.HRMS_TravelGroupType_MS.Include(h => h.HRMS_TravelMode_MS);
            return View(hRMS_TravelGroupType_MS.ToList());
        }

        // GET: TravelGroupType/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_TravelGroupType_MS hRMS_TravelGroupType_MS = db.HRMS_TravelGroupType_MS.Find(id);
            if (hRMS_TravelGroupType_MS == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_TravelGroupType_MS);
        }

        // GET: TravelGroupType/Create
        public ActionResult Create()
        {
            ViewBag.Mode_ID = new SelectList(db.HRMS_TravelMode_MS, "Mode_ID", "Mode_Name");
            return View();
        }

        // POST: TravelGroupType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Mode_ID,Date,Group_Name")] HRMS_TravelGroupType_MS hRMS_TravelGroupType_MS)
        {
            if (ModelState.IsValid)
            {
                bool isValid = db.HRMS_TravelGroupType_MS.Any(x => (x.Mode_ID == hRMS_TravelGroupType_MS.Mode_ID && x.Group_Name == hRMS_TravelGroupType_MS.Group_Name));
                if (!isValid)
                {

                    db.HRMS_TravelGroupType_MS.Add(hRMS_TravelGroupType_MS);
                    db.SaveChanges();
                    ViewBag.success = "Travel Group is Successfully Added!";
                    ModelState.Clear();
                    ViewBag.Mode_ID = new SelectList(db.HRMS_TravelMode_MS, "Mode_ID", "Mode_Name");
                    return View();
                }
                else
                {
                   
                        ViewBag.error = "Sorry! Record is already exist!";
                    ViewBag.Mode_ID = new SelectList(db.HRMS_TravelMode_MS, "Mode_ID", "Mode_Name");
                    return View(hRMS_TravelGroupType_MS);
                    

                }
            }
            ViewBag.Mode_ID = new SelectList(db.HRMS_TravelMode_MS, "Mode_ID", "Mode_Name");
            return View(hRMS_TravelGroupType_MS);
        }

        // GET: TravelGroupType/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_TravelGroupType_MS hRMS_TravelGroupType_MS = db.HRMS_TravelGroupType_MS.Find(id);
            if (hRMS_TravelGroupType_MS == null)
            {
                return HttpNotFound();
            }
            ViewBag.Mode_ID = new SelectList(db.HRMS_TravelMode_MS, "Mode_ID", "Mode_Name", hRMS_TravelGroupType_MS.Mode_ID);
            return View(hRMS_TravelGroupType_MS);
        }

        // POST: TravelGroupType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Mode_ID,Date,Group_Name")] HRMS_TravelGroupType_MS hRMS_TravelGroupType_MS)
        {
            if (ModelState.IsValid)
            {
                bool isValid = db.HRMS_TravelGroupType_MS.Any(x => (x.ID != hRMS_TravelGroupType_MS.ID) && (x.Mode_ID == hRMS_TravelGroupType_MS.Mode_ID && x.Group_Name == hRMS_TravelGroupType_MS.Group_Name));
                if (!isValid)
                {
                    db.Entry(hRMS_TravelGroupType_MS).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.success = "Your Record Successfully Updated!";
                    ViewBag.Mode_ID = new SelectList(db.HRMS_TravelMode_MS, "Mode_ID", "Mode_Name", hRMS_TravelGroupType_MS.Mode_ID);
                    return View();
                }
                else
                {
                        ViewBag.error = "Record is Already exist!";
                    ViewBag.Mode_ID = new SelectList(db.HRMS_TravelMode_MS, "Mode_ID", "Mode_Name", hRMS_TravelGroupType_MS.Mode_ID);
                    return View();
      
                }
                
            }
            ViewBag.Mode_ID = new SelectList(db.HRMS_TravelMode_MS, "Mode_ID", "Mode_Name", hRMS_TravelGroupType_MS.Mode_ID);
            return View(hRMS_TravelGroupType_MS);
        }

        public bool delete(long id)
        {
            HRMS_TravelGroupType_MS hRMS_TravelGroupType_MS = db.HRMS_TravelGroupType_MS.Find(id);
            if (hRMS_TravelGroupType_MS != null)
            {
                db.HRMS_TravelGroupType_MS.Remove(hRMS_TravelGroupType_MS);
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
