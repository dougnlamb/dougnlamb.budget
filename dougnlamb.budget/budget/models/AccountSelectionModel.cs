using dougnlamb.core.security;
using System;
using System.Collections.Generic;

namespace dougnlamb.budget.models {
    public class AccountSelectionModel : IAccountSelectionModel {
        private ISecurityContext mSecurityContext;
        private IUser mUser;

        public AccountSelectionModel() { }
        public AccountSelectionModel(ISecurityContext securityContext, IUser user, IAccount account) {
            mSecurityContext = securityContext;
            mUser = user;
        }

        private IList<IAccountViewModel> mAccounts;
        public IList<IAccountViewModel> Accounts {
            get {
                if(mAccounts == null) {
                    mAccounts = new List<IAccountViewModel>();
                    foreach(IAccount acct in mUser.Accounts) {
                        mAccounts.Add(acct.View(mSecurityContext));
                    }
                }
                return mAccounts;
            }
        }

        public int SelectedAccountId { get; set; }
    }
}