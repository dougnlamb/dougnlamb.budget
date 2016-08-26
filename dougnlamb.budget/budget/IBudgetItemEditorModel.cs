using System;

namespace dougnlamb.budget {
    public interface IBudgetItemEditorModel {
        int oid { get; set; }

        string Name { get; set; }
        IMoney Amount { get; set; }
        string Notes { get; set; }
        DateTime ReminderDate { get; set; }
        DateTime DueDate { get; set; }
    }
}