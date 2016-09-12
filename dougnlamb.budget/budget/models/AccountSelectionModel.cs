using dougnlamb.core.security;
using System;
using System.Collections.Generic;

namespace dougnlamb.budget.models {
    public class AccountSelectionModel : IAccountSelectionModel {
        private ISecurityContext mSecurityContext;

        public AccountSelectionModel() { }
        public AccountSelectionModel(ISecurityContext securityContext, IAccount account) {
            mSecurityContext = securityContext;
            SelectedItem = account?.View(securityContext) ?? null;
        }

        public IList<IAccountViewModel> Accounts {
            get {
                throw new NotImplementedException();
            }
        }

        public IAccountViewModel SelectedItem { get; set; }

        public IAccount SelectedAccount {
            get {
                if(SelectedItem == null) {
                    return null;
                }
                else {
                    return new Account(mSecurityContext, SelectedItem.oid);
                }
            }
        }
    }
}