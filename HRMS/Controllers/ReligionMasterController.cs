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
    public class ReligionMasterController : Controller
    {
        private HRMSEntities db = new HRMSEntities();

        // GET: ReligionMaster
        public ActionResult Index(int? page)
        {
            return View(db.ReligionMaster.ToList().ToPagedList(page ?? 1, 3));
        }

        // GET: ReligionMaster/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReligionMaster religionMaster = db.ReligionMaster.Find(id);
            if (religionMaster == null)
            {
                return HttpNotFound();
            }
            return View(religionMaster);
        }

        // GET: ReligionMaster/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReligionMaster/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReligionID,ReligionShortName,ReligionName")] ReligionMaster religionMaster)
        {
            if (ModelState.IsValid)
            {
                bool isValid = db.ReligionMaster.Any(x => x.ReligionShortName == religionMaster.ReligionShortName || x.ReligionName == religionMaster.ReligionName);
                if (!isValid)
                {
                    db.ReligionMaster.Add(religionMaster);
                    db.SaveChanges();
                    ViewBag.success = "Religion is Successfully created!";
                    ModelState.Clear();
                    return View();
                }
                else
                {
                    if (db.ReligionMaster.Any(x => x.ReligionName == religionMaster.ReligionName))
                    {
                        ViewBag.error = "Sorry! Religion name is already exist!";
                        return View(religionMaster);
                    }
                    else
                    {
                        ViewBag.error = "Sorry! Religion Short Name is already exist!";
                        return View(religionMaster);
                    }
                }
            }

            return View(religionMaster);
        }

        // GET: ReligionMaster/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReligionMaster religionMaster = db.ReligionMaster.Find(id);
            if (religionMaster == null)
            {
                return HttpNotFound();
            }
            return View(religionMaster);
        }

        // POST: ReligionMaster/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReligionID,ReligionShortName,ReligionName")] ReligionMaster religionMaster)
        {
            if (ModelState.IsValid)
            {
                bool isValid = db.ReligionMaster.Any(x => (x.ReligionID != religionMaster.ReligionID) && (x.ReligionShortName == religionMaster.ReligionShortName || x.ReligionName == religionMaster.ReligionName));
                if (!isValid)
                {
                    db.Entry(religionMaster).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.success = "Your Record Successfully Updated!";
                    return View();
                }
                else
                {
                    if (db.ReligionMaster.Any(x => (x.ReligionID != religionMaster.ReligionID) && (x.ReligionShortName == religionMaster.ReligionShortName)))
                    {
                        ViewBag.error = "Religion Short Name is Already exist!";
                        return View();
                    }
                    else
                    {
                        ViewBag.error = "Religion Name is Already exist!";
                        return View();
                    }
                }
            }
            return View(religionMaster);
        }

       
        public bool delete(long id)
        {
            ReligionMaster religionMaster = db.ReligionMaster.Find(id);
            if (religionMaster != null)
            {
                db.ReligionMaster.Remove(religionMaster);
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
