using dougnlamb.core.security;

namespace dougnlamb.budget.dao {
    public interface ITransactionDao {
        ITransaction Retrieve(ISecurityContext securityContext, int oid);
        int Save(ISecurityContext securityContext, ITransaction transaction);
    }
}