using dougnlamb.core.security;

namespace dougnlamb.budget.models {
    public interface IUserRegistrationModel {

        string UserId { get; set; }
        string DisplayName { get; set; }
        string Email { get; set; }

        ICurrency DefaultCurrency { get; set; }

        IUser Save(ISecurityContext securityContext);
    }
}