using System.Collections.Generic;

namespace dougnlamb.budget.models {
    public interface IBudgetItemSelectionModel {
        IBudgetItem SelectedItem { get; set; }
        IList<IBudgetItem> BudgetItems { get; }
    }
}