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
    
    public class TrainingApprovalController : Controller
    {
        private HRMSEntities db = new HRMSEntities();

        // GET: TrainingApproval
        public ActionResult Index()
        {
            if (User.IsInRole("emp"))
            {
                return View(db.HRMS_ProgramDetail.Where(x => x.TrainingStatus != "Cancel").ToList());

                //long id = Convert.ToInt64(Session["id"]);

                //long ?facID = db.HRMS_Faculty_MS.Where(x=>x.EMP_ID==id).Select(x=>x.ID).FirstOrDefault();
                //if (facID != null) //assigned as faculty
                //{


                //    List<HRMS_ProgramDetail> Y = (from s in db.HRMS_ProgramDetail
                //                                  join sa in db.HRMS_ProgramFaculty on s.ID equals sa.ProgramID
                //                                  where sa.FacultyID != facID
                //                                  select s).ToList();

                //    // return View(db.HRMS_ProgramDetail.Where(x => x.TrainingStatus != "Cancel").ToList());
                //    return View(Y);
                //}
                //else //not assigned as faculty
                //{
                //       
                //}

            }

            else
            {
               
                return View(db.HRMS_ProgramDetail.Where(x => x.TrainingStatus != "Cancel").ToList());
            }

         }

        [HttpGet]
        //View Program
        public ActionResult View(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            HRMS_ProgramDetail obj = db.HRMS_ProgramDetail.Find(id.Value);
            HRMS_TrainingApproval obj1 = new HRMS_TrainingApproval();

            obj1.EMP_ID = Convert.ToInt64(Session["id"]);

            HRMS_Emp_Details Detail = db.HRMS_Emp_Details.Where(x=>x.EMP_ID==obj1.EMP_ID).FirstOrDefault();
            obj1.Designation = Detail.Designation;
            obj1.EmpDept = Detail.Department;
            obj1.Program_ID = id.Value;
            obj1.EmpName = Detail.First_Name + " " + Detail.Last_Name;
           // obj1.HRMS_ProgramDetail.FromDate = obj.FromDate;

            //readonly dropdown
            ViewBag.Designation = new SelectList(db.HRMS_DESG_MS.Where(x=>x.Desg_Id==obj1.Designation), "Desg_Id", "Desg_Name");
            ViewBag.EmpDept = new SelectList(db.HRMS_DEPT.Where(x=>x.Dept_Id==obj1.EmpDept), "Dept_Id", "Dept_Name");
            ViewBag.Program_ID = new SelectList(db.HRMS_ProgramDetail.Where(x=>x.ID==obj1.Program_ID), "ID", "ProgramName");

            // ViewBag.xyz = new SelectList(db.HRMS_ProgramDetail.Where(x => x.ID == obj1.Program_ID), "FromDate", "FromDate");
            obj1.FromDate = obj.FromDate;
            obj1.ToDate = obj.ToDate;
            obj1.FromTime = obj.FromTime;
            obj1.ToTime = obj.ToTime;


            //NoOfDays
            TimeSpan? difference = obj.ToDate - obj.FromDate;
            ViewBag.NoOfDays = difference.Value.TotalDays;

            ViewBag.remarks = obj.Remarks;

            //already Exist or completed
            if (User.IsInRole("emp"))
            {
                bool IsExist = db.HRMS_TrainingApproval.Any(x => x.EMP_ID == obj1.EMP_ID && x.Program_ID == id.Value);
                bool IsExist1 = db.HRMS_ProgramDetail.Any(x => x.ID == id.Value && x.TrainingStatus == "Completed");
                if (IsExist || IsExist1)
                {
                    ViewBag.exist = "Exist";

                }

                HRMS_TrainingApproval status = db.HRMS_TrainingApproval.Where(x=>x.EMP_ID==obj1.EMP_ID && x.Program_ID==id.Value).FirstOrDefault();
                if (status != null)
                {
                    if (status.Status == 1)
                    {
                        ViewBag.msg = "Your Request is in Pending!";
                    }
                    else if (status.Status == 2)
                    {
                        ViewBag.msg = "Your Request is Approved!";
                    }
                    else if (status.Status == 3)
                    {
                        ViewBag.msg = "Your Request is Cancelled!";
                    }
                   
                }
            }


            return View(obj1);
        }

        //post method
        [HttpPost]
        public ActionResult Vieww(HRMS_TrainingApproval obj)
        {
            obj.Status = 1; //pending request

            db.HRMS_TrainingApproval.Add(obj);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult RequestList(long id)
        {
            ViewBag.ProID = id;
            if (db.HRMS_TrainingApproval.Where(x => x.Program_ID == id && x.Status == 1).Count() == 0)
            {
                ViewBag.NoPending = "No";
            }
            return View(db.HRMS_TrainingApproval.Where(x=>x.Program_ID==id && x.Status==1).ToList());
        }


        //approve
        public ActionResult Approve(long id)
        {
            HRMS_TrainingApproval obj = db.HRMS_TrainingApproval.Where(x => x.ID == id).FirstOrDefault();
            obj.ApproveBy = Convert.ToInt64(Session["id"]);
            obj.ApproveDate = DateTime.Now;
            obj.Status = 2;//approved
            db.Entry(obj).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("RequestList",new {id=obj.Program_ID});

        }

        //Cancel
        public ActionResult Reject(long id)
        {
            HRMS_TrainingApproval obj = db.HRMS_TrainingApproval.Where(x => x.ID == id).FirstOrDefault();
            
            obj.Status = 3;//Reject
            db.Entry(obj).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("RequestList", new { id=obj.Program_ID });

        }

        //Approve ALL
        public ActionResult ApproveAll(long id)
        {
            var Listt = db.HRMS_TrainingApproval.Where(x =>x.Program_ID==id && x.Status==1).ToList();
            Listt.ForEach(x => x.Status = 2);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        //Cancel ALL
        public ActionResult RejectAll(long id)
        {
            var Listt = db.HRMS_TrainingApproval.Where(x => x.Program_ID == id && x.Status==1).ToList();
            Listt.ForEach(x => x.Status = 3);
            db.SaveChanges();
            return RedirectToAction("Index");

        }


        
    }
}
