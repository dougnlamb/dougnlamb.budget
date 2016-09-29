using dougnlamb.budget.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dougnlamb.budget.mvc.Controllers {
    [Authorize()]
    public class BudgetController : Controller {
        // GET: Budget
        public ActionResult Create() {
            IUser user = dougnlamb.budget.User.GetDao().Retrieve(null, User.Identity.Name);
            BudgetEditorModel model = new BudgetEditorModel(null, user);
            model.DefaultCurrencySelector.SelectedCurrencyCode = user.DefaultCurrency?.oid ?? 0;

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(BudgetEditorModel model) {
            IUser user = dougnlamb.budget.User.GetDao().Retrieve(null, User.Identity.Name);
            model.Owner = user;
            model.Save(null);

            return RedirectToAction("Index", "Home");
        }
    }
}