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
    public class TrainingMaterialTransactionController : Controller
    {
        private HRMSEntities db = new HRMSEntities();

        public decimal GetTotalRate(long? id)
        {
            decimal Total = 0;
            //db.HRMS_TRAINING_MATERIALSET.Where(x => x.ProgDet_ID == id.Value).Sum(x=>x.MaterialSet_Amt);


            int Count = db.HRMS_TRAINING_MATERIALSET.Where(x => x.ProgDet_ID == id).Count();
            if (Count != 0)
            {
                Total = db.HRMS_TRAINING_MATERIALSET.Where(x => x.ProgDet_ID == id).Sum(x => x.MaterialSet_Amt);
            }
            return Total;
        }
        public decimal GetTotalBudget(long? id, string value1)
        {
            decimal Total = 0;
            //db.HRMS_TRAINING_MATERIALSET.Where(x => x.ProgDet_ID == id.Value).Sum(x=>x.MaterialSet_Amt);


            int Count = db.HRMS_TRAINING_MATERIALSET.Where(x => x.ProgDet_ID == id && x.Bud_Act == value1).Count();
            if (Count != 0)
            {
                Total = db.HRMS_TRAINING_MATERIALSET.Where(x => x.ProgDet_ID == id && x.Bud_Act == value1).Sum(x => x.MaterialSet_Amt);
            }
            return Total;
        }
        public decimal GetTotalActual(long? id, string value1)
        {
            decimal Total = 0;
            //db.HRMS_TRAINING_MATERIALSET.Where(x => x.ProgDet_ID == id.Value).Sum(x=>x.MaterialSet_Amt);


            int Count = db.HRMS_TRAINING_MATERIALSET.Where(x => x.ProgDet_ID == id && x.Bud_Act == value1).Count();
            if (Count != 0)
            {
                Total = db.HRMS_TRAINING_MATERIALSET.Where(x => x.ProgDet_ID == id && x.Bud_Act == value1).Sum(x => x.MaterialSet_Amt);
            }
            return Total;
        }


        public decimal GetDefaultRate(long? id)
        {
            if (id == 0)
            {
                return 0;
            }

            //checking for configuration
            HRMS_TRAINING_MATERIAL_MS objl = db.HRMS_TRAINING_MATERIAL_MS.Where(x => x.Material_ID == id).FirstOrDefault();
            //HRMS_EMP_GRA_POL obj1 = db.HRMS_EMP_GRA_POL.Where(x => x.Emp_ID == emp_id).FirstOrDefault();
            //decimal val = db.HRMS_TRAVEL_MILEAGE_CONFIG.Where(x => x.Travel_Mileage_Emp_Grade.StartsWith(obj1.Gra_ID.ToString()) && x.Travel_Group == obj1.Pol_ID).Select(x => x.Travel_Mileage_Four).FirstOrDefault();
            decimal Amount = objl.Material_Rate;

            return Amount;


        }

        public ActionResult Data(long? id)
        {
            if (id != null)
            {
                HRMS_TRAINING_MATERIAL_MS obj = db.HRMS_TRAINING_MATERIAL_MS.Where(x => x.Material_ID == id.Value).FirstOrDefault();

                if (obj != null)
                {
                    var result = new { obj.Material_Name };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AutoComplete(string prefix)
        {

            var Result1 = (from Result in db.HRMS_TRAINING_MATERIAL_MS
                           where Result.Material_Name.StartsWith(prefix)
                           select new
                           {
                               label = Result.Material_Name,
                               val = Result.Material_ID
                           }).ToList();

            return Json(Result1);
        }


        // GET: HRMS_TRAINING_MATERIALSET
        public ActionResult Index()
        {
            var hRMS_TRAINING_MATERIALSET = db.HRMS_TRAINING_MATERIALSET.Include(h => h.HRMS_ProgramDetail).Include(h => h.HRMS_TRAINING_MATERIAL_MS);
            return View(hRMS_TRAINING_MATERIALSET.ToList());
        }
        public ActionResult Index1()
        {
            var hRMS_ProgramDetail = db.HRMS_ProgramDetail.ToList();
            return View(hRMS_ProgramDetail);
        }

        // GET: HRMS_TRAINING_MATERIALSET/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_TRAINING_MATERIALSET hRMS_TRAINING_MATERIALSET = db.HRMS_TRAINING_MATERIALSET.Find(id);
            if (hRMS_TRAINING_MATERIALSET == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_TRAINING_MATERIALSET);
        }

        // GET: HRMS_TRAINING_MATERIALSET/Create
        public ActionResult Create(long? id)
        {
            ViewBag.ProgDet_ID = new SelectList(db.HRMS_ProgramDetail, "ID", "ProgramName");
            ViewBag.Material_ID = new SelectList(db.HRMS_TRAINING_MATERIAL_MS, "Material_ID", "Material_Name");
            HRMS_ProgramDetail hRMS_ProgramDetail1 = db.HRMS_ProgramDetail.Find(id);
            HRMS_TRAINING_MATERIALSET hRMS_TRAINING_MATERIALSET1 = new HRMS_TRAINING_MATERIALSET();
            List<HRMS_TRAINING_MATERIALSET> hRMS_TRAINING_MATERIALSET3 = db.HRMS_TRAINING_MATERIALSET.Where(x => x.ProgDet_ID == id).ToList();
            dynamic MultiView = new ExpandoObject();
            MultiView.hRMS_ProgramDetail = hRMS_ProgramDetail1;
            MultiView.hRMS_TRAINING_MATERIALSET = hRMS_TRAINING_MATERIALSET1;
            MultiView.hRMS_TRAINING_MATERIALSET4 = hRMS_TRAINING_MATERIALSET3;

            return View("Create", MultiView);


            //return View();
        }

        // POST: HRMS_TRAINING_MATERIALSET/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaterialSet_ID,ProgDet_ID,Material_ID,Material_Name,Bud_Act,MaterialSet_Qty,MaterialSet_Rate,MaterialSet_Amt")] HRMS_TRAINING_MATERIALSET hRMS_TRAINING_MATERIALSET)
        {
            ViewBag.ProgDet_ID = new SelectList(db.HRMS_ProgramDetail, "ID", "ProgramName", hRMS_TRAINING_MATERIALSET.ProgDet_ID);
            ViewBag.Material_ID = new SelectList(db.HRMS_TRAINING_MATERIAL_MS, "Material_ID", "Material_Name", hRMS_TRAINING_MATERIALSET.Material_ID);
            long id = Convert.ToInt64(Request["Prog_id"]);
            HRMS_ProgramDetail hRMS_ProgramDetail1 = db.HRMS_ProgramDetail.Find(id);
            HRMS_TRAINING_MATERIALSET hRMS_TRAINING_MATERIALSET1 = new HRMS_TRAINING_MATERIALSET();
            List<HRMS_TRAINING_MATERIALSET> hRMS_TRAINING_MATERIALSET3 = db.HRMS_TRAINING_MATERIALSET.Where(x => x.ProgDet_ID == id).ToList();
            dynamic MultiView = new ExpandoObject();
            MultiView.hRMS_ProgramDetail = hRMS_ProgramDetail1;
            MultiView.hRMS_TRAINING_MATERIALSET = hRMS_TRAINING_MATERIALSET1;
            MultiView.hRMS_TRAINING_MATERIALSET4 = hRMS_TRAINING_MATERIALSET3;


            if (ModelState.IsValid)
            {
                long a = Convert.ToInt64(Request["Prog_id"]);
                long b = Convert.ToInt64(Request["M_id"]);

                if (b != 0 && a != 0)
                {
                    ViewBag.Status = "";
                    if (!db.HRMS_TRAINING_MATERIALSET.Where(x => x.ProgDet_ID == a && x.Material_ID == b).Any())
                    {
                        HRMS_ProgramDetail hRMS_ProgramDetaill = db.HRMS_ProgramDetail.Find(id);
                        HRMS_TRAINING_MATERIAL_MS hRMS_TRAINING_MATERIAL_MS = db.HRMS_TRAINING_MATERIAL_MS.Find(b);
                        hRMS_TRAINING_MATERIALSET.ProgDet_ID = a;
                        hRMS_TRAINING_MATERIALSET.Material_ID = b;
                        hRMS_TRAINING_MATERIALSET.Material_Name = hRMS_TRAINING_MATERIAL_MS.Material_Name;
                        hRMS_TRAINING_MATERIALSET.MaterialSet_Qty = Convert.ToInt64(Request["M_qty"]);
                        hRMS_TRAINING_MATERIALSET.MaterialSet_Rate = Convert.ToDecimal(Request["M_rate"]);
                        hRMS_TRAINING_MATERIALSET.MaterialSet_Amt = Convert.ToDecimal(Request["M_amt"]);
                        hRMS_TRAINING_MATERIALSET.Bud_Act = Convert.ToString(Request["Bud_Act"]);
                        db.HRMS_TRAINING_MATERIALSET.Add(hRMS_TRAINING_MATERIALSET);
                        db.SaveChanges();
                        decimal total = GetTotalRate(a);
                        if (hRMS_ProgramDetaill.Program_Amount != total)
                        {
                            hRMS_ProgramDetaill.Program_Amount = total;
                            db.Entry(hRMS_ProgramDetaill).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        if (hRMS_ProgramDetaill.Material == false)
                        {
                            hRMS_ProgramDetaill.Material = true;
                            db.Entry(hRMS_ProgramDetaill).State = EntityState.Modified;
                            db.SaveChanges();
                        }


                        HRMS_ProgramDetail hRMS_ProgramDetail11 = db.HRMS_ProgramDetail.Find(id);
                        HRMS_TRAINING_MATERIALSET hRMS_TRAINING_MATERIALSET11 = new HRMS_TRAINING_MATERIALSET();
                        List<HRMS_TRAINING_MATERIALSET> hRMS_TRAINING_MATERIALSET31 = db.HRMS_TRAINING_MATERIALSET.Where(x => x.ProgDet_ID == id).ToList();
                        MultiView = new ExpandoObject();
                        MultiView.hRMS_ProgramDetail = hRMS_ProgramDetail11;
                        MultiView.hRMS_TRAINING_MATERIALSET = hRMS_TRAINING_MATERIALSET11;
                        MultiView.hRMS_TRAINING_MATERIALSET4 = hRMS_TRAINING_MATERIALSET31;
                        ViewBag.Status = "New Material added successfully!!!";
                        return View("Create", MultiView);
                    }
                    ViewBag.Status = "Material already assigned to this Program";
                    return View("Create", MultiView);
                }
            }
            return View("Create", MultiView);
        }

        // GET: HRMS_TRAINING_MATERIALSET/Edit/5
        public ActionResult Edit(long? id)
        {
            //HRMS_TRAINING_MATERIALSET hRMS_TRAINING_MATERIALSET = db.HRMS_TRAINING_MATERIALSET.Find(id);






            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_TRAINING_MATERIALSET hRMS_TRAINING_MATERIALSET = db.HRMS_TRAINING_MATERIALSET.Find(id);
            if (hRMS_TRAINING_MATERIALSET == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProgDet_ID = new SelectList(db.HRMS_ProgramDetail, "ID", "ProgramName", hRMS_TRAINING_MATERIALSET.ProgDet_ID);
            ViewBag.Material_ID = new SelectList(db.HRMS_TRAINING_MATERIAL_MS, "Material_ID", "Material_Name", hRMS_TRAINING_MATERIALSET.Material_ID);

            return View(hRMS_TRAINING_MATERIALSET);
        }

        // POST: HRMS_TRAINING_MATERIALSET/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaterialSet_ID,ProgDet_ID,Material_ID,Material_Name,Bud_Act,MaterialSet_Qty,MaterialSet_Rate,MaterialSet_Amt")] HRMS_TRAINING_MATERIALSET hRMS_TRAINING_MATERIALSET)
        {
            if (ModelState.IsValid)
            {
                HRMS_ProgramDetail hRMS_ProgramDetaill = db.HRMS_ProgramDetail.Find(hRMS_TRAINING_MATERIALSET.ProgDet_ID);
                long b = Convert.ToInt64(Request["M_id"]);
                HRMS_TRAINING_MATERIAL_MS hRMS_TRAINING_MATERIAL_MS = db.HRMS_TRAINING_MATERIAL_MS.Find(b);
                hRMS_TRAINING_MATERIALSET.Material_ID = b;
                hRMS_TRAINING_MATERIALSET.Material_Name = hRMS_TRAINING_MATERIAL_MS.Material_Name;
                hRMS_TRAINING_MATERIALSET.MaterialSet_Qty = Convert.ToInt64(Request["M_qty"]);
                hRMS_TRAINING_MATERIALSET.MaterialSet_Rate = Convert.ToDecimal(Request["M_rate"]);
                hRMS_TRAINING_MATERIALSET.MaterialSet_Amt = Convert.ToDecimal(Request["M_amt"]);
                db.Entry(hRMS_TRAINING_MATERIALSET).State = EntityState.Modified;
                db.SaveChanges();
                decimal total = GetTotalRate(hRMS_ProgramDetaill.ID);
                if (hRMS_ProgramDetaill.Program_Amount != total)
                {
                    hRMS_ProgramDetaill.Program_Amount = total;
                    db.Entry(hRMS_ProgramDetaill).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("Index1");
            }
            //ViewBag.ProgDet_ID = new SelectList(db.HRMS_ProgramDetail, "ID", "ProgramName", hRMS_TRAINING_MATERIALSET.ProgDet_ID);
            //ViewBag.Material_ID = new SelectList(db.HRMS_TRAINING_MATERIAL_MS, "Material_ID", "Material_Name", hRMS_TRAINING_MATERIALSET.Material_ID);
            return View(hRMS_TRAINING_MATERIALSET);
        }

        //// GET: HRMS_TRAINING_MATERIALSET/Delete/5
        //public ActionResult Delete(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    HRMS_TRAINING_MATERIALSET hRMS_TRAINING_MATERIALSET = db.HRMS_TRAINING_MATERIALSET.Find(id);
        //    if (hRMS_TRAINING_MATERIALSET == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(hRMS_TRAINING_MATERIALSET);
        //}

        //// POST: HRMS_TRAINING_MATERIALSET/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(long id)
        //{
        //    HRMS_TRAINING_MATERIALSET hRMS_TRAINING_MATERIALSET = db.HRMS_TRAINING_MATERIALSET.Find(id);
        //    db.HRMS_TRAINING_MATERIALSET.Remove(hRMS_TRAINING_MATERIALSET);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}


        public bool Delete(long id)
        {
            HRMS_TRAINING_MATERIALSET hRMS_TRAINING_MATERIALSET = db.HRMS_TRAINING_MATERIALSET.Find(id);
            if (hRMS_TRAINING_MATERIALSET != null)
            {
                long P_id = hRMS_TRAINING_MATERIALSET.ProgDet_ID;
                db.HRMS_TRAINING_MATERIALSET.Remove(hRMS_TRAINING_MATERIALSET);
                db.SaveChanges();
                HRMS_ProgramDetail hRMS_ProgramDetaill = db.HRMS_ProgramDetail.Find(P_id);
                if (!db.HRMS_TRAINING_MATERIALSET.Where(x => x.ProgDet_ID == P_id).Any())
                {

                    if (hRMS_ProgramDetaill.Material == true)
                    {
                        hRMS_ProgramDetaill.Material = false;
                        db.Entry(hRMS_ProgramDetaill).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                }
                decimal total = GetTotalRate(hRMS_ProgramDetaill.ID);
                if (hRMS_ProgramDetaill.Program_Amount != total)
                {
                    hRMS_ProgramDetaill.Program_Amount = total;
                    db.Entry(hRMS_ProgramDetaill).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return true;
            }
            else
            {
                return false;
            }

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
