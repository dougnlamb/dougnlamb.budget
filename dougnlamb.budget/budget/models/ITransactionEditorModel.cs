using System;

namespace dougnlamb.budget.models {
    public interface ITransactionEditorModel {
        int oid { get; }

        IMoney TransactionAmount { get; set; }
        string Note { get; set; }
        DateTime TransactionDate { get; set; }
    }
}