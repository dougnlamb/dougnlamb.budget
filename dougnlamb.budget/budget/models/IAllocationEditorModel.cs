using dougnlamb.core.security;

namespace dougnlamb.budget.models {
    public interface IAllocationEditorModel {
        int oid { get; }

        IBudgetItemSelectionModel BudgetItemSelector { get; set; }
        ITransactionSelectionModel TransactionSelector { get; set; }

        IMoneyEditorModel Amount { get; set; }
        string Notes { get; set; }

        IAllocation Save(ISecurityContext securityContext);
    }
}