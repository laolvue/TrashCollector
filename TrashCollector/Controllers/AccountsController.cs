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
    public class AccountsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Accounts
        public ActionResult Index()
        {
            return View(db.Accounts.ToList());
        }

        // GET: Accounts/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // GET: Accounts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName,LastName,StreetAddress,City,State,ZipCode")] Account account)
        {
            if (ModelState.IsValid)
            {
                Zip zip = new Zip
                {
                    ZipCode = account.ZipCode
                };
                db.ZipCodes.Add(zip);

                State state = new State
                {
                    StateName = account.State
                };
                db.States.Add(state);
                db.SaveChanges();
                //var device = new Zip();
                //device.ZipId = (from a in db.ZipCodes where a.ZipCode == account.ZipCode select a.ZipId).Single();
                //var deviceb = new State();
                //deviceb.StateId = (from a in db.ZipCodes where a.ZipCode == account.ZipCode select a.ZipId).Single();

                City city = new City
                {
                    CityName = account.City,
                    StateId = (from a in db.States where a.StateName == account.State select a.StateId).First(),
                    ZipId = (from a in db.ZipCodes where a.ZipCode == account.ZipCode select a.ZipId).First()
                };
                db.Cities.Add(city);
                db.SaveChanges();

                Address address = new Address
                {
                    AddressName = account.StreetAddress,
                    CityId = (from a in db.Cities where a.CityName == account.City select a.CityId).First()
                };
                db.Addresses.Add(address);
                db.SaveChanges();

                Person person = new Person
                {
                    FirstName = account.FirstName,
                    LastName= account.LastName,
                    AddressId = (from a in db.Addresses where a.AddressName == account.StreetAddress select a.AddressId).First()
                };
                db.Persons.Add(person);

                db.SaveChanges();
                /*
                db.Accounts.Add(account);
                db.SaveChanges();*/
                return RedirectToAction("Index");
            }

            return View(account);
        }

        // GET: Accounts/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FirstName,LastName,StreetAddress,City,State,ZipCode")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(account);
        }

        // GET: Accounts/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Account account = db.Accounts.Find(id);
            db.Accounts.Remove(account);
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
