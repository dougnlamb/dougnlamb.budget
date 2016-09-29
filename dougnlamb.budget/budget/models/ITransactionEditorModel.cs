using System;
using System.Collections.Generic;
using dougnlamb.core.security;

namespace dougnlamb.budget.models {
    public interface ITransactionEditorModel {
        int oid { get; }

        IMoney TransactionAmount { get; }
        IAccount Account { get; }

        string Note { get; }
        DateTime TransactionDate { get; }

        ITransaction Save(ISecurityContext securityContext);
    }
}