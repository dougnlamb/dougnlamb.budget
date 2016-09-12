using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dougnlamb.core.collections;
using dougnlamb.core.security;

namespace dougnlamb.budget.dao {
    public class AllocationDao : IAllocationDao {
        public IObservableList<IAllocation> Retrieve(ISecurityContext securityContext, IBudgetItem budgetItem) {
            return MockDatabase.RetrieveAllocations(budgetItem);
        }
        public IObservableList<IAllocation> Retrieve(ISecurityContext securityContext, ITransaction transaction) {
            return MockDatabase.RetrieveAllocations(transaction);
        }

        public IAllocation Retrieve(ISecurityContext securityContext, int oid) {
            return MockDatabase.RetrieveAllocation(oid);
        }

        public int Save(ISecurityContext securityContext, IAllocation account) {
            if (account.oid == 0) {
                return MockDatabase.InsertAllocation(account);
            }
            else {
                MockDatabase.UpdateAllocation(account);
                return account.oid;
            }
        }
    }
}
