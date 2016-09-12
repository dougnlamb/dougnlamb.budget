using dougnlamb.core.security;
using System;
using System.Collections.Generic;

namespace dougnlamb.budget.models {
    public class TransactionSelectionModel : ITransactionSelectionModel {
        private ISecurityContext mSecurityContext;

        public TransactionSelectionModel() { }
        public TransactionSelectionModel(ISecurityContext securityContext, ITransaction transaction) {
            mSecurityContext = securityContext;
            SelectedItem = transaction?.View(securityContext) ?? null;
        }

        public IList<ITransactionViewModel> Transactions {
            get {
                throw new NotImplementedException();
            }
        }

        public ITransactionViewModel SelectedItem { get; set; }
        public ITransaction SelectedTransaction {
            get {
                if (SelectedItem == null) {
                    return null;
                }
                else {
                    return new Transaction(mSecurityContext, SelectedItem.oid);
                }
            }
        }
    }
}