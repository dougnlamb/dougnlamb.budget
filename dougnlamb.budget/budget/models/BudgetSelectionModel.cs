using dougnlamb.core.security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dougnlamb.budget.models {
    public class BudgetSelectionModel : IBudgetSelectionModel {
        private ISecurityContext mSecurityContext;
        private IUser mUser;

        public BudgetSelectionModel() { }
        public BudgetSelectionModel(ISecurityContext securityContext, IUser user, IBudget budget) {
            this.mSecurityContext = securityContext;
            this.mUser = user;
            this.SelectedBudgetId = budget?.oid ?? 0;
        }

        public int SelectedBudgetId { get; set; }

        private IList<IBudgetViewModel> mBudgets;
        public IList<IBudgetViewModel> Budgets {
            get {
                if (mBudgets == null) {
                    mBudgets = new List<IBudgetViewModel>();
                    if (mUser != null) {
                        foreach (IBudget itm in mUser.Budgets) {
                            mBudgets.Add(itm.View(mSecurityContext));
                        }
                    }
                }
                return mBudgets;
            }
        }
    }
}
