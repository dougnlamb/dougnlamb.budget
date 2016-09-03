using dougnlamb.core.security;

namespace dougnlamb.budget {
    public interface ICurrencyDao {
        ICurrency Retrieve(ISecurityContext securityContext, int oid);
    }
}