using dougnlamb.core.security;

namespace dougnlamb.budget.models {
    public interface IBudgetViewModel {
        int oid { get; }

        string Name { get; }

        ICurrencyViewModel DefaultCurrency { get; }
        IUserViewModel Owner { get; }
        IBudgetPeriodViewModel Period { get; }
    }
}