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

        bool MarkClosed { get; set; }
        bool UpdateBalance { get; set; }

        IAccount DefaultAccount { get; set; }
        IBudget Budget { get; set; }

        IMoney Balance { get; }
        IBudgetItem Save(ISecurityContext securityContext);
    }
}