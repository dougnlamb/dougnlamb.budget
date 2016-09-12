using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dougnlamb.core.collections;
using dougnlamb.core.security;

namespace dougnlamb.budget.dao {
    public class BudgetDao : IBudgetDao {
        public IPagedList<IBudget> Find(ISecurityContext securityContext, string name) {
            throw new NotImplementedException();
        }

        public IBudget Retrieve(ISecurityContext securityContext, int oid) {
            return MockDatabase.RetrieveBudget(oid);
        }

        public IList<IBudget> RetrieveAll(ISecurityContext mSecurityContext) {
            throw new NotImplementedException();
        }

        public int Save(ISecurityContext securityContext, IBudget budget) {
            if(budget.oid == 0 ) {
                return MockDatabase.InsertBudget(budget);
            }
            else {
                MockDatabase.UpdateBudget(budget);
                return budget.oid;
            }
        }
    }
}
