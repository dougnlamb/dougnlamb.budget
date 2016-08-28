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
            return new Budget {
                oid = oid,
                Name = "Bubba's budget",
            };
        }

        public void Save(ISecurityContext securityContext, IBudget budget) {
            throw new NotImplementedException();
        }
    }
}
