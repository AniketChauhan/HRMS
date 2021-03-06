﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HRMS.Models;
using PagedList;
using PagedList.Mvc;

namespace HRMS.Controllers
{
    public class EmployeeContactDetailsController : Controller
    {
        private HRMSEntities db = new HRMSEntities();

        // GET: EmployeeContactDetails
        [Authorize(Roles = "admin")]
        public ActionResult Index(string Data, string Search, int? page)
        {
            //var hRMS_Contact = db.HRMS_Contact.Include(h => h.Accounts);
            //return View(hRMS_Contact.ToList());

            if (Data == "1" && Search != "")
            {
                long ser = Convert.ToInt64(Search);
                return View(db.HRMS_Contact.Include(h => h.Accounts).Where(x => x.Employee_ID == ser).ToList().ToPagedList(page ?? 1, 7));
            }
            else if (Data == "2")
            {
                return View(db.HRMS_Contact.Include(h => h.Accounts).Where(x => x.Phone_Work.StartsWith(Search) || Search == null).ToList().ToPagedList(page ?? 1, 7));
            }
            else
            {
                return View(db.HRMS_Contact.Include(h => h.Accounts).Where(x => x.Corporate_Email.StartsWith(Search) || Search == null).ToList().ToPagedList(page ?? 1, 7));
            }

        }

        // GET: EmployeeContactDetails/Details/5
        [Authorize(Roles = "admin,emp")]
        public ActionResult Details(long? id, string name)
        {
            long emp_id = Convert.ToInt64(Session["id"]);
            string role = db.Accounts.Where(x => x.ID == emp_id).Select(x => x.role).FirstOrDefault();
            if (role == "admin")
            {
                ViewBag.Role = "admin";

                //from admin side: Employee fill Data (Next And Prev Button)
                if (name != null)
                {
                    bool isThere = db.HRMS_Contact.Any(x => x.Employee_ID == id.Value);
                    if (!isThere)
                    {
                        return RedirectToAction("Create", "EmployeeContactDetails", new { ID = id.Value });
                    }
                    else
                    {
                        ViewBag.EditVisible = "No";
                        HRMS_Contact employee_Personal_Detail = db.HRMS_Contact.Where(x => x.Employee_ID == id.Value).FirstOrDefault();
                        return View(employee_Personal_Detail);
                    }
                }
            }

            bool isExist = db.HRMS_Contact.Any(x => x.Employee_ID == emp_id);
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
                    id = db.HRMS_Contact.Where(x => x.Employee_ID == emp_id).Select(x => x.ID).FirstOrDefault();
                }
                HRMS_Contact hRMS_Contact = db.HRMS_Contact.Find(id);
                if (hRMS_Contact == null)
                {
                    return HttpNotFound();
                }
                return View(hRMS_Contact);
            }
            }

        // GET: EmployeeContactDetails/Create
        [Authorize(Roles = "admin,emp")]
        public ActionResult Create(long? ID)
        {
            HRMS_Contact obj = new HRMS_Contact();
            long emp_id = Convert.ToInt64(Session["id"]);
            string role = db.Accounts.Where(x => x.ID == emp_id).Select(x => x.role).FirstOrDefault();
            if (role == "admin")
            {
                ViewBag.Role = "admin";
                obj.Employee_ID = ID.Value;
                bool isExist = db.HRMS_Contact.Any(x => x.Employee_ID == obj.Employee_ID);
                if (isExist)
                {
                    return RedirectToAction("Create", "EmployeeReferenceDetail", new { ID = obj.Employee_ID });
                }
            }

            //if attck by direct URL
            if (role == "emp")
            {
                bool isExist = db.HRMS_Contact.Any(x => x.Employee_ID == emp_id);
                if (isExist)
                {
                    long id = db.HRMS_Contact.Where(x => x.Employee_ID == emp_id).Select(x => x.ID).FirstOrDefault();
                    return RedirectToAction("Details", "EmployeeContactDetails", new { id });
                }
            }

            //ViewBag.Employee_ID = db.Accounts;
            return View(obj);
        }


        [HttpPost]
        [Authorize(Roles = "admin,emp")]
        public ActionResult Create(HRMS_Contact hRMS_Contact)
        {
            long emp_id = Convert.ToInt64(Session["id"]);
            string role = db.Accounts.Where(x => x.ID == emp_id).Select(x => x.role).FirstOrDefault();

            if (role == "emp")
            {
                ModelState.Remove("Employee_ID");
                hRMS_Contact.Employee_ID = emp_id;
            }


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
                            ModelState.Clear();
                            ViewBag.ContactStatus = "Employee Contact detail is added successfully";

                           
                            if (role == "emp")
                            {
                                long id = db.HRMS_Contact.Where(x => x.Employee_ID == emp_id).Select(x => x.ID).FirstOrDefault();
                                return RedirectToAction("Details", "EmployeeContactDetails", new { id });
                            }

                            //ViewBag.Employee_ID = db.Accounts;

                            if (role == "admin")
                            {
                                ViewBag.Role = "admin";
                                return RedirectToAction("Create", "EmployeeReferenceDetail", new { ID = hRMS_Contact.Employee_ID });
                            }

                            //return View();
                        }
                        else
                        {
                            ViewBag.ContactStatus = "Phone Work number is already exist for another employee";
                            //ViewBag.Employee_ID = db.Accounts;

                            if (role == "admin")
                            {
                                ViewBag.Role = "admin";
                            }

                            return View(hRMS_Contact);
                        }
                    }
                    else
                    {
                        ViewBag.ContactStatus = "Corporate Email address is already exist for another employee";
                        //ViewBag.Employee_ID = db.Accounts;
                        if (role == "admin")
                        {
                            ViewBag.Role = "admin";
                        }
                        return View(hRMS_Contact);
                    }
                }
                else
                {
                    ViewBag.ContactStatus = "Employee Contact details is already exist!";
                    //ViewBag.Employee_ID = db.Accounts;
                    if (role == "admin")
                    {
                        ViewBag.Role = "admin";
                    }
                    return View(hRMS_Contact);
                }
            }

            //ViewBag.Employee_ID = db.Accounts;
            if (role == "admin")
            {
                ViewBag.Role = "admin";
            }
            return View(hRMS_Contact);
        }

        // GET: EmployeeContactDetails/Edit/5
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
                id = db.HRMS_Contact.Where(x => x.Employee_ID == emp_id).Select(x => x.ID).FirstOrDefault();
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
        [Authorize(Roles = "admin,emp")]
        public ActionResult Edit(HRMS_Contact hRMS_Contact)
        {
            long emp_id = Convert.ToInt64(Session["id"]);
            string role = db.Accounts.Where(x => x.ID == emp_id).Select(x => x.role).FirstOrDefault();

            if (role == "emp")
            {
                ModelState.Remove("Employee_ID");
                hRMS_Contact.Employee_ID = emp_id;
            }


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

                        if (role == "emp")
                        {
                            long id = db.HRMS_Contact.Where(x => x.Employee_ID == emp_id).Select(x => x.ID).FirstOrDefault();
                            return RedirectToAction("Details", "EmployeeContactDetails", new { id });
                        }

                       

                        ViewBag.ContactStatus = "Employee Contact detail is Updated successfully";
                        ViewBag.Employee_ID = db.Accounts.Where(rec => rec.ID == hRMS_Contact.Employee_ID);
                        if (role == "admin")
                        {
                            ViewBag.Role = "admin";
                        }
                        return View(hRMS_Contact);
                    }
                    else
                    {
                        ViewBag.ContactStatus = "Phone Work number is already exist for another employee";
                        ViewBag.Employee_ID = db.Accounts.Where(rec => rec.ID == hRMS_Contact.Employee_ID);
                        if (role == "admin")
                        {
                            ViewBag.Role = "admin";
                        }
                        return View(hRMS_Contact);
                    }
                }
                else
                {
                    ViewBag.ContactStatus = "Corporate Email address is already exist for another employee";
                    ViewBag.Employee_ID = db.Accounts.Where(rec => rec.ID == hRMS_Contact.Employee_ID);
                    if (role == "admin")
                    {
                        ViewBag.Role = "admin";
                    }
                    return View(hRMS_Contact);
                }
            }

            ViewBag.Employee_ID = db.Accounts.Where(rec => rec.ID == hRMS_Contact.Employee_ID);
            if (role == "admin")
            {
                ViewBag.Role = "admin";
            }
            return View(hRMS_Contact);
        }


        [HttpPost]
        [Authorize(Roles = "admin")]
        public bool Delete(long id)
        {
            HRMS_Contact hRMS_Contact = db.HRMS_Contact.Find(id);

            db.HRMS_Contact.Remove(hRMS_Contact);
            db.SaveChanges(); ModelState.Clear();

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
