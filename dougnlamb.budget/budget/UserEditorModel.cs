using dougnlamb.core.security;
using System;

namespace dougnlamb.budget {
    internal class UserEditorModel : IUserEditorModel {

        private IUser mUser;

        public UserEditorModel(IUser user) {
            this.mUser = user;

            this.UserId = user.UserId;
            this.DisplayName = user.DisplayName;
            this.Email = user.Email;
        }

        public int oid { get; protected set; }
        public string UserId { get; protected set;}

        public string DisplayName { get; set; }

        public string Email { get; set;}

        public IUser Save(ISecurityContext securityContext) {
            if (mUser == null) {
                if (this.oid > 0) {
                    mUser = User.GetDao().Retrieve(securityContext, this.oid);
                }
                else {
                    mUser = new User();
                }
            }

            mUser.Save(securityContext, this);

            return mUser;
        }
    }
}