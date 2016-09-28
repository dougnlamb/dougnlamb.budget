using dougnlamb.budget.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dougnlamb.budget.mvc.Controllers {
    [Authorize]
    public class UserController : Controller {
        // GET: User
        public ActionResult Index() {
            IUser usr = dougnlamb.budget.User.GetDao().Retrieve(null, User.Identity.Name);

            if (usr == null) {
                ICurrency curr = dougnlamb.budget.Currency.GetDao().Retrieve(null, 1);
                return View(new UserEditorModel() {
                    UserId = User.Identity.Name,
                    Email = User.Identity.Name,
                    DefaultCurrency = curr
                });
            }
            else {
                return View(new UserEditorModel(usr));
            }
        }

        [ValidateAntiForgeryToken]
        public ActionResult Save(UserEditorModel mdl) {
            mdl.Save(null);

            return RedirectToAction("Index");
        }
    }
}