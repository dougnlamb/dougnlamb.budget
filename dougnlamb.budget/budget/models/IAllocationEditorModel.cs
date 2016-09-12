using dougnlamb.core.security;

namespace dougnlamb.budget.models {
    public interface IAllocationEditorModel {
        int oid { get; }

        IBudgetItem BudgetItem { get; set; }
        ITransaction Transaction { get; set; }

        IMoney Amount { get; set; }
        string Notes { get; set; }

        IAllocation Save(ISecurityContext securityContext);
    }
}