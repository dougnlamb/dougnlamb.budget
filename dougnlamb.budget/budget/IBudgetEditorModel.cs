namespace dougnlamb.budget {
    public interface IBudgetEditorModel {
        int oid { get; }

        string Name { get; set; }
        IBudgetPeriod Period { get; set; }
    }
}