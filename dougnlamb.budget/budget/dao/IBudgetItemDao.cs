using System.Collections.Generic;
using dougnlamb.core.security;

namespace dougnlamb.budget.dao {
    public interface IBudgetItemDao {
        IBudgetItem Retrieve(ISecurityContext securityContext, int oid);
        IList<IBudgetItem> Retrieve(ISecurityContext mSecurityContext, IBudget budget);
        IList<IBudgetItem> RetrieveOpen(ISecurityContext mSecurityContext, IBudget budget);
        IList<IBudgetItem> RetrieveOpen(ISecurityContext mSecurityContext);
        int Save(ISecurityContext securityContext, IBudgetItem budgetItem);
    }
}