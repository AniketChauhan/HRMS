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
    public class TravelModeConfigurationController : Controller
    {
        private HRMSEntities db = new HRMSEntities();

        // GET: HRMS_TRAVEL_MODE_CONFIG
        public ActionResult Index()
        {

            return View(db.HRMS_TRAVEL_MODE_CONFIG.ToList());
        }

        // GET: HRMS_TRAVEL_MODE_CONFIG/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_TRAVEL_MODE_CONFIG hRMS_TRAVEL_MODE_CONFIG = db.HRMS_TRAVEL_MODE_CONFIG.Find(id);
            if (hRMS_TRAVEL_MODE_CONFIG == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_TRAVEL_MODE_CONFIG);
        }

        //public JsonResult GetgradeList(long policy_id)
        //{
        //    db.Configuration.ProxyCreationEnabled = false;
        //    long[] a = db.HRMS_EMP_GRA_POL.Where(x => x.Pol_ID == policy_id).Select(x => x.Gra_ID).ToList().ToArray();
        //    List<List<HRMS_CATEGORY_GRADE>> gradeList = new List<List<HRMS_CATEGORY_GRADE>>();
        //    for (int j=0;j<a.Length;j++)
        //    {

        //        var g_l = db.HRMS_CATEGORY_GRADE.Where(x => x.Category_ID == a[j]).FirstOrDefault() ;
        //        gradeList.Add(g_l);
        //    }
        //    //List<HRMS_CATEGORY_GRADE> gradeList = db.HRMS_CATEGORY_GRADE.Where(x => x.CountryID == CountryID).ToList();
        //    return Json(gradeList, JsonRequestBehavior.AllowGet);
        //}

        // GET: HRMS_TRAVEL_MODE_CONFIG/Create
        public ActionResult Create()
        {

            HRMS_TRAVEL_MODE_CONFIG hRMS_TRAVEL_MODE_CONFIG1 = new HRMS_TRAVEL_MODE_CONFIG();
            List<HRMS_CATEGORY_GRADE> hRMS_CATEGORY_GRADEs1 = db.HRMS_CATEGORY_GRADE.ToList();
            List<HRMS_TravelGroupType_MS> hRMS_TravelGroupType_Ms1 = db.HRMS_TravelGroupType_MS.ToList();

            dynamic MultiView = new ExpandoObject();
            MultiView.hRMS_TRAVEL_MODE_CONFIG = hRMS_TRAVEL_MODE_CONFIG1;
            MultiView.hRMS_CATEGORY_GRADEs = hRMS_CATEGORY_GRADEs1;
            MultiView.hRMS_TravelGroupType_Ms = hRMS_TravelGroupType_Ms1;

            return View("Create", MultiView);


        }

        // POST: HRMS_TRAVEL_MODE_CONFIG/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Travel_Mode_ID,Travel_Group,Travel_Policy,Travel_By_Auto,Travel_By_Taxi,Travel_Local_Conveyance_Max,Travel_Emp_Grade")] HRMS_TRAVEL_MODE_CONFIG hRMS_TRAVEL_MODE_CONFIG)
        {
            HRMS_TRAVEL_MODE_CONFIG hRMS_TRAVEL_MODE_CONFIG3 = new HRMS_TRAVEL_MODE_CONFIG();
            HRMS_TRAVEL_MODE_CONFIG hRMS_TRAVEL_MODE_CONFIG1 = new HRMS_TRAVEL_MODE_CONFIG();
            List<HRMS_CATEGORY_GRADE> hRMS_CATEGORY_GRADEs1 = db.HRMS_CATEGORY_GRADE.ToList();
            List<HRMS_TravelGroupType_MS> hRMS_TravelGroupType_Ms1 = db.HRMS_TravelGroupType_MS.ToList();
            //ViewBag.grade = db.HRMS_CATEGORY_GRADE.Where(rec => rec.Category_ID==);
            dynamic MultiView = new ExpandoObject();
            MultiView.hRMS_TRAVEL_MODE_CONFIG = hRMS_TRAVEL_MODE_CONFIG1;
            MultiView.hRMS_CATEGORY_GRADEs = hRMS_CATEGORY_GRADEs1;
            MultiView.hRMS_TravelGroupType_Ms = hRMS_TravelGroupType_Ms1;


            if (ModelState.IsValid)
            {
                ViewBag.status = "";
                var T_G = Convert.ToInt64(Request["T_group"]);
                //var T_P = Request["T_policy"];
                //var T_B_A = Convert.ToBoolean(Request["T_auto"]);
                //var T_B_T = Convert.ToBoolean(Request["T_taxi"]);
                //var T_L_C_M = Convert.ToDecimal(Request["T_max"]);
                var T_E_G = Request["T_grade"];
                int[] nums = Array.ConvertAll(T_E_G.Split(','), int.Parse);

                if (T_G != 0 && T_E_G != null && T_E_G != "")
                {

                    //if (db.HRMS_TRAVEL_MODE_CONFIG.Where(rec => rec.Travel_Group == T_G).Any())
                    //{
                    //    ViewBag.status = "Group entry already exist!!" +
                    //        "if want to change you can edit";
                    //}
                    //else
                    //{
                    for (int i = 0; i < nums.Length; i++)
                    {
                        var b = nums[i].ToString();
                        if (db.HRMS_TRAVEL_MODE_CONFIG.Where(rec => rec.Travel_Group == T_G && rec.Travel_Emp_Grade == b).Any())
                        {

                            ViewBag.status = ViewBag.status + Environment.NewLine + "ID:" + b + "Group entry already exist!!" +
                        "if want to change you can edit";

                        }
                        else
                        {
                            hRMS_TRAVEL_MODE_CONFIG3.Travel_Group = Convert.ToInt64(Request["T_group"]);
                            hRMS_TRAVEL_MODE_CONFIG3.Travel_Policy = Request["T_policy"];
                            hRMS_TRAVEL_MODE_CONFIG3.Travel_By_Auto = Convert.ToBoolean(Request["T_auto"]);
                            hRMS_TRAVEL_MODE_CONFIG3.Travel_By_Taxi = Convert.ToBoolean(Request["T_taxi"]);
                            hRMS_TRAVEL_MODE_CONFIG3.Travel_Local_Conveyance_Max = Convert.ToDecimal(Request["T_max"]);
                            hRMS_TRAVEL_MODE_CONFIG3.Travel_Emp_Grade = nums[i].ToString();
                            db.HRMS_TRAVEL_MODE_CONFIG.Add(hRMS_TRAVEL_MODE_CONFIG3);
                            db.SaveChanges();
                            ViewBag.status = ViewBag.status + Environment.NewLine + "ID:" + b + "Successfully Added!!";
                        }
                    }

                    //hRMS_TRAVEL_MODE_CONFIG3.Travel_Group = Convert.ToInt64(Request["T_group"]);
                    //hRMS_TRAVEL_MODE_CONFIG3.Travel_Policy = Request["T_policy"];
                    //hRMS_TRAVEL_MODE_CONFIG3.Travel_By_Auto = Convert.ToBoolean(Request["T_auto"]);
                    //hRMS_TRAVEL_MODE_CONFIG3.Travel_By_Taxi = Convert.ToBoolean(Request["T_taxi"]);
                    //hRMS_TRAVEL_MODE_CONFIG3.Travel_Local_Conveyance_Max = Convert.ToDecimal(Request["T_max"]);
                    //hRMS_TRAVEL_MODE_CONFIG3.Travel_Emp_Grade = Request["T_grade"];
                    //db.HRMS_TRAVEL_MODE_CONFIG.Add(hRMS_TRAVEL_MODE_CONFIG3);
                    //db.SaveChanges();
                    //ViewBag.status = "Successfully Added!!";

                    return View("Create", MultiView);
                    //}
                }
                else
                {
                    ViewBag.status = "Please Entered all Data!!";

                }



            }

            return View("Create", MultiView);
        }

        // GET: HRMS_TRAVEL_MODE_CONFIG/Edit/5
        public ActionResult Edit(long? id)
        {
            HRMS_TRAVEL_MODE_CONFIG hRMS_TRAVEL_MODE_CONFIG1 = db.HRMS_TRAVEL_MODE_CONFIG.Where(rec => rec.Travel_Mode_ID == id).FirstOrDefault();
            List<HRMS_CATEGORY_GRADE> hRMS_CATEGORY_GRADEs1 = db.HRMS_CATEGORY_GRADE.ToList();
            List<HRMS_TravelGroupType_MS> hRMS_TravelGroupType_Ms1 = db.HRMS_TravelGroupType_MS.ToList();

            dynamic MultiView = new ExpandoObject();
            MultiView.hRMS_TRAVEL_MODE_CONFIG = hRMS_TRAVEL_MODE_CONFIG1;
            MultiView.hRMS_CATEGORY_GRADEs = hRMS_CATEGORY_GRADEs1;
            MultiView.hRMS_TravelGroupType_Ms = hRMS_TravelGroupType_Ms1;


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_TRAVEL_MODE_CONFIG hRMS_TRAVEL_MODE_CONFIG = db.HRMS_TRAVEL_MODE_CONFIG.Find(id);
            if (hRMS_TRAVEL_MODE_CONFIG == null)
            {
                return HttpNotFound();
            }
            return View("Edit", MultiView);
        }

        // POST: HRMS_TRAVEL_MODE_CONFIG/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Travel_Mode_ID,Travel_Group,Travel_Policy,Travel_By_Auto,Travel_By_Taxi,Travel_Local_Conveyance_Max,Travel_Emp_Grade")]HRMS_TRAVEL_MODE_CONFIG hRMS_TRAVEL_MODE_CONFIG5)
        {

            HRMS_TRAVEL_MODE_CONFIG hRMS_TRAVEL_MODE_CONFIG1 = db.HRMS_TRAVEL_MODE_CONFIG.Find(Convert.ToInt64(Request["T_id"]));
            List<HRMS_CATEGORY_GRADE> hRMS_CATEGORY_GRADEs1 = db.HRMS_CATEGORY_GRADE.ToList();
            List<HRMS_TravelGroupType_MS> hRMS_TravelGroupType_Ms1 = db.HRMS_TravelGroupType_MS.ToList();

            dynamic MultiView = new ExpandoObject();
            MultiView.hRMS_TRAVEL_MODE_CONFIG = hRMS_TRAVEL_MODE_CONFIG1;
            MultiView.hRMS_CATEGORY_GRADEs = hRMS_CATEGORY_GRADEs1;
            MultiView.hRMS_TravelGroupType_Ms = hRMS_TravelGroupType_Ms1;

            //HRMS_TRAVEL_MODE_CONFIG hRMS_TRAVEL_MODE_CONFIG3 = new HRMS_TRAVEL_MODE_CONFIG();
            if (ModelState.IsValid)
            {
                long T_G1 = 0;
                ViewBag.status = "";
                var T_ID = Convert.ToInt64(Request["T_id"]);
                T_G1 = Convert.ToInt64(Request["T_group"]);
                var T_P = Request["T_policy"];
                var T_B_A = Convert.ToBoolean(Request["T_auto"]);
                var T_B_T = Convert.ToBoolean(Request["T_taxi"]);
                var T_L_C_M = Convert.ToDecimal(Request["T_max"]);
                var T_E_G = Request["T_grade"];

                if (T_G1 != 0 && T_E_G != null && T_E_G != "")
                {

                    if (db.HRMS_TRAVEL_MODE_CONFIG.Where(rec => rec.Travel_Group == T_G1 && rec.Travel_Mode_ID == T_ID && rec.Travel_By_Auto == T_B_A && rec.Travel_By_Taxi == T_B_T && rec.Travel_Local_Conveyance_Max == T_L_C_M && rec.Travel_Emp_Grade == T_E_G).Any())
                    {
                        ViewBag.status = "No changes occurs";

                    }
                    else
                    {
                        if (db.HRMS_TRAVEL_MODE_CONFIG.Where(rec => rec.Travel_Group == T_G1 && rec.Travel_Mode_ID == T_ID).Any())
                        {
                            hRMS_TRAVEL_MODE_CONFIG5.Travel_Mode_ID = Convert.ToInt64(Request["T_id"]);
                            hRMS_TRAVEL_MODE_CONFIG5.Travel_Group = Convert.ToInt64(Request["T_group"]);
                            hRMS_TRAVEL_MODE_CONFIG5.Travel_Policy = Request["T_policy"];
                            hRMS_TRAVEL_MODE_CONFIG5.Travel_By_Auto = Convert.ToBoolean(Request["T_auto"]);
                            hRMS_TRAVEL_MODE_CONFIG5.Travel_By_Taxi = Convert.ToBoolean(Request["T_taxi"]);
                            hRMS_TRAVEL_MODE_CONFIG5.Travel_Local_Conveyance_Max = Convert.ToDecimal(Request["T_max"]);
                            hRMS_TRAVEL_MODE_CONFIG5.Travel_Emp_Grade = Request["T_grade"];

                            //db.Entry(hRMS_TRAVEL_MODE_CONFIG).State = EntityState.Detached;
                            db.Entry(hRMS_TRAVEL_MODE_CONFIG5).State = EntityState.Modified;
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
                    ViewBag.status = "You Entered Invalid Data!!";

                }
            }
            return View("Edit", MultiView);
        }

        // GET: HRMS_TRAVEL_MODE_CONFIG/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_TRAVEL_MODE_CONFIG hRMS_TRAVEL_MODE_CONFIG = db.HRMS_TRAVEL_MODE_CONFIG.Find(id);
            if (hRMS_TRAVEL_MODE_CONFIG == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_TRAVEL_MODE_CONFIG);
        }

        // POST: HRMS_TRAVEL_MODE_CONFIG/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            HRMS_TRAVEL_MODE_CONFIG hRMS_TRAVEL_MODE_CONFIG = db.HRMS_TRAVEL_MODE_CONFIG.Find(id);
            db.HRMS_TRAVEL_MODE_CONFIG.Remove(hRMS_TRAVEL_MODE_CONFIG);
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

        //        *******************************
        // GET: HRMS_TRAVEL_MODE_CONFIG/Edit/5
        public ActionResult Edit1(long? id)
        {
            ViewBag.Travel_policy = new SelectList(db.HRMS_TravelGroupType_MS, "ID", "Group_Name");
            //ViewBag.Emp_grade = new SelectList(db.HRMS_CATEGORY_GRADE, "Category_ID", "Grade_Name", "Grade_Detail");
            ViewBag.Emp_grade = db.HRMS_CATEGORY_GRADE.ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_TRAVEL_MODE_CONFIG hRMS_TRAVEL_MODE_CONFIG = db.HRMS_TRAVEL_MODE_CONFIG.Find(id);
            if (hRMS_TRAVEL_MODE_CONFIG == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_TRAVEL_MODE_CONFIG);
        }

        // POST: HRMS_TRAVEL_MODE_CONFIG/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit1([Bind(Include = "Travel_Mode_ID,Travel_Group,Travel_Policy,Travel_By_Auto,Travel_By_Taxi,Travel_Local_Conveyance_Max,Travel_Emp_Grade")]HRMS_TRAVEL_MODE_CONFIG hRMS_TRAVEL_MODE_CONFIG)
        {
            ViewBag.status = "";
            ViewBag.Travel_policy = new SelectList(db.HRMS_TravelGroupType_MS, "ID", "Group_Name");
            ViewBag.Emp_grade = db.HRMS_CATEGORY_GRADE.ToList();
            //HRMS_TRAVEL_MODE_CONFIG hRMS_TRAVEL_MODE_CONFIG3 = new HRMS_TRAVEL_MODE_CONFIG();
            if (ModelState.IsValid)
            {
                db.Entry(hRMS_TRAVEL_MODE_CONFIG).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(hRMS_TRAVEL_MODE_CONFIG);

        }

    }




}
