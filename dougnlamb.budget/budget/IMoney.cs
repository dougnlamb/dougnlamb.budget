using dougnlamb.budget.models;
using dougnlamb.core.security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dougnlamb.budget {
    public interface IMoney {
        decimal Amount { get; set; }
        ICurrency Currency { get; set; }

        IMoneyViewModel View(ISecurityContext securityContext);
        void Add(IMoney amount);
    }
}
