namespace dougnlamb.budget {
    public interface IUserEditorModel {
        string UserId { get; }
        string DisplayName { get; set; }
        string Email { get; set; }
    }
}