﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dougnlamb.core.collections;
using dougnlamb.budget.dao;
using dougnlamb.core.security;
using dougnlamb.budget.models;
using dougnlamb.core;

namespace dougnlamb.budget {
    public class Account : BaseObject, IAccount {

        public Account(ISecurityContext securityContext) : base(securityContext) {
            mIsLoaded = true;
        }

        public Account(ISecurityContext securityContext, int oid) : base(securityContext) {
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

        private IMoney mBalance;
        public IMoney Balance {
            get {
                return mBalance;
            }
            internal set {
                mBalance = value;
            }
        }

        private IMoney mClearedBalance;
        public IMoney ClearedBalance {
            get {
                return mClearedBalance;
            }
            internal set {
                mClearedBalance = value;
            }
        }

        public static IAccountDao GetDao() {
            return new AccountDao();
        }

        private IPagedList<ITransaction> mTransactions;
        public IPagedList<ITransaction> Transactions {
            get {
                if (mTransactions == null) {
                    mTransactions = Transaction.GetDao().Retrieve(mSecurityContext, this);
                }
                return mTransactions;
            }
        }

        public IReadOnlyList<IUserAccess> UserAccessList {
            get {
                throw new NotImplementedException();
            }
        }

        public IUserAccess AddUserAccess(IUser user, UserAccessMode accessMode) {
            throw new NotImplementedException();
        }

        public override bool CanRead(IUser user) {
            throw new NotImplementedException();
        }

        public override bool CanUpdate(IUser user) {
            throw new NotImplementedException();
        }

        public IPagedList<ITransaction> GetTransactionsSince(DateTime date) {
            throw new NotImplementedException();
        }

        public IAccountViewModel View(ISecurityContext securityContext) {
            return new AccountViewModel(securityContext, this);
        }

        // Using the editor model to update properties before the save.
        public void Save(ISecurityContext securityContext, IAccountEditorModel model) {
            if (this.oid != model.oid) {
                throw new InvalidOperationException("Oid mismatch.");
            }

            IUser owner = User.GetDao().Retrieve(securityContext, model.Owner.oid);
            IUser createdBy = this.CreatedBy ?? owner;

            Account acct = new Account(mSecurityContext) {
                oid = this.oid,
                Name = model.Name,
                Owner = owner,
                DefaultCurrency = model.DefaultCurrency,
                CreatedBy = createdBy,
                CreatedDate = this.oid == 0 ? DateTime.Now : this.CreatedDate,
                // TODO: Fix UpdatedBy
                //UpdatedBy = model.UpdatedBy,
                UpdatedDate = DateTime.Now
            };

            this.oid = GetDao().Save(securityContext, acct);
            if (acct.oid == 0) {
                acct.oid = this.oid;
            }

            RefreshFrom(acct);
        }

        public override void Refresh() {
            IAccount account = GetDao().Retrieve(mSecurityContext, this.oid);
            RefreshFrom(account);
        }

        private void RefreshFrom(IAccount account) {
            if (this.oid != account.oid) {
                throw new InvalidOperationException("Oid mismatch.");
            }

            this.Name = account.Name;
            this.Owner = account.Owner;
            this.DefaultCurrency = account.DefaultCurrency;

            this.mTransactions = null;

            base.RefreshFrom(account);
        }

        public ITransaction AddTransaction(ISecurityContext securityContext, ITransactionEditorModel model) {
            //model.Account = this;
            ITransaction transaction = model.Save(securityContext);
            if (mTransactions != null) {
                //mTransactions.Add(transaction);
                mTransactions = null;
            }

            UpdateBalance(securityContext);
            return transaction;
        }

        private void UpdateBalance(ISecurityContext securityContext) {
            IAccount account = GetDao().Retrieve(securityContext, oid);
            IMoney balance = new Money() { Currency = DefaultCurrency };
            foreach(ITransaction trans in account.Transactions.AllItems) {
                balance.Add(trans.TransactionAmount);
            }

            ((Account)account).mBalance = balance;

            GetDao().Save(securityContext, account);

            RefreshFrom(account);
        }
    }
}
