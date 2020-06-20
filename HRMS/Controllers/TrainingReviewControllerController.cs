using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRMS.Models;
using HRMS.ViewModel;

namespace HRMS.Controllers
{
    //class for generic function
    //Zipping three List
    public static class MyFunkyExtensions
    {
        public static IEnumerable<TResult> ZipThree<T1, T2, T3, TResult>(
            this IEnumerable<T1> source,
            IEnumerable<T2> second,
            IEnumerable<T3> third,
            Func<T1, T2, T3, TResult> func)
        {
            using (var e1 = source.GetEnumerator())
            using (var e2 = second.GetEnumerator())
            using (var e3 = third.GetEnumerator())
            {
                while (e1.MoveNext() && e2.MoveNext() && e3.MoveNext())
                    yield return func(e1.Current, e2.Current, e3.Current);
            }
        }
    }

    public class TrainingReviewControllerController : Controller
    {
        public HRMSEntities db = new HRMSEntities();



        // GET: TrainingReview
        public ActionResult Index()
        {
            //return View(db.HRMS_ProgramDetail.Where(x => x.TrainingStatus == "Completed").ToList());
            long empid = Convert.ToInt64(Session["id"]);
            return View(db.HRMS_TrainingApproval.Where(x => x.EMP_ID == empid && x.Status == 2).ToList());

        }
        public ActionResult AdminIndex()
        {
            return View(db.HRMS_ProgramDetail.Where(x => x.TrainingStatus == "Completed").ToList());
        }

        public ActionResult ReviewList(long id)
        {

            var a = db.HRMS_TrainingReview.Where(x => x.ProgramID == id).GroupBy(i => i.ProgramID).Select(g => g.Average(i => (1.0 * i.NonDescriptive))).FirstOrDefault();
            int A = Convert.ToInt32(a);
            ViewBag.Rating = A;
            return View(db.HRMS_TrainingApproval.Where(x => x.Program_ID == id && x.IsReviewDone == 1).ToList());
        }

        public ActionResult Review(long id)
        {
            ViewBag.Error = TempData["Error"];
            long empid = Convert.ToInt64(Session["id"]);
            TempData["ProgramID"] = id;
            bool IsExist = db.HRMS_TrainingReview.Any(x => x.Emp_ID == empid && x.ProgramID == id);
            if (IsExist == true) //already review given
            {
                TempData["viewmode"] = "InViewMode";
                return RedirectToAction("Vieww", new { id });
            }
            return View(db.HRMS_Evalution_Question.ToList());
        }

        //        var numbers = new[] { 1, 2, 3, 4 };
        //        var words = new[] { "one", "two", "three", "four" };

        //        var numbersAndWords = numbers.Zip(words, (n, w) => new { Number = n, Word = w });
        //foreach(var nw in numbersAndWords)
        //{
        //    Console.WriteLine(nw.Number + nw.Word);
        //}



        //            foreach (var nw in numbers.Zip(words, Tuple.Create)) 
        //{
        //    Console.WriteLine(nw.Item1 + nw.Item2);
        //}


        [HttpPost]
        public ActionResult Revieww(IEnumerable<string> Answer, IEnumerable<long> Question, IEnumerable<string> flag, String Textt, string Check)
        {
            //Answer.Zip(Question, Tuple.Create)
            //a:answer
            //b:question
            //c:flag
            long ProID = Convert.ToInt64(TempData["ProgramID"]);
            if (Textt == "")
            {
                TempData["Error"] = "Please!Fill the Rating!";
                return RedirectToAction("Review", new { id = ProID });
            }

            long empid = Convert.ToInt64(Session["id"]);
            // long ProID =Convert.ToInt64(TempData["ProgramID"]);
            var results = Answer.ZipThree(Question, flag, (a, b, c) => new { a, b, c });

            foreach (var item in results)
            {
                HRMS_TrainingReview obj = new HRMS_TrainingReview();
                obj.Emp_ID = empid;
                obj.ProgramID = ProID;
                obj.QuestionID = item.b;
                obj.Date = DateTime.Now;
                obj.OtherComments = Textt;
                if (Check != null)
                {
                    obj.IsWorthy = true;
                }
                else
                {
                    obj.IsWorthy = false;
                }

                if (item.c == "False")  //dropdown
                {
                    obj.NonDescriptive = Convert.ToInt32(item.a);
                }
                else
                {
                    obj.Descriptive = item.a;
                }
                db.HRMS_TrainingReview.Add(obj);
                db.SaveChanges();
            }

            //flag insert in trainingApproval
            HRMS_TrainingApproval TA = db.HRMS_TrainingApproval.Where(x => x.Program_ID == ProID && x.EMP_ID == empid).FirstOrDefault();
            TA.IsReviewDone = 1; //Review Done
            db.Entry(TA).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Vieww(long id, long? empid)
        {
            //db.HRMS_TrainingReview.Where(x => x.Emp_ID == empid && x.ProgramID == id).ToList()
            if (User.IsInRole("emp"))
            {
                empid = Convert.ToInt64(Session["id"]);
            }
            else
            {
                ViewBag.ProgramID = id;
            }

            return View(db.HRMS_TrainingReview.Where(x => x.Emp_ID == empid.Value && x.ProgramID == id).ToList());
        }

    }
}