using System;
using dougnlamb.budget.models;
using dougnlamb.core.security;

namespace dougnlamb.budget {
    public class BudgetItemEditorModel : IBudgetItemEditorModel {
        private IBudgetItem mBudgetItem;
        private ISecurityContext mSecurityContext;

        public BudgetItemEditorModel() {
            this.DefaultAccountSelector = new AccountSelectionModel();
            this.BudgetSelector = new BudgetSelectionModel();
        }

        public BudgetItemEditorModel(ISecurityContext securityContext, IUser user, IBudget budget) {
            this.mSecurityContext = securityContext;
            this.mBudgetItem = new BudgetItem(securityContext);
            this.Name = "";
            this.Notes = "";
            this.AmountEditor = new MoneyEditorModel();
            this.IsClosed = false;
            this.ClosedBy = null;
            this.DueDate = DateTime.Now.AddMonths(1);
            this.ReminderDate = DueDate.AddDays(-7);

            this.DefaultAccountSelector = new AccountSelectionModel(mSecurityContext, user, null);
            this.BudgetSelector = new BudgetSelectionModel(mSecurityContext, user, budget);
        }

        public BudgetItemEditorModel(ISecurityContext securityContext, IUser user, IBudgetItem budgetItem) {
            this.mSecurityContext = securityContext;
            this.mBudgetItem = budgetItem;
            this.oid = budgetItem?.oid ?? 0;
            this.Name = budgetItem?.Name ?? "";
            this.Notes = budgetItem?.Notes ?? "";
            this.AmountEditor = new MoneyEditorModel(budgetItem?.BudgetAmount);
            this.IsClosed = budgetItem?.IsClosed ?? false;
            this.ClosedBy = budgetItem?.ClosedBy ?? null;
            this.DueDate = budgetItem?.DueDate ?? DateTime.Now.AddMonths(1);
            this.ReminderDate = budgetItem?.ReminderDate ?? DueDate.AddDays(-7);

            this.DefaultAccountSelector = new AccountSelectionModel(mSecurityContext, user, budgetItem?.DefaultAccount);
            this.BudgetSelector = new BudgetSelectionModel(mSecurityContext, user, budgetItem?.Budget);
        }

        public int oid { get; internal set; }
        public IMoney Amount {
            get {
                return new Money() { Value = AmountEditor.Amount, Currency = AmountEditor.Currency };
            }
            set {
                AmountEditor.Amount = value?.Value ?? 0;
                AmountEditor.CurrencySelector.SelectedCurrencyCode = value?.Currency?.oid ?? 0;
            }
        } 
        public MoneyEditorModel AmountEditor { get; set; }
        public DateTime DueDate { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public DateTime ReminderDate { get; set; }
        public bool IsClosed { get; internal set; }
        public IUser ClosedBy { get; internal set; }
        public DateTime ClosedDate { get; internal set; }
        public IMoney Balance { get; internal set; }
        public bool MarkClosed { get; set; }
        public bool UpdateBalance { get; set; }

        public BudgetSelectionModel BudgetSelector { get; set; }
        public IAccountSelectionModel DefaultAccountSelector { get; set; }

        public IAccount DefaultAccount {
            get {
                if(DefaultAccountSelector.SelectedAccountId > 0 ) {
                    return new Account(mSecurityContext, DefaultAccountSelector.SelectedAccountId);
                }
                else {
                    return null;
                }
            }
            set {
                DefaultAccountSelector.SelectedAccountId = value?.oid ?? 0;
            }
        }

        public IBudget Budget {
            get {
                if(BudgetSelector.SelectedBudgetId > 0) {
                    return new Budget(mSecurityContext, BudgetSelector.SelectedBudgetId);
                }
                return null;
            }
            set {
                BudgetSelector.SelectedBudgetId = value?.oid ?? 0;
            }
        }

        private void Close() {
            IsClosed = true;
            //ClosedBy = user;
            ClosedDate = DateTime.Now;
        }

        private void UpdateBudgetItemBalance() {
            IMoney bal = new Money() { Value = Amount.Value, Currency = Amount.Currency };

            foreach (IAllocation allocation in mBudgetItem?.Allocations) {
                bal.Add(allocation.Amount);
            }
            Balance = bal;
        }

        public IBudgetItem Save(ISecurityContext securityContext) {
            if (mBudgetItem == null) {
                if (oid == 0) {
                    mBudgetItem = new BudgetItem(securityContext);
                }
                else {
                    mBudgetItem = BudgetItem.GetDao().Retrieve(securityContext, oid);
                }
            }

            if (MarkClosed) {
                Close();
            }

            if (UpdateBalance) {
                UpdateBudgetItemBalance();
            }

            mBudgetItem.Save(securityContext, this);

            return mBudgetItem;
        }
    }
}