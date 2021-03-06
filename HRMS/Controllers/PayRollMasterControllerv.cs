﻿using System;
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
    public class PayRollMasterController : Controller
    {
        private HRMSEntities db = new HRMSEntities();

        // GET: PayRollMaster
        public ActionResult Index(int? page)
        {
            return View(db.PayRollMaster.ToList().ToPagedList(page ?? 1, 3));
        }

        // GET: PayRollMaster/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PayRollMaster payRollMaster = db.PayRollMaster.Find(id);
            if (payRollMaster == null)
            {
                return HttpNotFound();
            }
            return View(payRollMaster);
        }

        // GET: PayRollMaster/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PayRollMaster/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PayRollCode,PayRollName")] PayRollMaster payRollMaster)
        {
            if (ModelState.IsValid)
            {
                bool isValid = db.PayRollMaster.Any(x => x.PayRollName == payRollMaster.PayRollName);
                if (!isValid)
                {

                    db.PayRollMaster.Add(payRollMaster);
                    db.SaveChanges();
                    ViewBag.success = "PayRoll is Successfully created!";
                    ModelState.Clear();
                    return View();
                }
                else
                {
                    ViewBag.error = "Sorry! PayRoll is already exist!";
                    return View(payRollMaster);
                }
            }

            return View(payRollMaster);
        }

        // GET: PayRollMaster/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PayRollMaster payRollMaster = db.PayRollMaster.Find(id);
            if (payRollMaster == null)
            {
                return HttpNotFound();
            }
            return View(payRollMaster);
        }

        // POST: PayRollMaster/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PayRollCode,PayRollName")] PayRollMaster payRollMaster)
        {
            if (ModelState.IsValid)
            {
                bool isValid = db.PayRollMaster.Any(x => (x.PayRollCode != payRollMaster.PayRollCode) && (x.PayRollName == payRollMaster.PayRollName));
                if (!isValid)
                {
                    db.Entry(payRollMaster).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.success = "Your Record Successfully Updated!";
                    return View();
                }
                else
                {
                    ViewBag.error = "PayRoll is Already exist!";
                    return View();

                }
            }
            return View(payRollMaster);
        }

        
        public bool delete(long id)
        {
            PayRollMaster payRollMaster = db.PayRollMaster.Find(id);
            if (payRollMaster != null)
            {
                db.PayRollMaster.Remove(payRollMaster);
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
