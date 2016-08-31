using dougnlamb.core.security;

namespace dougnlamb.budget.models {
    public interface IBudgetEditorModel {
        int oid { get; }

        string Name { get; set; }
        IUser Owner { get; set; }
        IBudgetPeriod Period { get; set; }

        IBudget Save(ISecurityContext securityContext);
    }
}