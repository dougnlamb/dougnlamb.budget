using System;
using dougnlamb.budget.models;
using dougnlamb.core.security;

namespace dougnlamb.budget {
    public class TransactionEditorModel : ITransactionEditorModel {
        private Account account;
        private ISecurityContext securityContext;
        private ITransaction mTransaction;

        public TransactionEditorModel(ISecurityContext securityContext, Account account) {
            this.securityContext = securityContext;
            this.account = account;

            TransactionAmount =  new MoneyEditorModel(0, account.DefaultCurrency);
        }

        public int oid { get; internal set; }
        public string Note { get; set; }
        public IMoneyEditorModel TransactionAmount { get; set; }

        public DateTime TransactionDate { get; set; }

        public ITransaction Save(ISecurityContext securityContext) {
            if (mTransaction == null) {
                if (this.oid > 0) {
                    mTransaction = Transaction.GetDao().Retrieve(securityContext, this.oid);
                }
                else {
                    mTransaction = new Transaction();
                }
            }

            mTransaction.Save(securityContext, this);

            return mTransaction;
        }
    }
}