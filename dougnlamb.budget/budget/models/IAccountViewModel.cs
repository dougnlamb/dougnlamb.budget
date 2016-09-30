using dougnlamb.core.collections;
using dougnlamb.core.security;

namespace dougnlamb.budget.models {
    public interface IAccountViewModel {
        int oid { get; }

        string Name { get; }

        IUserViewModel Owner { get; }
        ICurrencyViewModel DefaultCurrency { get; }

        IMoneyViewModel Balance { get; }

        IPagedList<ITransactionViewModel> Transactions { get; }

    }
}