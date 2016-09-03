using dougnlamb.core.security;

namespace dougnlamb.budget.models {
    public interface ICurrencyViewModel {
        int oid { get; }
        string Code { get; }
        string Description { get; }
    }
}