using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dougnlamb.core.collections;
using dougnlamb.budget.dao;
using dougnlamb.core.security;

namespace dougnlamb.budget {
    public class Account : IAccount {
        private bool mIsLoaded;

        public Account() {
            mIsLoaded = true;
        }

        public Account(int oid) {
            mIsLoaded = false;
            this.oid = oid;
        }

        public int oid { get; internal set; }

        private string mName;
        public string Name {
            get {
                Load();
                return mName;
            }
            internal set {
                mName = value;
            }
        }

        private IUser mOwner;
        public IUser Owner {
            get {
                Load();
                return mOwner;
            }
            internal set {
                mOwner = value;
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

        public IMoney Balance {
            get {
                throw new NotImplementedException();
            }
        }

        public IMoney ClearedBalance {
            get {
                throw new NotImplementedException();
            }
        }

        public static IAccountDao GetDao() {
            return new AccountDao();
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

        public IPagedList<ITransaction> Transactions {
            get {
                throw new NotImplementedException();
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

        public IReadOnlyList<IUserAccess> UserAccessList {
            get {
                throw new NotImplementedException();
            }
        }

        public IAllocation AddAllocatedTransaction(IMoney amount, IBudgetItem budgetItem, IUser reportingUser, string notes) {
            throw new NotImplementedException();
        }

        public ITransaction AddTransaction(IMoney amount, IUser reportingUser, string notes) {
            throw new NotImplementedException();
        }

        public IUserAccess AddUserAccess(IUser user, UserAccessMode accessMode) {
            throw new NotImplementedException();
        }

        public bool CanRead(IUser user) {
            throw new NotImplementedException();
        }

        public bool CanUpdate(IUser user) {
            throw new NotImplementedException();
        }

        public IPagedList<ITransaction> GetTransactionsSince(DateTime date) {
            throw new NotImplementedException();
        }

        public IAccountEditorModel Edit(ISecurityContext securityContext) {
            return new AccountEditorModel(securityContext, this);
        }

        // Using the editor model to update properties before the save.
        public void Save(ISecurityContext securityContext, IAccountEditorModel model) {
            if (this.oid != model.oid) {
                throw new InvalidOperationException("Oid mismatch.");
            }

            Account acct = new Account() {
                oid = this.oid,
                Name = model.Name,
                Owner = model.Owner,
                DefaultCurrency = model.DefaultCurrency,
                CreatedBy = this.CreatedBy,
                CreatedDate = this.CreatedDate,
                // TODO: Fix UpdatedBy
                //UpdatedBy = model.UpdatedBy,
                UpdatedDate = DateTime.Now
            };

            this.oid = GetDao().Save(securityContext, acct);
            if(acct.oid == 0) {
                acct.oid = this.oid;
            }

            RefreshFrom(acct);
        }

        public void Refresh(ISecurityContext securityContext) {
            IAccount account = GetDao().Retrieve(securityContext, this.oid);
            RefreshFrom(account);
        }

        private void RefreshFrom(IAccount account) {
            if (this.oid != account.oid) {
                throw new InvalidOperationException("Oid mismatch.");
            }

            this.Name = account.Name;
            this.Owner = account.Owner;
            this.CreatedBy = account.CreatedBy;
            this.CreatedDate = account.CreatedDate;
            this.UpdatedBy = account.UpdatedBy;
            this.UpdatedDate = account.UpdatedDate;
            this.DefaultCurrency = account.DefaultCurrency;
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
            IAccount account = GetDao().Retrieve(null, this.oid);
            this.Name = account.Name;
            this.Owner = account.Owner;
            this.DefaultCurrency = account.DefaultCurrency;

            this.CreatedBy = account.CreatedBy;
            this.CreatedDate = account.CreatedDate;
            this.UpdatedBy = account.UpdatedBy;
            this.UpdatedDate = account.UpdatedDate;
        }
    }
}
