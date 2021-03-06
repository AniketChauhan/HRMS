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
    public class WorkLocationMasterController : Controller
    {
        // GET: WorkLocationMaster
       private HRMSEntities db = new HRMSEntities();

            // GET: WorkLocationMaster
            public ActionResult Index(int? page)
            {
                return View(db.WorkLocationMaster.ToList().ToPagedList(page ?? 1, 3));
            }

            // GET: WorkLocationMaster/Details/5
            public ActionResult Details(long? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                WorkLocationMaster workLocationMaster = db.WorkLocationMaster.Find(id);
                if (workLocationMaster == null)
                {
                    return HttpNotFound();
                }
                return View(workLocationMaster);
            }

            // GET: WorkLocationMaster/Create
            public ActionResult Create()
            {
                return View();
            }

            // POST: WorkLocationMaster/Create
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
            // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Create([Bind(Include = "WorkID,WorkLocationName,SAPCode")] WorkLocationMaster workLocationMaster)
            {

                if (ModelState.IsValid)
                {
                    bool isValid = db.WorkLocationMaster.Any(x => x.WorkLocationName == workLocationMaster.WorkLocationName);
                    if (!isValid)
                    {

                        db.WorkLocationMaster.Add(workLocationMaster);
                        db.SaveChanges();
                        ViewBag.success = "Work Location is Successfully created!";
                        ModelState.Clear();
                        return View();
                    }
                    else
                    {
                        ViewBag.error = "Sorry! Work Location is already exist!";
                        return View(workLocationMaster);
                    }
                }

                return View(workLocationMaster);
            }

            // GET: WorkLocationMaster/Edit/5
            public ActionResult Edit(long? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                WorkLocationMaster workLocationMaster = db.WorkLocationMaster.Find(id);
                if (workLocationMaster == null)
                {
                    return HttpNotFound();
                }
                return View(workLocationMaster);
            }

            // POST: WorkLocationMaster/Edit/5
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
            // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Edit([Bind(Include = "WorkID,WorkLocationName,SAPCode")] WorkLocationMaster workLocationMaster)
            {

                if (ModelState.IsValid)
                {
                    bool isValid = db.WorkLocationMaster.Any(x => (x.WorkID != workLocationMaster.WorkID) && (x.WorkLocationName == workLocationMaster.WorkLocationName));
                    if (!isValid)
                    {
                        db.Entry(workLocationMaster).State = EntityState.Modified;
                        db.SaveChanges();
                        ViewBag.success = "Your Record Successfully Updated!";
                        return View();
                    }
                    else
                    {
                        ViewBag.error = "Work Location is Already exist!";
                        return View();

                    }
                }
                return View(workLocationMaster);
            }

            
        public bool Delete(long id)
        {
            WorkLocationMaster workLocationMaster = db.WorkLocationMaster.Find(id);
            db.WorkLocationMaster.Remove(workLocationMaster);
            db.SaveChanges();
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
