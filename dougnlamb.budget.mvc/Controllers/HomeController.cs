using dougnlamb.budget.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dougnlamb.budget.mvc.Controllers {
    [Authorize]
    public class HomeController : Controller {
        public ActionResult Index() {
            IUser usr = dougnlamb.budget.User.GetDao().Retrieve(null, 1);
            List<IAccountViewModel> accounts = new List<IAccountViewModel>();
            foreach (IAccount acct in usr.Accounts) {
                accounts.Add(acct.View(null));
            }

            return View(usr.View(null));
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}