using dougnlamb.core.security;

namespace dougnlamb.budget.models {
    public class QuickTransactionReportModel { 

        private TransactionEditorModel mTransactionModel;
        public QuickTransactionReportModel() {
        }

        IBudgetItemSelectionModel BudgetItemSelector { get; }

        public TransactionEditorModel TransactionModel { get; set; }

        public ITransaction Save(ISecurityContext securityContext) {
            ITransaction transaction = TransactionModel.Save(securityContext);

            AllocationEditorModel allocationEditor = new AllocationEditorModel(securityContext);
            allocationEditor.Amount = TransactionModel.TransactionAmount;
            allocationEditor.BudgetItemSelector = BudgetItemSelector;

            allocationEditor.Save(securityContext);

            return transaction;
        }
    }
}
