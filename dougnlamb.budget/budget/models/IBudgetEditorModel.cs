using dougnlamb.core.security;

namespace dougnlamb.budget.models {
    public interface IBudgetEditorModel {
        int oid { get; }

        string Name { get; set; }
        IUserViewModel Owner { get; set; }
        IBudgetPeriodViewModel Period { get; set; }

        int DefaultCurrencyId { get; }

        IBudget Save(ISecurityContext securityContext);
    }
}