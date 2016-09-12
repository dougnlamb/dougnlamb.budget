using dougnlamb.core.security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dougnlamb.budget.models {
    public class BudgetSelectionModel : IBudgetSelectionModel {
        private ISecurityContext mSecurityContext;

        public BudgetSelectionModel() { }
        public BudgetSelectionModel(ISecurityContext securityContext, IBudget budget) {
            this.mSecurityContext = securityContext;
            this.SelectedItem = budget?.View(securityContext) ?? null;
        }

        private IList<IBudgetViewModel> mBudgets;
        public IList<IBudgetViewModel> Budgets {
            get {
                if (mBudgets == null) {
                    IList<IBudget> itms = Budget.GetDao().RetrieveAll(mSecurityContext);

                    mBudgets = new List<IBudgetViewModel>();
                    foreach (IBudget itm in itms) {
                        mBudgets.Add(itm.View(mSecurityContext));
                    }
                }
                return mBudgets;
            }
        }

        public IBudget SelectedBudget {
            get {
                if (SelectedItem == null) {
                    return null;
                }
                else {
                    return new Budget(mSecurityContext, SelectedItem.oid);
                }
            }
        }
        public IBudgetViewModel SelectedItem { get; set; }
    }
}
