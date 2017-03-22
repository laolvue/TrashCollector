using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrashCollector.Models;

namespace TrashCollector.Controllers
{
    public class SetWeekSchedulesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SetWeekSchedules
        public ActionResult Index()
        {
            var setWeekSchedules = db.SetWeekSchedules.Include(s => s.Day).Include(s => s.Time).Include(s => s.Week);
            return View(setWeekSchedules.ToList());
        }

        // GET: SetWeekSchedules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SetWeekSchedule setWeekSchedule = db.SetWeekSchedules.Find(id);
            if (setWeekSchedule == null)
            {
                return HttpNotFound();
            }
            return View(setWeekSchedule);
        }

        // GET: SetWeekSchedules/Create
        public ActionResult Create()
        {
            ViewBag.DayId = new SelectList(db.Days, "DayId", "DayName");
            ViewBag.TimeId = new SelectList(db.Times, "TimeId", "TimeName");
            ViewBag.WeekId = new SelectList(db.Weeks, "WeekId", "StartingWeek");
            return View();
        }

        // POST: SetWeekSchedules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,WeekId,TimeId,DayId")] SetWeekSchedule setWeekSchedule)
        {
                        var userEmail = User.Identity.Name;

            if (ModelState.IsValid)
            {
                var pop = db.Schedules.Where(a => a.PersonId == (from b in db.Persons where b.Email == userEmail select b.PersonId).FirstOrDefault()).ToList();
               
                
                foreach(var ice in pop)
                {
                    if(ice.WeekId == setWeekSchedule.WeekId)
                    {
                        ice.TimeId = setWeekSchedule.TimeId;
                        db.SaveChanges();
                        ice.DayId = setWeekSchedule.DayId;
                        db.SaveChanges();
                        break;
                    }
                }



                //db.SetWeekSchedules.Add(setWeekSchedule);
                //db.SaveChanges();
                return RedirectToAction("Index","SetSchedules");
            }

            ViewBag.DayId = new SelectList(db.Days, "DayId", "DayName", setWeekSchedule.DayId);
            ViewBag.TimeId = new SelectList(db.Times, "TimeId", "TimeName", setWeekSchedule.TimeId);
            ViewBag.WeekId = new SelectList(db.Weeks, "WeekId", "StartingWeek", setWeekSchedule.WeekId);
            return View(setWeekSchedule);
        }

        // GET: SetWeekSchedules/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SetWeekSchedule setWeekSchedule = db.SetWeekSchedules.Find(id);
            if (setWeekSchedule == null)
            {
                return HttpNotFound();
            }
            ViewBag.DayId = new SelectList(db.Days, "DayId", "DayName", setWeekSchedule.DayId);
            ViewBag.TimeId = new SelectList(db.Times, "TimeId", "TimeName", setWeekSchedule.TimeId);
            ViewBag.WeekId = new SelectList(db.Weeks, "WeekId", "StartingWeek", setWeekSchedule.WeekId);
            return View(setWeekSchedule);
        }

        // POST: SetWeekSchedules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,WeekId,TimeId,DayId")] SetWeekSchedule setWeekSchedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(setWeekSchedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DayId = new SelectList(db.Days, "DayId", "DayName", setWeekSchedule.DayId);
            ViewBag.TimeId = new SelectList(db.Times, "TimeId", "TimeName", setWeekSchedule.TimeId);
            ViewBag.WeekId = new SelectList(db.Weeks, "WeekId", "StartingWeek", setWeekSchedule.WeekId);
            return View(setWeekSchedule);
        }

        // GET: SetWeekSchedules/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SetWeekSchedule setWeekSchedule = db.SetWeekSchedules.Find(id);
            if (setWeekSchedule == null)
            {
                return HttpNotFound();
            }
            return View(setWeekSchedule);
        }

        // POST: SetWeekSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SetWeekSchedule setWeekSchedule = db.SetWeekSchedules.Find(id);
            db.SetWeekSchedules.Remove(setWeekSchedule);
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
