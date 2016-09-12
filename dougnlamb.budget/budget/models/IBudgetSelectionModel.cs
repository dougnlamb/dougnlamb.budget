using System.Collections.Generic;

namespace dougnlamb.budget.models {
    public interface IBudgetSelectionModel {
        IBudget SelectedBudget { get; }

        IBudgetViewModel SelectedItem { get; set; }
        IList<IBudgetViewModel> Budgets { get; }
    }
}