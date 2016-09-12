using dougnlamb.core.security;
using System;

namespace dougnlamb.budget.models {
    public class BudgetViewModel : IBudgetViewModel {

        private IBudget mBudget;

        public BudgetViewModel(ISecurityContext securityContext, IBudget budget) {
            this.mBudget = budget;
            this.oid = budget.oid;
            this.Name = budget.Name;
            // TODO: Fix period.
//            this.Period = budget.Period;
        }

        public int oid { get; internal set; }
        public string Name { get; internal set; }
        public IUserViewModel Owner { get; internal set; }
        public IBudgetPeriodViewModel Period { get;  internal set; }
    }
}