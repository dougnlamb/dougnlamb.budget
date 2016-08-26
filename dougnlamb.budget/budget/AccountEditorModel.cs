using System;

namespace dougnlamb.budget {
    public class AccountEditorModel : IAccountEditorModel {
        public AccountEditorModel(IUser editingUser, Account account) {
            this.oid = account.oid;
            this.Name = account.Name;
            this.DefaultCurrency = account.DefaultCurrency;
            this.Owner = account.Owner;
            if(this.Owner == null) {
                this.Owner = editingUser;
            }
        }

        public int oid { get; protected set; }
        public IUser Owner { get; set; }
        public string Name { get; set; }
        public ICurrency DefaultCurrency { get; set; }

    }
}