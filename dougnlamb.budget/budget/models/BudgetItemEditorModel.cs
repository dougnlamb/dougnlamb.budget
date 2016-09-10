using System;
using dougnlamb.budget.models;
using dougnlamb.core.security;

namespace dougnlamb.budget {
    internal class BudgetItemEditorModel : IBudgetItemEditorModel {
        private IBudgetItem mBudgetItem;
        private ISecurityContext securityContext;

        public BudgetItemEditorModel(ISecurityContext securityContext, IBudgetItem budgetItem) {
            this.securityContext = securityContext;
            this.mBudgetItem = budgetItem;
            this.oid = budgetItem?.oid ?? 0;
        }

        public int oid { get; internal set; }
        public IMoneyEditorModel Amount { get; set; }
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

        private void Close() {
            IsClosed = true;
            //ClosedBy = user;
            ClosedDate = DateTime.Now;
        }

        private void UpdateBudgetItemBalance() {
            IMoney bal = new Money() { Amount = Amount.Amount };
            bal.Currency = new Currency() { Code = Amount.Currency.Code };

            foreach(IAllocation allocation in mBudgetItem?.Allocations) {
                bal.Add(allocation.Amount);
            }
            Balance = bal;
        }

        public IBudgetItem Save(ISecurityContext securityContext) {
            if( mBudgetItem == null) {
                if(oid == 0) {
                    mBudgetItem = new BudgetItem(securityContext);
                }
                else {
                    mBudgetItem = BudgetItem.GetDao().Retrieve(securityContext, oid);
                }
            }

            if(MarkClosed) {
                Close();
            }

            if(UpdateBalance) {
                UpdateBudgetItemBalance();
            }

            mBudgetItem.Save(securityContext, this);

            return mBudgetItem;
        }
    }
}