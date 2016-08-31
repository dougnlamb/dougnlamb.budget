using dougnlamb.core.security;

namespace dougnlamb.budget.models {
    public interface IUserEditorModel {
        int oid { get; }

        string UserId { get; }
        string DisplayName { get; set; }
        string Email { get; set; }

        IUser Save(ISecurityContext securityContext);
    }
}