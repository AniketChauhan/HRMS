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
    public class CastMasterController : Controller
    {

        private HRMSEntities db = new HRMSEntities();

        public ActionResult Index(int? page)
        {
            var CastMasters = db.CastMaster.Include(c => c.ReligionMaster);
            return View(CastMasters.ToList().ToPagedList(page ?? 1, 3));
        }

        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CastMaster CastMasters = db.CastMaster.Find(id);
            if (CastMasters == null)
            {
                return HttpNotFound();
            }
            return View(CastMasters);
        }

        // GET: CastMasters/Create
        public ActionResult Create()
        {
            ViewBag.ReligionID = new SelectList(db.ReligionMaster, "ReligionID", "ReligionName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CastCode,ReligionID,CastName")] CastMaster CastMasters)
        {
            //if (ModelState.IsValid)
            //{
            //    db.CastMasters.Add(CastMasters);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //ViewBag.ReligionID = new SelectList(db.ReligionMaster, "ReligionID", "ReligionShortName", CastMasters.ReligionID);
            //return View(CastMasters);

            if (ModelState.IsValid)
            {
                bool isValid = db.CastMaster.Any(x => x.ReligionID == CastMasters.ReligionID && x.CastName == CastMasters.CastName);
                if (!isValid)
                {
                    db.CastMaster.Add(CastMasters);
                    db.SaveChanges();
                    ViewBag.success = "Cast Name is Successfully created!";
                    ModelState.Clear();
                    ViewBag.ReligionID = new SelectList(db.ReligionMaster, "ReligionID", "ReligionName", CastMasters.ReligionID);
                    return View();
                }
                else
                {

                    ViewBag.error = "Sorry! Cast Name is already exist!";
                    ViewBag.ReligionID = new SelectList(db.ReligionMaster, "ReligionID", "ReligionName", CastMasters.ReligionID);
                    return View(CastMasters);

                }
            }
            ViewBag.ReligionID = new SelectList(db.ReligionMaster, "ReligionID", "ReligionName", CastMasters.ReligionID);

            return View(CastMasters);
        }

        // GET: CastMasters/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CastMaster CastMasters = db.CastMaster.Find(id);
            if (CastMasters == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReligionID = new SelectList(db.ReligionMaster, "ReligionID", "ReligionName", CastMasters.ReligionID);
            return View(CastMasters);
        }

        // POST: CastMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CastCode,ReligionID,CastName")] CastMaster CastMasters)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Entry(CastMasters).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //ViewBag.ReligionID = new SelectList(db.ReligionMaster, "ReligionID", "ReligionShortName", CastMasters.ReligionID);
            //return View(CastMasters);

            if (ModelState.IsValid)
            {
                bool isValid = db.CastMaster.Any(x => (x.CastCode != CastMasters.CastCode) && (x.ReligionID == CastMasters.ReligionID && x.CastName == CastMasters.CastName));
                if (!isValid)
                {
                    db.Entry(CastMasters).State = EntityState.Modified;
                    db.SaveChanges();
                    ModelState.Clear();

                    ViewBag.success = "Your Record Successfully Updated!";
                    ViewBag.ReligionID = new SelectList(db.ReligionMaster, "ReligionID", "ReligionName", CastMasters.ReligionID);
                    return View();
                }
                else
                {

                    ViewBag.error = "Sorry! This record is Already exist!";
                    ViewBag.ReligionID = new SelectList(db.ReligionMaster, "ReligionID", "ReligionName", CastMasters.ReligionID);
                    return View();

                }
            }
            return View(CastMasters);
        }

        public bool Delete(long id)
        {
            CastMaster CastMasters = db.CastMaster.Find(id);
            if (CastMasters != null)
            {
                db.CastMaster.Remove(CastMasters);
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
