namespace dougnlamb.budget {
    public interface IAccountEditorModel {
        int oid { get; set; }

        string Name { get; set; }
        ICurrency DefaultCurrency { get; set; }
    }
}