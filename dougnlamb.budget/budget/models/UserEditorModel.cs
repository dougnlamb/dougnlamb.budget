using dougnlamb.core.security;
using System;

namespace dougnlamb.budget.models {
    public class UserEditorModel : IUserEditorModel {

        private IUser mUser;

        public UserEditorModel() {
            this.UserId = "";
            this.DisplayName = "";
            this.Email = "";
            this.DefaultCurrencySelector = new CurrencySelectionModel();
        }

        public UserEditorModel(IUser user) {
            this.oid = user?.oid ?? 0;
            this.mUser = user;

            this.UserId = user?.UserId ?? "";
            this.DisplayName = user?.DisplayName ?? "";
            this.Email = user?.Email ?? "";
            this.DefaultCurrencySelector = new CurrencySelectionModel();
            this.DefaultCurrency = user?.DefaultCurrency ?? null;
        }

        public int oid { get; set; }
        public string UserId { get; protected set; }

        public string DisplayName { get; set; }

        public string Email { get; set; }

        public ICurrency DefaultCurrency {
            get {
                return DefaultCurrencySelector.SelectedCurrency;
            }
            set {
                DefaultCurrencySelector.SelectedItem = value?.View(null) ?? null;
            }
        }

        public CurrencySelectionModel DefaultCurrencySelector { get; set; }

        public IUser Save(ISecurityContext securityContext) {
            if (mUser == null) {
                if (this.oid > 0) {
                    mUser = User.GetDao().Retrieve(securityContext, this.oid);
                    this.UserId = mUser.UserId;
                    this.DisplayName = string.IsNullOrWhiteSpace(DisplayName) ? mUser.DisplayName : DisplayName;
                    this.Email = string.IsNullOrWhiteSpace(Email) ? mUser.Email : Email;
                    this.DefaultCurrency = DefaultCurrency ?? mUser.DefaultCurrency;
                }
                else {
                    mUser = new User(securityContext);
                }
            }

            mUser.Save(securityContext, this);

            return mUser;
        }
    }
}