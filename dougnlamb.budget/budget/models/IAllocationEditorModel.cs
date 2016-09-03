namespace dougnlamb.budget.models {
    public interface IAllocationEditorModel {
        int oid { get; }

        IMoneyEditorModel Amount { get; set; }
        string Notes { get; set; }
    }
}