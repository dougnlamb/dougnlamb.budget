using System.Collections.Generic;
using dougnlamb.core.security;
using dougnlamb.core.collections;

namespace dougnlamb.budget.dao {
    public interface IBudgetItemDao {
        IBudgetItem Retrieve(ISecurityContext securityContext, int oid);
        IObservableList<IBudgetItem> Retrieve(ISecurityContext mSecurityContext, IBudget budget);
        IObservableList<IBudgetItem> RetrieveOpen(ISecurityContext mSecurityContext, IBudget budget);
        IObservableList<IBudgetItem> RetrieveOpen(ISecurityContext mSecurityContext, IUser user);
        int Save(ISecurityContext securityContext, IBudgetItem budgetItem);
    }
}