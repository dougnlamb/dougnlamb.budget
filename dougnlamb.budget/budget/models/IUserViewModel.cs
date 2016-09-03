using dougnlamb.core.security;

namespace dougnlamb.budget.models {
    public interface IUserViewModel {
        int oid { get; }

        string UserId { get; }
        string DisplayName { get; }
        string Email { get; }

    }
}