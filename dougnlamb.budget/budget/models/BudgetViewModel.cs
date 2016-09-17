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
        private IUserViewModel mOwner;
        public IUserViewModel Owner {
            get {
                if(mOwner ==null) {
                    mOwner = mBudget?.Owner?.View(null) ?? new UserViewModel(null, null);
                }
                return mOwner;
            }
        }
        public IBudgetPeriodViewModel Period { get; internal set; }

        private ICurrencyViewModel mDefaultCurrency;
        public ICurrencyViewModel DefaultCurrency {
            get {
                if (mDefaultCurrency == null) {
                    mDefaultCurrency = mBudget?.DefaultCurrency?.View(null);
                }
                return mDefaultCurrency;
            }
        }
    }
}