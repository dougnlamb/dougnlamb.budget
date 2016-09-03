using dougnlamb.core.security;

namespace dougnlamb.budget.models {
    public interface IMoneyViewModel {
        decimal Amount { get; }
        ICurrencyViewModel Currency { get; }
    }
}