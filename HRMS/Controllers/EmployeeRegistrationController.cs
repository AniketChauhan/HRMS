using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using HRMS.Models;


namespace HRMS.Controllers
{
    [Authorize(Roles ="admin")]
    public class EmployeeRegistrationController : Controller
    {
        private HRMSEntities db = new HRMSEntities();


        public string PasswordFN()
        {
            string newPassword = Membership.GeneratePassword(6, 0);
            Random rnd = new Random();
            newPassword = Regex.Replace(newPassword, @"[^a-z0-9]", m => rnd.Next(0, 10).ToString());
            return newPassword;
        }

        // GET: EmployeeRegistration
        public ActionResult Index()
        {
            return View(db.Accounts.ToList());
        }


        //FillUP Employee Details
        public ActionResult FillData()
        {
            //var employeeRecord = from e in employees
            //                     join d in departments on e.Department_Id equals d.DepartmentId into table1
            //                     from d in table1.ToList()
            //                     join i in incentives on e.Incentive_Id equals i.IncentiveId into table2
            //                     from i in table2.ToList()
            //                     select new ViewModel
            //                     {
            //                         employee = e,
            //                         department = d,
            //                         incentive = i
            //                     };


            var list= db.Accounts.SqlQuery("select Accounts.ID from Accounts except(select Employee_Personal_Detail.EMP_ID from Employee_Personal_Detail inner join HRMS_Emp_Details on Employee_Personal_Detail.EMP_ID = HRMS_Emp_Details.EMP_ID inner join HRMS_EMP_ReferenceDetail on HRMS_EMP_ReferenceDetail.EMP_ID = HRMS_Emp_Details.EMP_ID inner join HRMS_Contact on HRMS_Contact.Employee_ID = HRMS_EMP_ReferenceDetail.EMP_ID inner join HRMS_EMP_Attachment_Details on HRMS_EMP_Attachment_Details.EMP_ID = HRMS_Contact.Employee_ID inner join HRMS_EMP_PHOTO_SIGN on HRMS_EMP_PHOTO_SIGN.EMP_ID = HRMS_EMP_Attachment_Details.EMP_ID)").ToList();
            return View(list);
        }

        // GET: EmployeeRegistration/Details/5
        //public ActionResult Details(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Accounts accounts = db.Accounts.Find(id);
        //    if (accounts == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(accounts);
        //}

        // GET: EmployeeRegistration/Create
        public ActionResult Create()
        {
            int count = db.Accounts.Where(x => x.role == "emp").Count();
            Accounts obj = new Accounts();
            if (count == 0)
            {
                obj.ID = 10001;
            }
            else
            {
                obj.ID = db.Accounts.Where(x => x.role == "emp").Max(x=>x.ID);
                obj.ID++;
            }


            //string newPassword = Membership.GeneratePassword(6, 0);
            //newPassword = Regex.Replace(newPassword, @"[^a-zA-Z0-9]", m => "");
            //newPassword = newPassword.Substring(0, 6);

            obj.password = PasswordFN();

                //checking for password already exist or not
            bool isExist = true;
            while (isExist)
            {
                isExist = db.Accounts.Any(x => x.password == obj.password);
                if (isExist)
                {
                    obj.password = PasswordFN();
                }
                else
                {
                    isExist = false;
                }
            }
            return View(obj);
        }

        // POST: EmployeeRegistration/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Accounts accounts)
        {
            if (ModelState.IsValid)
            {
                if (accounts.UserName != accounts.ConfirmUsername)
                {
                    ViewBag.AccountStatus = "UserName Mismatched!";
                    return View();
                }

                bool isExist = db.Accounts.Any(x=>x.UserName==accounts.UserName);
                if (isExist)
                {
                    ViewBag.AccountStatus = "UserName Already Exist!";
                    return View();
                }
                accounts.role = "emp";
                db.Accounts.Add(accounts);
                db.SaveChanges();

                //sending Email
                MailMessage mail = new MailMessage();
                //mail.To = accounts.UserName;
                mail.To.Add(accounts.UserName);
                mail.From = new MailAddress("Humanresourceshrms@gmail.com");
                mail.Subject = "Your Password for HRMS";
               
                string htmlString = @"<html>
                      <body>
                      <br><br><br>
                      <p><i>Regards,</i><br><b>Team HRMS<b></p>
                      </body>
                      </html>
                     ";
                //mail.Body = "password:"+accounts.password;
                mail.Body = @"<html>
                     
                      <b>Your UserName : <b>
                     
                      </html>
                     " + accounts.UserName + @"<html>
                     <br>
                      <b>Your Password : <b>
                     
                      </html>
                     " + accounts.password+ htmlString;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
               // smtp.UseDefaultCredentials = false;
                smtp.UseDefaultCredentials = true;

                smtp.Credentials = new System.Net.NetworkCredential("Humanresourceshrms@gmail.com", "Aniket@Vikalp"); // Enter seders User name and password  
                smtp.EnableSsl = true;
                smtp.Send(mail);


                return RedirectToAction("Index");
            }

            return View(accounts);
        }

        // GET: EmployeeRegistration/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accounts accounts = db.Accounts.Find(id);
            if (accounts == null)
            {
                return HttpNotFound();
            }
            return View(accounts);
        }

        // POST: EmployeeRegistration/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit (Accounts accounts)
        {
            ModelState.Remove("password");  //because password is required

            if (ModelState.IsValid)
            {
                if (accounts.UserName != accounts.ConfirmUsername)
                {
                    ViewBag.AccountStatus = "UserName Mismatched!";
                    return View();
                }

                bool isExist = db.Accounts.Any(x => x.ID!=accounts.ID && x.UserName == accounts.UserName);
                if (isExist)
                {
                    ViewBag.AccountStatus = "UserName Already Exist!";
                    return View();
                }

                db.Entry(accounts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accounts);
        }

        // GET: EmployeeRegistration/Delete/5
        //public ActionResult Delete(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Accounts accounts = db.Accounts.Find(id);
        //    if (accounts == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(accounts);
        //}

        //// POST: EmployeeRegistration/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(long id)
        //{
        //    Accounts accounts = db.Accounts.Find(id);
        //    db.Accounts.Remove(accounts);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}


        [HttpPost]
        [Authorize(Roles = "admin")]
        public bool Delete(long id)
        {

            Accounts accounts = db.Accounts.Find(id);
            db.Accounts.Remove(accounts);
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
