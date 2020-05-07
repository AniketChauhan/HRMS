﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRMS.Models;


namespace HRMS.Controllers
{
    public class EmployeeAttachmentDetailController : Controller
    {
        
        HRMSEntities db = new HRMSEntities();
       
        public ActionResult Index()
        {
            ViewBag.Attachment_Type_ID = new SelectList(db.HRMS_ATTACHMENT_TYPE, "Attachment_Type_ID", "Attachment_Type_Name");
            ViewData["users"] = db.HRMS_EMP_Attachment_Details.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase files, HRMS_EMP_Attachment_Details model)
        {

            if (files != null)
            {
                //files already there or not
                bool IsValid = db.HRMS_EMP_Attachment_Details.Any(x => (x.EMP_ID == model.EMP_ID && x.Attachment_Type_ID == model.Attachment_Type_ID));
                if (IsValid)
                {
                    ViewBag.IsExist = "File is already Uploaded!";
                    ViewBag.Attachment_Type_ID = new SelectList(db.HRMS_ATTACHMENT_TYPE, "Attachment_Type_ID", "Attachment_Type_Name");
                    ViewData["users"] = db.HRMS_EMP_Attachment_Details.ToList();
                    return View();
                }

                else
                {
                    //fetching Attachment Name from Attachment ID
                    var AttachmentName = db.HRMS_ATTACHMENT_TYPE.Where(x => x.Attachment_Type_ID == model.Attachment_Type_ID).Select(x => x.Attachment_Type_Name).FirstOrDefault();

                    var Extension = Path.GetExtension(files.FileName);

                    //only pdf
                    if (Extension == ".pdf" || Extension == ".PDF")
                    {
                        var fileName = model.EMP_ID + "_" + AttachmentName + Extension;
                        string path = Path.Combine(Server.MapPath("~/UploadedFiles"), fileName);
                        model.FILEURL = Url.Content(Path.Combine("~/UploadedFiles/", fileName));
                        model.FILENAME = fileName;
                        files.SaveAs(path);
                        db.HRMS_EMP_Attachment_Details.Add(model);
                        db.SaveChanges();


                        ViewBag.Success = "File is Succeccfully Uploaded!";
                        ViewBag.Attachment_Type_ID = new SelectList(db.HRMS_ATTACHMENT_TYPE, "Attachment_Type_ID", "Attachment_Type_Name");
                        ViewData["users"] = db.HRMS_EMP_Attachment_Details.ToList();
                        ModelState.Clear();
                        return View();
                    }

                    else
                    {
                        ViewBag.Format = "Please Upload only pdf format!";
                        ViewBag.Attachment_Type_ID = new SelectList(db.HRMS_ATTACHMENT_TYPE, "Attachment_Type_ID", "Attachment_Type_Name");
                        ViewData["users"] = db.HRMS_EMP_Attachment_Details.ToList();
                        return View();
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Please Choose Correct File Type !!");
                ViewBag.Attachment_Type_ID = new SelectList(db.HRMS_ATTACHMENT_TYPE, "Attachment_Type_ID", "Attachment_Type_Name");
                ViewData["users"] = db.HRMS_EMP_Attachment_Details.ToList();
                return View(model);
            }

        }

        public ActionResult DownloadFile(string filePath)
        {
            string fullName = Server.MapPath("~" + filePath);

            byte[] fileBytes = GetFile(fullName);
            var fn = Path.GetFileName(filePath);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fn);
        }

        byte[] GetFile(string s)
        {
            System.IO.FileStream fs = System.IO.File.OpenRead(s);
            byte[] data = new byte[fs.Length];
            int br = fs.Read(data, 0, data.Length);
            if (br != fs.Length)
                throw new System.IO.IOException(s);
            return data;
        }

        public ActionResult Delete(string filePath, int id)
        {
            bool isValid = RemoveFileFromServer(filePath);
            if (isValid)
            {
                HRMS_EMP_Attachment_Details model = db.HRMS_EMP_Attachment_Details.Find(id);
                db.HRMS_EMP_Attachment_Details.Remove(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }
        public bool RemoveFileFromServer(string path)
        {
            string filePath = Server.MapPath("~" + path);
            if (System.IO.File.Exists(filePath))
            {
                try 
                {
                    System.IO.File.Delete(filePath);
                    return true;
                }
                catch (Exception e)
                {
                    //msg
                }
            }
            return false;
        }
    }
}