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
    public class TravelTypeController : Controller
    {
        private HRMSEntities db = new HRMSEntities();

        // GET: TravelType
        public ActionResult Index()
        {
            return View(db.HRMS_Travel_type.ToList());
        }

        // GET: TravelType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_Travel_type hRMS_Travel_type = db.HRMS_Travel_type.Find(id);
            if (hRMS_Travel_type == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_Travel_type);
        }

        // GET: TravelType/Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Short_Name,Name,Description")] HRMS_Travel_type hRMS_Travel_type)
        {
            if (ModelState.IsValid)
            {
                HRMS_Travel_type short_typealready = db.HRMS_Travel_type.Where(rec => rec.Short_Name == hRMS_Travel_type.Short_Name).FirstOrDefault();
                if (short_typealready == null)
                {
                    HRMS_Travel_type type_name_already = db.HRMS_Travel_type.Where(rec => rec.Name == hRMS_Travel_type.Name).FirstOrDefault();
                    if (type_name_already == null)
                    {

                        db.HRMS_Travel_type.Add(hRMS_Travel_type);
                        db.SaveChanges(); ModelState.Clear();

                        ViewBag.TypeStatus = "New Travel Type is added successfully.";
                        return View();
                    }
                    else
                    {
                        ViewBag.TypeStatus = "The name is already exist in another travel type!";
                        return View();
                    }
                }
                else
                {
                    ViewBag.TypeStatus = "The short name is already exist in another travel type!";
                    return View();
                }
            }

            return View(hRMS_Travel_type);
        }

        // GET: TravelType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_Travel_type hRMS_Travel_type = db.HRMS_Travel_type.Find(id);
            if (hRMS_Travel_type == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_Travel_type);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Short_Name,Name,Description")] HRMS_Travel_type hRMS_Travel_type)
        {
            if (ModelState.IsValid)
            {
                HRMS_Travel_type short_typealready = db.HRMS_Travel_type.Where(rec => rec.Short_Name == hRMS_Travel_type.Short_Name && rec.ID != hRMS_Travel_type.ID).FirstOrDefault();
                if (short_typealready == null)
                {
                    HRMS_Travel_type type_name_already = db.HRMS_Travel_type.Where(rec => rec.Name == hRMS_Travel_type.Name && rec.ID != hRMS_Travel_type.ID).FirstOrDefault();
                    if (type_name_already == null)
                    {

                        db.Entry(hRMS_Travel_type).State = EntityState.Modified;
                        db.SaveChanges(); ModelState.Clear();

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.TypeStatus = "The name is already exist in another travel type!";
                        return View();
                    }
                }
                else
                {
                    ViewBag.TypeStatus = "The short name is already exist in another travel type!";
                    return View();
                }
            }

            return View(hRMS_Travel_type);
        }



        [HttpPost]
        public bool Delete(long id)
        {
            HRMS_Travel_type hRMS_Travel_type = db.HRMS_Travel_type.Find(id);
            db.HRMS_Travel_type.Remove(hRMS_Travel_type);
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
