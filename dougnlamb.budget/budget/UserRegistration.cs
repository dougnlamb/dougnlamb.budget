using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dougnlamb.budget.models;
using dougnlamb.core.security;

namespace dougnlamb.budget {
    public class UserRegistration : IUserRegistration {
        public IUserRegistrationModel RegisterUser() {
            return new UserRegistrationModel();
        }

        public IUser Save(ISecurityContext securityContext, IUserRegistrationModel model) {
            IUser usr = new User(null);
            usr.Save(securityContext, model);
            return usr;
        }
    }
}
