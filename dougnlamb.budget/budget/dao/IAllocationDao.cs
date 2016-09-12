using dougnlamb.core.collections;
using dougnlamb.core.security;

namespace dougnlamb.budget.dao {
    public interface IAllocationDao {
        IAllocation Retrieve(ISecurityContext securityContext, int oid);
        IObservableList<IAllocation> Retrieve(ISecurityContext securityContext, IBudgetItem budgetItem);
        IObservableList<IAllocation> Retrieve(ISecurityContext securityContext, ITransaction transaction);
        int Save(ISecurityContext securityContext, IAllocation account);
    }
}