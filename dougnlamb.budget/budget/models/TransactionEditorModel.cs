using System;
using dougnlamb.budget.models;
using dougnlamb.core.security;

namespace dougnlamb.budget {
    public class TransactionEditorModel : ITransactionEditorModel {
        private ISecurityContext securityContext;
        private ITransaction mTransaction;

        public TransactionEditorModel() {
        }

        public TransactionEditorModel(ISecurityContext securityContext, IUser user, IAccount account) {
            this.securityContext = securityContext;
            AccountSelector = new AccountSelectionModel(securityContext, user, account);
            TransactionAmountEditor = new MoneyEditorModel(new Money() { Value = 0, Currency = account.DefaultCurrency });
        }

        public int oid { get; internal set; }
        public AccountSelectionModel AccountSelector { get; set; }
        public string Note { get; set; }
        public IMoney TransactionAmount {
            get {
                return new Money() { Value = TransactionAmountEditor.Amount, Currency = TransactionAmountEditor.Currency };
            }
            internal set {
                TransactionAmountEditor.Amount = value?.Value ?? 0;
                TransactionAmountEditor.CurrencySelector.SelectedCurrencyCode = value?.Currency?.oid ?? 0;
            }
        }
        public MoneyEditorModel TransactionAmountEditor { get; set; }

        public DateTime TransactionDate { get; set; }

        public IAccount Account {
            get {
                if(AccountSelector.SelectedAccountId > 0 ) {
                    return new Account(null, AccountSelector.SelectedAccountId);
                }
                else {
                    return null;
                }
            }
            internal set {
                AccountSelector.SelectedAccountId = value?.oid ?? 0;
            }
        }

        public ITransaction Save(ISecurityContext securityContext) {
            if (mTransaction == null) {
                if (this.oid > 0) {
                    mTransaction = Transaction.GetDao().Retrieve(securityContext, this.oid);
                }
                else {
                    mTransaction = new Transaction(null);
                }
            }

            mTransaction.Save(securityContext, this);

            return mTransaction;
        }
    }
}