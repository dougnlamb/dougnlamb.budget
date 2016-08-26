using dougnlamb.core;
using dougnlamb.core.collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dougnlamb.budget {
    public interface IAccount : IBaseObject {
        int oid { get; }
        IUser Owner { get; }
        string Name { get; }

        ICurrency DefaultCurrency { get; }
        void UpdateDefaultCurrency(ICurrency currency, IUser user);

        ITransaction AddTransaction(IMoney amount, IUser reportingUser, string notes);

        IPagedList<ITransaction> Transactions { get; }
        IPagedList<ITransaction> GetTransactionsSince(DateTime date);

        IMoney ClearedBalance { get; }
        IMoney Balance { get; }

        IReadOnlyList<IUserAccess> UserAccessList { get; }
        IUserAccess AddUserAccess(IUser user, UserAccessMode accessMode);
    }
}
