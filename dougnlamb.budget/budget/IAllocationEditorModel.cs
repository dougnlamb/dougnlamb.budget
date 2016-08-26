namespace dougnlamb.budget {
    public interface IAllocationEditorModel {
        int oid { get; }

        IMoney Amount { get; set; }
        string Notes { get; set; }
    }
}