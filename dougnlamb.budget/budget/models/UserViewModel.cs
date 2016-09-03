using System;
using dougnlamb.budget.models;
using dougnlamb.core.security;

namespace dougnlamb.budget {
    public class UserViewModel : IUserViewModel {
        private ISecurityContext securityContext;
        private User user;

        public UserViewModel(ISecurityContext securityContext, User user) {
            this.securityContext = securityContext;
            this.user = user;
        }

        public string DisplayName {
            get {
                return this.user.DisplayName;
            }
        }

        public string Email {
            get {
                return this.user.Email;
            }
        }

        public int oid {
            get {
                return this.user.oid;
            }
        }

        public string UserId {
            get {
                return this.user.UserId;
            }
        }
    }
}