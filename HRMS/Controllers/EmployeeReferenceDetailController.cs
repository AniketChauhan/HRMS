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
    public class EmployeeReferenceDetailController : Controller
    {
        private HRMSEntities db = new HRMSEntities();

        public JsonResult GetStateList(long CountryX)
        {
            db.Configuration.ProxyCreationEnabled = false;
           // long CountryID = db.Country.Where(x => x.CountryName == CountryX).Select(x => x.CountryID).SingleOrDefault();
            List<State> StateList = db.State.Where(x => x.CountryID == CountryX).ToList();
            return Json(StateList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCityList(long StateX)
        {
            db.Configuration.ProxyCreationEnabled = false;
           // long StateID = db.State.Where(x => x.StateName == StateX).Select(x => x.StateID).SingleOrDefault();
            List<City> CityList = db.City.Where(x => x.StateID == StateX).ToList();
            return Json(CityList, JsonRequestBehavior.AllowGet);
        }


        // GET: EmployeeReferenceDetail
        public ActionResult Index()
        {
            var hRMS_EMP_ReferenceDetail = db.HRMS_EMP_ReferenceDetail.Include(h => h.City1).Include(h => h.Country1).Include(h => h.Accounts).Include(h => h.State1);
            return View(hRMS_EMP_ReferenceDetail.ToList());
        }

        // GET: EmployeeReferenceDetail/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_EMP_ReferenceDetail hRMS_EMP_ReferenceDetail = db.HRMS_EMP_ReferenceDetail.Find(id);
            if (hRMS_EMP_ReferenceDetail == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_EMP_ReferenceDetail);
        }

        // GET: EmployeeReferenceDetail/Create
        public ActionResult Create()
        {
            
            ViewBag.Country = new SelectList(db.Country, "CountryID", "CountryName");
            ViewBag.State = new SelectList("");
            ViewBag.City = new SelectList("");
            //ViewBag.EMP_ID = new SelectList(db.HRMS_Emp_Details, "Emp_ID", "First_Name");
            
            return View();
        }

        // POST: EmployeeReferenceDetail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,EMP_ID,Is_Ref_Emp,Name,Country,State,City,Relationship,Pincode,Address,Designation,Company,PhoneNo,MobileNo,Email")] HRMS_EMP_ReferenceDetail hRMS_EMP_ReferenceDetail)
        {
            if (ModelState.IsValid)
            {
                bool isValid = db.HRMS_EMP_ReferenceDetail.Any(x => x.EMP_ID == hRMS_EMP_ReferenceDetail.EMP_ID);
                if (!isValid)
                {
                   // HRMS_EMP_ReferenceDetail.IsActive = true;
                    db.HRMS_EMP_ReferenceDetail.Add(hRMS_EMP_ReferenceDetail);
                    db.SaveChanges();
                    ViewBag.success = "Reference is Successfully created!";
                    ModelState.Clear();
                    ViewBag.Country = new SelectList(db.Country, "CountryID", "CountryName");
                    ViewBag.State = new SelectList("");
                    ViewBag.City = new SelectList("");
                    return View();
                }
                else
                {
                    ViewBag.error = "Reference is already exist!";
                    //long cid = db.Country.Where(x => x.CountryName == unitMaster.Country).Select(x => x.CountryID).SingleOrDefault();
                    //long sid = db.State.Where(x => x.StateName == unitMaster.State).Select(x => x.StateID).SingleOrDefault();
        
                    ViewBag.Country = new SelectList(db.Country, "CountryID", "CountryName");
                    ViewBag.State = new SelectList(db.State.Where(x=>x.CountryID==hRMS_EMP_ReferenceDetail.Country),"StateID","StateName");
                    ViewBag.City = new SelectList(db.City.Where(x => x.StateID == hRMS_EMP_ReferenceDetail.State), "CityID", "CityName");

                    return View(hRMS_EMP_ReferenceDetail);
                }
            }

            ViewBag.Country = new SelectList(db.Country, "CountryID", "CountryName");
            ViewBag.State = new SelectList(db.State.Where(x => x.CountryID == hRMS_EMP_ReferenceDetail.Country), "StateID", "StateName");
            ViewBag.City = new SelectList(db.City.Where(x => x.StateID == hRMS_EMP_ReferenceDetail.State), "CityID", "CityName");
            return View(hRMS_EMP_ReferenceDetail);
        }

        // GET: EmployeeReferenceDetail/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_EMP_ReferenceDetail hRMS_EMP_ReferenceDetail = db.HRMS_EMP_ReferenceDetail.Find(id);
            if (hRMS_EMP_ReferenceDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.Country = new SelectList(db.Country, "CountryID", "CountryName");
            ViewBag.State = new SelectList(db.State.Where(x => x.CountryID == hRMS_EMP_ReferenceDetail.Country), "StateID", "StateName");
            ViewBag.City = new SelectList(db.City.Where(x => x.StateID == hRMS_EMP_ReferenceDetail.State), "CityID", "CityName");
            return View(hRMS_EMP_ReferenceDetail);
        }

        // POST: EmployeeReferenceDetail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,EMP_ID,Is_Ref_Emp,Name,Country,State,City,Relationship,Pincode,Address,Designation,Company,PhoneNo,MobileNo,Email")] HRMS_EMP_ReferenceDetail hRMS_EMP_ReferenceDetail)
        {
            if (ModelState.IsValid)
            {
                bool isValid = db.HRMS_EMP_ReferenceDetail.Any(x => (x.ID != hRMS_EMP_ReferenceDetail.ID) && (x.EMP_ID == hRMS_EMP_ReferenceDetail.EMP_ID ));
                if (!isValid)
                {
                    db.Entry(hRMS_EMP_ReferenceDetail).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.success = "Your Record Successfully Updated!";

                    ViewBag.Country = new SelectList(db.Country, "CountryID", "CountryName");
                    ViewBag.State = new SelectList(db.State.Where(x => x.CountryID == hRMS_EMP_ReferenceDetail.Country), "StateID", "StateName");
                    ViewBag.City = new SelectList(db.City.Where(x => x.StateID == hRMS_EMP_ReferenceDetail.State), "CityID", "CityName");
                    return View();
                }
                else
                {
                    ViewBag.error = "Sorry!";
                    ViewBag.Country = new SelectList(db.Country, "CountryID", "CountryName");
                    ViewBag.State = new SelectList(db.State.Where(x => x.CountryID == hRMS_EMP_ReferenceDetail.Country), "StateID", "StateName");
                    ViewBag.City = new SelectList(db.City.Where(x => x.StateID == hRMS_EMP_ReferenceDetail.State), "CityID", "CityName");
                    return View();
                    
                }
            }
            ViewBag.Country = new SelectList(db.Country, "CountryID", "CountryName");
            ViewBag.State = new SelectList(db.State.Where(x => x.CountryID == hRMS_EMP_ReferenceDetail.Country), "StateID", "StateName");
            ViewBag.City = new SelectList(db.City.Where(x => x.StateID == hRMS_EMP_ReferenceDetail.State), "CityID", "CityName");
            return View(hRMS_EMP_ReferenceDetail);
        }

        // GET: EmployeeReferenceDetail/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_EMP_ReferenceDetail hRMS_EMP_ReferenceDetail = db.HRMS_EMP_ReferenceDetail.Find(id);
            if (hRMS_EMP_ReferenceDetail == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_EMP_ReferenceDetail);
        }

        // POST: EmployeeReferenceDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            HRMS_EMP_ReferenceDetail hRMS_EMP_ReferenceDetail = db.HRMS_EMP_ReferenceDetail.Find(id);
            db.HRMS_EMP_ReferenceDetail.Remove(hRMS_EMP_ReferenceDetail);
            db.SaveChanges();
            return RedirectToAction("Index");
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
