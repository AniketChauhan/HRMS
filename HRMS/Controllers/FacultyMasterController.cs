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
    public class FacultyMasterController : Controller
    {
        private HRMSEntities db = new HRMSEntities();


        public ActionResult Data(long? id)
        {
            if (id != null)
            {
                HRMS_Contact obj = db.HRMS_Contact.Where(x => x.Employee_ID == id.Value).FirstOrDefault();

                if (obj != null)
                {
                    var result = new { Mo = obj.Mobile_No_Work, Email = obj.Corporate_Email };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                    
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AutoComplete(string prefix)
        {
            
            var Result1 = (from Result in db.HRMS_Emp_Details
                             where Result.First_Name.StartsWith(prefix) || Result.Last_Name.StartsWith(prefix)
                             select new
                             {
                                 label = Result.First_Name +" "+Result.Last_Name+" "+Result.EMP_ID,
                                 val = Result.EMP_ID
                             }).ToList();

            return Json(Result1);
        }

        // GET: FacultyMaster
        public ActionResult Index()
        {
            return View(db.HRMS_Faculty_MS.ToList());
        }

        // GET: FacultyMaster/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_Faculty_MS hRMS_Faculty_MS = db.HRMS_Faculty_MS.Find(id);
            if (hRMS_Faculty_MS == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_Faculty_MS);
        }

        // GET: FacultyMaster/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FacultyMaster/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FacultyType,EMP_ID,External_Name,PAN_No,ServiceTaxNo,Qualification,Contact,Email,Remark,IsActive,Empty")] HRMS_Faculty_MS hRMS_Faculty_MS)
        {
            if (ModelState.IsValid)
            {
                if (hRMS_Faculty_MS.FacultyType == "Internal" && hRMS_Faculty_MS.EMP_ID != null)
                {
                    bool IsExist = db.HRMS_Faculty_MS.Any(x => x.EMP_ID == hRMS_Faculty_MS.EMP_ID);
                    if (IsExist)
                    {
                        ViewBag.error = "Faculty type already assigned to this Employee!";
                        ModelState.Clear();
                        return View();
                    }
                    else
                    {
                        HRMS_Emp_Details obj = db.HRMS_Emp_Details.Where(x=>x.EMP_ID==hRMS_Faculty_MS.EMP_ID).FirstOrDefault();
                        string name = obj.First_Name + " " + obj.Last_Name;
                        hRMS_Faculty_MS.External_Name = name;
                        db.HRMS_Faculty_MS.Add(hRMS_Faculty_MS);
                        db.SaveChanges();
                        ModelState.Clear();
                        ViewBag.success = "Successfully Faculty type assigned to this Employee!";
                        return View();
                    }
                }
                db.HRMS_Faculty_MS.Add(hRMS_Faculty_MS);
                db.SaveChanges();
                ModelState.Clear();
                ViewBag.success = "Successfully Faculty type assigned to this Employee!";
                return View();
            }

            return View(hRMS_Faculty_MS);
        }

        // GET: FacultyMaster/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_Faculty_MS hRMS_Faculty_MS = db.HRMS_Faculty_MS.Find(id);
            if (hRMS_Faculty_MS == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_Faculty_MS);
        }

        // POST: FacultyMaster/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FacultyType,EMP_ID,External_Name,PAN_No,ServiceTaxNo,Qualification,Contact,Email,Remark,IsActive,Empty")] HRMS_Faculty_MS hRMS_Faculty_MS)
        {
            if (ModelState.IsValid)
            {
                if (hRMS_Faculty_MS.EMP_ID != null)
                {
                    bool IsExist = db.HRMS_Faculty_MS.Any(x => (x.ID != hRMS_Faculty_MS.ID) && (x.EMP_ID == hRMS_Faculty_MS.EMP_ID));
                    if (!IsExist)
                    {
                        db.Entry(hRMS_Faculty_MS).State = EntityState.Modified;
                        db.SaveChanges();
                        ViewBag.success = "Data Successfully changed!";
                        return View(hRMS_Faculty_MS);
                    }
                    else
                    {
                        ViewBag.error = "Record Already Exist!";
                        return View(hRMS_Faculty_MS);
                    }
                }

                else
                {
                        db.Entry(hRMS_Faculty_MS).State = EntityState.Modified;
                        db.SaveChanges();
                        ViewBag.success = "Data Successfully changed!";
                        return View(hRMS_Faculty_MS);
                }

                
            }
            return View(hRMS_Faculty_MS);
        }

        // GET: FacultyMaster/Delete/5
        public bool Delete(long id)
        {
            HRMS_Faculty_MS employee_Personal_Detail = db.HRMS_Faculty_MS.Find(id);
            if (employee_Personal_Detail != null)
            {
                db.HRMS_Faculty_MS.Remove(employee_Personal_Detail);
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
