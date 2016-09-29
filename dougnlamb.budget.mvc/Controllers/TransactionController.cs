using dougnlamb.budget.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dougnlamb.budget.mvc.Controllers {
    [Authorize()]
    public class TransactionController : Controller {
        // GET: Transaction
        public ActionResult Create(int accountId) {
            IUser user = dougnlamb.budget.User.GetDao().Retrieve(null, User.Identity.Name);
            IAccount account = dougnlamb.budget.Account.GetDao().Retrieve(null, accountId);
            TransactionEditorModel model = new TransactionEditorModel(null, user, account);
            model.AccountSelector.SelectedAccountId = account.oid;
            model.TransactionAmountEditor.Amount = 0;
            model.TransactionAmountEditor.CurrencySelector.SelectedCurrencyCode = account.DefaultCurrency.oid;

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(TransactionEditorModel model) {
            IUser user = dougnlamb.budget.User.GetDao().Retrieve(null, User.Identity.Name);
            model.TransactionDate = DateTime.Now;
            ITransaction transaction = model.Account.AddTransaction(null, model);

            return RedirectToAction("View", "Account", new { accountId = transaction.Account.oid });
        }
    }
}