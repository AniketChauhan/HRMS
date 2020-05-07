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
    public class TravelMealExpenseController : Controller
    {
        private HRMSEntities db = new HRMSEntities();

        // GET: HRMS_TRAVEL_MEAL_EXPENSE_MS
        public ActionResult Index()
        {
            return View(db.HRMS_TRAVEL_MEAL_EXPENSE_MS.ToList());
        }

        // GET: HRMS_TRAVEL_MEAL_EXPENSE_MS/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_TRAVEL_MEAL_EXPENSE_MS hRMS_TRAVEL_MEAL_EXPENSE_MS = db.HRMS_TRAVEL_MEAL_EXPENSE_MS.Find(id);
            if (hRMS_TRAVEL_MEAL_EXPENSE_MS == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_TRAVEL_MEAL_EXPENSE_MS);
        }

        // GET: HRMS_TRAVEL_MEAL_EXPENSE_MS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HRMS_TRAVEL_MEAL_EXPENSE_MS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Meal_Expense_ID,Meal_Expense_NM,Meal_Expense_Status")] HRMS_TRAVEL_MEAL_EXPENSE_MS hRMS_TRAVEL_MEAL_EXPENSE_MS)
        {
            if (ModelState.IsValid)
            {
                db.HRMS_TRAVEL_MEAL_EXPENSE_MS.Add(hRMS_TRAVEL_MEAL_EXPENSE_MS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hRMS_TRAVEL_MEAL_EXPENSE_MS);
        }

        // GET: HRMS_TRAVEL_MEAL_EXPENSE_MS/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_TRAVEL_MEAL_EXPENSE_MS hRMS_TRAVEL_MEAL_EXPENSE_MS = db.HRMS_TRAVEL_MEAL_EXPENSE_MS.Find(id);
            if (hRMS_TRAVEL_MEAL_EXPENSE_MS == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_TRAVEL_MEAL_EXPENSE_MS);
        }

        // POST: HRMS_TRAVEL_MEAL_EXPENSE_MS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Meal_Expense_ID,Meal_Expense_NM,Meal_Expense_Status")] HRMS_TRAVEL_MEAL_EXPENSE_MS hRMS_TRAVEL_MEAL_EXPENSE_MS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hRMS_TRAVEL_MEAL_EXPENSE_MS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hRMS_TRAVEL_MEAL_EXPENSE_MS);
        }

        // GET: HRMS_TRAVEL_MEAL_EXPENSE_MS/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_TRAVEL_MEAL_EXPENSE_MS hRMS_TRAVEL_MEAL_EXPENSE_MS = db.HRMS_TRAVEL_MEAL_EXPENSE_MS.Find(id);
            if (hRMS_TRAVEL_MEAL_EXPENSE_MS == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_TRAVEL_MEAL_EXPENSE_MS);
        }

        // POST: HRMS_TRAVEL_MEAL_EXPENSE_MS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            HRMS_TRAVEL_MEAL_EXPENSE_MS hRMS_TRAVEL_MEAL_EXPENSE_MS = db.HRMS_TRAVEL_MEAL_EXPENSE_MS.Find(id);
            db.HRMS_TRAVEL_MEAL_EXPENSE_MS.Remove(hRMS_TRAVEL_MEAL_EXPENSE_MS);
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
