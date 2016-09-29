using dougnlamb.core.collections;
using dougnlamb.core.security;

namespace dougnlamb.budget.dao {
    public interface ITransactionDao {
        ITransaction Retrieve(ISecurityContext securityContext, int oid);
        IPagedList<ITransaction> Retrieve(ISecurityContext securityContext, IAccount account);
        int Save(ISecurityContext securityContext, ITransaction transaction);
    }
}