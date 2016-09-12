using dougnlamb.core.security;

namespace dougnlamb.budget.models {
    public class QuickTransactionReportModel { 

        public QuickTransactionReportModel() {
        }

        IBudgetItemSelectionModel BudgetItemSelector { get; }
        public TransactionEditorModel TransactionEditor { get; set; }

        public ITransaction Save(ISecurityContext securityContext) {
            ITransaction transaction = TransactionEditor.Save(securityContext);

            AllocationEditorModel allocationEditor = new AllocationEditorModel(securityContext,null);
            allocationEditor.Amount = TransactionEditor.TransactionAmount;
            allocationEditor.BudgetItem = BudgetItemSelector.SelectedBudgetItem;

            allocationEditor.Save(securityContext);

            return transaction;
        }
    }
}
