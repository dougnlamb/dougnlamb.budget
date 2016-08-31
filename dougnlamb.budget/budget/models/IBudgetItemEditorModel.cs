using dougnlamb.core.security;
using System;

namespace dougnlamb.budget.models {
    public interface IBudgetItemEditorModel {
        int oid { get; }

        string Name { get; set; }
        IMoney Amount { get; set; }
        string Notes { get; set; }

        DateTime ReminderDate { get; set; }
        DateTime DueDate { get; set; }

        IBudget Save(ISecurityContext securityContext);
    }
}