using dougnlamb.budget.models;
using dougnlamb.core.security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dougnlamb.budget {
    public interface IMoney {
        decimal Amount { get; }
        ICurrency Currency { get; }
        void Add(IMoney amount);

        IMoneyViewModel View(ISecurityContext securityContext);
    }
}
