using dougnlamb.core.security;
using System;

namespace dougnlamb.budget.models {
    public class UserRegistrationModel : IUserRegistrationModel {

        public UserRegistrationModel() {
            this.UserId = "";
            this.DisplayName = "";
            this.Email = "";
        }

        public string UserId { get; set;}
        public string DisplayName { get; set; }
        public string Email { get; set;}

        public IUser Save(ISecurityContext securityContext) {
            IUserRegistration registration = new UserRegistration();

            return  registration.Save(securityContext, this);
        }
    }
}