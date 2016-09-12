using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dougnlamb.core.collections;
using dougnlamb.core.security;

namespace dougnlamb.budget.dao {
    public class BudgetItemDao : IBudgetItemDao {
        public IBudgetItem Retrieve(ISecurityContext securityContext, int oid) {
            return MockDatabase.RetrieveBudgetItem(oid);
        }

        public IList<IBudgetItem> Retrieve(ISecurityContext mSecurityContext, IBudget budget) {
            return MockDatabase.RetrieveBudgetItems(budget);
        }

        public IList<IBudgetItem> RetrieveOpen(ISecurityContext mSecurityContext, IBudget budget) {
            throw new NotImplementedException();
        }

        public IList<IBudgetItem> RetrieveOpen(ISecurityContext mSecurityContext) {
            throw new NotImplementedException();
        }

        public int Save(ISecurityContext securityContext, IBudgetItem budgetItem) {
            if (budgetItem.oid == 0) {
                return MockDatabase.InsertBudgetItem(budgetItem);
            }
            else {
                MockDatabase.UpdateBudgetItem(budgetItem);
                return budgetItem.oid;
            }
        }
    }
}
