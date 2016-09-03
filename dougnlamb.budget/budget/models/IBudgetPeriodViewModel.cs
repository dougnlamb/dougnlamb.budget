using dougnlamb.core.security;
using System;

namespace dougnlamb.budget.models {
    public interface IBudgetPeriodViewModel {
        string Name { get; }

        DateTime FromDate { get; }
        DateTime ToDate { get; }
    }
}