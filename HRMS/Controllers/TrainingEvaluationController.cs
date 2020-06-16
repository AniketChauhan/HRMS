using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HRMS.Models;
using HRMS.ViewModel;

namespace HRMS.Controllers
{
    [Authorize(Roles ="admin")]
    public class TrainingEvaluationController : Controller
    {
        private HRMSEntities db = new HRMSEntities();

        // GET: TrainingEvaluation
        public JsonResult GetQuestions(string headerId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            long head = Convert.ToInt64(headerId);

            List<HRMS_Evalution_Question> QuestionList = db.HRMS_Evalution_Question.Where(x => x.Header_ID == head).ToList();
            return Json(QuestionList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View(db.HRMS_TrainingEvalution_Header.ToList());
        }
        public ActionResult Create()
        {
            EvalutionCommon evalutionCommon = new EvalutionCommon();
            List<HRMS_Evalution_Question> _Evalution_Questions = new List<HRMS_Evalution_Question>();
            evalutionCommon.QuestionLists = _Evalution_Questions;
            return View(evalutionCommon);
        }
        [HttpPost]
        public JsonResult Create(string Head)
        {

            HRMS_TrainingEvalution_Header obj = new HRMS_TrainingEvalution_Header();
            bool isValid = db.HRMS_TrainingEvalution_Header.Any(x => x.Header == Head);
            if (!isValid)
            {
                obj.Header = Head;
                //obj.CreatedBy = Convert.ToInt64(Session["id"]);
                //obj.CreateDate = DateTime.Now.Date;
                db.HRMS_TrainingEvalution_Header.Add(obj);
                db.SaveChanges();
                ModelState.Clear();
                var header = db.HRMS_TrainingEvalution_Header.Where(rec => rec.Header == Head).FirstOrDefault();

                ViewBag.HeaderStatus = "Header Added successfully.";
                return Json(new
                {
                    headerId = header.ID,
                    headername = header.Header
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ViewBag.HeaderStatus = "Header is already exist.";
                return Json(new
                {
                    headerId = 0,
                    headername = "it is already exist!"
                }, JsonRequestBehavior.AllowGet);
                //if (ModelState.IsValid)
                //{
                //    var headername = db.HRMS_TrainingEvalution_Header.FirstOrDefault(rec => rec.Header == Header.header.Header);
                //    if (headername == null)
                //    {
                //        db.HRMS_TrainingEvalution_Header.Add(Header.header);
                //        db.SaveChanges();
                //        ModelState.Clear();
                //        var header = db.HRMS_TrainingEvalution_Header.Where(rec => rec.Header == Header.header.Header).FirstOrDefault();
                //        ViewBag.HeaderStatus = "Header Added successfully.";
                //        return Json(new
                //        {
                //            headerId = header.ID,
                //            headername = header.Header
                //        }, JsonRequestBehavior.AllowGet);
                //    }
                //    else
                //    {
                //        ViewBag.HeaderStatus = "Header is already exist.";
                //        var header = "it is already exist";
                //        return Json(new
                //        {
                //            headerId = 0,
                //            headername = "it is already exist!"
                //        }, JsonRequestBehavior.AllowGet);
                //    }



                //        //}
                //    }
                //    return Json("its not working", JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult CreateQuestion(HRMS_Evalution_Question question)
        {
            var QuestionName = db.HRMS_Evalution_Question.FirstOrDefault(rec => rec.Question == question.Question && rec.Header_ID == question.Header_ID);
            if (QuestionName == null)
            {
                db.HRMS_Evalution_Question.Add(question);
                db.SaveChanges();
                ModelState.Clear();
                var list = db.HRMS_Evalution_Question.Where(rec => rec.Header_ID == question.Header_ID).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ViewBag.QuestionStatus = "Question is already exist!";
                var list = "this Question is already exist!";
                return Json(list, JsonRequestBehavior.AllowGet);
            }

        }

        public bool deleteQuestion(long id)
        {
            HRMS_Evalution_Question question = db.HRMS_Evalution_Question.Find(id);
            if (question != null)
            {
                db.HRMS_Evalution_Question.Remove(question);
                db.SaveChanges(); ModelState.Clear();

                return true;
            }
            else
            {
                return false;
            }
        }

        public string UpdateQuestiondata(long id, string Question)
        {
            var question = db.HRMS_Evalution_Question.Where(rec => rec.ID == id).FirstOrDefault();


            var exist = db.HRMS_Evalution_Question.Where(rec => rec.Question == Question && rec.ID != id && rec.Header_ID == question.Header_ID).Any();
            if (exist == false)
            {
                question.Question = Question;
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges(); ModelState.Clear();
                return ("Data Updated Successfully.");

            }
            else
            {
                return ("this Question is already exist!");
            }
        }

        public bool deleteHeader(long id)
        {
            HRMS_TrainingEvalution_Header header = db.HRMS_TrainingEvalution_Header.Find(id);
            if (header != null)
            {
                List<HRMS_Evalution_Question> questions = db.HRMS_Evalution_Question.Where(rec => rec.Header_ID == header.ID).ToList();

                db.HRMS_TrainingEvalution_Header.Remove(header);
                if (questions != null)
                {
                    db.HRMS_Evalution_Question.RemoveRange(questions);
                }
                db.SaveChanges(); ModelState.Clear();

                return true;
            }
            else
            {
                return false;
            }
        }

        public ActionResult EditHeader(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            HRMS_TrainingEvalution_Header evalution_Header = db.HRMS_TrainingEvalution_Header.FirstOrDefault(rec => rec.ID == id);
            EvalutionCommon common = new EvalutionCommon();
            if (evalution_Header == null)
            {
                return HttpNotFound();
            }

            common.header = evalution_Header;
            return View(common);
        }
        [HttpPost]
        public JsonResult EditHeader(HRMS_TrainingEvalution_Header header)
        {
            if (ModelState.IsValid)
            {
                var Headername = db.HRMS_TrainingEvalution_Header.FirstOrDefault(rec => rec.Header == header.Header && rec.ID != header.ID);
                if (Headername == null)
                {
                    db.Entry(header).State = EntityState.Modified;
                    db.SaveChanges(); ModelState.Clear();

                    return Json(new
                    {
                        headerId = header.ID,
                        headername = header.Header
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    ViewBag.Salutation_Status = "It is already exist!";
                    return Json(new
                    {
                        headerId = 0,
                        headername = "it is already exist!"
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new
            {
                headerId = header.ID,
                headername = header.Header
            }, JsonRequestBehavior.AllowGet);

        }
    }

}