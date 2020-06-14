using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HRMS.Models;
using HRMS.ViewModel;

//programflag=1 : Active
//programflag=1 : Completed
//programflag=1 : Cancel

namespace HRMS.Controllers
{
    [Authorize(Roles = "admin")]
    public class TrainingProgramDetailController : Controller
    {
        private HRMSEntities db = new HRMSEntities();

        public JsonResult GetFacultyList(string ProgrammID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            long PID = Convert.ToInt64(ProgrammID);

            List<HRMS_ProgramFaculty> Flist = db.HRMS_ProgramFaculty.Where(x => x.ProgramID == PID).ToList();
            return Json(Flist, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AutoComplete(string prefix)
        {

            var Result1 = (from Result in db.HRMS_Faculty_MS
                           where Result.External_Name.StartsWith(prefix)
                           select new
                           {
                               label = Result.External_Name +" "+Result.Email,
                               val = Result.ID
                           }).ToList();

            return Json(Result1);
        }

        [HttpPost]
        public JsonResult AddFacultyy(string facid, string proid)
        {
            db.Configuration.ProxyCreationEnabled = false;

            //adding into table 
            long fac_id = Convert.ToInt64(facid);
            long pro_id = Convert.ToInt64(proid);

            bool IsExist = db.HRMS_ProgramFaculty.Any(x=>x.ProgramID==pro_id && x.FacultyID==fac_id);
            if (!IsExist)
            {
                HRMS_Faculty_MS obj = db.HRMS_Faculty_MS.Where(x => x.ID == fac_id).FirstOrDefault();

                HRMS_ProgramFaculty obj1 = new HRMS_ProgramFaculty();
                obj1.FacultyID = fac_id;
                obj1.FacultyName = obj.External_Name;
                obj1.Email = obj.Email;
                obj1.ProgramID = pro_id;

                db.HRMS_ProgramFaculty.Add(obj1);
                db.SaveChanges();


                //List<HRMS_ProgramFaculty> FacList = db.HRMS_ProgramFaculty.Where(x => x.ProgramID == pro_id).ToList();
                var FacList = pro_id;
                return Json(FacList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var FacList = "Faculty Already Exist!";
                return Json(FacList, JsonRequestBehavior.AllowGet);
            }
        }

        //delete faculty
        public bool deletefaculty(long id)
        {
            HRMS_ProgramFaculty fac = db.HRMS_ProgramFaculty.Find(id);
            if (fac != null)
            {
                db.HRMS_ProgramFaculty.Remove(fac);
                db.SaveChanges(); 
                //ModelState.Clear();

                return true;
            }
            else
            {
                return false;
            }
        }

        //GetAdminID
        public long GetAdminID()
        {
            long id =Convert.ToInt64(Session["id"]);
            return id;
        }



        // GET: TrainingProgramDetail
        public ActionResult Index()
        {
            //var hRMS_ProgramDetail = db.HRMS_ProgramDetail.Include(h => h.HRMS_Training_Request_Application);
            //return View(hRMS_ProgramDetail.ToList());
            return View(db.HRMS_Training_Request_Application.ToList());
        }

        // GET: TrainingProgramDetail/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_ProgramDetail hRMS_ProgramDetail = db.HRMS_ProgramDetail.Find(id);
            if (hRMS_ProgramDetail == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_ProgramDetail);
        }

        // GET: TrainingProgramDetail/Create
        public ActionResult Create(long? id)
        {
            HRMS_ProgramDetail obj = new HRMS_ProgramDetail();
            obj.TransactionDate = DateTime.Now;
            obj.TrainingID = id.Value;
            TrainingProgramCommon O = new TrainingProgramCommon();
            O.ProDetail = obj;


            //ViewBag.TrainingID = new SelectList(db.HRMS_Training_Request_Application.Where(x => x.ApplicationId == id.Value), "ApplicationId", "Training_Name");
            return View(O);
        }

        // POST: TrainingProgramDetail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult Create(TrainingProgramCommon hRMS_ProgramDetail)
        {
            
                HRMS_ProgramDetail obj = hRMS_ProgramDetail.ProDetail;
            if (obj.ToDate < obj.FromDate)
            {
                var msg = "ToDate must be greater than FromDate!";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            else
            {

                db.HRMS_ProgramDetail.Add(obj);
                db.SaveChanges();
                ViewBag.msg = "Program Added Succesfully! Now Add Faculty!";
                var ID = db.HRMS_ProgramDetail.Where(x => x.TrainingID == obj.TrainingID).Select(x => x.ID).FirstOrDefault();

                //update in training request table (programFlag)
                HRMS_Training_Request_Application req = db.HRMS_Training_Request_Application.Find(obj.TrainingID);
                req.ProgramFlag = 1;  //admin can only edit program detail
                db.Entry(req).State = EntityState.Modified;
                db.SaveChanges();


                return Json(ID, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("Index");
            }
        }

        // GET: TrainingProgramDetail/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_ProgramDetail hRMS_ProgramDetail = db.HRMS_ProgramDetail.Where(x=>x.TrainingID==id.Value).FirstOrDefault();
            System.TimeSpan y;
            //hRMS_ProgramDetail.FromTime =y;

            

            if (hRMS_ProgramDetail == null)
            {
                return HttpNotFound();
            }
            TrainingProgramCommon obj = new TrainingProgramCommon();
            obj.ProDetail = hRMS_ProgramDetail;
            //ViewBag.TrainingID = new SelectList(db.HRMS_Training_Request_Application, "ApplicationId", "Training_Name", hRMS_ProgramDetail.TrainingID);
            return View(obj);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult Edit(TrainingProgramCommon hRMS_ProgramDetail)
        {

            HRMS_ProgramDetail obj = hRMS_ProgramDetail.ProDetail;

            if (obj.ToDate < obj.FromDate)
            {
                var msg = "ToDate must be greater than FromDate!";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (obj.TrainingStatus == "Completed")
                {
                    HRMS_Training_Request_Application req = db.HRMS_Training_Request_Application.Where(x => x.ApplicationId == obj.TrainingID).FirstOrDefault();
                    req.ProgramFlag = 2; //completed
                    db.Entry(req).State = EntityState.Modified;
                    db.SaveChanges();
                }

                if (obj.TrainingStatus == "Cancel")
                {
                    HRMS_Training_Request_Application req = db.HRMS_Training_Request_Application.Where(x => x.ApplicationId == obj.TrainingID).FirstOrDefault();
                    req.ProgramFlag = 3; //Cancel
                    db.Entry(req).State = EntityState.Modified;
                    db.SaveChanges();
                }

                db.Entry(obj).State = EntityState.Modified;
                db.SaveChanges();

                return Json(true, JsonRequestBehavior.AllowGet);
            }

        }



        // POST: TrainingProgramDetail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID,TransactionDate,TrainingID,ProgramName,Subject,FromDate,ToDate,FromTime,ToTime,TrainingType,ProgramType,SubjectType,Type,ProgramMode,Venue,Budget,Address,City,BenefitsToOrg,Remarks,RatingMethod,TrainingStatus,CompletedBy,CompleteDate,RemarksOther,Flag")] HRMS_ProgramDetail hRMS_ProgramDetail)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(hRMS_ProgramDetail).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.TrainingID = new SelectList(db.HRMS_Training_Request_Application, "ApplicationId", "Training_Name", hRMS_ProgramDetail.TrainingID);
        //    return View(hRMS_ProgramDetail);
        //}

        // GET: TrainingProgramDetail/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_ProgramDetail hRMS_ProgramDetail = db.HRMS_ProgramDetail.Find(id);
            if (hRMS_ProgramDetail == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_ProgramDetail);
        }

        // POST: TrainingProgramDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            HRMS_ProgramDetail hRMS_ProgramDetail = db.HRMS_ProgramDetail.Find(id);
            db.HRMS_ProgramDetail.Remove(hRMS_ProgramDetail);
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
