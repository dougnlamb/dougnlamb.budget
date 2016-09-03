using dougnlamb.core.security;
using System;

namespace dougnlamb.budget.models {
    public interface IBudgetItemViewModel {
        int oid { get; }

        string Name { get; }
        IMoneyViewModel Amount { get; }
        string Notes { get; }

        DateTime ReminderDate { get; }
        DateTime DueDate { get; }
    }
}