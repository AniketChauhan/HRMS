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
    public class TravelModeMasterController : Controller
    {
        private HRMSEntities db = new HRMSEntities();

        // GET: TravelModeMaster
        public ActionResult Index()
        {
            return View(db.HRMS_TravelMode_MS.ToList());
        }

        // GET: TravelModeMaster/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_TravelMode_MS hRMS_TravelMode_MS = db.HRMS_TravelMode_MS.Find(id);
            if (hRMS_TravelMode_MS == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_TravelMode_MS);
        }

        // GET: TravelModeMaster/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TravelModeMaster/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Mode_ID,Mode_Type,Mode_Name,Mode_Short_Name")] HRMS_TravelMode_MS hRMS_TravelMode_MS)
        {
            if (ModelState.IsValid)
            {
                bool isValid = db.HRMS_TravelMode_MS.Any(x =>(x.Mode_Name == hRMS_TravelMode_MS.Mode_Name || x.Mode_Short_Name==hRMS_TravelMode_MS.Mode_Short_Name));
                if (!isValid)
                {

                    db.HRMS_TravelMode_MS.Add(hRMS_TravelMode_MS);
                    db.SaveChanges();
                    ViewBag.success = "Travel Mode is Successfully Added!";
                    ModelState.Clear();
                    return View();
                }
                else
                {
                    if (db.HRMS_TravelMode_MS.Any(x =>x.Mode_Name == hRMS_TravelMode_MS.Mode_Name))
                    {
                        ViewBag.error = "Sorry! Mode Name is already exist!";
                        return View(hRMS_TravelMode_MS);
                    }
                    else
                    {
                        ViewBag.error = "Sorry! Mode Short Name is already exist!";
                        return View(hRMS_TravelMode_MS);
                    }
                       
                }
            }

            return View(hRMS_TravelMode_MS);
        }

        // GET: TravelModeMaster/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_TravelMode_MS hRMS_TravelMode_MS = db.HRMS_TravelMode_MS.Find(id);
            if (hRMS_TravelMode_MS == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_TravelMode_MS);
        }

        // POST: TravelModeMaster/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Mode_ID,Mode_Type,Mode_Name,Mode_Short_Name")] HRMS_TravelMode_MS hRMS_TravelMode_MS)
        {
            if (ModelState.IsValid)
            {
                bool isValid = db.HRMS_TravelMode_MS.Any(x => (x.Mode_ID != hRMS_TravelMode_MS.Mode_ID) && ((x.Mode_Name == hRMS_TravelMode_MS.Mode_Name || x.Mode_Short_Name == hRMS_TravelMode_MS.Mode_Short_Name)));
                if (!isValid)
                {
                    db.Entry(hRMS_TravelMode_MS).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.success = "Your Record Successfully Updated!";
                    return View();
                }
                else
                {
                    if (db.HRMS_TravelMode_MS.Any(x => (x.Mode_ID != hRMS_TravelMode_MS.Mode_ID) && ((x.Mode_Name == hRMS_TravelMode_MS.Mode_Name))))
                    {
                        ViewBag.error = "Model Name is Already exist!";
                        return View();
                    }
                    else
                    {
                        ViewBag.error = "Model Short Name is Already exist!";
                        return View();
                    }
                    
                   

                }
            }
            return View(hRMS_TravelMode_MS);
        }

       

        public bool delete(long id)
        {
            HRMS_TravelMode_MS hRMS_TravelMode_MS = db.HRMS_TravelMode_MS.Find(id);
            if (hRMS_TravelMode_MS != null)
            {
                db.HRMS_TravelMode_MS.Remove(hRMS_TravelMode_MS);
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
