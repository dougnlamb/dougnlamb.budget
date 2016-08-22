using dougnlamb.core.collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dougnlamb.budget {
    public interface IAccount {
        int oid { get; set; }
        IUser Owner { get; set; }
        string Name { get; set; }

        IPagedList<ITransaction> Transactions { get; }
        IPagedList<ITransaction> GetTransactionsSince(DateTime date);

        Decimal ClearedBalance { get; set; }
        Decimal Balance { get; set; }
    }
}
