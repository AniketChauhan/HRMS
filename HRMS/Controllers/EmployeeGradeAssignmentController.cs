using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRMS.Models;

namespace HRMS.Controllers
{
    public class EmployeeGradeAssignmentController : Controller
    {
        HRMSEntities db = new HRMSEntities();




        public JsonResult CrudOperation(long GradeID,long EmpID)
        {
           
            db.Configuration.ProxyCreationEnabled = false;
            bool isValid = db.EMP_Grade_Assignment.Any(x => x.EMP_ID == EmpID);
            if (!isValid)
            {
                EMP_Grade_Assignment obj = new EMP_Grade_Assignment();
                obj.EMP_ID = EmpID;
                obj.Grade_ID = GradeID;
                db.EMP_Grade_Assignment.Add(obj);
                db.SaveChanges(); ModelState.Clear();

            }
            else
            {

                EMP_Grade_Assignment obj = db.EMP_Grade_Assignment.Where(x => x.EMP_ID == EmpID).FirstOrDefault();
                obj.Grade_ID = GradeID;
                db.SaveChanges(); ModelState.Clear();

            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult CrudOperation(int CountryX)
        //{
        //    db.Configuration.ProxyCreationEnabled = false;
        //    // long CountryID = db.Countries.Where(x => x.CountryName == CountryX).Select(x => x.CountryID).SingleOrDefault();
        //    List<State> StateList = db.States.Where(x => x.CountryID == CountryX).ToList();
        //    return Json(StateList, JsonRequestBehavior.AllowGet);
        //}

        // GET: EmployeeGradeAssignment
        public ActionResult Index()
        {
            
            //var EmpList = db.HRMS_Emp_Details.Select(x => x.Emp_ID);
            //return View(EmpList.ToList());

            ViewBag.Grade_ID = new SelectList(db.HRMS_CATEGORY_GRADE, "Category_ID", "Grade_Name");

            return View(db.HRMS_Emp_Details.ToList());
        }
    }
}