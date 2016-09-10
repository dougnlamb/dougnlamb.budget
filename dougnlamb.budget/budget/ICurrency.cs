using dougnlamb.budget.models;
using dougnlamb.core.security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dougnlamb.budget {
    public interface ICurrency {
        int oid { get; set; }
        string Code { get; set; }
        string Description { get; set; }

        IMoney Convert(IMoney money);

        decimal GetConversionFactor(ICurrency currency);

        ICurrencyViewModel View(ISecurityContext securityContext);
    }
}
