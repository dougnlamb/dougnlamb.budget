using dougnlamb.core;
using dougnlamb.core.collections;
using dougnlamb.core.security;
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

        ITransactionEditorModel CreateTransaction(ISecurityContext securityContext);
        ITransaction AddTransaction(ISecurityContext securityContext, ITransactionEditorModel model);

        IPagedList<ITransaction> Transactions { get; }
        IPagedList<ITransaction> GetTransactionsSince(DateTime date);

        IMoney ClearedBalance { get; }
        IMoney Balance { get; }

        IReadOnlyList<IUserAccess> UserAccessList { get; }
        IUserAccess AddUserAccess(IUser user, UserAccessMode accessMode);

        IAccountEditorModel Edit(ISecurityContext securityContext);
        void Save(ISecurityContext securityContext, IAccountEditorModel model);

        void Refresh(ISecurityContext securityContext);
    }
}
