using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TrashCollector.Models;

namespace TrashCollector.Controllers
{
    public class EmployeeRoutesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public HomeController identifier = new HomeController();
        private ApplicationUserManager _userManager;

        public void AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: EmployeeRoutes
        public ActionResult Index()
        {
            var employeeRoutes = db.EmployeeRoutes.Include(e => e.Week).Include(e => e.Zip);
            return View(employeeRoutes.ToList());
        }



        public ActionResult DisplayPickups()
        {
            EmployeeRoute gene = TempData["employee"] as EmployeeRoute;
            if(gene == null)
            {
                return RedirectToAction("Create");
            }
            var userEmail = User.Identity.Name;
            
            //var scheduleOfUser = db.Schedules.Where(a => a.PersonId == (from b in db.Persons where b.Email == userEmail select b.PersonId).FirstOrDefault()).ToList();

            var citiesInZip = db.Cities.Where(r => r.ZipId == gene.ZipId).ToList();
            List<Address> wwe = new List<Address>();
            foreach(var breh in db.Addresses)
            {
                if(breh.CityId == citiesInZip.First().CityId)
                {
                    wwe.Add(breh);
                }
            }

            List<Person> ttw = db.Persons.ToList();
            List<Person> newttw = new List<Person>();
            for(int i=0; i < wwe.Count(); i++)
            {
                for(int j = 0; j < db.Persons.Count(); j++)
                {
                    if(wwe[i].AddressId == ttw[j].AddressId)
                    {
                        newttw.Add(ttw[j]);
                    }
                }
            }

            List<Schedule> pwo = db.Schedules.ToList();
            List<Schedule> woot = new List<Schedule>();
            for(int k=0;k<newttw.Count; k++)
            {
                for(int m=0; m < pwo.Count; m++)
                {
                    if(newttw[k].PersonId==pwo[m].PersonId && gene.WeekId==pwo[m].WeekId)
                    {
                        woot.Add(pwo[m]);
                    }
                }
            }




            List<string> timezz = new List<string>();
            List<string> weekzz = new List<string>();
            List<string> dayzz = new List<string>();
            List<string> addrezz = new List<string>();
            List<int> peerz = new List<int>();


            foreach (var blue in woot)
            {
                var getAddress = from p in db.Persons
                              where p.PersonId == blue.PersonId
                              select p.AddressId;

                int doomw = 0;
                foreach(var llle in getAddress)
                {
                    doomw = llle;
                }
                var addresser = from g in db.Addresses
                                where g.AddressId == doomw
                                select g.AddressName;

                string cow = "";
                foreach(var gjo in addresser)
                {
                    cow = gjo;
                }
                addrezz.Add(cow);

                var time = from r in db.Times
                           where r.TimeId == blue.TimeId
                           select r.TimeName;


                string dak = "";
                foreach (var efw in time)
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

                var peeepz = from k in db.Persons
                          where k.PersonId == blue.PersonId
                          select k.PersonId;


                int keeew = 0;
                foreach (var weq in peeepz)
                {
                    keeew = weq;
                }
                peerz.Add(keeew);
            }

            ViewData["MyProduct4"] = weekzz;
            ViewData["MyProduct5"] = timezz;
            ViewData["MyProduct6"] = dayzz;
            ViewData["MyProduct7"] = addrezz;
            ViewData["MyProduct8"] = peerz;
            TempData["Addressez"] = addrezz;
            


            if (UserManager.IsInRole(User.Identity.GetUserId(), "Employee"))
            {
                ViewData["MyProduct"] = "1";
            }

            return View();
        }





        // GET: EmployeeRoutes/Details/
        public ActionResult Details()
        {
            List<string> opo = new List<string>();
            List<string> genxe = TempData["Addressez"] as List<string>;
            foreach( var oee in genxe)
            {
                var fjaoie = from d in db.Addresses
                             where d.AddressName == oee
                             select d;
                var fwe = fjaoie.ToList();
                List<City> pokew = new List<City>();
                foreach(var oio in fwe)
                {
                    pokew = db.Cities.Where(u => u.CityId == oio.CityId).ToList();
                }
                var oiow = pokew.ToList();

                opo.Add($"{oee} {oiow[0].CityName}");
            }

            ViewData["MyProduct600"] = opo;
            if (UserManager.IsInRole(User.Identity.GetUserId(), "Employee"))
            {
                ViewData["MyProduct"] = "1";
            }
            return View();
        }

        // GET: EmployeeRoutes/Create
        public ActionResult Create()
        {
            if (UserManager.IsInRole(User.Identity.GetUserId(), "Employee"))
            {
                ViewData["MyProduct"] = "1";
            }
            ViewBag.WeekId = new SelectList(db.Weeks, "WeekId", "StartingWeek");
            ViewBag.ZipId = new SelectList(db.ZipCodes, "ZipId", "ZipCode");
            return View();
        }

        // POST: EmployeeRoutes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RouteId,ZipId,WeekId")] EmployeeRoute employeeRoute)
        {
            
            if (ModelState.IsValid && employeeRoute.ZipId!=0)
            {
                db.EmployeeRoutes.Add(employeeRoute);
                db.SaveChanges();
                TempData["employee"] = employeeRoute;
                return RedirectToAction("DisplayPickups");
            }

            ViewBag.WeekId = new SelectList(db.Weeks, "WeekId", "StartingWeek", employeeRoute.WeekId);
            ViewBag.ZipId = new SelectList(db.ZipCodes, "ZipId", "ZipCode", employeeRoute.ZipId);
            return View(employeeRoute);
        }

        public ActionResult PickUp( int id, string memberid)
        {
            var weekIDz = from oio in db.Weeks
                          where memberid == oio.StartingWeek
                          select oio.WeekId;
            var oip = weekIDz.ToList();

            var removeFromGroup = from weo in db.Schedules
                                  where id == weo.PersonId
                                  select weo;
            int pot = oip[0];
            var jowo = from poooe in removeFromGroup
                       where pot == poooe.WeekId
                       select poooe;
            var item = removeFromGroup.ToList();
            foreach (var wer in jowo.ToList())
            {
                db.Schedules.Remove(wer);
                var billz = from biller in db.Billings
                            where biller.PersonId == wer.PersonId
                            select biller;
                foreach(var poet in db.Billings.Where(u=> u.PersonId == wer.PersonId))
                {
                    poet.BillAmount += 5;
                }
                
                db.SaveChanges();
            }
            return RedirectToAction("create");
        }

        // GET: EmployeeRoutes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeRoute employeeRoute = db.EmployeeRoutes.Find(id);
            if (employeeRoute == null)
            {
                return HttpNotFound();
            }
            ViewBag.WeekId = new SelectList(db.Weeks, "WeekId", "StartingWeek", employeeRoute.WeekId);
            ViewBag.ZipId = new SelectList(db.ZipCodes, "ZipId", "ZipId", employeeRoute.ZipId);
            return View(employeeRoute);
        }

        // POST: EmployeeRoutes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RouteId,ZipId,WeekId")] EmployeeRoute employeeRoute)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employeeRoute).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.WeekId = new SelectList(db.Weeks, "WeekId", "StartingWeek", employeeRoute.WeekId);
            ViewBag.ZipId = new SelectList(db.ZipCodes, "ZipId", "ZipId", employeeRoute.ZipId);
            return View(employeeRoute);
        }

        // GET: EmployeeRoutes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeRoute employeeRoute = db.EmployeeRoutes.Find(id);
            if (employeeRoute == null)
            {
                return HttpNotFound();
            }
            return View(employeeRoute);
        }

        // POST: EmployeeRoutes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmployeeRoute employeeRoute = db.EmployeeRoutes.Find(id);
            db.EmployeeRoutes.Remove(employeeRoute);
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
