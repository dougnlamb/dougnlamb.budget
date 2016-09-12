using System;
using dougnlamb.budget.models;
using dougnlamb.core.security;

namespace dougnlamb.budget {
    public class TransactionEditorModel : ITransactionEditorModel {
        private ISecurityContext securityContext;
        private ITransaction mTransaction;

        public TransactionEditorModel() {
        }

        public TransactionEditorModel(ISecurityContext securityContext, Account account) {
            this.securityContext = securityContext;
            AccountSelector = new AccountSelectionModel(securityContext, account);
            TransactionAmountEditor = new MoneyEditorModel(new Money() { Amount = 0, Currency = account.DefaultCurrency });
        }

        public int oid { get; internal set; }
        public IAccountSelectionModel AccountSelector { get; set; }
        public string Note { get; set; }
        public IMoney TransactionAmount {
            get {
                return new Money() { Amount = TransactionAmountEditor.Amount, Currency = TransactionAmountEditor.Currency };
            }
            set {
                TransactionAmountEditor.Amount = value?.Amount ?? 0;
                TransactionAmountEditor.CurrencySelector.SelectedItem = value?.Currency?.View(securityContext) ?? null;
            }
        }
        public MoneyEditorModel TransactionAmountEditor { get; set; }

        public DateTime TransactionDate { get; set; }

        public IAccount Account {
            get {
                return AccountSelector.SelectedAccount;
            }
            set {
                AccountSelector.SelectedItem = value?.View(securityContext);
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