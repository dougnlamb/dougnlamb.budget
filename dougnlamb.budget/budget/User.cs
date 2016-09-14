using dougnlamb.budget.dao;
using dougnlamb.budget.models;
using dougnlamb.core.collections;
using dougnlamb.core.security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dougnlamb.core;

namespace dougnlamb.budget {
    public class User : BaseObject, IUser {

        public User(ISecurityContext securityContext) : base(securityContext) {
            mIsLoaded = true;
        }

        public User(ISecurityContext securityContext, int oid) : base(securityContext) {
            this.oid = oid;
            mIsLoaded = false;
        }

        public int oid { get; internal set; }

        private string mUserId;
        public string UserId {
            get {
                Load();
                return mUserId;
            }
            internal set {
                mUserId = value;
            }
        }

        private string mDisplayName;
        public string DisplayName {
            get {
                Load();
                return mDisplayName;
            }

            internal set {
                mDisplayName = value;
            }
        }

        private string mEmail;
        public string Email {
            get {
                Load();
                return mEmail;
            }

            internal set {
                mEmail = value;
            }
        }

        private ICurrency mDefaultCurrency;
        public ICurrency DefaultCurrency {
            get {
                Load();
                return mDefaultCurrency;
            }
            internal set {
                mDefaultCurrency = value;
            }
        }

        private IObservableList<IAccount> mAccounts;
        public IObservableList<IAccount> Accounts {
            get {
                if (mAccounts == null) {
                    mAccounts = MockDatabase.RetrieveAccounts(this);
                }
                return mAccounts;
            }
        }

        private ObservableList<IBudget> mBudgets;
        public IObservableList<IBudget> Budgets {
            get {
                if (mBudgets == null) {
                    mBudgets = new ObservableList<IBudget>();
                }
                return mBudgets;
            }
        }

        public IAccountEditorModel CreateAccount(ISecurityContext securityContext) {
            return new AccountEditorModel(securityContext, this);
        }

        public IAccount AddAccount(ISecurityContext securityContext, IAccountEditorModel model) {
            IAccount acct = model.Save(securityContext);
            Accounts.Add(acct);
            return acct;
        }

        public IBudgetEditorModel CreateBudget(ISecurityContext securityContext) {
            return new BudgetEditorModel(securityContext, this);
        }

        public IBudget AddBudget(ISecurityContext securityContext, IBudgetEditorModel model) {
            IBudget budget = model.Save(securityContext);
            Budgets.Add(budget);
            return budget;
        }

        public IUserEditorModel Edit(ISecurityContext securityContext) {
            return new UserEditorModel(this);
        }


        public void Save(ISecurityContext securityContext, IUserEditorModel model) {
            if (model.oid != this.oid) {
                throw new InvalidOperationException("Oid mismatch.");
            }

            User usr = new User(null) {
                oid = this.oid,
                UserId = model.UserId,
                DisplayName = model.DisplayName,
                Email = model.Email,
                CreatedBy = this.CreatedBy,
                CreatedDate = this.CreatedDate,
                // TODO: Fix UpdatedBy
                //UpdatedBy = model.UpdatedBy,
                UpdatedDate = DateTime.Now
            };

            this.oid = GetDao().Save(securityContext, usr);
            if (usr.oid == 0) {
                usr.oid = this.oid;
            }

            RefreshFrom(usr);
        }

        public override void Refresh() {
            if (this.oid > 0) {
                IUser user = GetDao().Retrieve(mSecurityContext, this.oid);
                RefreshFrom(user);
            }
        }

        private void RefreshFrom(IUser user) {
            if (this.oid != user.oid) {
                throw new InvalidOperationException("Oid mismatch.");
            }

            this.UserId = user.UserId;
            this.DefaultCurrency = user.DefaultCurrency;
            this.DisplayName = user.DisplayName;
            this.Email = user.Email;

            base.RefreshFrom(user);
        }

        public static IUserDao GetDao() {
            return new UserDao();
        }

        public override bool CanRead(IUser user) {
            throw new NotImplementedException();
        }

        public override bool CanUpdate(IUser user) {
            throw new NotImplementedException();
        }

        public IUserViewModel View(ISecurityContext securityContext) {
            return new UserViewModel(securityContext, this);
        }

        public void Save(ISecurityContext securityContext, IUserRegistrationModel model) {
            UserId = model.UserId;
            DisplayName = model.DisplayName;
            Email = model.Email;
            CreatedDate = DateTime.Now;
            // TODO: Fix UpdatedBy
            //UpdatedBy = model.UpdatedBy,
            UpdatedDate = DateTime.Now;

            this.oid = GetDao().Save(securityContext, this);
        }
    }
}
