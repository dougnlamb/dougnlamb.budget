using dougnlamb.budget.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dougnlamb.budget.mvc.Controllers {
    [Authorize()]
    public class AccountController : Controller {

        public ActionResult View(int accountId) {
            IAccount account = Account.GetDao().Retrieve(null, accountId);

            return View(account.View(null));
        }
        // GET: Account
        public ActionResult Create() {
            IUser user = dougnlamb.budget.User.GetDao().Retrieve(null, User.Identity.Name);
            AccountEditorModel model = new AccountEditorModel(null, user);
            model.DefaultCurrencySelector.SelectedCurrencyCode = user.DefaultCurrency?.oid ?? 0;

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(AccountEditorModel model) {
            IUser user = dougnlamb.budget.User.GetDao().Retrieve(null, User.Identity.Name);
            model.Owner = user;
            model.Save(null);

            return RedirectToAction("Index", "Home");
        }
    }
}