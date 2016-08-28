using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dougnlamb.core.collections;
using dougnlamb.core.security;

namespace dougnlamb.budget.dao {
    public class AccountDao : IAccountDao {

        public IPagedList<IAccount> Find(ISecurityContext securityContext, string name) {
            throw new NotImplementedException();
        }

        public IAccount Retrieve(ISecurityContext securityContext, int oid) {
            return MockDatabase.RetrieveAccount(oid);
        }

        public int Save(ISecurityContext securityContext, IAccount account) {
            if (account.oid == 0) {
                return MockDatabase.InsertAccount(account);
            }
            else {
                MockDatabase.UpdateAccount(account);
                return account.oid;
            }
        }
    }
}
