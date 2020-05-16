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
    public class TravelExpenseApplicationController : Controller
    {
        private HRMSEntities db = new HRMSEntities();

        //ajax method
        public decimal TotalAmount(long? id)
        {
            decimal Total = 0;
            int Count = db.HRMS_Travel_Expense_App.Where(x=>x.Travel_App_ID==id.Value).Count();
            if (Count != 0)
            {
                Total = db.HRMS_Travel_Expense_App.Where(x => x.Travel_App_ID == id.Value).Sum(x => x.Amount);
            }
            return Total;
        }

        // GET: TravelExpenseApplication
        [Authorize(Roles =("admin,emp"))]
        public ActionResult TravelList()
        {
            //showing Approved Travel Application
            long emp_id = Convert.ToInt64(Session["id"]);
            string role = db.Accounts.Where(x => x.ID == emp_id).Select(x => x.role).FirstOrDefault();
            return View(db.HRMS_Travel_Application.Where(x=>x.emp_id==emp_id && x.Status==1).ToList());
        }

        [Authorize(Roles = ("admin,emp"))]
        public ActionResult TravelView(long? id)
        {
            HRMS_Travel_Application obj = new HRMS_Travel_Application();
            obj = db.HRMS_Travel_Application.Find(id);
            return View(obj);
        }


        [Authorize(Roles = ("admin,emp"))]
        [HttpGet]
        public ActionResult AddExpense(long? id)
        {
            HRMS_Travel_Expense_App obj = new HRMS_Travel_Expense_App();
            obj.Travel_App_ID = id.Value;
            


            //for list
            ViewData["users"] = db.HRMS_Travel_Expense_App.Where(x=>x.Travel_App_ID==id.Value).ToList();

            //savechanges without any rows
            ViewBag.Error = TempData["Error"];


            return View(obj);


        }


        [Authorize(Roles = ("admin,emp"))]
        [HttpPost]
        public ActionResult AddExpense(HRMS_Travel_Expense_App obj)
        {

            long emp_id = Convert.ToInt64(Session["id"]);
            string role = db.Accounts.Where(x => x.ID == emp_id).Select(x => x.role).FirstOrDefault();
            obj.EMP_ID = emp_id;
            obj.Status = 0;
            db.HRMS_Travel_Expense_App.Add(obj);
            db.SaveChanges();

            return RedirectToAction("AddExpense",new { id=obj.Travel_App_ID});


        }


        [Authorize(Roles = ("admin,emp"))]
        [HttpGet]
        public ActionResult Edit(long? id)
        {
            HRMS_Travel_Expense_App obj = db.HRMS_Travel_Expense_App.Find(id);
            return View(obj);
        }

        [Authorize(Roles = ("admin,emp"))]
        [HttpPost]
        public ActionResult Edit(HRMS_Travel_Expense_App obj)
        {

            db.Entry(obj).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("AddExpense",new { id = obj.Travel_App_ID});

        }

        [Authorize(Roles = ("admin,emp"))]
        public bool Delete(long id)
        {
            HRMS_Travel_Expense_App obj = db.HRMS_Travel_Expense_App.Find(id);
            if (obj != null)
            {
                db.HRMS_Travel_Expense_App.Remove(obj);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        [Authorize(Roles = ("admin,emp"))]
        public ActionResult SaveChanges(long id)
        {
            int count = db.HRMS_Travel_Expense_App.Where(x => x.Travel_App_ID == id).Count();
            if (count != 0)
            {
                HRMS_Travel_Application obj = db.HRMS_Travel_Application.Find(id);
                obj.ExpenseSubmitted = 1;
                db.Entry(obj).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("TravelList");
            }
            else
            {
                TempData["Error"] = "Please insert Atleast one row!!";
                return RedirectToAction("AddExpense", new { id=id});
            }
            }

        [Authorize(Roles = ("admin,emp"))]
        public ActionResult ViewExpense(long id)
        {

            int CancelCount = db.HRMS_Travel_Expense_App.Where(x => x.Travel_App_ID == id && x.Status == 2).Count();
            int TotalCount = db.HRMS_Travel_Expense_App.Where(x => x.Travel_App_ID == id).Count();
            int ApproveCount = db.HRMS_Travel_Expense_App.Where(x => x.Travel_App_ID == id && x.Status == 1).Count();
            int PendingCount = db.HRMS_Travel_Expense_App.Where(x => x.Travel_App_ID == id && x.Status == 0).Count();

            //for changing status of ExpenseSubmitted
            if (User.IsInRole("admin"))
            {
                
                if (TotalCount == CancelCount)
                {
                    HRMS_Travel_Application obj = db.HRMS_Travel_Application.Find(id);
                    obj.ExpenseSubmitted = 3; //cancel
                    db.Entry(obj).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else if (PendingCount == 0)
                {
                    HRMS_Travel_Application obj = db.HRMS_Travel_Application.Find(id);
                    obj.ExpenseSubmitted = 2; //Approve
                    db.Entry(obj).State = EntityState.Modified;
                    db.SaveChanges();
                }

            }

            if (CancelCount == TotalCount)
            {
                ViewBag.TotalAmount = 0;
            }
            else
            {
                ViewBag.TotalAmount = db.HRMS_Travel_Expense_App.Where(x => x.Travel_App_ID == id && (x.Status == 0 || x.Status == 1)).Sum(x => x.Amount);
            }
                return View(db.HRMS_Travel_Expense_App.Where(x => x.Travel_App_ID == id).ToList());
            
        }


        [Authorize(Roles = ("admin"))]
        public ActionResult AdminExpenseList()
        {
            return View(db.HRMS_Travel_Application.Where(x=>x.ExpenseSubmitted==1 || x.ExpenseSubmitted==2 || x.ExpenseSubmitted == 3).ToList());
        }

        [Authorize(Roles = ("admin"))]
        public ActionResult Approve(long id)
        {
            HRMS_Travel_Expense_App obj = db.HRMS_Travel_Expense_App.Find(id);
            obj.Status = 1;
            db.Entry(obj).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ViewExpense",new { id = obj.Travel_App_ID});
        }

        [Authorize(Roles = ("admin"))]
        public ActionResult Cancel(long id)
        {
            HRMS_Travel_Expense_App obj = db.HRMS_Travel_Expense_App.Find(id);
            obj.Status = 2;
            db.Entry(obj).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ViewExpense", new { id = obj.Travel_App_ID });
        }


        //public ActionResult Index()
        //{
        //    var hRMS_Travel_Expense_App = db.HRMS_Travel_Expense_App.Include(h => h.HRMS_Travel_Application).Include(h => h.Accounts);
        //    return View(hRMS_Travel_Expense_App.ToList());
        //}

        //// GET: TravelExpenseApplication/Details/5
        //public ActionResult Details(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    HRMS_Travel_Expense_App hRMS_Travel_Expense_App = db.HRMS_Travel_Expense_App.Find(id);
        //    if (hRMS_Travel_Expense_App == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(hRMS_Travel_Expense_App);
        //}

        //// GET: TravelExpenseApplication/Create
        //public ActionResult Create()
        //{
        //    ViewBag.Travel_App_ID = new SelectList(db.HRMS_Travel_Application, "ID", "Remark");
        //    ViewBag.EMP_ID = new SelectList(db.Accounts, "ID", "UserName");
        //    return View();
        //}

        //// POST: TravelExpenseApplication/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,Travel_App_ID,Travel_Date,Journey_Mode,From_Place,To_Place,Distance,Amount,Purpose,FileName,FileUrl,EMP_ID")] HRMS_Travel_Expense_App hRMS_Travel_Expense_App)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.HRMS_Travel_Expense_App.Add(hRMS_Travel_Expense_App);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.Travel_App_ID = new SelectList(db.HRMS_Travel_Application, "ID", "Remark", hRMS_Travel_Expense_App.Travel_App_ID);
        //    ViewBag.EMP_ID = new SelectList(db.Accounts, "ID", "UserName", hRMS_Travel_Expense_App.EMP_ID);
        //    return View(hRMS_Travel_Expense_App);
        //}

        //// GET: TravelExpenseApplication/Edit/5
        //public ActionResult Edit(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    HRMS_Travel_Expense_App hRMS_Travel_Expense_App = db.HRMS_Travel_Expense_App.Find(id);
        //    if (hRMS_Travel_Expense_App == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.Travel_App_ID = new SelectList(db.HRMS_Travel_Application, "ID", "Remark", hRMS_Travel_Expense_App.Travel_App_ID);
        //    ViewBag.EMP_ID = new SelectList(db.Accounts, "ID", "UserName", hRMS_Travel_Expense_App.EMP_ID);
        //    return View(hRMS_Travel_Expense_App);
        //}

        //// POST: TravelExpenseApplication/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID,Travel_App_ID,Travel_Date,Journey_Mode,From_Place,To_Place,Distance,Amount,Purpose,FileName,FileUrl,EMP_ID")] HRMS_Travel_Expense_App hRMS_Travel_Expense_App)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(hRMS_Travel_Expense_App).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.Travel_App_ID = new SelectList(db.HRMS_Travel_Application, "ID", "Remark", hRMS_Travel_Expense_App.Travel_App_ID);
        //    ViewBag.EMP_ID = new SelectList(db.Accounts, "ID", "UserName", hRMS_Travel_Expense_App.EMP_ID);
        //    return View(hRMS_Travel_Expense_App);
        //}

        //// GET: TravelExpenseApplication/Delete/5
        //public ActionResult Delete(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    HRMS_Travel_Expense_App hRMS_Travel_Expense_App = db.HRMS_Travel_Expense_App.Find(id);
        //    if (hRMS_Travel_Expense_App == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(hRMS_Travel_Expense_App);
        //}

        //// POST: TravelExpenseApplication/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(long id)
        //{
        //    HRMS_Travel_Expense_App hRMS_Travel_Expense_App = db.HRMS_Travel_Expense_App.Find(id);
        //    db.HRMS_Travel_Expense_App.Remove(hRMS_Travel_Expense_App);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
