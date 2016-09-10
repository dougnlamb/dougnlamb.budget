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
            throw new NotImplementedException();
        }

        public IList<IBudgetItem> Retrieve(ISecurityContext mSecurityContext, IBudget budget) {
            throw new NotImplementedException();
        }

        public IList<IBudgetItem> RetrieveOpen(ISecurityContext mSecurityContext, IBudget budget) {
            throw new NotImplementedException();
        }

        public IList<IBudgetItem> RetrieveOpen(ISecurityContext mSecurityContext) {
            throw new NotImplementedException();
        }

        public int Save(ISecurityContext securityContext, IBudgetItem budgetItem) {
            throw new NotImplementedException();
        }
    }
}
