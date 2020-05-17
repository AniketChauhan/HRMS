using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HRMS.Models;

namespace HRMS.Controllers
{
    public class TravelMileageConfigurationController : Controller
    {
        private HRMSEntities db = new HRMSEntities();

        // GET: HRMS_TRAVEL_MILEAGE_CONFIG
        public ActionResult Index()
        {
            return View(db.HRMS_TRAVEL_MILEAGE_CONFIG.ToList());
        }

        // GET: HRMS_TRAVEL_MILEAGE_CONFIG/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_TRAVEL_MILEAGE_CONFIG hRMS_TRAVEL_MILEAGE_CONFIG = db.HRMS_TRAVEL_MILEAGE_CONFIG.Find(id);
            if (hRMS_TRAVEL_MILEAGE_CONFIG == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_TRAVEL_MILEAGE_CONFIG);
        }

        // GET: HRMS_TRAVEL_MILEAGE_CONFIG/Create
        public ActionResult Create()
        {
            HRMS_TRAVEL_MILEAGE_CONFIG hRMS_TRAVEL_MILEAGE_CONFIG1 = new HRMS_TRAVEL_MILEAGE_CONFIG();
            List<HRMS_CATEGORY_GRADE> hRMS_CATEGORY_GRADEs1 = db.HRMS_CATEGORY_GRADE.ToList();
            List<HRMS_TravelGroupType_MS> hRMS_TravelGroupType_Ms1 = db.HRMS_TravelGroupType_MS.ToList();

            dynamic MultiView = new ExpandoObject();
            MultiView.hRMS_TRAVEL_MILEAGE_CONFIG = hRMS_TRAVEL_MILEAGE_CONFIG1;
            MultiView.hRMS_CATEGORY_GRADEs = hRMS_CATEGORY_GRADEs1;
            MultiView.hRMS_TravelGroupType_Ms = hRMS_TravelGroupType_Ms1;

            return View("Create", MultiView);

        }

        // POST: HRMS_TRAVEL_MILEAGE_CONFIG/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Travel_Mileage_ID,Travel_Group,Travel_Policy,Travel_Mileage_Two,Travel_Mileage_Four,Travel_Mileage_Moped,Travel_Mileage_Emp_Grade")] HRMS_TRAVEL_MILEAGE_CONFIG hRMS_TRAVEL_MILEAGE_CONFIG)
        {
            HRMS_TRAVEL_MILEAGE_CONFIG hRMS_TRAVEL_MILEAGE_CONFIG3 = new HRMS_TRAVEL_MILEAGE_CONFIG();
            HRMS_TRAVEL_MILEAGE_CONFIG hRMS_TRAVEL_MILEAGE_CONFIG1 = new HRMS_TRAVEL_MILEAGE_CONFIG();
            List<HRMS_CATEGORY_GRADE> hRMS_CATEGORY_GRADEs1 = db.HRMS_CATEGORY_GRADE.ToList();
            List<HRMS_TravelGroupType_MS> hRMS_TravelGroupType_Ms1 = db.HRMS_TravelGroupType_MS.ToList();

            dynamic MultiView = new ExpandoObject();
            MultiView.hRMS_TRAVEL_MILEAGE_CONFIG = hRMS_TRAVEL_MILEAGE_CONFIG1;
            MultiView.hRMS_CATEGORY_GRADEs = hRMS_CATEGORY_GRADEs1;
            MultiView.hRMS_TravelGroupType_Ms = hRMS_TravelGroupType_Ms1;


            if (ModelState.IsValid)
            {
                ViewBag.status = "";
                var T_G = Convert.ToInt64(Request["T_group"]);
                var T_P = Request["T_policy"];
                var T_M_T = Convert.ToDecimal(Request["T_M_two"]);
                var T_M_F = Convert.ToDecimal(Request["T_M_four"]);
                var T_M_M = Convert.ToDecimal(Request["T_M_moped"]);
                var T_M_E_G = Request["T_M_grade"];
                int[] nums = Array.ConvertAll(T_M_E_G.Split(','), int.Parse);
                if (T_G != 0 && T_M_E_G != null && T_M_E_G != "" && T_M_T >= 0 && T_M_F >= 0 && T_M_M >= 0)
                {

                    //if (db.HRMS_TRAVEL_MILEAGE_CONFIG.Where(rec => rec.Travel_Group == T_G).Any())
                    //{
                    //    ViewBag.status = "Group entry already exist!!" +
                    //        "if want to change you can edit";
                    //}
                    //else
                    //{
                    for (int i = 0; i < nums.Length; i++)
                    {
                        var b = nums[i].ToString();
                        if (db.HRMS_TRAVEL_MILEAGE_CONFIG.Where(rec => rec.Travel_Group == T_G && rec.Travel_Mileage_Emp_Grade == b).Any())
                        {

                            ViewBag.status = ViewBag.status + Environment.NewLine + "ID:" + b + "Group entry already exist!!" +
                        "if want to change you can edit";

                        }
                        else
                        {
                            hRMS_TRAVEL_MILEAGE_CONFIG3.Travel_Group = Convert.ToInt64(Request["T_group"]);
                            hRMS_TRAVEL_MILEAGE_CONFIG3.Travel_Policy = Request["T_policy"];
                            hRMS_TRAVEL_MILEAGE_CONFIG3.Travel_Mileage_Two = Convert.ToDecimal(Request["T_M_two"]);
                            hRMS_TRAVEL_MILEAGE_CONFIG3.Travel_Mileage_Four = Convert.ToDecimal(Request["T_M_four"]);
                            hRMS_TRAVEL_MILEAGE_CONFIG3.Travel_Mileage_Moped = Convert.ToDecimal(Request["T_M_moped"]);
                            hRMS_TRAVEL_MILEAGE_CONFIG3.Travel_Mileage_Emp_Grade = nums[i].ToString();
                            db.HRMS_TRAVEL_MILEAGE_CONFIG.Add(hRMS_TRAVEL_MILEAGE_CONFIG3);
                            db.SaveChanges();
                            ViewBag.status = ViewBag.status + Environment.NewLine + "ID:" + b + "Successfully Added!!";

                        }
                    }
                    //}
                }
                else
                {
                    ViewBag.status = "Please Entered all Data!!";

                }
            }

            return View("Create", MultiView);
        }

        // GET: HRMS_TRAVEL_MILEAGE_CONFIG/Edit/5
        public ActionResult Edit(long? id)
        {
            HRMS_TRAVEL_MILEAGE_CONFIG hRMS_TRAVEL_MILEAGE_CONFIG1 = db.HRMS_TRAVEL_MILEAGE_CONFIG.Find(id);
            List<HRMS_CATEGORY_GRADE> hRMS_CATEGORY_GRADEs1 = db.HRMS_CATEGORY_GRADE.ToList();
            List<HRMS_TravelGroupType_MS> hRMS_TravelGroupType_Ms1 = db.HRMS_TravelGroupType_MS.ToList();

            dynamic MultiView = new ExpandoObject();
            MultiView.hRMS_TRAVEL_MILEAGE_CONFIG = hRMS_TRAVEL_MILEAGE_CONFIG1;
            MultiView.hRMS_CATEGORY_GRADEs = hRMS_CATEGORY_GRADEs1;
            MultiView.hRMS_TravelGroupType_Ms = hRMS_TravelGroupType_Ms1;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_TRAVEL_MILEAGE_CONFIG hRMS_TRAVEL_MILEAGE_CONFIG = db.HRMS_TRAVEL_MILEAGE_CONFIG.Find(id);
            if (hRMS_TRAVEL_MILEAGE_CONFIG == null)
            {
                return HttpNotFound();
            }
            return View("Edit", MultiView);
        }

        // POST: HRMS_TRAVEL_MILEAGE_CONFIG/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Travel_Mileage_ID,Travel_Group,Travel_Policy,Travel_Mileage_Two,Travel_Mileage_Four,Travel_Mileage_Moped,Travel_Mileage_Emp_Grade")] HRMS_TRAVEL_MILEAGE_CONFIG hRMS_TRAVEL_MILEAGE_CONFIG)
        {
            HRMS_TRAVEL_MILEAGE_CONFIG hRMS_TRAVEL_MILEAGE_CONFIG1 = db.HRMS_TRAVEL_MILEAGE_CONFIG.Find(Convert.ToInt64(Request["T_M_id"]));
            List<HRMS_CATEGORY_GRADE> hRMS_CATEGORY_GRADEs1 = db.HRMS_CATEGORY_GRADE.ToList();
            List<HRMS_TravelGroupType_MS> hRMS_TravelGroupType_Ms1 = db.HRMS_TravelGroupType_MS.ToList();

            dynamic MultiView = new ExpandoObject();
            MultiView.hRMS_TRAVEL_MILEAGE_CONFIG = hRMS_TRAVEL_MILEAGE_CONFIG1;
            MultiView.hRMS_CATEGORY_GRADEs = hRMS_CATEGORY_GRADEs1;
            MultiView.hRMS_TravelGroupType_Ms = hRMS_TravelGroupType_Ms1;


            if (ModelState.IsValid)
            {
                ViewBag.status = "";
                var T_M_ID = Convert.ToInt64(Request["T_M_id"]);
                var T_G = Convert.ToInt64(Request["T_group"]);
                var T_P = Request["T_policy"];
                var T_M_T = Convert.ToDecimal(Request["T_M_two"]);
                var T_M_F = Convert.ToDecimal(Request["T_M_four"]);
                var T_M_M = Convert.ToDecimal(Request["T_M_moped"]);
                var T_M_E_G = Request["T_M_grade"];

                if (T_G != 0 && T_M_E_G != null && T_M_E_G != "" && T_M_T > 0 && T_M_F > 0 && T_M_M > 0)
                {

                    if (db.HRMS_TRAVEL_MILEAGE_CONFIG.Where(rec => rec.Travel_Group == T_G && rec.Travel_Mileage_Two == T_M_T && rec.Travel_Mileage_Four == T_M_F && rec.Travel_Mileage_Moped == T_M_M && rec.Travel_Mileage_Emp_Grade == T_M_E_G).Any())
                    {
                        ViewBag.status = "No changes occur";
                    }
                    else
                    {
                        if (db.HRMS_TRAVEL_MILEAGE_CONFIG.Where(rec => rec.Travel_Group == T_G && rec.Travel_Mileage_ID == T_M_ID).Any())
                        {
                            hRMS_TRAVEL_MILEAGE_CONFIG.Travel_Mileage_ID = Convert.ToInt64(Request["T_M_id"]);
                            hRMS_TRAVEL_MILEAGE_CONFIG.Travel_Group = Convert.ToInt64(Request["T_group"]);
                            hRMS_TRAVEL_MILEAGE_CONFIG.Travel_Policy = Request["T_policy"];
                            hRMS_TRAVEL_MILEAGE_CONFIG.Travel_Mileage_Two = Convert.ToDecimal(Request["T_M_two"]);
                            hRMS_TRAVEL_MILEAGE_CONFIG.Travel_Mileage_Four = Convert.ToDecimal(Request["T_M_four"]);
                            hRMS_TRAVEL_MILEAGE_CONFIG.Travel_Mileage_Moped = Convert.ToDecimal(Request["T_M_moped"]);
                            hRMS_TRAVEL_MILEAGE_CONFIG.Travel_Mileage_Emp_Grade = Request["T_M_grade"];
                            db.Entry(hRMS_TRAVEL_MILEAGE_CONFIG).State = EntityState.Modified;
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ViewBag.status = "You can't edit other group Data!!";
                        }
                    }
                }
                else
                {
                    ViewBag.status = "Please Entered all Data!!";

                }
                //////////////
                //db.Entry(hRMS_TRAVEL_MILEAGE_CONFIG).State = EntityState.Modified;
                //db.SaveChanges();
                //return RedirectToAction("Index");
            }
            return View("Edit", MultiView);
        }

        // GET: HRMS_TRAVEL_MILEAGE_CONFIG/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_TRAVEL_MILEAGE_CONFIG hRMS_TRAVEL_MILEAGE_CONFIG = db.HRMS_TRAVEL_MILEAGE_CONFIG.Find(id);
            if (hRMS_TRAVEL_MILEAGE_CONFIG == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_TRAVEL_MILEAGE_CONFIG);
        }

        // POST: HRMS_TRAVEL_MILEAGE_CONFIG/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            HRMS_TRAVEL_MILEAGE_CONFIG hRMS_TRAVEL_MILEAGE_CONFIG = db.HRMS_TRAVEL_MILEAGE_CONFIG.Find(id);
            db.HRMS_TRAVEL_MILEAGE_CONFIG.Remove(hRMS_TRAVEL_MILEAGE_CONFIG);
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


        // GET: HRMS_TRAVEL_MILEAGE_CONFIG/Edit/5
        public ActionResult Edit1(long? id)
        {
            ViewBag.Travel_policy = new SelectList(db.HRMS_TravelGroupType_MS, "ID", "Group_Name");
            ViewBag.Emp_grade = db.HRMS_CATEGORY_GRADE.ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_TRAVEL_MILEAGE_CONFIG hRMS_TRAVEL_MILEAGE_CONFIG = db.HRMS_TRAVEL_MILEAGE_CONFIG.Find(id);
            if (hRMS_TRAVEL_MILEAGE_CONFIG == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_TRAVEL_MILEAGE_CONFIG);
        }

        // POST: HRMS_TRAVEL_MILEAGE_CONFIG/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit1([Bind(Include = "Travel_Mileage_ID,Travel_Group,Travel_Policy,Travel_Mileage_Two,Travel_Mileage_Four,Travel_Mileage_Moped,Travel_Mileage_Emp_Grade")] HRMS_TRAVEL_MILEAGE_CONFIG hRMS_TRAVEL_MILEAGE_CONFIG)
        {
            ViewBag.Travel_policy = new SelectList(db.HRMS_TravelGroupType_MS, "ID", "Group_Name");
            ViewBag.Emp_grade = db.HRMS_CATEGORY_GRADE.ToList();
            if (ModelState.IsValid)
            {

                db.Entry(hRMS_TRAVEL_MILEAGE_CONFIG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hRMS_TRAVEL_MILEAGE_CONFIG);
        }

    }
}
