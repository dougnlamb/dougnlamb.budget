﻿using dougnlamb.core.security;

namespace dougnlamb.budget.models {
    public interface IBudgetViewModel {
        int oid { get; }

        string Name { get; }
        IUserViewModel Owner { get; }
        IBudgetPeriodViewModel Period { get; }
    }
}