using dougnlamb.core.security;

namespace dougnlamb.budget.models {
    public interface IAccountViewModel {
        int oid { get; }

        string Name { get; }

        IUserViewModel Owner { get; }
        ICurrencyViewModel DefaultCurrency { get; }

    }
}