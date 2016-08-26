using System;

namespace dougnlamb.budget {
    internal class UserEditorModel : IUserEditorModel {

        public UserEditorModel(IUser user) {
            this.UserId = user.UserId;
            this.DisplayName = user.DisplayName;
            this.Email = user.Email;
        }
        public string UserId { get; protected set;}

        public string DisplayName { get; set; }

        public string Email { get; set;}
    }
}