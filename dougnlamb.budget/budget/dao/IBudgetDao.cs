using dougnlamb.core.collections;
using dougnlamb.core.security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dougnlamb.budget.dao {
    public interface IBudgetDao {
        IBudget Retrieve(ISecurityContext securityContext, int oid);
        IObservableList<IBudget> Retrieve(ISecurityContext securityContext, IUser user);
        IObservableList<IBudget> Find(ISecurityContext securityContext, string name);
        int Save(ISecurityContext securityContext, IBudget budget);
    }
}
