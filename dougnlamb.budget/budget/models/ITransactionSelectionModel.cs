using System.Collections.Generic;

namespace dougnlamb.budget.models {
    public interface ITransactionSelectionModel {
        ITransaction SelectedItem { get; set; }
        IList<ITransaction> Transactions { get; }
    }
}