using dougnlamb.core.security;
using System.Collections.Generic;

namespace dougnlamb.budget {
    public interface ICurrencyDao {
        ICurrency Retrieve(ISecurityContext securityContext, int oid);
        IList<ICurrency> RetrieveAll(ISecurityContext securityContext);
    }
}