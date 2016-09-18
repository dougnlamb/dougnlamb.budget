using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dougnlamb.budget.models;
using dougnlamb.core.security;

namespace dougnlamb.budget {
    public class Money : IMoney {
        public decimal Value { get; set; }
        public ICurrency Currency { get; set; }

        public void Add(IMoney money) {
            Value += money.Currency.Convert(money).Value;
        }

        public IMoneyViewModel View(ISecurityContext securityContext) {
            return new MoneyViewModel(securityContext, this);
        }
    }
}
