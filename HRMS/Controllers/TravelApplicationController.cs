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
    public class TravelApplicationController : Controller
    {
        private HRMSEntities db = new HRMSEntities();

        // GET: TravelApplication
        public ActionResult Index()
        {
            if (User.IsInRole("admin")) { 
            var hRMS_Travel_Application = db.HRMS_Travel_Application.Include(h => h.Accounts).Include(h => h.Accounts1).Include(h => h.HRMS_CATEGORY_GRADE).Include(h => h.HRMS_DESG_MS).Include(h => h.HRMS_Travel_Purpose).Include(h => h.HRMS_Travel_type).Include(h => h.HRMS_TravelMode_MS);
            return View(hRMS_Travel_Application.ToList());
            }
            else
            {
                long emp_id = Convert.ToInt64(Session["id"]);
                var hRMS_Travel_Application = db.HRMS_Travel_Application.Include(h => h.Accounts).Include(h => h.Accounts1).Include(h => h.HRMS_CATEGORY_GRADE).Include(h => h.HRMS_DESG_MS).Include(h => h.HRMS_Travel_Purpose).Include(h => h.HRMS_Travel_type).Include(h => h.HRMS_TravelMode_MS).Where(rec=>rec.emp_id == emp_id);
                return View(hRMS_Travel_Application.ToList());
            }
        }

        // GET: TravelApplication/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_Travel_Application hRMS_Travel_Application = db.HRMS_Travel_Application.Find(id);
            if (hRMS_Travel_Application == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_Travel_Application);
        }
        [Authorize(Roles ="emp")]
        // GET: TravelApplication/Create
        public ActionResult Create()
        {
            var user = Convert.ToInt64(Session["id"]);
            HRMS_Travel_Application hRMS_Travel_Application = new HRMS_Travel_Application();
            hRMS_Travel_Application.emp_id = user;
            //HRMS_TravelMode_MS hRMS_TravelMode_MS = db.HRMS_TravelMode_MS.Where(rec => rec.Mode_Type == "Local Coveyance").FirstOrDefault();
            //HRMS_Travel_type hRMS_Travel_Type = db.HRMS_Travel_type.Where(rec => rec.Name == "Domestic").FirstOrDefault();
            //hRMS_Travel_Application.Travel_Type = hRMS_Travel_Type.ID;
            //hRMS_Travel_Application.Travel_App_Type = hRMS_TravelMode_MS.Mode_ID;

            //ViewBag.emp_id = new SelectList(db.Accounts, "ID", "UserName");
            //hRMS_Travel_Application.Grade = db.EMP_Grade_Assignment.Where(rec => rec.EMP_ID == user).Select(rec => rec.Grade_ID);
            //ViewBag.Approved_by = new SelectList(db.Accounts, "ID", "UserName");
            // ViewBag.Grade = new SelectList(db.HRMS_CATEGORY_GRADE, "Category_ID", "Category_Name");
            //ViewBag.Designation = new SelectList(db.HRMS_DESG_MS, "Desg_Id", "Desg_Name");
            // ViewBag.Travel_Purpose = db.HRMS_Travel_Purpose;

            //ViewBag.Travel_Type = db.HRMS_Travel_type;
            //ViewBag.Travel_App_Type = db.HRMS_TravelMode_MS;
            ViewBag.dataTravelPurpose = new SelectList(db.HRMS_Travel_Purpose, "ID", "Name");
            HRMS_TravelMode_MS hRMS_TravelMode_MS = db.HRMS_TravelMode_MS.Where(rec => rec.Mode_Type == "Local Coveyance").FirstOrDefault();
            HRMS_Travel_type hRMS_Travel_Type = db.HRMS_Travel_type.Where(rec => rec.Name == "Domestic").FirstOrDefault();
            //hRMS_Travel_Application.Travel_Type = hRMS_Travel_Type.ID;
            //hRMS_Travel_Application.Travel_App_Type = hRMS_TravelMode_MS.Mode_ID;
            ////hRMS_Travel_Application.Travel_Type = hRMS_Travel_Type.ID;
            ViewBag.dataTravelType = new SelectList(db.HRMS_Travel_type, "ID", "Short_Name", hRMS_Travel_Type.ID);

            ViewBag.EntidadList = new SelectList(db.HRMS_TravelMode_MS, "Mode_ID", "Mode_Type", hRMS_TravelMode_MS.Mode_ID);
            return View(hRMS_Travel_Application);
        }


        //Travel_Application_Date
        //Designation
        //Grade
        //,Status,Approved_by,Approved_date,Approval_Remark
        //[Bind(Include = "ID,Travel_App_Type,emp_id,Travel_Type,Travel_Purpose,From_Date,To_Date,Remark")]

        [HttpPost]
        [Authorize(Roles = "emp")]
        public ActionResult Create( HRMS_Travel_Application hRMS_Travel_Application)
        {
            HRMS_TravelMode_MS hRMS_TravelMode_MS = db.HRMS_TravelMode_MS.Where(rec => rec.Mode_Type == "Local Coveyance").FirstOrDefault();
            HRMS_Travel_type hRMS_Travel_Type = db.HRMS_Travel_type.Where(rec => rec.Name == "Domestic").FirstOrDefault();
            ModelState.Remove("Travel_Type");

            hRMS_Travel_Application.Travel_Type = hRMS_Travel_Type.ID;
            ModelState.Remove("Travel_App_Type");

            hRMS_Travel_Application.Travel_App_Type = hRMS_TravelMode_MS.Mode_ID;
            ModelState.Remove("Travel_Application_Date");
            hRMS_Travel_Application.Travel_Application_Date = DateTime.Now;
            
            ModelState.Remove("Designation");
            var designation = db.HRMS_Emp_Details.Where(rec => rec.EMP_ID == hRMS_Travel_Application.emp_id).SingleOrDefault();
            hRMS_Travel_Application.Designation = designation.Designation;

            ModelState.Remove("Grade");
            var grade = db.HRMS_EMP_GRA_POL.Where(r => r.Emp_ID == hRMS_Travel_Application.emp_id).SingleOrDefault();
            hRMS_Travel_Application.Grade = grade.Gra_ID;

            hRMS_Travel_Application.Status = 0;

            if (ModelState.IsValid)
            {
               
               var fromdate = Convert.ToDateTime(hRMS_Travel_Application.From_Date);
               var todate = Convert.ToDateTime(hRMS_Travel_Application.To_Date);
                int result = DateTime.Compare(fromdate, todate);

                if (result < 0) {
                    var samedate = db.HRMS_Travel_Application.Where(rec => rec.emp_id == hRMS_Travel_Application.emp_id && rec.From_Date == hRMS_Travel_Application.From_Date && rec.To_Date == hRMS_Travel_Application.To_Date).FirstOrDefault();
                    if(samedate == null) {     
                    db.HRMS_Travel_Application.Add(hRMS_Travel_Application);
                    db.SaveChanges();
                    ModelState.Clear();

                        ViewBag.ApplicationStatus = "Application generated successfully.";

                        ViewBag.dataTravelPurpose = new SelectList(db.HRMS_Travel_Purpose, "ID", "Name");

                        ViewBag.dataTravelType = new SelectList(db.HRMS_Travel_type, "ID", "Short_Name", hRMS_Travel_Type.ID);

                        ViewBag.EntidadList = new SelectList(db.HRMS_TravelMode_MS, "Mode_ID", "Mode_Type", hRMS_TravelMode_MS.Mode_ID);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.ApplicationStatus = "You have a trip with same From date and To date";

                        ViewBag.dataTravelPurpose = new SelectList(db.HRMS_Travel_Purpose, "ID", "Name");
               
                        ViewBag.dataTravelType = new SelectList(db.HRMS_Travel_type, "ID", "Short_Name");

                        ViewBag.EntidadList = new SelectList(db.HRMS_TravelMode_MS, "Mode_ID", "Mode_Type");
                        return View(hRMS_Travel_Application);
                    }
                }
                 else if (result > 0)
                {
                    ViewBag.ApplicationStatus = "from date is greater then two date!!!";
                    ViewBag.dataTravelPurpose = new SelectList(db.HRMS_Travel_Purpose, "ID", "Name");
                   
                    ViewBag.dataTravelType = new SelectList(db.HRMS_Travel_type, "ID", "Short_Name");

                    ViewBag.EntidadList = new SelectList(db.HRMS_TravelMode_MS, "Mode_ID", "Mode_Type");
                    return View(hRMS_Travel_Application);
                }
               
            }
            ViewBag.ApplicationStatus = "model is in invalid state.";


            ViewBag.dataTravelPurpose = new SelectList(db.HRMS_Travel_Purpose, "ID", "Name");
        
            ViewBag.dataTravelType = new SelectList(db.HRMS_Travel_type, "ID", "Short_Name");

            ViewBag.EntidadList = new SelectList(db.HRMS_TravelMode_MS, "Mode_ID", "Mode_Type");
            return View(hRMS_Travel_Application);
        }

        // GET: TravelApplication/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(long? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_Travel_Application hRMS_Travel_Application = db.HRMS_Travel_Application.Find(id);
            var user = Convert.ToInt64(Session["id"]);
            hRMS_Travel_Application.Approved_by = user;
            hRMS_Travel_Application.Approved_date = DateTime.Now;
            ViewBag.Fromdate = hRMS_Travel_Application.From_Date;
            ViewBag.todate = hRMS_Travel_Application.To_Date;


            if (hRMS_Travel_Application == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_Travel_Application);
        }

        // POST: TravelApplication/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Edit(HRMS_Travel_Application hRMS_Travel_Application)
        {

            //ModelState.Remove("From_Date");
            //var FromDate = db.HRMS_Travel_Application.Where(rec => rec.ID == hRMS_Travel_Application.ID).SingleOrDefault();
            //hRMS_Travel_Application.From_Date = FromDate.From_Date;
            //ModelState.Remove("To_Date");
            //hRMS_Travel_Application.From_Date = FromDate.To_Date;
            //db.Entry(FromDate).State = EntityState.Detached;
            //HRMS_Travel_Application hRMS_Travel_ = db.HRMS_Travel_Application.Find(hRMS_Travel_Application.ID);
            //hRMS_Travel_.Status = hRMS_Travel_Application.Status;
            //db.Entry(hRMS_Travel_Application).State = EntityState.Modified;
            //db.SaveChanges();
            //ModelState.Clear();
            //return RedirectToAction("Index");

            if (ModelState.IsValid)
            {
                db.Entry(hRMS_Travel_Application).State = EntityState.Modified;
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("Index");
                //}
                //    ViewBag.emp_id = new SelectList(db.Accounts, "ID", "UserName", hRMS_Travel_Application.emp_id);
                //ViewBag.Approved_by = new SelectList(db.Accounts, "ID", "UserName", hRMS_Travel_Application.Approved_by);
                //ViewBag.Grade = new SelectList(db.HRMS_CATEGORY_GRADE, "Category_ID", "Category_Name", hRMS_Travel_Application.Grade);
                //ViewBag.Designation = new SelectList(db.HRMS_DESG_MS, "Desg_Id", "Desg_Name", hRMS_Travel_Application.Designation);
                //ViewBag.Travel_Purpose = new SelectList(db.HRMS_Travel_Purpose, "ID", "Name", hRMS_Travel_Application.Travel_Purpose);
                //ViewBag.Travel_Type = new SelectList(db.HRMS_Travel_type, "ID", "Short_Name", hRMS_Travel_Application.Travel_Type);
                //ViewBag.Travel_App_Type = new SelectList(db.HRMS_TravelMode_MS, "Mode_ID", "Mode_Type", hRMS_Travel_Application.Travel_App_Type);
            }
            return RedirectToAction("Index");

        }
        [Authorize(Roles = "admin")]

        public ActionResult Approve(long id)
        {
            var application = db.HRMS_Travel_Application.Where(rec => rec.ID == id).SingleOrDefault();
            if(application.Status == 2)
            {
                return RedirectToAction("Index");
            }
            application.Status = 1;
            db.Entry(application).State = EntityState.Modified;
            db.SaveChanges();
            ModelState.Clear();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "admin")]

        public ActionResult Cancel(long id)
        {
            var application = db.HRMS_Travel_Application.Where(rec => rec.ID == id).SingleOrDefault();
            application.Status = 3;
            db.Entry(application).State = EntityState.Modified;
            db.SaveChanges();
            ModelState.Clear();
            return RedirectToAction("Index");
        }

        // GET: TravelApplication/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_Travel_Application hRMS_Travel_Application = db.HRMS_Travel_Application.Find(id);
            if (hRMS_Travel_Application == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_Travel_Application);
        }

        // POST: TravelApplication/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            HRMS_Travel_Application hRMS_Travel_Application = db.HRMS_Travel_Application.Find(id);
            db.HRMS_Travel_Application.Remove(hRMS_Travel_Application);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public int getTravelCount()
        {
            int Count = 0;

            Count = db.HRMS_Travel_Application.Where(rec => rec.Status == 0).Count();

            return Count;
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
