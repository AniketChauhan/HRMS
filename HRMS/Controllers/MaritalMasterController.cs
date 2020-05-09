using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HRMS.Models;
using PagedList;
using PagedList.Mvc;



namespace HRMS.Controllers
{
    [Authorize(Roles = "admin")]
    public class MaritalMastersController : Controller
    {
        // GET: MaritalMasters
        private HRMSEntities db = new HRMSEntities();

        // GET: MaritalMasters
        public ActionResult Index(string Search, int? page)
        {

            return View(db.MaritalMaster.Where(x => x.MaritalName.StartsWith(Search) || Search == null).ToList().ToPagedList(page ?? 1, 3));

        }

        // GET: MaritalMasters/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaritalMaster MaritalMasters = db.MaritalMaster.Find(id);
            if (MaritalMasters == null)
            {
                return HttpNotFound();
            }
            return View(MaritalMasters);
        }

        // GET: MaritalMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MaritalMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaritalID,MaritalName")] MaritalMaster MaritalMasters)
        {
            if (ModelState.IsValid)
            {
                bool isValid = db.MaritalMaster.Any(x => x.MaritalName == MaritalMasters.MaritalName);
                if (!isValid)
                {

                    db.MaritalMaster.Add(MaritalMasters);
                    db.SaveChanges();
                    ViewBag.success = "Marital Status is Successfully created!";
                    ModelState.Clear();
                    return View();
                }
                else
                {
                    ViewBag.error = "Sorry! Marital Status is already exist!";
                    return View(MaritalMasters);
                }
            }

            return View(MaritalMasters);
        }

        // GET: MaritalMasters/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaritalMaster MaritalMasters = db.MaritalMaster.Find(id);
            if (MaritalMasters == null)
            {
                return HttpNotFound();
            }
            return View(MaritalMasters);
        }

        // POST: MaritalMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaritalID,MaritalName")] MaritalMaster MaritalMasters)
        {
            if (ModelState.IsValid)
            {
                bool isValid = db.MaritalMaster.Any(x => (x.MaritalID != MaritalMasters.MaritalID) && (x.MaritalName == MaritalMasters.MaritalName));
                if (!isValid)
                {
                    db.Entry(MaritalMasters).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.success = "Your Record Successfully Updated!";
                    return View();
                }
                else
                {
                    ViewBag.error = "Marital Name is Already exist!";
                    return View();

                }
            }
            return View(MaritalMasters);
        }
        public bool delete(long id)
        {
            MaritalMaster MaritalMasters = db.MaritalMaster.Find(id);
            if (MaritalMasters != null)
            {
                db.MaritalMaster.Remove(MaritalMasters);
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
