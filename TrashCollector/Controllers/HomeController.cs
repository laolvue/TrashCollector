using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrashCollector.Models;

namespace TrashCollector.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            ViewData["MyProduct"] = DetermineEmployee();
            return View();
        }

        public string DetermineEmployee()
        {
            if (isEmployeeUser())
            {
                return "1";
            }
            else
            {
                return "2";
            }
        }


        public Boolean isEmployeeUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var s = UserManager.GetRoles(user.GetUserId());
                if (s.Count() > 0)
                {
                    if (s[0].ToString() == "Employee")
                    {
                        return true;
                    }
                }

                else
                {
                    return false;
                }
            }
            return false;
        }





        public ActionResult About()
        {
            var joiw = from feww in db.Persons
                       where feww.Email == User.Identity.Name
                       select feww.PersonId;
            var cow = joiw.ToList();
            int idOfUser = cow[0];

            var oit = from jin in db.Billings
                      where jin.PersonId == idOfUser
                      select jin.BillAmount;
            var pwee = oit.ToList();
            double pww = pwee[0];

            ViewData["MyProduct"] = DetermineEmployee();
            ViewData["MyProduct10"] = pww;

            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewData["MyProduct"] = DetermineEmployee();
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Schedule()
        {
            ViewData["MyProduct"] = DetermineEmployee();
            ViewBag.Message = "Your schedule page.";

            return View();
        }
    }
}