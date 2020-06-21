using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using HRMS.Models;

namespace HRMS.Controllers
{
    [Authorize(Roles="midman,admin")]
    public class TrainingEffectiveRemarksController : Controller
    {
        private HRMSEntities db = new HRMSEntities();

        // GET: HRMS_TRAINING_EFFECTIVENESS_REMARKS
        public ActionResult Index(long id)
        {
            var hRMS_TRAINING_EFFECTIVENESS_REMARKS = db.HRMS_TRAINING_EFFECTIVENESS_REMARKS.Where(h => h.Program_Det_ID == id).Include(h => h.HRMS_ProgramDetail);

            return View(hRMS_TRAINING_EFFECTIVENESS_REMARKS.ToList());
        }

        // GET: HRMS_TRAINING_EFFECTIVENESS_REMARKS/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_TRAINING_EFFECTIVENESS_REMARKS hRMS_TRAINING_EFFECTIVENESS_REMARKS = db.HRMS_TRAINING_EFFECTIVENESS_REMARKS.Find(id);
            if (hRMS_TRAINING_EFFECTIVENESS_REMARKS == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_TRAINING_EFFECTIVENESS_REMARKS);
        }
        public ActionResult Index1()
        {
            long id =Convert.ToInt64(Session["id"]);

            if (User.IsInRole("midman"))
            {
                var c = db.HRMS_ProgramDetail.Where(x => x.TrainingStatus == "Completed" && x.HRMS_Training_Request_Application.EmpID == id).Count();
                //var n = db.HRMS_Training_Request_Application.Where(x => x.EmpID == id).Select(x => x.ApplicationId).ToList();
                // var hRMS_ProgramDetail = db.HRMS_ProgramDetail.Where(x => x.TrainingStatus == "Completed" && x.TrainingID==n).ToList();

                if (c != 0)
                {
                    List<HRMS_ProgramDetail> hRMS_ProgramDetail = db.HRMS_ProgramDetail.Where(x => x.TrainingStatus == "Completed" && x.HRMS_Training_Request_Application.EmpID == id).ToList();
                    //foreach (var item in n)
                    //{
                    //    hRMS_ProgramDetail = db.HRMS_ProgramDetail.Where(x => x.TrainingStatus == "Completed" && x.HRMS_Training_Request_Application.EmpID == id).ToList();
                    //}
                    return View(hRMS_ProgramDetail.ToList());
                }
                else
                {
                    List<HRMS_ProgramDetail> hRMS_ProgramDetail = db.HRMS_ProgramDetail.ToList();
                    ViewBag.status = "No Data Available";
                    return View(hRMS_ProgramDetail);
                }
            }
            else
            {
                List<HRMS_ProgramDetail> hRMS_ProgramDetail = db.HRMS_ProgramDetail.Where(x => x.TrainingStatus == "Completed").ToList();
                //ViewBag.status = "No Data Available";
                return View(hRMS_ProgramDetail);
            }
        }
        // GET: HRMS_TRAINING_EFFECTIVENESS_REMARKS/Create
        public ActionResult Create(long id)
        {
            ViewBag.Program_Det_ID = db.HRMS_ProgramDetail.Where(x => x.ID == id).FirstOrDefault();
            var a = db.HRMS_ProgramDetail.Where(x => x.ID == id).Select(x => x.TrainingID).FirstOrDefault();
            var b = db.HRMS_Training_Request_Application.Where(x => x.ApplicationId == a).Select(x => x.EmpID).FirstOrDefault();
            ViewBag.HOD_Detail = db.Accounts.Where(x => x.ID == b).FirstOrDefault();
            ViewBag.EMP_List = db.HRMS_TrainingApproval.Where(x => x.Remark == false).ToList();
            return View();
        }

        // POST: HRMS_TRAINING_EFFECTIVENESS_REMARKS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Remark_ID,Program_Det_ID,Emp_ID,HOD_ID,HOD_Remarks,Trainee_Remarks,HOD_View,Remark_Date")] HRMS_TRAINING_EFFECTIVENESS_REMARKS hRMS_TRAINING_EFFECTIVENESS_REMARKS)
        {

            ViewBag.Program_Det_ID = db.HRMS_ProgramDetail.Where(x => x.ID == hRMS_TRAINING_EFFECTIVENESS_REMARKS.Program_Det_ID).FirstOrDefault();
            var a = db.HRMS_ProgramDetail.Where(x => x.ID == hRMS_TRAINING_EFFECTIVENESS_REMARKS.Program_Det_ID).Select(x => x.TrainingID).FirstOrDefault();
            var b = db.HRMS_Training_Request_Application.Where(x => x.ApplicationId == a).Select(x => x.EmpID).FirstOrDefault();
            ViewBag.HOD_Detail = db.Accounts.Where(x => x.ID == b).FirstOrDefault();
            ViewBag.EMP_List = db.HRMS_TrainingApproval.Where(x => x.Remark == false).ToList();
            if (ModelState.IsValid)
            {




                if (!db.HRMS_TRAINING_EFFECTIVENESS_REMARKS.Where(x => x.Program_Det_ID == hRMS_TRAINING_EFFECTIVENESS_REMARKS.Program_Det_ID && x.Emp_ID == hRMS_TRAINING_EFFECTIVENESS_REMARKS.Emp_ID).Any() && hRMS_TRAINING_EFFECTIVENESS_REMARKS.Emp_ID != 0)
                {

                    db.HRMS_TRAINING_EFFECTIVENESS_REMARKS.Add(hRMS_TRAINING_EFFECTIVENESS_REMARKS);

                    db.SaveChanges();
                   
                    var c = db.HRMS_TrainingApproval.Where(x => x.EMP_ID == hRMS_TRAINING_EFFECTIVENESS_REMARKS.Emp_ID && x.Program_ID ==hRMS_TRAINING_EFFECTIVENESS_REMARKS.Program_Det_ID).Select(x => x.ID).FirstOrDefault();
                    HRMS_TrainingApproval hRMS_TrainingApproval = db.HRMS_TrainingApproval.Find(c);
                    if (hRMS_TrainingApproval.Remark != true)
                    {
                        hRMS_TrainingApproval.Remark = true;
                        db.Entry(hRMS_TrainingApproval).State = EntityState.Modified;
                        db.SaveChanges();
                        ModelState.Clear();
                    }
                   
                    ViewBag.Program_Det_ID = db.HRMS_ProgramDetail.Where(x => x.ID == hRMS_TRAINING_EFFECTIVENESS_REMARKS.Program_Det_ID).FirstOrDefault();
                    var b3 = db.HRMS_ProgramDetail.Where(x => x.ID == hRMS_TRAINING_EFFECTIVENESS_REMARKS.Program_Det_ID).Select(x => x.TrainingID).FirstOrDefault();
                    var z1 = db.HRMS_Training_Request_Application.Where(x => x.ApplicationId == b3).Select(x => x.EmpID).FirstOrDefault();
                    ViewBag.HOD_Detail = db.Accounts.Where(x => x.ID == z1).FirstOrDefault();
                    ViewBag.EMP_List = db.HRMS_TrainingApproval.Where(x => x.Remark == false).ToList();
                    return View();
                    //return RedirectToAction("Index1");
                }



            }
            else if (hRMS_TRAINING_EFFECTIVENESS_REMARKS.Emp_ID == 0 && hRMS_TRAINING_EFFECTIVENESS_REMARKS.HOD_Remarks != null && hRMS_TRAINING_EFFECTIVENESS_REMARKS.Trainee_Remarks != null && hRMS_TRAINING_EFFECTIVENESS_REMARKS.HOD_View != null && hRMS_TRAINING_EFFECTIVENESS_REMARKS.Program_Det_ID != 0 && hRMS_TRAINING_EFFECTIVENESS_REMARKS.HOD_ID != 0 && hRMS_TRAINING_EFFECTIVENESS_REMARKS.Remark_Date != null)
            {

                var a1 = db.HRMS_TrainingApproval.Where(x => x.Remark != true && x.Program_ID == hRMS_TRAINING_EFFECTIVENESS_REMARKS.Program_Det_ID).Select(x => x.EMP_ID).ToList();
                if (a1 != null)
                {
                    foreach (var item in a1)
                    {
                        hRMS_TRAINING_EFFECTIVENESS_REMARKS.Emp_ID = item;

                        db.HRMS_TRAINING_EFFECTIVENESS_REMARKS.Add(hRMS_TRAINING_EFFECTIVENESS_REMARKS);
                        db.SaveChanges();
                        var c = db.HRMS_TrainingApproval.Where(x => x.EMP_ID == hRMS_TRAINING_EFFECTIVENESS_REMARKS.Emp_ID && x.Program_ID==hRMS_TRAINING_EFFECTIVENESS_REMARKS.Program_Det_ID).Select(x => x.ID).FirstOrDefault();
                        HRMS_TrainingApproval hRMS_TrainingApproval = db.HRMS_TrainingApproval.Find(c);
                        if (hRMS_TrainingApproval.Remark != true)
                        {
                            hRMS_TrainingApproval.Remark = true;
                            db.Entry(hRMS_TrainingApproval).State = EntityState.Modified;
                            db.SaveChanges();
                        }

                    }

                }


                ViewBag.Program_Det_ID = db.HRMS_ProgramDetail.Where(x => x.ID == hRMS_TRAINING_EFFECTIVENESS_REMARKS.Program_Det_ID).FirstOrDefault();
                var b1 = db.HRMS_ProgramDetail.Where(x => x.ID == hRMS_TRAINING_EFFECTIVENESS_REMARKS.Program_Det_ID).Select(x => x.TrainingID).FirstOrDefault();
                var z2 = db.HRMS_Training_Request_Application.Where(x => x.ApplicationId == b1).Select(x => x.EmpID).FirstOrDefault();
                ViewBag.HOD_Detail = db.Accounts.Where(x => x.ID == z2).FirstOrDefault();
                ViewBag.EMP_List = db.HRMS_TrainingApproval.Where(x => x.Remark == false).ToList();
                return View();

            }
            else
            {
                ViewBag.Program_Det_ID = db.HRMS_ProgramDetail.Where(x => x.ID == hRMS_TRAINING_EFFECTIVENESS_REMARKS.Program_Det_ID).FirstOrDefault();
                var b2 = db.HRMS_ProgramDetail.Where(x => x.ID == hRMS_TRAINING_EFFECTIVENESS_REMARKS.Program_Det_ID).Select(x => x.TrainingID).FirstOrDefault();
                var z3 = db.HRMS_Training_Request_Application.Where(x => x.ApplicationId == b2).Select(x => x.EmpID).FirstOrDefault();
                ViewBag.HOD_Detail = db.Accounts.Where(x => x.ID == z3).FirstOrDefault();
                ViewBag.EMP_List = db.HRMS_TrainingApproval.Where(x => x.Remark == false).ToList();
                return View();

            }

            //ViewBag.Program_Det_ID = new SelectList(db.HRMS_ProgramDetail, "ID", "ProgramName", hRMS_TRAINING_EFFECTIVENESS_REMARKS.Program_Det_ID);
            ViewBag.Program_Det_ID = db.HRMS_ProgramDetail.Where(x => x.ID == hRMS_TRAINING_EFFECTIVENESS_REMARKS.Program_Det_ID).FirstOrDefault();
            var z4 = db.HRMS_ProgramDetail.Where(x => x.ID == hRMS_TRAINING_EFFECTIVENESS_REMARKS.Program_Det_ID).Select(x => x.TrainingID).FirstOrDefault();
            var z5 = db.HRMS_Training_Request_Application.Where(x => x.ApplicationId == z4).Select(x => x.EmpID).FirstOrDefault();
            ViewBag.HOD_Detail = db.Accounts.Where(x => x.ID == z5).FirstOrDefault();
            ViewBag.EMP_List = db.HRMS_TrainingApproval.Where(x => x.Remark == false).ToList();
            return View();
        }

        // GET: HRMS_TRAINING_EFFECTIVENESS_REMARKS/Edit/5
        


        // POST: HRMS_TRAINING_EFFECTIVENESS_REMARKS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
       
        
        public bool Delete(long id)
        {
            HRMS_TRAINING_EFFECTIVENESS_REMARKS hRMS_TRAINING_EFFECTIVENESS_REMARKS = db.HRMS_TRAINING_EFFECTIVENESS_REMARKS.Find(id);
            if (hRMS_TRAINING_EFFECTIVENESS_REMARKS != null)
            {
                //long P_id = hRMS_TRAINING_EFFECTIVENESS_REMARKS.Program_Det_ID;

                db.HRMS_TRAINING_EFFECTIVENESS_REMARKS.Remove(hRMS_TRAINING_EFFECTIVENESS_REMARKS);
                db.SaveChanges();
                var c = db.HRMS_TrainingApproval.Where(x => x.EMP_ID == hRMS_TRAINING_EFFECTIVENESS_REMARKS.Emp_ID && x.Program_ID ==hRMS_TRAINING_EFFECTIVENESS_REMARKS.Program_Det_ID).Select(x => x.ID).FirstOrDefault();
                HRMS_TrainingApproval hRMS_TrainingApproval = db.HRMS_TrainingApproval.Find(c);
                if (hRMS_TrainingApproval.Remark != false)
                {
                    hRMS_TrainingApproval.Remark = false;
                    db.Entry(hRMS_TrainingApproval).State = EntityState.Modified;
                    db.SaveChanges();
                }
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
