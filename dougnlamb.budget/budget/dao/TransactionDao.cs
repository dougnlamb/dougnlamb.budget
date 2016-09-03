using System;
using dougnlamb.core.security;

namespace dougnlamb.budget.dao {
    public class TransactionDao : ITransactionDao {
        public ITransaction Retrieve(ISecurityContext securityContext, int oid) {
            return MockDatabase.RetrieveTransaction(oid);
        }

        public int Save(ISecurityContext securityContext, ITransaction transaction) {
            return MockDatabase.InsertTransaction(transaction);
        }
    }
}