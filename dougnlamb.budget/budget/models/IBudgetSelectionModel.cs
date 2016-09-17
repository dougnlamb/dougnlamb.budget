using System.Collections.Generic;

namespace dougnlamb.budget.models {
    public interface IBudgetSelectionModel {
        int SelectedBudgetId { get; }
        IList<IBudgetViewModel> Budgets { get; }
    }
}