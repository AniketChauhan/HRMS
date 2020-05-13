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
using PagedList;
using PagedList.Mvc;


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
        public ActionResult Index(string Data, string Search, int? page)
        {
            //return View(db.Accounts.ToList());
            if (Data == "1" && Search != "")
            {
                long ser = Convert.ToInt64(Search);
                return View(db.Accounts.Where(x => x.ID == ser).ToList().ToPagedList(page ?? 1, 10));
            }
            else 
            {
                return View(db.Accounts.Where(x => x.UserName.StartsWith(Search) || Search == null).ToList().ToPagedList(page ?? 1, 10));
            }
            //else
            //{
            //    return View(db.HRMS_Emp_Details.Include(h => h.HRMS_COST_CENTER).Include(h => h.HRMS_DEPT).Include(h => h.HRMS_DESG_MS).Include(h => h.HRMS_SALUTATION).Include(h => h.UnitMaster).Include(h => h.WorkLocationMaster).Where(x => x.UnitMaster.UnitName.StartsWith(Search) || Search == null).ToList().ToPagedList(page ?? 1, 7));
            //}

        }


        //FillUP Employee Details
        public ActionResult FillData(string Data, string Search, int? page)
        {


            //return View(db.FillEmployee().ToList());

            if (Data == "1" && Search != "")
            {
                long ser = Convert.ToInt64(Search);
                return View(db.FillEmployee().Where(x => x.ID == ser).ToList().ToPagedList(page ?? 1, 10));
            }
            else
            {
                return View(db.FillEmployee().Where(x =>Search == null || x.UserName.StartsWith(Search)).ToList().ToPagedList(page ?? 1, 10));
            }
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
