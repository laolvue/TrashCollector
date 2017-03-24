using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TrashCollector.Models;

namespace TrashCollector.Controllers
{
    public class SetSchedulesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SetSchedules
        public ActionResult Index()
        {
            var userEmail = User.Identity.Name;
            var scheduleOfUser = db.Schedules.Where(a => a.PersonId == (from b in db.Persons where b.Email == userEmail select b.PersonId).FirstOrDefault()).ToList();
            List<string> timezz = new List<string>();
            List<string> weekzz = new List<string>();
            List<string> dayzz = new List<string>();
            foreach (var blue in scheduleOfUser)
            {
                var time = from r in db.Times
                          where r.TimeId == blue.TimeId
                          select r.TimeName;
                
                
                string dak="";
                foreach(var efw in time)
                {
                    dak = efw;
                }
                timezz.Add(dak);

                var week = from a in db.Weeks
                           where a.WeekId == blue.WeekId
                           select a.StartingWeek;

                string bak = "";
                foreach (var efw in week)
                {
                    bak = efw;
                }
                weekzz.Add(bak);

                var day = from r in db.Days
                           where r.DayId == blue.DayId
                           select r.DayName;


                string lak = "";
                foreach (var efw in day)
                {
                    lak = efw;
                }
                dayzz.Add(lak);
            }
           
            ViewData["MyProduct1"] = weekzz;
            ViewData["MyProduct2"] = timezz;
            ViewData["MyProduct3"] = dayzz;

            return View();
        }

        // GET: SetSchedules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SetSchedule setSchedule = db.SetSchedules.Find(id);
            if (setSchedule == null)
            {
                return HttpNotFound();
            }
            return View(setSchedule);
        }

        // GET: SetSchedules/Create
        public ActionResult Create()
        {
            ViewBag.DayId = new SelectList(db.Days, "DayId", "DayName");
            ViewBag.TimeId = new SelectList(db.Times, "TimeId", "TimeName");
            return View();
        }

        // POST: SetSchedules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TimeId,DayId")] SetSchedule setSchedule)
        {
            
            var userEmail = User.Identity.Name;
            if (ModelState.IsValid)
            {
                var pop = db.Schedules.Where(a => a.PersonId == (from b in db.Persons where b.Email == userEmail select b.PersonId).FirstOrDefault()).ToList();
                if(pop.Count == 0)
                {
                    int numberOfWeeks = db.Weeks.Count();
                    for (int i = 0; i < numberOfWeeks; i++)
                    {
                        Schedule schedule = new Schedule
                        {
                            WeekId = (i + 1),
                            TimeId = setSchedule.TimeId,
                            DayId = setSchedule.DayId,
                            PersonId = (from a in db.Persons where a.Email == userEmail select a.PersonId).First()
                        };
                        db.Schedules.Add(schedule);
                        db.SaveChanges();
                    }
                }
                else
                {
                    int numberOfWeeks = db.Weeks.Count();
                    foreach(var junk in pop)
                    {
                        junk.TimeId = setSchedule.TimeId;
                        junk.DayId = setSchedule.DayId;
                    }
                    db.SaveChanges();
                }
                //db.SetSchedules.Add(setSchedule);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DayId = new SelectList(db.Days, "DayId", "DayName", setSchedule.DayId);
            ViewBag.TimeId = new SelectList(db.Times, "TimeId", "TimeName", setSchedule.TimeId);
            return View(setSchedule);
        }

        // GET: SetSchedules/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SetSchedule setSchedule = db.SetSchedules.Find(id);
            if (setSchedule == null)
            {
                return HttpNotFound();
            }
            ViewBag.DayId = new SelectList(db.Days, "DayId", "DayName", setSchedule.DayId);
            ViewBag.TimeId = new SelectList(db.Times, "TimeId", "TimeName", setSchedule.TimeId);
            return View(setSchedule);
        }

        // POST: SetSchedules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TimeId,DayId")] SetSchedule setSchedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(setSchedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DayId = new SelectList(db.Days, "DayId", "DayName", setSchedule.DayId);
            ViewBag.TimeId = new SelectList(db.Times, "TimeId", "TimeName", setSchedule.TimeId);
            return View(setSchedule);
        }

        // GET: SetSchedules/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SetSchedule setSchedule = db.SetSchedules.Find(id);
            if (setSchedule == null)
            {
                return HttpNotFound();
            }
            return View(setSchedule);
        }

        // POST: SetSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SetSchedule setSchedule = db.SetSchedules.Find(id);
            db.SetSchedules.Remove(setSchedule);
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
