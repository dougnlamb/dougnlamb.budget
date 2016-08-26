namespace dougnlamb.budget {
    public interface IAllocationEditorModel {
        int oid { get; set; }

        IMoney Amount { get; set; }
        string Notes { get; set; }
    }
}