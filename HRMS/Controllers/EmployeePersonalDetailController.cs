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
    public class EmployeePersonalDetailController : Controller
    {   
        private HRMSEntities db = new HRMSEntities();

        public JsonResult GetCastList(int ReligionID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<CastMaster> CastList = db.CastMaster.Where(x => x.ReligionID == ReligionID).ToList();
            return Json(CastList, JsonRequestBehavior.AllowGet);
        }

        // GET: EmployeePersonalDetail
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            var employee_Personal_Detail = db.Employee_Personal_Detail.Include(e => e.CastMaster).Include(e => e.HRMS_CATEGORY_GRADE).Include(e => e.HRMS_EMP_CITIZENSHIP_MS).Include(e => e.HRMS_EMP_GENDER_MS).Include(e => e.MaritalMaster).Include(e => e.ReligionMaster);
            return View(employee_Personal_Detail.ToList());
        }

        // GET: EmployeePersonalDetail/Details/5
        [Authorize(Roles = "admin,emp")]
        public ActionResult Details(long? id)
        {
            long emp_id = Convert.ToInt64(Session["id"]);
            string role = db.Accounts.Where(x => x.ID == emp_id).Select(x => x.role).FirstOrDefault();
            if (role == "admin")
            {
                ViewBag.Role = "admin";
            }

            bool isExist = db.Employee_Personal_Detail.Any(x => x.EMP_ID == emp_id);
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
                    id = db.Employee_Personal_Detail.Where(x => x.EMP_ID == emp_id).Select(x => x.ID).FirstOrDefault();
                }
                Employee_Personal_Detail employee_Personal_Detail = db.Employee_Personal_Detail.Find(id);
                if (employee_Personal_Detail == null)
                {
                    return HttpNotFound();
                }
                return View(employee_Personal_Detail);
            }
            }

        // GET: EmployeePersonalDetail/Create
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
                bool isExist = db.Employee_Personal_Detail.Any(x => x.EMP_ID == emp_id);
                if (isExist)
                {
                    long id = db.Employee_Personal_Detail.Where(x => x.EMP_ID == emp_id).Select(x => x.ID).FirstOrDefault();
                    return RedirectToAction("Details", "EmployeePersonalDetail", new { id });
                }
            }

            //ViewBag.Caste = new SelectList(db.CastMasters, "CastCode", "CastName");
            ViewBag.Caste = new SelectList("");
            ViewBag.Category = new SelectList(db.HRMS_CATEGORY_GRADE, "Category_ID", "Category_Name");
            ViewBag.Citizenship = new SelectList(db.HRMS_EMP_CITIZENSHIP_MS, "CitizenShip_ID", "CitizenShip_Country_NM");
            ViewBag.Gender = new SelectList(db.HRMS_EMP_GENDER_MS, "Gender_ID", "Gender_Value");
            ViewBag.MarraigeStatus = new SelectList(db.MaritalMaster, "MaritalID", "MaritalName");
            ViewBag.Religion = new SelectList(db.ReligionMaster, "ReligionID", "ReligionName");
            return View();
        }

        // POST: EmployeePersonalDetail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,emp")]
        public ActionResult Create([Bind(Include = "ID,Gender,DOB,Category,IdentityMark1,IdentityMark2,Religion,Citizenship,Caste,Race,MarraigeStatus,MarraigeDate,NoOfChild,NoOfDependents,AadharNo,SIN,AKA,MilitaryService,BirthCity,Note,Hobbies,MilitaryServiceDetail,EMP_ID")] Employee_Personal_Detail employee_Personal_Detail)
        {
            

            long emp_id = Convert.ToInt64(Session["id"]);
            string role = db.Accounts.Where(x => x.ID == emp_id).Select(x => x.role).FirstOrDefault();

            if (role == "emp")
            {
                ModelState.Remove("EMP_ID");
                employee_Personal_Detail.EMP_ID = emp_id;
            }

            if (ModelState.IsValid)
            {
                bool isValid = db.Employee_Personal_Detail.Any(x => x.EMP_ID == employee_Personal_Detail.EMP_ID);
                if (!isValid)
                {

                    db.Employee_Personal_Detail.Add(employee_Personal_Detail);
                    db.SaveChanges();
                    ViewBag.success = "Your Personal Details is Successfully Added!";
                    ModelState.Clear();

                    if (role == "emp")
                    {
                        long id = db.Employee_Personal_Detail.Where(x => x.EMP_ID == emp_id).Select(x => x.ID).FirstOrDefault();
                        return RedirectToAction("Details", "EmployeePersonalDetail", new { id });
                    }

                    ViewBag.Caste = new SelectList("");
                    ViewBag.Category = new SelectList(db.HRMS_CATEGORY_GRADE, "Category_ID", "Category_Name");
                    ViewBag.Citizenship = new SelectList(db.HRMS_EMP_CITIZENSHIP_MS, "CitizenShip_ID", "CitizenShip_Country_NM");
                    ViewBag.Gender = new SelectList(db.HRMS_EMP_GENDER_MS, "Gender_ID", "Gender_Value");
                    ViewBag.MarraigeStatus = new SelectList(db.MaritalMaster, "MaritalID", "MaritalName");
                    ViewBag.Religion = new SelectList(db.ReligionMaster, "ReligionID", "ReligionName");

                    if (role == "admin")
                    {
                        ViewBag.Role = "admin";
                    }
                    return View();
                }
                else
                {
                    ViewBag.error = "Sorry! This record is already exist!";
                    ViewBag.Caste = new SelectList(db.CastMaster.Where(x => x.ReligionID == employee_Personal_Detail.Religion), "CastCode", "CastName");
                    ViewBag.Category = new SelectList(db.HRMS_CATEGORY_GRADE, "Category_ID", "Category_Name");
                    ViewBag.Citizenship = new SelectList(db.HRMS_EMP_CITIZENSHIP_MS, "CitizenShip_ID", "CitizenShip_Country_NM");
                    ViewBag.Gender = new SelectList(db.HRMS_EMP_GENDER_MS, "Gender_ID", "Gender_Value");
                    ViewBag.MarraigeStatus = new SelectList(db.MaritalMaster, "MaritalID", "MaritalName");
                    ViewBag.Religion = new SelectList(db.ReligionMaster, "ReligionID", "ReligionName");

                    if (role == "admin")
                    {
                        ViewBag.Role = "admin";
                    }
                    return View(employee_Personal_Detail);
                }
            }

            ViewBag.Caste = new SelectList("");
            ViewBag.Category = new SelectList(db.HRMS_CATEGORY_GRADE, "Category_ID", "Category_Name");
            ViewBag.Citizenship = new SelectList(db.HRMS_EMP_CITIZENSHIP_MS, "CitizenShip_ID", "CitizenShip_Country_NM");
            ViewBag.Gender = new SelectList(db.HRMS_EMP_GENDER_MS, "Gender_ID", "Gender_Value");
            ViewBag.MarraigeStatus = new SelectList(db.MaritalMaster, "MaritalID", "MaritalName");
            ViewBag.Religion = new SelectList(db.ReligionMaster, "ReligionID", "ReligionName");

            if (role == "admin")
            {
                ViewBag.Role = "admin";
            }
            return View(employee_Personal_Detail);
        }

        // GET: EmployeePersonalDetail/Edit/5
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
                id = db.Employee_Personal_Detail.Where(x => x.EMP_ID == emp_id).Select(x => x.ID).FirstOrDefault();
            }

            Employee_Personal_Detail employee_Personal_Detail = db.Employee_Personal_Detail.Find(id);
            if (employee_Personal_Detail == null)
            {
                return HttpNotFound();
            }
            // ViewBag.Caste = new SelectList(db.CastMasters, "CastCode", "CastName", employee_Personal_Detail.Caste);
            ViewBag.Caste = new SelectList(db.CastMaster.Where(x => x.ReligionID == employee_Personal_Detail.Religion), "CastCode", "CastName");

            ViewBag.Category = new SelectList(db.HRMS_CATEGORY_GRADE, "Category_ID", "Category_Name", employee_Personal_Detail.Category);
            ViewBag.Citizenship = new SelectList(db.HRMS_EMP_CITIZENSHIP_MS, "CitizenShip_ID", "CitizenShip_Country_NM", employee_Personal_Detail.Citizenship);
            ViewBag.Gender = new SelectList(db.HRMS_EMP_GENDER_MS, "Gender_ID", "Gender_Value", employee_Personal_Detail.Gender);
            ViewBag.MarraigeStatus = new SelectList(db.MaritalMaster, "MaritalID", "MaritalName", employee_Personal_Detail.MarraigeStatus);
            ViewBag.Religion = new SelectList(db.ReligionMaster, "ReligionID", "ReligionName", employee_Personal_Detail.Religion);
            return View(employee_Personal_Detail);
        }

        // POST: EmployeePersonalDetail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,emp")]
        public ActionResult Edit([Bind(Include = "ID,Gender,DOB,Category,IdentityMark1,IdentityMark2,Religion,Citizenship,Caste,Race,MarraigeStatus,MarraigeDate,NoOfChild,NoOfDependents,AadharNo,SIN,AKA,MilitaryService,BirthCity,Note,Hobbies,MilitaryServiceDetail,EMP_ID")] Employee_Personal_Detail employee_Personal_Detail)
        {  //removed EMP_ID from model bind because its only ReadOnly

            long emp_id = Convert.ToInt64(Session["id"]);
            string role = db.Accounts.Where(x => x.ID == emp_id).Select(x => x.role).FirstOrDefault();

            if (role == "emp")
            {
                ModelState.Remove("EMP_ID");
                employee_Personal_Detail.EMP_ID = emp_id;
            }

            if (ModelState.IsValid)
            {
                db.Entry(employee_Personal_Detail).State = EntityState.Modified;
                db.SaveChanges();

                if (role == "emp")
                {
                    long id = db.Employee_Personal_Detail.Where(x => x.EMP_ID == emp_id).Select(x => x.ID).FirstOrDefault();
                    return RedirectToAction("Details", "EmployeePersonalDetail", new { id });
                }

                ViewBag.success = "Your Record Successfully Updated!";
                ViewBag.Caste = new SelectList(db.CastMaster.Where(x => x.ReligionID == employee_Personal_Detail.Religion), "CastCode", "CastName");
                ViewBag.Category = new SelectList(db.HRMS_CATEGORY_GRADE, "Category_ID", "Category_Name", employee_Personal_Detail.Category);
                ViewBag.Citizenship = new SelectList(db.HRMS_EMP_CITIZENSHIP_MS, "CitizenShip_ID", "CitizenShip_Country_NM", employee_Personal_Detail.Citizenship);
                ViewBag.Gender = new SelectList(db.HRMS_EMP_GENDER_MS, "Gender_ID", "Gender_Value", employee_Personal_Detail.Gender);
                ViewBag.MarraigeStatus = new SelectList(db.MaritalMaster, "MaritalID", "MaritalName", employee_Personal_Detail.MarraigeStatus);
                ViewBag.Religion = new SelectList(db.ReligionMaster, "ReligionID", "ReligionName", employee_Personal_Detail.Religion);

                if (role == "admin")
                {
                    ViewBag.Role = "admin";
                }
                return View();
            }
            ViewBag.Caste = new SelectList(db.CastMaster.Where(x => x.ReligionID == employee_Personal_Detail.Religion), "CastCode", "CastName");
            ViewBag.Category = new SelectList(db.HRMS_CATEGORY_GRADE, "Category_ID", "Category_Name", employee_Personal_Detail.Category);
            ViewBag.Citizenship = new SelectList(db.HRMS_EMP_CITIZENSHIP_MS, "CitizenShip_ID", "CitizenShip_Country_NM", employee_Personal_Detail.Citizenship);
            ViewBag.Gender = new SelectList(db.HRMS_EMP_GENDER_MS, "Gender_ID", "Gender_Value", employee_Personal_Detail.Gender);
            ViewBag.MarraigeStatus = new SelectList(db.MaritalMaster, "MaritalID", "MaritalName", employee_Personal_Detail.MarraigeStatus);
            ViewBag.Religion = new SelectList(db.ReligionMaster, "ReligionID", "ReligionName", employee_Personal_Detail.Religion);

            if (role == "admin")
            {
                ViewBag.Role = "admin";
            }
            return View(employee_Personal_Detail);
        }

        [Authorize(Roles = "admin")]
        public bool Delete(long id)
        {
            Employee_Personal_Detail employee_Personal_Detail = db.Employee_Personal_Detail.Find(id);
            if (employee_Personal_Detail != null)
            {
                db.Employee_Personal_Detail.Remove(employee_Personal_Detail);
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
