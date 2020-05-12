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
        public ActionResult Details(long? id, string name)
        {
            long emp_id = Convert.ToInt64(Session["id"]);
            string role = db.Accounts.Where(x => x.ID == emp_id).Select(x => x.role).FirstOrDefault();
            if (role == "admin")
            {
                ViewBag.Role = "admin";

                if (name != null)
                {
                    bool isThere = db.HRMS_Emp_Details.Any(x => x.EMP_ID == id.Value);
                    if (!isThere)
                    {
                        return RedirectToAction("Create", "EmployeeDetail", new { ID = id.Value });
                    }
                    else
                    {
                        ViewBag.EditVisible = "No";
                        HRMS_Emp_Details employee_Personal_Detail = db.HRMS_Emp_Details.Where(x => x.EMP_ID == id.Value).FirstOrDefault();
                        return View(employee_Personal_Detail);
                    }

                }
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
        public ActionResult Create(long? ID)
        {
            HRMS_Emp_Details obj = new HRMS_Emp_Details();
            long emp_id = Convert.ToInt64(Session["id"]);
            string role = db.Accounts.Where(x => x.ID == emp_id).Select(x => x.role).FirstOrDefault();
            if (role == "admin")
            {
                
                ViewBag.Role = "admin";
                obj.EMP_ID = ID.Value;
                bool isExist = db.HRMS_Emp_Details.Any(x => x.EMP_ID == obj.EMP_ID);
                if (isExist)
                {
                    return RedirectToAction("Create", "EmployeePersonalDetail", new { ID=obj.EMP_ID });
                }

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

           // obj.Join_Date = ;
            return View(obj);
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
                           

                            db.HRMS_Emp_Details.Add(hRMS_Emp_Details);
                            db.SaveChanges();
                            ViewBag.EmpDetailStatus = "It is Created successfully!";
                            ModelState.Clear();
                        if (role == "emp")
                        {
                            long id = db.HRMS_Emp_Details.Where(x => x.EMP_ID == emp_id).Select(x => x.ID).FirstOrDefault();
                            return RedirectToAction("Details","EmployeeDetail",new {id});
                        }

                        //ViewBag.Cost_Center = db.HRMS_COST_CENTER;
                        //ViewBag.Designation = db.HRMS_DESG_MS.Where(rec => rec.IsActive == true);
                        //ViewBag.Department = db.HRMS_DEPT.Where(rec => rec.Parent_ID != null && rec.IsActive == true);
                        //ViewBag.salutation = db.HRMS_SALUTATION;
                        //ViewBag.Unit = db.UnitMaster;
                        //ViewBag.Work_Location = db.WorkLocationMaster;
                        //ViewBag.DivisionData = db.HRMS_DEPT.Where(rec => rec.Parent_ID == null && rec.IsActive == true);
                        
                        if (role == "admin")
                        {
                            ViewBag.Role = "admin";
                            
                            return RedirectToAction("Create", "EmployeePersonalDetail", new { ID=hRMS_Emp_Details.EMP_ID });

                        }
                        



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
                        return View(hRMS_Emp_Details);
                        }
                    
                    
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
                    return View(hRMS_Emp_Details);
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
                    return View(hRMS_Emp_Details);
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
                    return View(hRMS_Emp_Details);
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
