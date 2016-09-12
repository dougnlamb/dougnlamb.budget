using System.Collections.Generic;

namespace dougnlamb.budget.models {
    public interface IBudgetItemSelectionModel {
        IBudgetItem SelectedBudgetItem { get; }

        IBudgetItemViewModel SelectedItem { get; set; }
        IList<IBudgetItemViewModel> BudgetItems { get; }
    }
}