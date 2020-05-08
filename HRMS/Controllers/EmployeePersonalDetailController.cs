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
        public ActionResult Index()
        {
            var employee_Personal_Detail = db.Employee_Personal_Detail.Include(e => e.CastMaster).Include(e => e.HRMS_CATEGORY_GRADE).Include(e => e.HRMS_EMP_CITIZENSHIP_MS).Include(e => e.HRMS_EMP_GENDER_MS).Include(e => e.MaritalMaster).Include(e => e.ReligionMaster);
            return View(employee_Personal_Detail.ToList());
        }

        // GET: EmployeePersonalDetail/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee_Personal_Detail employee_Personal_Detail = db.Employee_Personal_Detail.Find(id);
            if (employee_Personal_Detail == null)
            {
                return HttpNotFound();
            }
            return View(employee_Personal_Detail);
        }

        // GET: EmployeePersonalDetail/Create
        public ActionResult Create()
        {
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
        public ActionResult Create([Bind(Include = "ID,Gender,DOB,Category,IdentityMark1,IdentityMark2,Religion,Citizenship,Caste,Race,MarraigeStatus,MarraigeDate,NoOfChild,NoOfDependents,AadharNo,SIN,AKA,MilitaryService,BirthCity,Note,Hobbies,MilitaryServiceDetail,EMP_ID")] Employee_Personal_Detail employee_Personal_Detail)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Employee_Personal_Detail.Add(employee_Personal_Detail);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //ViewBag.Caste = new SelectList(db.CastMasters, "CastCode", "CastName", employee_Personal_Detail.Caste);
            //ViewBag.Category = new SelectList(db.HRMS_CATEGORY_GRADE, "Category_ID", "Category_Name", employee_Personal_Detail.Category);
            //ViewBag.Citizenship = new SelectList(db.HRMS_EMP_CITIZENSHIP_MS, "CitizenShip_ID", "CitizenShip_Country_NM", employee_Personal_Detail.Citizenship);
            //ViewBag.Gender = new SelectList(db.HRMS_EMP_GENDER_MS, "Gender_ID", "Gender_Value", employee_Personal_Detail.Gender);
            //ViewBag.MarraigeStatus = new SelectList(db.MaritalMasters, "MaritalID", "MaritalName", employee_Personal_Detail.MarraigeStatus);
            //ViewBag.Religion = new SelectList(db.ReligionMasters, "ReligionID", "ReligionShortName", employee_Personal_Detail.Religion);
            //return View(employee_Personal_Detail);

            if (ModelState.IsValid)
            {
                bool isValid = db.Employee_Personal_Detail.Any(x => x.EMP_ID == employee_Personal_Detail.EMP_ID);
                if (!isValid)
                {

                    db.Employee_Personal_Detail.Add(employee_Personal_Detail);
                    db.SaveChanges();
                    ViewBag.success = "Your Personal Details is Successfully Added!";
                    ModelState.Clear();
                    ViewBag.Caste = new SelectList("");
                    ViewBag.Category = new SelectList(db.HRMS_CATEGORY_GRADE, "Category_ID", "Category_Name");
                    ViewBag.Citizenship = new SelectList(db.HRMS_EMP_CITIZENSHIP_MS, "CitizenShip_ID", "CitizenShip_Country_NM");
                    ViewBag.Gender = new SelectList(db.HRMS_EMP_GENDER_MS, "Gender_ID", "Gender_Value");
                    ViewBag.MarraigeStatus = new SelectList(db.MaritalMaster, "MaritalID", "MaritalName");
                    ViewBag.Religion = new SelectList(db.ReligionMaster, "ReligionID", "ReligionName");
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
                    return View(employee_Personal_Detail);
                }
            }

            ViewBag.Caste = new SelectList("");
            ViewBag.Category = new SelectList(db.HRMS_CATEGORY_GRADE, "Category_ID", "Category_Name");
            ViewBag.Citizenship = new SelectList(db.HRMS_EMP_CITIZENSHIP_MS, "CitizenShip_ID", "CitizenShip_Country_NM");
            ViewBag.Gender = new SelectList(db.HRMS_EMP_GENDER_MS, "Gender_ID", "Gender_Value");
            ViewBag.MarraigeStatus = new SelectList(db.MaritalMaster, "MaritalID", "MaritalName");
            ViewBag.Religion = new SelectList(db.ReligionMaster, "ReligionID", "ReligionName");
            return View(employee_Personal_Detail);
        }

        // GET: EmployeePersonalDetail/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
            ViewBag.Religion = new SelectList(db.ReligionMaster, "ReligionID", "ReligionShortName", employee_Personal_Detail.Religion);
            return View(employee_Personal_Detail);
        }

        // POST: EmployeePersonalDetail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Gender,DOB,Category,IdentityMark1,IdentityMark2,Religion,Citizenship,Caste,Race,MarraigeStatus,MarraigeDate,NoOfChild,NoOfDependents,AadharNo,SIN,AKA,MilitaryService,BirthCity,Note,Hobbies,MilitaryServiceDetail,EMP_ID")] Employee_Personal_Detail employee_Personal_Detail)
        {  //removed EMP_ID from model bind because its only ReadOnly
            if (ModelState.IsValid)
            {
                db.Entry(employee_Personal_Detail).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.success = "Your Record Successfully Updated!";
                ViewBag.Caste = new SelectList(db.CastMaster.Where(x => x.ReligionID == employee_Personal_Detail.Religion), "CastCode", "CastName");
                ViewBag.Category = new SelectList(db.HRMS_CATEGORY_GRADE, "Category_ID", "Category_Name", employee_Personal_Detail.Category);
                ViewBag.Citizenship = new SelectList(db.HRMS_EMP_CITIZENSHIP_MS, "CitizenShip_ID", "CitizenShip_Country_NM", employee_Personal_Detail.Citizenship);
                ViewBag.Gender = new SelectList(db.HRMS_EMP_GENDER_MS, "Gender_ID", "Gender_Value", employee_Personal_Detail.Gender);
                ViewBag.MarraigeStatus = new SelectList(db.MaritalMaster, "MaritalID", "MaritalName", employee_Personal_Detail.MarraigeStatus);
                ViewBag.Religion = new SelectList(db.ReligionMaster, "ReligionID", "ReligionShortName", employee_Personal_Detail.Religion);
                return View();
            }
            ViewBag.Caste = new SelectList(db.CastMaster.Where(x => x.ReligionID == employee_Personal_Detail.Religion), "CastCode", "CastName");
            ViewBag.Category = new SelectList(db.HRMS_CATEGORY_GRADE, "Category_ID", "Category_Name", employee_Personal_Detail.Category);
            ViewBag.Citizenship = new SelectList(db.HRMS_EMP_CITIZENSHIP_MS, "CitizenShip_ID", "CitizenShip_Country_NM", employee_Personal_Detail.Citizenship);
            ViewBag.Gender = new SelectList(db.HRMS_EMP_GENDER_MS, "Gender_ID", "Gender_Value", employee_Personal_Detail.Gender);
            ViewBag.MarraigeStatus = new SelectList(db.MaritalMaster, "MaritalID", "MaritalName", employee_Personal_Detail.MarraigeStatus);
            ViewBag.Religion = new SelectList(db.ReligionMaster, "ReligionID", "ReligionShortName", employee_Personal_Detail.Religion);
            return View(employee_Personal_Detail);
        }

    
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
