using dougnlamb.core.security;
using System;
using System.Collections.Generic;

namespace dougnlamb.budget.models {
    public class MoneyViewModel : IMoneyViewModel {
        public MoneyViewModel(ISecurityContext securityContext, IMoney money) {
            Amount = money?.Value ?? 0;
            Currency = money?.Currency?.View(securityContext) ?? new CurrencyViewModel(securityContext, null);
        }

        public decimal Amount { get; internal set; }
        public ICurrencyViewModel Currency { get; internal set; }
    }
}