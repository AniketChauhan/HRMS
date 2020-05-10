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
    public class DepartmentController : Controller
    {
        
        private HRMSEntities db = new HRMSEntities();

        public ActionResult Index()
        {
            return View(db.DepartmentData().ToList());
        }

        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_DEPT hRMS_DEPT = db.HRMS_DEPT.Find(id);
            if (hRMS_DEPT == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_DEPT);
        }
        public ActionResult Create()
        {
            var dropdowndata = db.HRMS_DEPT.Where(rec => rec.Parent_ID == null && rec.IsActive == true);
            if(dropdowndata != null)
            {
                ViewBag.DropdownData = dropdowndata;
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Dept_Id,Dept_Name,Sort_Name,Parent_ID,IsActive,CreatedBy,CreateDate,ModifyBy,ModifyDate")] HRMS_DEPT hRMS_DEPT)
        {
            if (ModelState.IsValid)
            {
                if(hRMS_DEPT.Parent_ID == null)
                {
                    var AlreadyParent = db.HRMS_DEPT.FirstOrDefault(rec => rec.Dept_Name == hRMS_DEPT.Dept_Name);
                    if(AlreadyParent == null)
                    {
                        db.HRMS_DEPT.Add(hRMS_DEPT);
                        db.SaveChanges(); ModelState.Clear();

                        ViewBag.Department_Status = "Parent Department added successfully.";
                        var dropdowndata = db.HRMS_DEPT.Where(rec => rec.Parent_ID == null && rec.IsActive == true);
                        if (dropdowndata != null)
                        {
                            ViewBag.DropdownData = dropdowndata;
                        }

                        return View();
                            }
                    else
                    {

                        ViewBag.Department_Status = "This parent Department is already exist";
                        var dropdowndata = db.HRMS_DEPT.Where(rec => rec.Parent_ID == null && rec.IsActive == true);
                        if (dropdowndata != null)
                        {
                            ViewBag.DropdownData = dropdowndata;
                        }
                        return View();
                    }
                }
                else
                {
                    var alreadySubDept = db.HRMS_DEPT.FirstOrDefault(rec => rec.Dept_Name == hRMS_DEPT.Dept_Name && rec.Parent_ID == hRMS_DEPT.Parent_ID);
                    if(alreadySubDept == null)
                    {
                        var parentDept = db.HRMS_DEPT.FirstOrDefault(rec => rec.Dept_Id == hRMS_DEPT.Parent_ID);
                      if(hRMS_DEPT.Dept_Name == parentDept.Dept_Name)
                        {
                            ViewBag.Department_Status = "Parent Department and Sub Department can not have Same Names!";
                            var dropdowndatanew = db.HRMS_DEPT.Where(rec => rec.Parent_ID == null && rec.IsActive == true);
                            if (dropdowndatanew != null)
                            {
                                ViewBag.DropdownData = dropdowndatanew;
                            }
                            return View();
                        }
                        else
                        {

                            db.HRMS_DEPT.Add(hRMS_DEPT);
                            db.SaveChanges(); ModelState.Clear();
                            ViewBag.Department_Status = "Sub Department added successfully.";
                            var dropdowndata = db.HRMS_DEPT.Where(rec => rec.Parent_ID == null && rec.IsActive == true);
                            if (dropdowndata != null)
                            {
                                ViewBag.DropdownData = dropdowndata;
                            }
                            return View();
                        }
                        }
                    else
                    {
                        ViewBag.Department_Status = "This Sub Department is already exist!";
                        var dropdowndata = db.HRMS_DEPT.Where(rec => rec.Parent_ID == null && rec.IsActive == true);
                        if (dropdowndata != null)
                        {
                            ViewBag.DropdownData = dropdowndata;
                        }
                        return View();
                    }
                }
            }
            return View(hRMS_DEPT);
        }
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_DEPT department = db.HRMS_DEPT.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            var dropdowndata = db.HRMS_DEPT.Where(rec => rec.Parent_ID == null && rec.IsActive == true);
            if (dropdowndata != null)
            {
                ViewBag.DropdownData = dropdowndata;
            }
            return View(department);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Dept_Id,Dept_Name,Sort_Name,Parent_ID,IsActive,CreatedBy,CreateDate,ModifyBy,ModifyDate")] HRMS_DEPT hRMS_DEPT)
        {
            if (ModelState.IsValid)
            {
                if (hRMS_DEPT.Parent_ID == null)
                {
                    var AlreadyParent = db.HRMS_DEPT.FirstOrDefault(rec => rec.Dept_Name == hRMS_DEPT.Dept_Name && rec.Dept_Id != hRMS_DEPT.Dept_Id);
                    if (AlreadyParent == null)
                    {
                        db.Entry(hRMS_DEPT).State = EntityState.Modified;
                        db.SaveChanges(); ModelState.Clear();

                        ViewBag.Department_Status = "Parent Department Updated successfully.";
                        var dropdowndata = db.HRMS_DEPT.Where(rec => rec.Parent_ID == null && rec.IsActive == true);
                        if (dropdowndata != null)
                        {
                            ViewBag.DropdownData = dropdowndata;
                        }

                        return View();
                    }
                    else
                    {

                        ViewBag.Department_Status = "This parent Department is already exist";
                        var dropdowndata = db.HRMS_DEPT.Where(rec => rec.Parent_ID == null && rec.IsActive == true);
                        if (dropdowndata != null)
                        {
                            ViewBag.DropdownData = dropdowndata;
                        }
                        return View();
                    }
                }
                else
                {
                    var alreadySubDept = db.HRMS_DEPT.FirstOrDefault(rec => rec.Dept_Name == hRMS_DEPT.Dept_Name && rec.Parent_ID == hRMS_DEPT.Parent_ID && rec.Dept_Id != hRMS_DEPT.Dept_Id);
                    if (alreadySubDept == null)
                    {
                        var parentDept = db.HRMS_DEPT.FirstOrDefault(rec => rec.Dept_Id == hRMS_DEPT.Parent_ID);
                        if (hRMS_DEPT.Dept_Name == parentDept.Dept_Name)
                        {
                            ViewBag.Department_Status = "Parent Department and Sub Department can not have Same Names!";
                            var dropdowndatanew = db.HRMS_DEPT.Where(rec => rec.Parent_ID == null && rec.IsActive == true);
                            if (dropdowndatanew != null)
                            {
                                ViewBag.DropdownData = dropdowndatanew;
                            }
                            return View(hRMS_DEPT);
                        }
                        else
                        {

                            db.Entry(hRMS_DEPT).State = EntityState.Modified;
                            db.SaveChanges();ModelState.Clear();
                            ViewBag.Department_Status = "Sub Department added successfully.";
                            var dropdowndata = db.HRMS_DEPT.Where(rec => rec.Parent_ID == null && rec.IsActive == true);
                            if (dropdowndata != null)
                            {
                                ViewBag.DropdownData = dropdowndata;
                            }
                            return View(hRMS_DEPT);
                        }
                    }
                    else
                    {
                        ViewBag.Department_Status = "This Sub Department is already exist for this parent Department!";
                        var dropdowndata = db.HRMS_DEPT.Where(rec => rec.Parent_ID == null && rec.IsActive == true);
                        if (dropdowndata != null)
                        {
                            ViewBag.DropdownData = dropdowndata;
                        }
                        return View(hRMS_DEPT);
                    }
                }
            }
            return View(hRMS_DEPT);
        }
        public ActionResult Delete(long id)
        {
            HRMS_DEPT hRMS_DEPT = db.HRMS_DEPT.Find(id);
            if(hRMS_DEPT.Parent_ID == null) {
                IEnumerable<HRMS_DEPT> AllSUbDepts = db.HRMS_DEPT.Where(rec => rec.Parent_ID == hRMS_DEPT.Dept_Id).ToList();
                db.HRMS_DEPT.RemoveRange(AllSUbDepts);
                db.HRMS_DEPT.Remove(hRMS_DEPT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                db.HRMS_DEPT.Remove(hRMS_DEPT);
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("Index");

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
