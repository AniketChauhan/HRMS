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
    public class CitizenShipController : Controller
    {
        private HRMSEntities db = new HRMSEntities();

        // GET: HRMS_EMP_CITIZENSHIP_MS
        public ActionResult Index()
        {
            return View(db.HRMS_EMP_CITIZENSHIP_MS.ToList());
        }

        // GET: HRMS_EMP_CITIZENSHIP_MS/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_EMP_CITIZENSHIP_MS hRMS_EMP_CITIZENSHIP_MS = db.HRMS_EMP_CITIZENSHIP_MS.Find(id);
            if (hRMS_EMP_CITIZENSHIP_MS == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_EMP_CITIZENSHIP_MS);
        }

        // GET: HRMS_EMP_CITIZENSHIP_MS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HRMS_EMP_CITIZENSHIP_MS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CitizenShip_ID,CitizenShip_Country_NM")] HRMS_EMP_CITIZENSHIP_MS hRMS_EMP_CITIZENSHIP_MS)
        {
            if (ModelState.IsValid)
            {
                bool isValid = db.HRMS_EMP_CITIZENSHIP_MS.Any(x => x.CitizenShip_Country_NM==hRMS_EMP_CITIZENSHIP_MS.CitizenShip_Country_NM);
                if (!isValid)
                {

                    db.HRMS_EMP_CITIZENSHIP_MS.Add(hRMS_EMP_CITIZENSHIP_MS);
                    db.SaveChanges();
                    ViewBag.success = "It is Successfully Added!";
                    ModelState.Clear();
                    return View();
                }
                else
                {

                    ViewBag.success = "Sorry! It is already exist!";
                    return View(hRMS_EMP_CITIZENSHIP_MS);
                }


                    //db.HRMS_EMP_CITIZENSHIP_MS.Add(hRMS_EMP_CITIZENSHIP_MS);
                    //db.SaveChanges(); 
                    //ModelState.Clear();

                    //return RedirectToAction("Index");
                }

                return View(hRMS_EMP_CITIZENSHIP_MS);
        }

        // GET: HRMS_EMP_CITIZENSHIP_MS/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_EMP_CITIZENSHIP_MS hRMS_EMP_CITIZENSHIP_MS = db.HRMS_EMP_CITIZENSHIP_MS.Find(id);
            if (hRMS_EMP_CITIZENSHIP_MS == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_EMP_CITIZENSHIP_MS);
        }

        // POST: HRMS_EMP_CITIZENSHIP_MS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CitizenShip_ID,CitizenShip_Country_NM")] HRMS_EMP_CITIZENSHIP_MS hRMS_EMP_CITIZENSHIP_MS)
        {
            if (ModelState.IsValid)
            {
                bool isValid = db.HRMS_EMP_CITIZENSHIP_MS.Any(x => (x.CitizenShip_ID != hRMS_EMP_CITIZENSHIP_MS.CitizenShip_ID) && (x.CitizenShip_Country_NM == hRMS_EMP_CITIZENSHIP_MS.CitizenShip_Country_NM ));
                if (!isValid)
                {
                    db.Entry(hRMS_EMP_CITIZENSHIP_MS).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.success = "Your Record Successfully Updated!";
                    return View();
                }
                else
                {

                    ViewBag.success = "It is Already exist!";
                    return View();


                }


                    //db.Entry(hRMS_EMP_CITIZENSHIP_MS).State = EntityState.Modified;
                    //db.SaveChanges();
                    //return RedirectToAction("Index");
                }
                return View(hRMS_EMP_CITIZENSHIP_MS);
        }
        public bool Delete(long id)
        {
            HRMS_EMP_CITIZENSHIP_MS hRMS_EMP_CITIZENSHIP_MS = db.HRMS_EMP_CITIZENSHIP_MS.Find(id);
            if (hRMS_EMP_CITIZENSHIP_MS != null)
            {
                db.HRMS_EMP_CITIZENSHIP_MS.Remove(hRMS_EMP_CITIZENSHIP_MS);
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