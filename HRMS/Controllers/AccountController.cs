﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRMS.Models;
using CaptchaMvc.HtmlHelpers;
using System.Web.Security;

namespace HRMS.Controllers
{
    //[AllowAnonymous]
    public class AccountController : Controller
    {
        HRMSEntities db = new HRMSEntities();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index( Accounts obj)
        {
            if (!this.IsCaptchaValid(""))
            {
                ViewBag.Error = "Captcha is invalid!";
                return View();
            }

            else
            {    //x.UserName.Equals(UserModel.UserName, StringComparison.Ordinal)
                // bool isValid = db.Accounts.Any(x=>x.UserName==obj.UserName && x.password==obj.password);
                //Accounts acc = db.Accounts.Where(x => x.UserName == obj.UserName && x.password==obj.password).FirstOrDefault();
                Accounts acc = db.Accounts.Where(x => x.UserName == obj.UserName && x.password == obj.password).FirstOrDefault();
                if (acc != null)
                {
                    // return Content(acc.role);
                    if (acc.role == "admin")
                    {
                        // return RedirectToAction("LogIn", "Account", new { area = "Admin" });
                        Session["id"] = acc.ID;
                        FormsAuthentication.SetAuthCookie(obj.UserName, true);
                        return RedirectToAction("Index", "EmployeeDetail");
                    
                    }
                    else
                    {
                        Session["id"] = acc.ID;
                        FormsAuthentication.SetAuthCookie(obj.UserName, true);

                        //checking for Employee data is already there or not
                        bool isExist = db.HRMS_Emp_Details.Any(x => x.EMP_ID == acc.ID);
                        long id = db.HRMS_Emp_Details.Where(x=>x.EMP_ID==acc.ID).Select(x=>x.ID).FirstOrDefault();
                        if (isExist)
                        {
                            return RedirectToAction("Details", "EmployeeDetail", new {id});
                        }
                        else
                        {
                            //return RedirectToAction("Create", "EmployeeDetail", new { acc.ID });
                            return RedirectToAction("Create", "EmployeeDetail");
                        }

                    }
                }
                else
                {
                    return Content("Wrong data!!");
                }
            }
           
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }

   
}