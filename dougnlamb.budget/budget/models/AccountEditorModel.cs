using dougnlamb.budget.dao;
using dougnlamb.core.security;
using System;
using System.Collections.Generic;

namespace dougnlamb.budget.models {
    public class AccountEditorModel : IAccountEditorModel {
        private ISecurityContext mSecurityContext;
        private IAccount mAccount;

        public AccountEditorModel(ISecurityContext securityContext, IUser user) {
            this.mSecurityContext = securityContext;
            this.mAccount = new Account(securityContext);

            this.Name = "";
            this.DefaultCurrencySelector = new CurrencySelectionModel(securityContext, user?.DefaultCurrency);
            this.Owner = user;
        }

        public AccountEditorModel(ISecurityContext securityContext, IAccount account) {
            this.mSecurityContext = securityContext;
            this.mAccount = account;

            this.oid = account.oid;
            this.Name = account.Name;
            this.DefaultCurrencySelector = new CurrencySelectionModel(securityContext, account?.DefaultCurrency);
            this.Owner = account.Owner;
        }

        public int oid { get; protected set; }
        public string Name { get; set; }

        public IUser Owner { get; set; }

        public ICurrency DefaultCurrency {
            get {
                return DefaultCurrencySelector.SelectedCurrency;
            }
            set {
                DefaultCurrencySelector.SelectedCurrencyCode = value?.oid ?? 0;
            }
        }

        public CurrencySelectionModel DefaultCurrencySelector { get; set; }

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