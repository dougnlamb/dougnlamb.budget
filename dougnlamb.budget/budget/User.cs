using dougnlamb.budget.dao;
using dougnlamb.budget.models;
using dougnlamb.core.collections;
using dougnlamb.core.security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dougnlamb.budget {
    public class User : IUser {
        private bool mIsLoaded;

        public User() {
            mIsLoaded = true;
        }

        public User(int oid) {
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

        private IUser mOwner;
        public IObservableList<IAccount> Accounts {
            get {
                throw new NotImplementedException();
            }
        }

        public IObservableList<IBudget> Budgets {
            get {
                throw new NotImplementedException();
            }
        }

        public IAccountEditorModel CreateAccount(ISecurityContext securityContext) {
            IAccount account = new Account();
            return account.Edit(securityContext);
        }

        public IBudgetEditorModel CreateBudget(ISecurityContext securityContext) {
            IBudget budget = new Budget();
            return budget.Edit(securityContext);
        }

        public IUserEditorModel Edit(ISecurityContext securityContext) {
            return new UserEditorModel(this);
        }

        private IUser mCreatedBy;
        public IUser CreatedBy {
            get {
                Load();
                return mCreatedBy;
            }
            internal set {
                mCreatedBy = value;
            }
        }

        private DateTime mCreatedDate;
        public DateTime CreatedDate {
            get {
                Load();
                return mCreatedDate;
            }
            internal set {
                mCreatedDate = value;
            }
        }

        private IUser mUpdatedBy;
        public IUser UpdatedBy {
            get {
                Load();
                return mUpdatedBy;
            }
            internal set {
                mUpdatedBy = value;
            }
        }

        private DateTime mUpdatedDate;
        public DateTime UpdatedDate {
            get {
                Load();
                return mUpdatedDate;
            }
            internal set {
                mUpdatedDate = value;
            }
        }

        public void Save(ISecurityContext securityContext, IUserEditorModel model) {
            if (model.oid != this.oid) {
                throw new InvalidOperationException("Oid mismatch.");
            }

            User usr = new User() {
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

        public void Refresh(ISecurityContext securityContext) {
            IUser user = GetDao().Retrieve(securityContext, this.oid);
            RefreshFrom(user);
        }

        private void RefreshFrom(IUser user) {
            if (this.oid != user.oid) {
                throw new InvalidOperationException("Oid mismatch.");
            }

            this.DisplayName = user.DisplayName;
            this.Email = user.Email;

            this.CreatedBy = user.CreatedBy;
            this.CreatedDate = user.CreatedDate;
            this.UpdatedBy = user.UpdatedBy;
            this.UpdatedDate = user.UpdatedDate;
        }

        public static IUserDao GetDao() {
            return new UserDao();
        }

        private void Load() {
            if (!mIsLoaded) {
                if (this.oid > 0) {
                    Refresh();
                }

                mIsLoaded = true;
            }
        }

        private void Refresh() {
            IUser usr = GetDao().Retrieve(null, this.oid);
            this.UserId = usr.UserId;
            this.Email = usr.Email;
            this.DisplayName = usr.DisplayName;

            this.CreatedBy = usr.CreatedBy;
            this.CreatedDate = usr.CreatedDate;
            this.UpdatedBy = usr.UpdatedBy;
            this.UpdatedDate = usr.UpdatedDate;
        }

        public bool CanRead(IUser user) {
            throw new NotImplementedException();
        }

        public bool CanUpdate(IUser user) {
            throw new NotImplementedException();
        }

        public IAccount AddAccount(ISecurityContext securityContext, IAccountEditorModel model) {
            throw new NotImplementedException();
        }

        public IBudget AddBudget(ISecurityContext securityContext, IBudgetEditorModel model) {
            throw new NotImplementedException();
        }
    }
}
