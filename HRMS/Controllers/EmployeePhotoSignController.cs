using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HRMS.Models;
using System.IO;

namespace HRMS.Controllers
{
    public class EmployeePhotoSignController : Controller
    {
        private HRMSEntities db = new HRMSEntities();

        // GET: HRMS_EMP_PHOTO_SIGN
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            var hRMS_EMP_PHOTO_SIGN = db.HRMS_EMP_PHOTO_SIGN.Include(h => h.Accounts);
            return View(hRMS_EMP_PHOTO_SIGN.ToList());
        }
        //public ActionResult AddNew()
        //{
        //    return View();
        //}
        // GET: HRMS_EMP_PHOTO_SIGN/Details/5
        [Authorize(Roles = "admin,emp")]
        public ActionResult Details(long? id)
        {
            long emp_id = Convert.ToInt64(Session["id"]);
            string role = db.Accounts.Where(x => x.ID == emp_id).Select(x => x.role).FirstOrDefault();
            if (role == "admin")
            {
                ViewBag.Role = "admin";
            }

            bool isExist = db.HRMS_EMP_PHOTO_SIGN.Any(x => x.Emp_ID == emp_id);
            if (!isExist)
            {
                return RedirectToAction("Create");
            }

            else
            {
                //    if (id == null)
                //{
                //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                //}
                if (role == "emp")
                {
                    id = db.HRMS_EMP_PHOTO_SIGN.Where(x => x.Emp_ID == emp_id).Select(x => x.Emp_Photos_ID).FirstOrDefault();
                }

                HRMS_EMP_PHOTO_SIGN hRMS_EMP_PHOTO_SIGN = db.HRMS_EMP_PHOTO_SIGN.Find(id);
                if (hRMS_EMP_PHOTO_SIGN == null)
                {
                    return HttpNotFound();
                }
                return View(hRMS_EMP_PHOTO_SIGN);
            }
            }

        // GET: HRMS_EMP_PHOTO_SIGN/Create
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
                bool isExist = db.HRMS_EMP_PHOTO_SIGN.Any(x => x.Emp_ID == emp_id);
                if (isExist)
                {
                    long id = db.HRMS_EMP_PHOTO_SIGN.Where(x => x.Emp_ID == emp_id).Select(x => x.Emp_Photos_ID).FirstOrDefault();
                    return RedirectToAction("Details", "EmployeePhotoSign", new { id });
                }
            }



            ViewBag.Emp_ID = new SelectList(db.Accounts, "ID", "ID");
            return View();
        }

        // POST: HRMS_EMP_PHOTO_SIGN/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,emp")]
        public ActionResult Create(HRMS_EMP_PHOTO_SIGN hRMS_EMP_PHOTO_SIGN)
        {
            long emp_id = Convert.ToInt64(Session["id"]);
            string role = db.Accounts.Where(x => x.ID == emp_id).Select(x => x.role).FirstOrDefault();

            if (role == "emp")
            {
                ModelState.Remove("Emp_ID");
                hRMS_EMP_PHOTO_SIGN.Emp_ID = emp_id;
            }


            //[Bind(Include = "Emp_Photos_ID,Emp_ID,Emp_Photo_Title,Emp_Photo_Path,Emp_Sign_Title,Emp_Sign_Path")] 
            if (ModelState.IsValid)
            {


                if (hRMS_EMP_PHOTO_SIGN.ImageFile.FileName == null || hRMS_EMP_PHOTO_SIGN.ImageFile.FileName == "")
                {
                    hRMS_EMP_PHOTO_SIGN.Emp_Photo_Path = "~/EmpPhoto/default_image.png";
                }
                else
                {
                    string photo_file_name = Path.GetFileNameWithoutExtension(hRMS_EMP_PHOTO_SIGN.ImageFile.FileName);
                    string photo_extension = Path.GetExtension(hRMS_EMP_PHOTO_SIGN.ImageFile.FileName);
                    photo_file_name = photo_file_name + DateTime.Now.ToString("yymmssfff") + photo_extension;
                    hRMS_EMP_PHOTO_SIGN.Emp_Photo_Path = "~/EmpPhoto/" + photo_file_name;
                    photo_file_name = Path.Combine(Server.MapPath("~/EmpPhoto/"), photo_file_name);
                    hRMS_EMP_PHOTO_SIGN.ImageFile.SaveAs(photo_file_name);
                }


                if (hRMS_EMP_PHOTO_SIGN.SignFile.FileName == null || hRMS_EMP_PHOTO_SIGN.SignFile.FileName == "")
                {
                    hRMS_EMP_PHOTO_SIGN.Emp_Sign_Path = "~/EmpPhoto/default_image.png";
                }
                else
                {
                    string sign_file_name = Path.GetFileNameWithoutExtension(hRMS_EMP_PHOTO_SIGN.SignFile.FileName);
                    string sign_extension = Path.GetExtension(hRMS_EMP_PHOTO_SIGN.SignFile.FileName);
                    sign_file_name = sign_file_name + DateTime.Now.ToString("yymmssfff") + sign_extension;
                    hRMS_EMP_PHOTO_SIGN.Emp_Sign_Path = "~/EmpSign/" + sign_file_name;
                    sign_file_name = Path.Combine(Server.MapPath("~/EmpSign/"), sign_file_name);
                    hRMS_EMP_PHOTO_SIGN.SignFile.SaveAs(sign_file_name);
                }

                db.HRMS_EMP_PHOTO_SIGN.Add(hRMS_EMP_PHOTO_SIGN);
                db.SaveChanges();

                if (role == "emp")
                {
                    long id = db.HRMS_EMP_PHOTO_SIGN.Where(x => x.Emp_ID == emp_id).Select(x => x.Emp_Photos_ID).FirstOrDefault();
                    return RedirectToAction("Details", "EmployeePhotoSign", new { id });
                }

                return View();
                //return RedirectToAction("Index");
            }

            ViewBag.Emp_ID = new SelectList(db.Accounts, "ID", "ID");
            return View(hRMS_EMP_PHOTO_SIGN);
        }

        // GET: HRMS_EMP_PHOTO_SIGN/Edit/5
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

            if (role == "emp")
            {
                id = db.HRMS_EMP_PHOTO_SIGN.Where(x => x.Emp_ID == emp_id).Select(x => x.Emp_Photos_ID).FirstOrDefault();
            }

            HRMS_EMP_PHOTO_SIGN hRMS_EMP_PHOTO_SIGN = db.HRMS_EMP_PHOTO_SIGN.Find(id);
            if (hRMS_EMP_PHOTO_SIGN == null)
            {
                return HttpNotFound();
            }
            ViewBag.Emp_ID = new SelectList(db.Accounts, "ID", "ID");
            return View(hRMS_EMP_PHOTO_SIGN);
        }

        // POST: HRMS_EMP_PHOTO_SIGN/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,emp")]
        public ActionResult Edit([Bind(Include = "Emp_Photos_ID,Emp_ID,Emp_Photo_Title,Emp_Photo_Path,Emp_Sign_Title,Emp_Sign_Path")] HRMS_EMP_PHOTO_SIGN hRMS_EMP_PHOTO_SIGN)
        {
            long emp_id = Convert.ToInt64(Session["id"]);
            string role = db.Accounts.Where(x => x.ID == emp_id).Select(x => x.role).FirstOrDefault();

            if (role == "emp")
            {
                ModelState.Remove("Emp_ID");
                hRMS_EMP_PHOTO_SIGN.Emp_ID = emp_id;
            }

            if (ModelState.IsValid)
            {
                if (hRMS_EMP_PHOTO_SIGN.ImageFile.FileName == null || hRMS_EMP_PHOTO_SIGN.ImageFile.FileName == "")
                {
                    hRMS_EMP_PHOTO_SIGN.Emp_Photo_Path = "~/EmpPhoto/default_image.png";
                }
                else
                {
                    string photo_file_name = Path.GetFileNameWithoutExtension(hRMS_EMP_PHOTO_SIGN.ImageFile.FileName);
                    string photo_extension = Path.GetExtension(hRMS_EMP_PHOTO_SIGN.ImageFile.FileName);
                    photo_file_name = photo_file_name + DateTime.Now.ToString("yymmssfff") + photo_extension;
                    hRMS_EMP_PHOTO_SIGN.Emp_Photo_Path = "~/EmpPhoto/" + photo_file_name;
                    photo_file_name = Path.Combine(Server.MapPath("~/EmpPhoto/"), photo_file_name);
                    hRMS_EMP_PHOTO_SIGN.ImageFile.SaveAs(photo_file_name);
                }


                if (hRMS_EMP_PHOTO_SIGN.SignFile.FileName == null || hRMS_EMP_PHOTO_SIGN.SignFile.FileName == "")
                {
                    hRMS_EMP_PHOTO_SIGN.Emp_Sign_Path = "~/EmpPhoto/default_image.png";
                }
                else
                {
                    string sign_file_name = Path.GetFileNameWithoutExtension(hRMS_EMP_PHOTO_SIGN.SignFile.FileName);
                    string sign_extension = Path.GetExtension(hRMS_EMP_PHOTO_SIGN.SignFile.FileName);
                    sign_file_name = sign_file_name + DateTime.Now.ToString("yymmssfff") + sign_extension;
                    hRMS_EMP_PHOTO_SIGN.Emp_Sign_Path = "~/EmpSign/" + sign_file_name;
                    sign_file_name = Path.Combine(Server.MapPath("~/EmpSign/"), sign_file_name);
                    hRMS_EMP_PHOTO_SIGN.SignFile.SaveAs(sign_file_name);
                }


                db.Entry(hRMS_EMP_PHOTO_SIGN).State = EntityState.Modified;
                db.SaveChanges();

                if (role == "emp")
                {
                    long id = db.HRMS_EMP_PHOTO_SIGN.Where(x => x.Emp_ID == emp_id).Select(x => x.Emp_Photos_ID).FirstOrDefault();
                    return RedirectToAction("Details", "EmployeePhotoSign", new { id });
                }

                return RedirectToAction("Index");
            }
            ViewBag.Emp_ID = new SelectList(db.Accounts, "ID", "ID");
            return View(hRMS_EMP_PHOTO_SIGN);
        }

        //GET: HRMS_EMP_PHOTO_SIGN/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_EMP_PHOTO_SIGN hRMS_EMP_PHOTO_SIGN = db.HRMS_EMP_PHOTO_SIGN.Find(id);
            if (hRMS_EMP_PHOTO_SIGN == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_EMP_PHOTO_SIGN);
        }

        // POST: HRMS_EMP_PHOTO_SIGN/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,emp")]
        public ActionResult DeleteConfirmed(long id)
        {
            HRMS_EMP_PHOTO_SIGN hRMS_EMP_PHOTO_SIGN = db.HRMS_EMP_PHOTO_SIGN.Find(id);
            db.HRMS_EMP_PHOTO_SIGN.Remove(hRMS_EMP_PHOTO_SIGN);
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
