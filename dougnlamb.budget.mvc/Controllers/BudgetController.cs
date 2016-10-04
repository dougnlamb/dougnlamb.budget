using dougnlamb.budget.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dougnlamb.budget.mvc.Controllers {
    [Authorize()]
    public class BudgetController : Controller {
        public ActionResult Index() {
            IUser user = dougnlamb.budget.User.GetDao().Retrieve(null, User.Identity.Name);

            IList<IBudget> budgets = Budget.GetDao().Retrieve(null, user);
            List<IBudgetViewModel> budgetList = new List<IBudgetViewModel>();
            foreach (IBudget budget in budgets) {
                budgetList.Add(budget.View(null));
            }
            return View(budgetList);
        }

        public ActionResult View(int id) {
            IBudget budget = Budget.GetDao().Retrieve(null, id);
            return View(budget.View(null));
        }

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

        public ActionResult AddBudgetItem(int budgetId) {
            IUser user = dougnlamb.budget.User.GetDao().Retrieve(null, User.Identity.Name);
            IBudget budget = dougnlamb.budget.Budget.GetDao().Retrieve(null, budgetId);
            BudgetItemEditorModel model = new BudgetItemEditorModel(null, user, budget);
            model.BudgetSelector.SelectedBudgetId = budget.oid;

            return View(model);
        }

        [HttpPost]
        public ActionResult AddBudgetItem(BudgetItemEditorModel model) {
            IUser user = dougnlamb.budget.User.GetDao().Retrieve(null, User.Identity.Name);
            IBudgetItem budgetItem = model.Save(null);

            return RedirectToAction("View", "Budget", new { id = budgetItem.Budget.oid });
        }
    }
}