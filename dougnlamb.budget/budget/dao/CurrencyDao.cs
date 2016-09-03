using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dougnlamb.core.security;

namespace dougnlamb.budget.dao {
    public class CurrencyDao : ICurrencyDao {
        public ICurrency Retrieve(ISecurityContext securityContext, int oid) {
            return MockDatabase.RetrieveCurrency(oid);
        }
    }
}
