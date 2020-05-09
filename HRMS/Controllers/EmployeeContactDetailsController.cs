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
    public class EmployeeContactDetailsController : Controller
    {
        private HRMSEntities db = new HRMSEntities();

        // GET: EmployeeContactDetails
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            var hRMS_Contact = db.HRMS_Contact.Include(h => h.Accounts);
            return View(hRMS_Contact.ToList());
        }

        // GET: EmployeeContactDetails/Details/5
        [Authorize(Roles = "admin,emp")]
        public ActionResult Details(long? id)
        {
            long emp_id = Convert.ToInt64(Session["id"]);
            string role = db.Accounts.Where(x => x.ID == emp_id).Select(x => x.role).FirstOrDefault();
            if (role == "admin")
            {
                ViewBag.Role = "admin";
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
                id = db.HRMS_Contact.Where(x => x.Employee_ID == emp_id).Select(x => x.ID).FirstOrDefault();

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
                bool isExist = db.HRMS_Contact.Any(x => x.Employee_ID == emp_id);
                if (isExist)
                {
                    long id = db.HRMS_Contact.Where(x => x.Employee_ID == emp_id).Select(x => x.ID).FirstOrDefault();
                    return RedirectToAction("Details", "EmployeeContactDetails", new { id });
                }
            }

            //ViewBag.Employee_ID = db.Accounts;
            return View();
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
                            }

                            return View();
                        }
                        else
                        {
                            ViewBag.ContactStatus = "Phone Work number is already exist for another employee";
                            //ViewBag.Employee_ID = db.Accounts;

                            if (role == "admin")
                            {
                                ViewBag.Role = "admin";
                            }

                            return View();
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
                        return View();
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
                    return View();
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
                        return View();
                    }
                    else
                    {
                        ViewBag.ContactStatus = "Phone Work number is already exist for another employee";
                        ViewBag.Employee_ID = db.Accounts.Where(rec => rec.ID == hRMS_Contact.Employee_ID);
                        if (role == "admin")
                        {
                            ViewBag.Role = "admin";
                        }
                        return View();
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
                    return View();
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
