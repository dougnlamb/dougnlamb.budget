using System;

namespace dougnlamb.budget.models {
    public interface ITransactionViewModel {
        int oid { get; }

        IMoneyViewModel TransactionAmount { get; }
        string Note { get; }
        DateTime TransactionDate { get; }
    }
}