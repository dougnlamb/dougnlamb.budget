using System;
using dougnlamb.budget.models;
using dougnlamb.core.collections;
using dougnlamb.core.security;

namespace dougnlamb.budget {
    public class UserViewModel : IUserViewModel {
        private ISecurityContext securityContext;
        private User user;

        public UserViewModel(ISecurityContext securityContext, User user) {
            this.securityContext = securityContext;
            this.user = user;
        }

        private IObservableList<IAccountViewModel> mAccounts;
        public IObservableList<IAccountViewModel> Accounts {
            get {
                if (mAccounts == null) {
                    mAccounts = new ObservableList<IAccountViewModel>();
                    foreach (IAccount account in user.Accounts) {
                        mAccounts.Add(account.View(securityContext));
                    }
                }
                return mAccounts;
            }
        }

        private IObservableList<IBudgetViewModel> mBudgets;
        public IObservableList<IBudgetViewModel> Budgets {
            get {
                if (mBudgets == null) {
                    mBudgets = new ObservableList<IBudgetViewModel>();
                    foreach (IBudget budget in user.Budgets) {
                        mBudgets.Add(budget.View(securityContext));
                    }
                }
                return mBudgets;
            }
        }

        public string DisplayName {
            get {
                return this.user.DisplayName;
            }
        }

        public string Email {
            get {
                return this.user.Email;
            }
        }

        public int oid {
            get {
                return this.user.oid;
            }
        }

        public string UserId {
            get {
                return this.user.UserId;
            }
        }
    }
}