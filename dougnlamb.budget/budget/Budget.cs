﻿using dougnlamb.budget.dao;
using dougnlamb.core.collections;
using dougnlamb.core.security;
using System;

namespace dougnlamb.budget {
    public class Budget : IBudget {
        private bool mIsLoaded;

        public Budget() {
            mIsLoaded = true;
        }

        public Budget(int oid) {
            mIsLoaded = false;
            this.oid = oid;
        }

        public IObservable<IBudgetItem> BudgetItems {
            get {
                throw new NotImplementedException();
            }
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

        public bool IsClosed {
            get {
                throw new NotImplementedException();
            }
        }

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

        public int oid { get; internal set; }

        private IUser mOwner;
        public IUser Owner {
            get {
                Load();
                return mOwner;
            }
            set {
                mOwner = value;
            }
        }

        private IBudgetPeriod mPeriod;
        public IBudgetPeriod Period {
            get {
                Load();
                return mPeriod;
            }
            set {
                mPeriod = value;
            }
        }

        public IBudgetItem AddBudgetItem(IMoney amount, string name, string notes, IUser creatingUser) {
            throw new NotImplementedException();
        }

        public void AddUserAccess(IUser user, UserAccessMode accessMode) {
            throw new NotImplementedException();
        }

        public bool CanRead(IUser user) {
            throw new NotImplementedException();
        }

        public bool CanUpdate(IUser user) {
            throw new NotImplementedException();
        }

        public void Close(IUser user) {
            throw new NotImplementedException();
        }

        public IBudgetEditorModel Edit(ISecurityContext securityContext) {
            return new BudgetEditorModel(securityContext, this);
        }

        public void Save(ISecurityContext securityContext, IBudgetEditorModel model) {
            if (this.oid != model.oid) {
                throw new InvalidOperationException("Oid mismatch.");
            }
            this.Name = model.Name;
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
            IBudget account = GetDao().Retrieve(null, this.oid);
            this.Name = account.Name;
            this.Owner = account.Owner;

            this.CreatedBy = account.CreatedBy;
            this.CreatedDate = account.CreatedDate;
            this.UpdatedBy = account.UpdatedBy;
            this.UpdatedDate = account.UpdatedDate;
        }

        public static IBudgetDao GetDao() {
            return new BudgetDao();
        }

    }
}