using System;
using System.Collections.Generic;

namespace dougnlamb.budget.models {
    public interface ITransactionEditorModel {
        int oid { get; }

        IMoneyEditorModel TransactionAmount { get; set; }

        string Note { get; set; }
        DateTime TransactionDate { get; set; }
    }
}