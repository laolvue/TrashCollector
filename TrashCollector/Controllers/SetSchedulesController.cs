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
    public class SetSchedulesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SetSchedules
        public ActionResult Index()
        {
            var setSchedules = db.SetSchedules.Include(s => s.Day).Include(s => s.Time);
            return View(setSchedules.ToList());
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
            ViewBag.PersonId = new SelectList(db.Times, "TimeId", "TimeName");
            return View();
        }

        // POST: SetSchedules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PersonId,DayId")] SetSchedule setSchedule)
        {
            if (ModelState.IsValid)
            {
                db.SetSchedules.Add(setSchedule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DayId = new SelectList(db.Days, "DayId", "DayName", setSchedule.DayId);
            ViewBag.PersonId = new SelectList(db.Times, "TimeId", "TimeName", setSchedule.PersonId);
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
            ViewBag.PersonId = new SelectList(db.Times, "TimeId", "TimeName", setSchedule.PersonId);
            return View(setSchedule);
        }

        // POST: SetSchedules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PersonId,DayId")] SetSchedule setSchedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(setSchedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DayId = new SelectList(db.Days, "DayId", "DayName", setSchedule.DayId);
            ViewBag.PersonId = new SelectList(db.Times, "TimeId", "TimeName", setSchedule.PersonId);
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
