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
using static TrashCollector.Models.Account;


namespace TrashCollector.Controllers
{
    public class AccountsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public HomeController identifier = new HomeController();

        public void AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
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

        // GET: Accounts
        public ActionResult Index()
        {
            ViewData["MyProduct"] = identifier.DetermineEmployee();
            return View(db.Accounts.ToList());
        }

        // GET: Accounts/Details/5
        public ActionResult Details(string id)
        {
            ViewData["MyProduct"] = identifier.DetermineEmployee();

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
        public ActionResult CreateAccount()
        {

            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAccount([Bind(Include = "FirstName,LastName,StreetAddress,City,State,ZipCode")] Account account)
        {
            if (ModelState.IsValid)
            {
                int zipp = -2;
                List<Zip> wee = db.ZipCodes.ToList();
                for(int i=0;i<wee.Count;i++)
                {
                    if(wee[i].ZipCode == account.ZipCode)
                    {
                        zipp = i;
                    }
                }
                if(zipp == -2)
                {
                    Zip zip = new Zip
                    {
                        ZipCode = account.ZipCode
                    };
                    db.ZipCodes.Add(zip);
                }


                int statee = -2;
                List<State> dee = db.States.ToList();
                for(int j=0; j < dee.Count; j++)
                {
                    if (dee[j].StateName == account.State)
                    {
                        statee = j;
                    }
                }
                if(statee == -2)
                {
                    State state = new State
                    {
                        StateName = account.State
                    };
                    db.States.Add(state);
                }

                db.SaveChanges();
                
                City city = new City
                {
                    CityName = account.City,
                    StateId = (from a in db.States where a.StateName == account.State select a.StateId).First(),
                    ZipId = (from a in db.ZipCodes where a.ZipCode == account.ZipCode select a.ZipId).First()
                };

                int tez = -2;
                List<City> jo = new List<City>();
                for(int n = 0; n<jo.Count;n++)
                {
                    if (jo[n].CityName == city.CityName)
                    {
                        tez = n;
                    }
                }
                if(tez == -2)
                {
                    db.Cities.Add(city);
                    db.SaveChanges();
                }
                

                Address address = new Address
                {
                    AddressName = account.StreetAddress,
                    CityId = (from a in db.Cities where a.CityName == account.City select a.CityId).First()
                };
                int gez = -2;
                List<Address> weo = new List<Address>();
                for (int n = 0; n < weo.Count; n++)
                {
                    if (weo[n].AddressName == address.AddressName)
                    {
                        gez = n;
                    }
                }
                if(gez == -2)
                {
                    db.Addresses.Add(address);
                    db.SaveChanges();
                }
                

                Person person = new Person
                {
                    FirstName = account.FirstName,
                    LastName= account.LastName,
                    AddressId = (from a in db.Addresses where a.AddressName == account.StreetAddress select a.AddressId).First()
                };
                db.Persons.Add(person);
                db.SaveChanges();

                return RedirectToAction("Register","Account");
            }

            return View(account);
        }

        // GET: Accounts/Edit/5
        public ActionResult Edit(string id)
        {
            ViewData["MyProduct"] = identifier.DetermineEmployee();

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
            ViewData["MyProduct"] = identifier.DetermineEmployee();

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
            ViewData["MyProduct"] = identifier.DetermineEmployee();

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
            ViewData["MyProduct"] = identifier.DetermineEmployee();

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

        // GET: /Account/Register   
        [AllowAnonymous]
        public ActionResult RegisterEmployee()
        {
            ViewBag.Name = new SelectList(db.Roles.Where(u => !u.Name.Contains("Admin"))
                                            .ToList(), "Name", "Name");
            return View();
        }


        // POST: /Account/Register   
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterEmployee(RegisterEmployee model)
        {

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771   
                    // Send an email with this link   
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);   
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);   
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");   
                    //Assign Role to user Here      
                    await this.UserManager.AddToRoleAsync(user.Id, model.UserRoles);
                    //Ends Here    
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.Name = new SelectList(db.Roles.Where(u => !u.Name.Contains("Admin"))
                                          .ToList(), "Name", "Name");
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form   
            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

    }
}
