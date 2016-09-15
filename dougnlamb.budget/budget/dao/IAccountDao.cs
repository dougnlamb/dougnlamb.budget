using dougnlamb.core.collections;
using dougnlamb.core.security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dougnlamb.budget.dao {
    public interface IAccountDao {
        IAccount Retrieve(ISecurityContext securityContext, int oid);
        IObservableList<IAccount> Retrieve(ISecurityContext securityContext, IUser user);
        IObservableList<IAccount> Find(ISecurityContext securityContext, string name);
        int Save(ISecurityContext securityContext, IAccount account);
    }
}
