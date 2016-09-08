using System;
using System.Collections.Generic;

namespace dougnlamb.budget.models {
    public class AccountSelectionModel : IAccountSelectionModel {
        public AccountSelectionModel(int oid) {
            SelectedAccountId = oid;
        }

        public IList<IAccountViewModel> Accounts {
            get {
                throw new NotImplementedException();
            }
        }

        public int SelectedAccountId { get; set; }

        public IAccountViewModel SelectedAccount {
            get {
                throw new NotImplementedException();
            }
        }
    }
}