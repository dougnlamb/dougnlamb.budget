using System;
using dougnlamb.core.security;

namespace dougnlamb.budget.models {
    public class AllocationEditorModel : IAllocationEditorModel {
        private ISecurityContext securityContext;

        public AllocationEditorModel(ISecurityContext securityContext) {
            this.securityContext = securityContext;
        }

        public int oid { get; internal set; }
        public string Notes { get; set; }
        public IMoneyEditorModel Amount { get; set; }

        public IBudgetItemSelectionModel BudgetItemSelector { get; set; }
        public ITransactionSelectionModel TransactionSelector { get; set; }

        public IAllocation Save(ISecurityContext securityContext) {
            throw new NotImplementedException();
        }
    }
}