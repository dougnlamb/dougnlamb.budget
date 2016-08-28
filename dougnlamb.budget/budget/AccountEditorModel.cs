using dougnlamb.budget.dao;
using dougnlamb.core.security;
using System;

namespace dougnlamb.budget {
    public class AccountEditorModel : IAccountEditorModel {
        private IAccount mAccount;
        public AccountEditorModel(ISecurityContext securityContext, IAccount account) {
            this.mAccount = account;

            this.oid = account.oid;
            this.Name = account.Name;
            this.DefaultCurrency = account.DefaultCurrency;
            this.Owner = account.Owner;
        }

        public int oid { get; protected set; }
        public IUser Owner { get; set; }
        public string Name { get; set; }
        public ICurrency DefaultCurrency { get; set; }

        public IAccount Save(ISecurityContext securityContext) {
            if (mAccount == null) {
                if (this.oid > 0) {
                    mAccount = Account.GetDao().Retrieve(securityContext, this.oid);
                }
                else {
                    mAccount = new Account();
                }
            }

            mAccount.Save(securityContext, this);

            return mAccount;
        }
    }
}