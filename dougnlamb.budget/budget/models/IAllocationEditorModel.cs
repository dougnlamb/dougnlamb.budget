namespace dougnlamb.budget.models {
    public interface IAllocationEditorModel {
        int oid { get; }

        IMoney Amount { get; set; }
        string Notes { get; set; }
    }
}