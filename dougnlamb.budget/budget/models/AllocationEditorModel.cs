using System;
using dougnlamb.core.security;

namespace dougnlamb.budget.models {
    public class AllocationEditorModel : IAllocationEditorModel {
        private IAllocation mAllocation;
        private ISecurityContext mSecurityContext;

        public AllocationEditorModel(ISecurityContext securityContext, IAllocation allocation) {
            this.mAllocation = allocation;
            this.mSecurityContext = securityContext;
            this.oid = allocation?.oid ?? 0;
            this.Notes = allocation?.Notes ?? "";
            this.Amount = allocation?.Amount ?? new Money();
            this.AmountEditor = Amount.Edit(securityContext);
            this.BudgetItemSelector = new BudgetItemSelectionModel(securityContext, null);
            this.TransactionSelector = new TransactionSelectionModel(securityContext, null);
            this.BudgetItem = allocation?.BudgetItem ?? null;
            this.Transaction = allocation?.Transaction ?? null;
        }

        public int oid { get; internal set; }
        public string Notes { get; set; }
        public IMoney Amount { get; set; }

        public IMoneyEditorModel AmountEditor { get; set; }
        public IBudgetItemSelectionModel BudgetItemSelector { get; set; }
        public ITransactionSelectionModel TransactionSelector { get; set; }

        public IBudgetItem BudgetItem {
            get {
                return BudgetItemSelector?.SelectedBudgetItem;
            }
            set {
                BudgetItemSelector.SelectedItem = value?.View(mSecurityContext) ?? null; 
            }
        }

        public ITransaction Transaction {
            get {
                return TransactionSelector.SelectedTransaction;
            }
            set {
                TransactionSelector.SelectedItem = value?.View(mSecurityContext) ?? null;
            }
        }

        public IAllocation Save(ISecurityContext securityContext) {
            if (mAllocation == null) {
                if (this.oid > 0) {
                    mAllocation = Allocation.GetDao().Retrieve(securityContext, this.oid);
                }
                else {
                    mAllocation = new Allocation(securityContext);
                }
            }

            mAllocation.Save(securityContext, this);

            IBudgetItemEditorModel budgetItemEditor = mAllocation.BudgetItem.Edit(securityContext);
            budgetItemEditor.UpdateBalance = true;
            budgetItemEditor.Save(securityContext);

            return mAllocation;
        }
    }
}