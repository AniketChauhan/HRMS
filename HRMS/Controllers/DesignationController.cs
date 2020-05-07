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
    public class DesignationController : Controller
    {
        private HRMSEntities db = new HRMSEntities();

        // GET: HRMS_DESG_MS
        public ActionResult Index()
        {
            return View(db.HRMS_DESG_MS.ToList());
        }

        // GET: HRMS_DESG_MS/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_DESG_MS hRMS_DESG_MS = db.HRMS_DESG_MS.Find(id);
            if (hRMS_DESG_MS == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_DESG_MS);
        }

        // GET: HRMS_DESG_MS/Create
        public ActionResult Create()
        {
            var dropdowndata = db.HRMS_DESG_MS.Where(rec => rec.Desg_Parent_ID == null && rec.IsActive == true);
            if (dropdowndata != null)
            {
                ViewBag.DropdownData = dropdowndata;
            }
            return View();
        }

        // POST: HRMS_DESG_MS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Desg_Id,Desg_Name,Desg_Short_Name,Desg_Parent_ID,IsActive,CreatedBy,CreateDate,ModifyBy,ModifyDate")] HRMS_DESG_MS hRMS_DESG_MS)
        {
            if (ModelState.IsValid)
            {
                //db.HRMS_DESG_MS.Add(hRMS_DESG_MS);
                //db.SaveChanges();
                //return RedirectToAction("Index");

                //*********************************************
                if (hRMS_DESG_MS.Desg_Parent_ID == null)
                {
                    var AlreadyParent = db.HRMS_DESG_MS.FirstOrDefault(rec => rec.Desg_Name == hRMS_DESG_MS.Desg_Name);
                    if (AlreadyParent == null)
                    {
                        db.HRMS_DESG_MS.Add(hRMS_DESG_MS);
                        db.SaveChanges();
                        ViewBag.Designation_Status = "Parent Designation added successfully.";
                        var dropdowndata = db.HRMS_DESG_MS.Where(rec => rec.Desg_Parent_ID == null && rec.IsActive == true);
                        if (dropdowndata != null)
                        {
                            ViewBag.DropdownData = dropdowndata;
                        }

                        return View();
                    }
                    else
                    {

                        ViewBag.Designation_Status = "This parent Designation is already exist";
                        var dropdowndata = db.HRMS_DESG_MS.Where(rec => rec.Desg_Parent_ID == null && rec.IsActive == true);
                        if (dropdowndata != null)
                        {
                            ViewBag.DropdownData = dropdowndata;
                        }
                        return View();
                    }
                }
                else
                {
                    var alreadySubDesg = db.HRMS_DESG_MS.FirstOrDefault(rec => rec.Desg_Name == hRMS_DESG_MS.Desg_Name && rec.Desg_Parent_ID == hRMS_DESG_MS.Desg_Parent_ID);
                    if (alreadySubDesg == null)
                    {
                        var parentDesg = db.HRMS_DESG_MS.FirstOrDefault(rec => rec.Desg_Id == hRMS_DESG_MS.Desg_Parent_ID);
                        if (hRMS_DESG_MS.Desg_Name == parentDesg.Desg_Name)
                        {
                            ViewBag.Designation_Status = "Parent Designation and Sub Designation can not have Same Names!";
                            var dropdowndatanew = db.HRMS_DESG_MS.Where(rec => rec.Desg_Parent_ID == null && rec.IsActive == true);
                            if (dropdowndatanew != null)
                            {
                                ViewBag.DropdownData = dropdowndatanew;
                            }
                            return View();
                        }
                        else
                        {

                            db.HRMS_DESG_MS.Add(hRMS_DESG_MS);
                            db.SaveChanges();
                            ViewBag.Designation_Status = "Sub Designation added successfully.";
                            var dropdowndata = db.HRMS_DESG_MS.Where(rec => rec.Desg_Parent_ID == null && rec.IsActive == true);
                            if (dropdowndata != null)
                            {
                                ViewBag.DropdownData = dropdowndata;
                            }
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.Designation_Status = "This Sub Designation is already exist!";
                        var dropdowndata = db.HRMS_DESG_MS.Where(rec => rec.Desg_Parent_ID == null && rec.IsActive == true);
                        if (dropdowndata != null)
                        {
                            ViewBag.DropdownData = dropdowndata;
                        }
                        return View();
                    }
                }


            }

            return View(hRMS_DESG_MS);
        }


        public ActionResult Edit_Parent(long? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_DESG_MS hRMS_DESG_MS = db.HRMS_DESG_MS.Find(id);
            if (hRMS_DESG_MS == null)
            {
                return HttpNotFound();
            }
            var dropdowndata = db.HRMS_DESG_MS.Where(rec => rec.Desg_Parent_ID == null && rec.IsActive == true);
            if (dropdowndata != null)
            {
                ViewBag.DropdownData = dropdowndata;
            }
            return View(hRMS_DESG_MS);
        }

        // POST: HRMS_DESG_MS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_Parent([Bind(Include = "Desg_Id,Desg_Name,Desg_Short_Name,Desg_Parent_ID,IsActive,CreatedBy,CreateDate,ModifyBy,ModifyDate")] HRMS_DESG_MS hRMS_DESG_MS)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(hRMS_DESG_MS).State = EntityState.Modified;
                //db.SaveChanges();
                //return RedirectToAction("Index");
                // ***********************************************

                if (hRMS_DESG_MS.Desg_Parent_ID == null)
                {
                    var AlreadyParent = db.HRMS_DESG_MS.FirstOrDefault(rec => rec.Desg_Name == hRMS_DESG_MS.Desg_Name);
                    if (AlreadyParent == null)
                    {
                        db.Entry(hRMS_DESG_MS).State = EntityState.Modified;

                        db.SaveChanges();
                        ViewBag.Designation_Status = "Parent Designation added successfully.";
                        var dropdowndata = db.HRMS_DESG_MS.Where(rec => rec.Desg_Parent_ID == null && rec.IsActive == true);
                        if (dropdowndata != null)
                        {
                            ViewBag.DropdownData = dropdowndata;
                        }

                        return View();
                    }
                    else
                    {

                        ViewBag.Designation_Status = "This parent Designation is already exist";

                        var dropdowndata = db.HRMS_DESG_MS.Where(rec => rec.Desg_Parent_ID == null && rec.IsActive == true);
                        if (dropdowndata != null)
                        {
                            ViewBag.DropdownData = dropdowndata;
                        }
                        return View();
                    }
                }
                else
                {
                    var alreadySubDesg = db.HRMS_DESG_MS.FirstOrDefault(rec => rec.Desg_Name == hRMS_DESG_MS.Desg_Name && rec.Desg_Parent_ID == hRMS_DESG_MS.Desg_Parent_ID);
                    if (alreadySubDesg == null)
                    {
                        var parentDesg = db.HRMS_DESG_MS.FirstOrDefault(rec => rec.Desg_Id == hRMS_DESG_MS.Desg_Parent_ID);
                        if (hRMS_DESG_MS.Desg_Name == parentDesg.Desg_Name)
                        {
                            ViewBag.Designation_Status = "Parent Designation and Sub Designation can not have Same Names!";
                            var dropdowndatanew = db.HRMS_DESG_MS.Where(rec => rec.Desg_Parent_ID == null && rec.IsActive == true);
                            if (dropdowndatanew != null)
                            {
                                ViewBag.DropdownData = dropdowndatanew;
                            }
                            return View();
                        }
                        else
                        {
                            db.Entry(hRMS_DESG_MS).State = EntityState.Modified;

                            db.SaveChanges();
                            ViewBag.Designation_Status = "Sub Designation added successfully.";
                            var dropdowndata = db.HRMS_DESG_MS.Where(rec => rec.Desg_Parent_ID == null && rec.IsActive == true);
                            if (dropdowndata != null)
                            {
                                ViewBag.DropdownData = dropdowndata;
                            }
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.Designation_Status = "This Sub Designation is already exist!";
                        var dropdowndata = db.HRMS_DESG_MS.Where(rec => rec.Desg_Parent_ID == null && rec.IsActive == true);
                        if (dropdowndata != null)
                        {
                            ViewBag.DropdownData = dropdowndata;
                        }
                        return View();
                    }
                }



            }
            return View(hRMS_DESG_MS);
        }



        // GET: HRMS_DESG_MS/Edit/5
        public ActionResult Edit(long? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_DESG_MS hRMS_DESG_MS = db.HRMS_DESG_MS.Find(id);
            if (hRMS_DESG_MS == null)
            {
                return HttpNotFound();
            }
            var dropdowndata = db.HRMS_DESG_MS.Where(rec => rec.Desg_Parent_ID == null && rec.IsActive == true);
            if (dropdowndata != null)
            {
                ViewBag.DropdownData = dropdowndata;
            }
            return View(hRMS_DESG_MS);
        }

        // POST: HRMS_DESG_MS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Desg_Id,Desg_Name,Desg_Short_Name,Desg_Parent_ID,IsActive,CreatedBy,CreateDate,ModifyBy,ModifyDate")] HRMS_DESG_MS hRMS_DESG_MS)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(hRMS_DESG_MS).State = EntityState.Modified;
                //db.SaveChanges();
                //return RedirectToAction("Index");
                // ***********************************************

                if (hRMS_DESG_MS.Desg_Parent_ID == null)
                {
                    var AlreadyParent = db.HRMS_DESG_MS.FirstOrDefault(rec => rec.Desg_Name == hRMS_DESG_MS.Desg_Name);
                    if (AlreadyParent == null)
                    {
                        db.Entry(hRMS_DESG_MS).State = EntityState.Modified;

                        db.SaveChanges();
                        ViewBag.Designation_Status = "Parent Designation added successfully.";
                        var dropdowndata = db.HRMS_DESG_MS.Where(rec => rec.Desg_Parent_ID == null && rec.IsActive == true);
                        if (dropdowndata != null)
                        {
                            ViewBag.DropdownData = dropdowndata;
                        }

                        return View();
                    }
                    else
                    {

                        ViewBag.Designation_Status = "This parent Designation is already exist";

                        var dropdowndata = db.HRMS_DESG_MS.Where(rec => rec.Desg_Parent_ID == null && rec.IsActive == true);
                        if (dropdowndata != null)
                        {
                            ViewBag.DropdownData = dropdowndata;
                        }
                        return View();
                    }
                }
                else
                {
                    var alreadySubDesg = db.HRMS_DESG_MS.FirstOrDefault(rec => rec.Desg_Name == hRMS_DESG_MS.Desg_Name && rec.Desg_Parent_ID == hRMS_DESG_MS.Desg_Parent_ID);
                    if (alreadySubDesg == null)
                    {
                        var parentDesg = db.HRMS_DESG_MS.FirstOrDefault(rec => rec.Desg_Id == hRMS_DESG_MS.Desg_Parent_ID);
                        if (hRMS_DESG_MS.Desg_Name == parentDesg.Desg_Name)
                        {
                            ViewBag.Designation_Status = "Parent Designation and Sub Designation can not have Same Names!";
                            var dropdowndatanew = db.HRMS_DESG_MS.Where(rec => rec.Desg_Parent_ID == null && rec.IsActive == true);
                            if (dropdowndatanew != null)
                            {
                                ViewBag.DropdownData = dropdowndatanew;
                            }
                            return View();
                        }
                        else
                        {
                            db.Entry(hRMS_DESG_MS).State = EntityState.Modified;

                            db.SaveChanges();
                            ViewBag.Designation_Status = "Sub Designation added successfully.";
                            var dropdowndata = db.HRMS_DESG_MS.Where(rec => rec.Desg_Parent_ID == null && rec.IsActive == true);
                            if (dropdowndata != null)
                            {
                                ViewBag.DropdownData = dropdowndata;
                            }
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.Designation_Status = "This Sub Designation is already exist!";
                        var dropdowndata = db.HRMS_DESG_MS.Where(rec => rec.Desg_Parent_ID == null && rec.IsActive == true);
                        if (dropdowndata != null)
                        {
                            ViewBag.DropdownData = dropdowndata;
                        }
                        return View();
                    }
                }



            }
            return View(hRMS_DESG_MS);
        }

        public ActionResult Delete_Parent(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_DESG_MS hRMS_DESG_MS = db.HRMS_DESG_MS.Find(id);
            if (hRMS_DESG_MS == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_DESG_MS);
        }
        [HttpPost, ActionName("Delete_Parent")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed1(long id)
        {
            HRMS_DESG_MS hRMS_DESG_MS = db.HRMS_DESG_MS.Find(id);
            hRMS_DESG_MS.IsActive = false;
            //db.HRMS_DESG_MS.Remove(hRMS_DESG_MS);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        // GET: HRMS_DESG_MS/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_DESG_MS hRMS_DESG_MS = db.HRMS_DESG_MS.Find(id);
            if (hRMS_DESG_MS == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_DESG_MS);
        }

        // POST: HRMS_DESG_MS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            HRMS_DESG_MS hRMS_DESG_MS = db.HRMS_DESG_MS.Find(id);

            db.HRMS_DESG_MS.Remove(hRMS_DESG_MS);
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
