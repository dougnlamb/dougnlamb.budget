using System.Collections.Generic;

namespace dougnlamb.budget.models {
    public interface IBudgetItemSelectionModel {
        IBudgetItemViewModel SelectedItem { get; set; }
        IList<IBudgetItemViewModel> BudgetItems { get; }
    }
}