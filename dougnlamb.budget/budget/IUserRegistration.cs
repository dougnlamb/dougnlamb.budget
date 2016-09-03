using dougnlamb.budget.models;
using dougnlamb.core.security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dougnlamb.budget {
    public interface IUserRegistration {
        IUserRegistrationModel RegisterUser();
        IUser Save(ISecurityContext securityContext, IUserRegistrationModel model);
    }
}
