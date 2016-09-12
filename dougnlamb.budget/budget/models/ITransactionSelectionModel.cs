using System.Collections.Generic;

namespace dougnlamb.budget.models {
    public interface ITransactionSelectionModel {
        ITransaction SelectedTransaction { get; }

        ITransactionViewModel SelectedItem { get; set; }
        IList<ITransactionViewModel> Transactions { get; }
    }
}