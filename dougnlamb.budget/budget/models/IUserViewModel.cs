using dougnlamb.core.collections;
using dougnlamb.core.security;

namespace dougnlamb.budget.models {
    public interface IUserViewModel {
        int oid { get; }

        string UserId { get; }
        string DisplayName { get; }
        string Email { get; }

        IObservableList<IAccountViewModel> Accounts { get; }
        IObservableList<IBudgetViewModel> Budgets { get; }

    }
}