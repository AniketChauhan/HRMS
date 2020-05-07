﻿using System;
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
    public class EmployeeContactDetailsController : Controller
    {
        private HRMSEntities db = new HRMSEntities();

        // GET: EmployeeContactDetails
        public ActionResult Index()
        {
            var hRMS_Contact = db.HRMS_Contact.Include(h => h.Accounts);
            return View(hRMS_Contact.ToList());
        }

        // GET: EmployeeContactDetails/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_Contact hRMS_Contact = db.HRMS_Contact.Find(id);
            if (hRMS_Contact == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_Contact);
        }

        // GET: EmployeeContactDetails/Create
        public ActionResult Create()
        {
            ViewBag.Employee_ID = db.HRMS_Emp_Details;
            return View();
        }


        [HttpPost]

        public ActionResult Create(HRMS_Contact hRMS_Contact)
        {
            if (ModelState.IsValid)
            {
                HRMS_Contact employeeExist = db.HRMS_Contact.Where(rec => rec.Employee_ID == hRMS_Contact.Employee_ID).FirstOrDefault();
                if (employeeExist == null)
                {
                    HRMS_Contact CorporateEmailExist = db.HRMS_Contact.Where(rec => rec.Corporate_Email == hRMS_Contact.Corporate_Email).FirstOrDefault();

                    if (CorporateEmailExist == null)
                    {
                        HRMS_Contact PhoneWorkNoExist = db.HRMS_Contact.Where(rec => rec.Phone_Work == hRMS_Contact.Phone_Work).FirstOrDefault();
                        if (PhoneWorkNoExist == null)
                        {
                            db.HRMS_Contact.Add(hRMS_Contact);
                            db.SaveChanges();
                            ViewBag.ContactStatus = "Employee Contact detail is added successfully";
                            ViewBag.Employee_ID = db.HRMS_Emp_Details;

                            return View();
                        }
                        else
                        {
                            ViewBag.ContactStatus = "Phone Work number is already exist for another employee";
                            ViewBag.Employee_ID = db.HRMS_Emp_Details;

                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.ContactStatus = "Corporate Email address is already exist for another employee";
                        ViewBag.Employee_ID = db.HRMS_Emp_Details;

                        return View();
                    }
                }
                else
                {
                    ViewBag.ContactStatus = "Employee Contact details is already exist!";
                    ViewBag.Employee_ID = db.HRMS_Emp_Details;

                    return View();
                }
            }

            ViewBag.Employee_ID = db.HRMS_Emp_Details;
            return View(hRMS_Contact);
        }

        // GET: EmployeeContactDetails/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_Contact hRMS_Contact = db.HRMS_Contact.Find(id);
            if (hRMS_Contact == null)
            {
                return HttpNotFound();
            }
            ViewBag.Employee_ID = db.Accounts.Where(rec => rec.ID == hRMS_Contact.Employee_ID);
            return View(hRMS_Contact);
        }


        [HttpPost]
        public ActionResult Edit(HRMS_Contact hRMS_Contact)
        {
            if (ModelState.IsValid)
            {
                HRMS_Contact CorporateEmailExist = db.HRMS_Contact.Where(rec => rec.Corporate_Email == hRMS_Contact.Corporate_Email && rec.ID != hRMS_Contact.ID).FirstOrDefault();

                if (CorporateEmailExist == null)
                {
                    HRMS_Contact PhoneWorkNoExist = db.HRMS_Contact.Where(rec => rec.Phone_Work == hRMS_Contact.Phone_Work && rec.ID != hRMS_Contact.ID).FirstOrDefault();
                    if (PhoneWorkNoExist == null)
                    {
                        db.Entry(hRMS_Contact).State = EntityState.Modified;
                        db.SaveChanges();
                        ViewBag.ContactStatus = "Employee Contact detail is Updated successfully";
                        ViewBag.Employee_ID = db.Accounts.Where(rec => rec.ID == hRMS_Contact.Employee_ID);

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.ContactStatus = "Phone Work number is already exist for another employee";
                        ViewBag.Employee_ID = db.Accounts.Where(rec => rec.ID == hRMS_Contact.Employee_ID);

                        return View();
                    }
                }
                else
                {
                    ViewBag.ContactStatus = "Corporate Email address is already exist for another employee";
                    ViewBag.Employee_ID = db.Accounts.Where(rec => rec.ID == hRMS_Contact.Employee_ID);

                    return View();
                }
            }

            ViewBag.Employee_ID = db.Accounts.Where(rec => rec.ID == hRMS_Contact.Employee_ID);
            return View(hRMS_Contact);
        }


        [HttpPost]
        public bool Delete(long id)
        {
            HRMS_Contact hRMS_Contact = db.HRMS_Contact.Find(id);

            db.HRMS_Contact.Remove(hRMS_Contact);
            db.SaveChanges();
            return true;


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