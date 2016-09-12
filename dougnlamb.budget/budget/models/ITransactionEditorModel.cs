using System;
using System.Collections.Generic;
using dougnlamb.core.security;

namespace dougnlamb.budget.models {
    public interface ITransactionEditorModel {
        int oid { get; }

        IMoney TransactionAmount { get; set; }
        IAccount Account { get; set; }

        string Note { get; set; }
        DateTime TransactionDate { get; set; }

        ITransaction Save(ISecurityContext securityContext);
    }
}