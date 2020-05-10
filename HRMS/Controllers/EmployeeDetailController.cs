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
    public class EmployeeDetailController : Controller
    {
        private HRMSEntities db = new HRMSEntities();

        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            //return Content(Session["id"].ToString());
            var hRMS_Emp_Details = db.HRMS_Emp_Details.Include(h => h.HRMS_COST_CENTER).Include(h => h.HRMS_DEPT).Include(h => h.HRMS_DESG_MS).Include(h => h.HRMS_SALUTATION).Include(h => h.UnitMaster).Include(h => h.WorkLocationMaster);
            return View(hRMS_Emp_Details.ToList());
        }
        [Authorize(Roles = "admin,emp")]
        public ActionResult Details(long? id)
        {
            long emp_id = Convert.ToInt64(Session["id"]);
            string role = db.Accounts.Where(x => x.ID == emp_id).Select(x => x.role).FirstOrDefault();
            if (role == "admin")
            {
                ViewBag.Role = "admin";
            }

            //if no entry it will redirect to create
            bool isExist = db.HRMS_Emp_Details.Any(x=>x.EMP_ID==emp_id);
            if (!isExist)
            {
                return RedirectToAction("Create");
            }

            else
            {

                //if (id == null)
                //{
                //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                //}
                if (role == "emp")
                {
                    id = db.HRMS_Emp_Details.Where(x => x.EMP_ID == emp_id).Select(x => x.ID).FirstOrDefault();
                }
                HRMS_Emp_Details hRMS_Emp_Details = db.HRMS_Emp_Details.Find(id);
                if (hRMS_Emp_Details == null)
                {
                    return HttpNotFound();
                }
                return View(hRMS_Emp_Details);
            }
            }

        [Authorize(Roles = "admin,emp")]
        public ActionResult Create()
        {
            long emp_id = Convert.ToInt64(Session["id"]);
            string role = db.Accounts.Where(x => x.ID == emp_id).Select(x => x.role).FirstOrDefault();
            if (role == "admin")
            {
                ViewBag.Role = "admin";
            }

            //if attck by direct URL
            if (role == "emp")
            {
                bool isExist = db.HRMS_Emp_Details.Any(x => x.EMP_ID == emp_id);
                if (isExist)
                {
                    long id = db.HRMS_Emp_Details.Where(x => x.EMP_ID == emp_id).Select(x => x.ID).FirstOrDefault();
                    return RedirectToAction("Details", "EmployeeDetail", new { id });
                }
            }


            ViewBag.Cost_Center = db.HRMS_COST_CENTER;
            ViewBag.Designation = db.HRMS_DESG_MS.Where(rec => rec.IsActive == true);
            ViewBag.Department = db.HRMS_DEPT.Where(rec => rec.Parent_ID != null && rec.IsActive == true);
            ViewBag.salutation = db.HRMS_SALUTATION;
            ViewBag.Unit = db.UnitMaster;
            ViewBag.Work_Location = db.WorkLocationMaster;
            ViewBag.DivisionData = db.HRMS_DEPT.Where(rec => rec.Parent_ID == null && rec.IsActive == true);
            return View();
        }

        [Authorize(Roles = "admin,emp")]
        public JsonResult GetDepartmentData(string DivisionData)
        {
            var DivisionID = Convert.ToInt64(DivisionData);
            List<HRMS_DEPT> DepartmentList = db.HRMS_DEPT.Where(x => x.Parent_ID == DivisionID).ToList();

            return Json(DepartmentList, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [Authorize(Roles = "admin,emp")]
        public ActionResult Create(HRMS_Emp_Details hRMS_Emp_Details)
        {
            long emp_id = Convert.ToInt64(Session["id"]);
            string role = db.Accounts.Where(x => x.ID == emp_id).Select(x => x.role).FirstOrDefault();
            
            if (role == "emp")
            {
                ModelState.Remove("EMP_ID");
                hRMS_Emp_Details.EMP_ID = emp_id;
            }

            if (ModelState.IsValid)
            {
               
                HRMS_Emp_Details AlreadyEmpId = db.HRMS_Emp_Details.Where(rec => rec.EMP_ID == hRMS_Emp_Details.EMP_ID).FirstOrDefault();

                if (AlreadyEmpId == null)
                {
                    //HRMS_Emp_Details AlreadyEmpCODE = db.HRMS_Emp_Details.Where(rec => rec.Emp_Cd == hRMS_Emp_Details.Emp_Cd).FirstOrDefault();
                    //if (AlreadyEmpCODE == null)
                    //{
                        bool AlreadyCard_Id = db.HRMS_Emp_Details.Any(rec => rec.Card_Id == hRMS_Emp_Details.Card_Id);

                        if (!AlreadyCard_Id || (AlreadyCard_Id && hRMS_Emp_Details.Card_Id == null))
                        {
                            // HRMS_Emp_Details Employee_data = new HRMS_Emp_Details();
                            // Employee_data.Emp_ID = hRMS_Emp_Details.Emp_ID;
                            // Employee_data.Emp_Cd = hRMS_Emp_Details.Emp_Cd;
                            // Employee_data.Old_Emp_Cd = hRMS_Emp_Details.Old_Emp_Cd;
                            // Employee_data.Join_Date = hRMS_Emp_Details.Join_Date;
                            // Employee_data.Card_Id = hRMS_Emp_Details.Card_Id;
                            // Employee_data.salutation = hRMS_Emp_Details.salutation;
                            // Employee_data.First_Name = hRMS_Emp_Details.First_Name;
                            // Employee_data.Middle_Name = hRMS_Emp_Details.Middle_Name;
                            // Employee_data.Last_Name = hRMS_Emp_Details.Last_Name;
                            // Employee_data.Display_Name = hRMS_Emp_Details.Display_Name;
                            // Employee_data.Designation_id = hRMS_Emp_Details.Designation_id;
                            // Employee_data.Work_Location = hRMS_Emp_Details.Work_Location;
                            // Employee_data.Unit = hRMS_Emp_Details.Unit;
                            // Employee_data.Department = hRMS_Emp_Details.Department;
                            // Employee_data.Cost_Center = hRMS_Emp_Details.Cost_Center;
                            //Employee_data.UAN_No_ = hRMS_Emp_Details.UAN_No_;


                            db.HRMS_Emp_Details.Add(hRMS_Emp_Details);
                            db.SaveChanges();
                            ViewBag.EmpDetailStatus = "It is Created successfully!";
                            ModelState.Clear();
                        if (role == "emp")
                        {
                            long id = db.HRMS_Emp_Details.Where(x => x.EMP_ID == emp_id).Select(x => x.ID).FirstOrDefault();
                            return RedirectToAction("Details","EmployeeDetail",new {id});
                        }

                        ViewBag.Cost_Center = db.HRMS_COST_CENTER;
                        ViewBag.Designation = db.HRMS_DESG_MS.Where(rec => rec.IsActive == true);
                        ViewBag.Department = db.HRMS_DEPT.Where(rec => rec.Parent_ID != null && rec.IsActive == true);
                        ViewBag.salutation = db.HRMS_SALUTATION;
                        ViewBag.Unit = db.UnitMaster;
                        ViewBag.Work_Location = db.WorkLocationMaster;
                        ViewBag.DivisionData = db.HRMS_DEPT.Where(rec => rec.Parent_ID == null && rec.IsActive == true);
                        
                        if (role == "admin")
                        {
                            ViewBag.Role = "admin";
                        }
                        return View();



                    }
                        else if(AlreadyCard_Id && hRMS_Emp_Details.Card_Id!=null)
                        {
                            ViewBag.EmpDetailStatus = "The Card ID is Already Exist for another employee!";

                            ViewBag.Cost_Center = db.HRMS_COST_CENTER;
                            ViewBag.Designation = db.HRMS_DESG_MS.Where(rec => rec.IsActive == true);
                            ViewBag.Department = db.HRMS_DEPT.Where(rec => rec.Parent_ID != null && rec.IsActive == true);
                            ViewBag.salutation = db.HRMS_SALUTATION;
                            ViewBag.Unit = db.UnitMaster;
                            ViewBag.Work_Location = db.WorkLocationMaster;
                            ViewBag.DivisionData = db.HRMS_DEPT.Where(rec => rec.Parent_ID == null && rec.IsActive == true);
                        
                        if (role == "admin")
                        {
                            ViewBag.Role = "admin";
                        }
                        return View();
                        }
                    
                    //else
                    //{
                    //    ViewBag.EmpDetailStatus = "The Employee Code is Already Exist for another employee!";
                    //    ViewBag.Cost_Center = db.HRMS_COST_CENTER;
                    //    ViewBag.Designation = db.HRMS_DESG_MS.Where(rec => rec.IsActive == true);
                    //    ViewBag.Department = db.HRMS_DEPT.Where(rec => rec.Parent_ID != null && rec.IsActive == true);
                    //    ViewBag.salutation = db.HRMS_SALUTATION;
                    //    ViewBag.Unit = db.UnitMasters;
                    //    ViewBag.Work_Location = db.WorkLocationMasters;
                    //    ViewBag.DivisionData = db.HRMS_DEPT.Where(rec => rec.Parent_ID == null && rec.IsActive == true);
                    //    return View();
                    //}
                }
                else
                {
                    ViewBag.EmpDetailStatus = "The Employee ID is Already Exist for another employee!";
                    ViewBag.Cost_Center = db.HRMS_COST_CENTER;
                    ViewBag.Designation = db.HRMS_DESG_MS.Where(rec => rec.IsActive == true);
                    ViewBag.Department = db.HRMS_DEPT.Where(rec => rec.Parent_ID != null && rec.IsActive == true);
                    ViewBag.salutation = db.HRMS_SALUTATION;
                    ViewBag.Unit = db.UnitMaster;
                    ViewBag.Work_Location = db.WorkLocationMaster;
                    ViewBag.DivisionData = db.HRMS_DEPT.Where(rec => rec.Parent_ID == null && rec.IsActive == true);
                    
                    if (role == "admin")
                    {
                        ViewBag.Role = "admin";
                    }
                    return View();
                }
            }

            ViewBag.Cost_Center = db.HRMS_COST_CENTER;
            ViewBag.Designation = db.HRMS_DESG_MS.Where(rec => rec.IsActive == true);
            ViewBag.Department = db.HRMS_DEPT.Where(rec => rec.Parent_ID != null && rec.IsActive == true);
            ViewBag.salutation = db.HRMS_SALUTATION;
            ViewBag.Unit = db.UnitMaster;
            ViewBag.Work_Location = db.WorkLocationMaster;
            ViewBag.DivisionData = db.HRMS_DEPT.Where(rec => rec.Parent_ID == null && rec.IsActive == true);
           
            if (role == "admin")
            {
                ViewBag.Role = "admin";
            }
            return View(hRMS_Emp_Details);
        }

        [Authorize(Roles = "admin,emp")]
        public ActionResult Edit(long? id)
        {
            long emp_id = Convert.ToInt64(Session["id"]);
            string role = db.Accounts.Where(x => x.ID == emp_id).Select(x => x.role).FirstOrDefault();
            if (role == "admin")
            {
                ViewBag.Role = "admin";
            }

           
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //URL Attack
            if (role == "emp")
            {
                id = db.HRMS_Emp_Details.Where(x => x.EMP_ID == emp_id).Select(x => x.ID).FirstOrDefault();
            }


            HRMS_Emp_Details hRMS_Emp_Details = db.HRMS_Emp_Details.Find(id);
            if (hRMS_Emp_Details == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cost_Center = db.HRMS_COST_CENTER;
            ViewBag.Designation = db.HRMS_DESG_MS.Where(rec => rec.IsActive == true);
            ViewBag.Department = db.HRMS_DEPT.Where(rec => rec.Parent_ID != null && rec.IsActive == true);
            ViewBag.salutation = db.HRMS_SALUTATION;
            ViewBag.Unit = db.UnitMaster;
            ViewBag.Work_Location = db.WorkLocationMaster;
            ViewBag.DivisionData = db.HRMS_DEPT.Where(rec => rec.Parent_ID == null && rec.IsActive == true);
            return View(hRMS_Emp_Details);
        }


        [HttpPost]
        [Authorize(Roles = "admin,emp")]
        public ActionResult Edit(HRMS_Emp_Details hRMS_Emp_Details)
        {
            long emp_id = Convert.ToInt64(Session["id"]);
            string role = db.Accounts.Where(x => x.ID == emp_id).Select(x => x.role).FirstOrDefault();

            if (role == "emp")
            {
                ModelState.Remove("EMP_ID");
                hRMS_Emp_Details.EMP_ID = emp_id;
            }


            if (ModelState.IsValid)
            {
                //HRMS_Emp_Details AlreadyEmpCODE = db.HRMS_Emp_Details.Where(rec => rec.Emp_Cd == hRMS_Emp_Details.Emp_Cd).FirstOrDefault();
                //if (AlreadyEmpCODE == null)
                //{
                    HRMS_Emp_Details AlreadyCard_Id = db.HRMS_Emp_Details.Where(rec => rec.ID != hRMS_Emp_Details.ID && rec.EMP_ID == hRMS_Emp_Details.EMP_ID).FirstOrDefault();

                    if (AlreadyCard_Id == null)
                    {

                        db.Entry(hRMS_Emp_Details).State = EntityState.Modified;
                        db.SaveChanges();
                        ViewBag.EmpDetailStatus = "It is Updated successfully!";

                        if (role == "emp")
                        {
                            long id = db.HRMS_Emp_Details.Where(x => x.EMP_ID == emp_id).Select(x => x.ID).FirstOrDefault();
                            return RedirectToAction("Details", "EmployeeDetail", new { id });
                        }


                    ViewBag.Cost_Center = db.HRMS_COST_CENTER;
                        ViewBag.Designation = db.HRMS_DESG_MS.Where(rec => rec.IsActive == true);
                        ViewBag.Department = db.HRMS_DEPT.Where(rec => rec.Parent_ID != null && rec.IsActive == true);
                        ViewBag.salutation = db.HRMS_SALUTATION;
                        ViewBag.Unit = db.UnitMaster;
                        ViewBag.Work_Location = db.WorkLocationMaster;
                        ViewBag.DivisionData = db.HRMS_DEPT.Where(rec => rec.Parent_ID == null && rec.IsActive == true);
                   
                    if (role == "admin")
                    {
                        ViewBag.Role = "admin";
                    }
                    return View();
                    }
                    else
                    {
                        ViewBag.EmpDetailStatus = "The Employee ID is Already Exist for another employee!";

                        ViewBag.Cost_Center = db.HRMS_COST_CENTER;
                        ViewBag.Designation = db.HRMS_DESG_MS.Where(rec => rec.IsActive == true);
                        ViewBag.Department = db.HRMS_DEPT.Where(rec => rec.Parent_ID != null && rec.IsActive == true);
                        ViewBag.salutation = db.HRMS_SALUTATION;
                        ViewBag.Unit = db.UnitMaster;
                        ViewBag.Work_Location = db.WorkLocationMaster;
                        ViewBag.DivisionData = db.HRMS_DEPT.Where(rec => rec.Parent_ID == null && rec.IsActive == true);

                   
                    if (role == "admin")
                    {
                        ViewBag.Role = "admin";
                    }
                    return View();
                    }
                //}
                //else
                //{
                //    ViewBag.EmpDetailStatus = "The Employee Code is Already Exist for another employee!";
                //    ViewBag.Cost_Center = db.HRMS_COST_CENTER;
                //    ViewBag.Designation = db.HRMS_DESG_MS.Where(rec => rec.IsActive == true);
                //    ViewBag.Department = db.HRMS_DEPT.Where(rec => rec.Parent_ID != null && rec.IsActive == true);
                //    ViewBag.salutation = db.HRMS_SALUTATION;
                //    ViewBag.Unit = db.UnitMasters;
                //    ViewBag.Work_Location = db.WorkLocationMasters;
                //    ViewBag.DivisionData = db.HRMS_DEPT.Where(rec => rec.Parent_ID == null && rec.IsActive == true);
                //    return View();
                //}

            }

            ViewBag.Cost_Center = db.HRMS_COST_CENTER;
            ViewBag.Designation = db.HRMS_DESG_MS.Where(rec => rec.IsActive == true);
            ViewBag.Department = db.HRMS_DEPT.Where(rec => rec.Parent_ID != null && rec.IsActive == true);
            ViewBag.salutation = db.HRMS_SALUTATION;
            ViewBag.Unit = db.UnitMaster;
            ViewBag.Work_Location = db.WorkLocationMaster;
            ViewBag.DivisionData = db.HRMS_DEPT.Where(rec => rec.Parent_ID == null && rec.IsActive == true);
           
            if (role == "admin")
            {
                ViewBag.Role = "admin";
            }
            return View(hRMS_Emp_Details);
        }


        [HttpPost]
        [Authorize(Roles = "admin")]
        public bool Delete(long id)
        {
            HRMS_Emp_Details hRMS_Emp_Details = db.HRMS_Emp_Details.Find(id);
            if (hRMS_Emp_Details != null)
            {
                db.HRMS_Emp_Details.Remove(hRMS_Emp_Details);
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
