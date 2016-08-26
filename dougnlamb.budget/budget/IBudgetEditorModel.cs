namespace dougnlamb.budget {
    public interface IBudgetEditorModel {
        int oid { get; set; }

        string Name { get; set; }
        IBudgetPeriod BudgetPeriod { get; set; }
    }
}