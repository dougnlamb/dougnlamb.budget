using dougnlamb.core.security;
using System;
using System.Collections.Generic;

namespace dougnlamb.budget.models {
    public class BudgetViewModel : IBudgetViewModel {

        private ISecurityContext mSecurityContext;
        private IBudget mBudget;

        public BudgetViewModel(ISecurityContext securityContext, IBudget budget) {
            this.mSecurityContext = securityContext;
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

        private IList<IBudgetItemViewModel> mBudgetItems;
        public IList<IBudgetItemViewModel> BudgetItems {
            get {
                if (mBudgetItems == null) {
                    mBudgetItems = new List<IBudgetItemViewModel>();
                    foreach(IBudgetItem item in mBudget.BudgetItems) {
                        mBudgetItems.Add(item.View(mSecurityContext));
                    }
                }
                return mBudgetItems;
            }
        }
    }
}