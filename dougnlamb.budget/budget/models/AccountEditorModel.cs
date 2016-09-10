using dougnlamb.budget.dao;
using dougnlamb.core.security;
using System;
using System.Collections.Generic;

namespace dougnlamb.budget.models {
    public class AccountEditorModel : IAccountEditorModel {
        private IAccount mAccount;
        public AccountEditorModel(ISecurityContext securityContext, IUser user) {
            this.mAccount = new Account(securityContext);

            this.Name = "";
            this.CurrencySelector = new CurrencySelectionModel(user?.DefaultCurrency?.oid ?? 0);
            this.Owner = user.View(securityContext);
        }

        public AccountEditorModel(ISecurityContext securityContext, IAccount account) {
            this.mAccount = account;

            this.oid = account.oid;
            this.Name = account.Name;
            this.CurrencySelector = new CurrencySelectionModel(account?.DefaultCurrency?.oid ?? 0);
            this.Owner = account.Owner.View(securityContext);
            OwnerId = this.Owner.oid;
        }

        public int oid { get; protected set; }
        public string Name { get; set; }

        public int OwnerId { get; set; }
        public IUserViewModel Owner { get; internal set; }
        public IList<IUserViewModel> PossibleOwners { get; }

        public int DefaultCurrencyId {
            get {
                return CurrencySelector.SelectedCurrencyId;
            }
        }
        public CurrencySelectionModel CurrencySelector { get; set; }

        public IAccount Save(ISecurityContext securityContext) {
            if (mAccount == null) {
                if (this.oid > 0) {
                    mAccount = Account.GetDao().Retrieve(securityContext, this.oid);
                }
                else {
                    mAccount = new Account(securityContext);
                }
            }

            mAccount.Save(securityContext, this);

            return mAccount;
        }
    }
}