using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using HRMS.Models;

namespace HRMS.Controllers
{
    [Authorize(Roles = "admin")]
    public class TrainingNotificationController : Controller
    {
        public HRMSEntities db = new HRMSEntities();
        // GET: TrainingNotification
        public ActionResult Index(long id)
        {
            if (TempData["Error"] != null)
            {
                ViewBag.Error = TempData["Error"];
            }
            TempData["Pro_ID"] = id;
            return View(db.HRMS_TrainingApproval.Where(x=>x.Program_ID==id && x.Status==2).ToList());
        }

        [HttpPost]
        public ActionResult EmailNotification(IEnumerable<string> ListOfEmp)
        {
            long programID = Convert.ToInt64(TempData["Pro_ID"]);
            if (ListOfEmp == null)
            {
                TempData["Error"] = "Please!Select atleast one employee!";
                return RedirectToAction("Index",new { id=programID});
            }
            else
            {
                
                HRMS_ProgramDetail objj = db.HRMS_ProgramDetail.Where(x => x.ID == programID).FirstOrDefault();
                string ProgramName = objj.ProgramName;
                string address;
                if (objj.Venue == "Internal")
                {
                    address = "On Company Location";
                }
                else
                {
                    address = objj.Address;
                }

                foreach (var item in ListOfEmp.ToList())
                {
                    //time and date
                    long EmpID = db.Accounts.Where(x => x.UserName == item).Select(x => x.ID).FirstOrDefault();
                    HRMS_TrainingApproval obj = db.HRMS_TrainingApproval.Where(x => x.Program_ID == programID && x.EMP_ID == EmpID).FirstOrDefault();


                    //sending Email
                    MailMessage mail = new MailMessage();
                    //mail.To = accounts.UserName;
                    mail.To.Add(item);
                    mail.From = new MailAddress("Humanresourceshrms@gmail.com");
                    mail.Subject = "Training Schedule";

                    string htmlString = @"<html>
                      <body>
                      <br><br><br>
                      <p><i>Regards,</i><br><b>Team HRMS<b></p>
                      </body>
                      </html>
                     ";
                    //mail.Body = "password:"+accounts.password;
                    mail.Body = @"<html>
                     
                      <b>Program Name : <b>
                     
                      </html>
                     " + ProgramName +
                         @"<html>
                     <br>
                      <b>From Date : <b>
                     
                      </html>
                     " + obj.FromDate?.ToShortDateString() +
                         @"<html>
                     <br>
                      <b>To Date : <b>
                     
                      </html>
                     " + obj.ToDate?.ToShortDateString() +
                         @"<html>
                     <br>
                      <b>From Time : <b>
                     
                      </html>
                     " + obj.FromTime +
                         @"<html>
                     <br>
                      <b>To Time : <b>
                     
                      </html>
                     " + obj.ToTime +
                          @"<html>
                     <br>
                      <b>Venue : <b>
                     
                      </html>
                     " + address +
                         htmlString;
                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    // smtp.UseDefaultCredentials = false;
                    smtp.UseDefaultCredentials = true;

                    smtp.Credentials = new System.Net.NetworkCredential("Humanresourceshrms@gmail.com", "Aniket@Vikalp"); // Enter seders User name and password  
                    smtp.EnableSsl = true;
                    smtp.Send(mail);

                    //update email flag
                    obj.IsEmailSent = 1; //email sent
                    db.Entry(obj).State = EntityState.Modified;
                    db.SaveChanges();

                }
                return RedirectToAction("Index", "TrainingApproval");
            }
        }
    }
}