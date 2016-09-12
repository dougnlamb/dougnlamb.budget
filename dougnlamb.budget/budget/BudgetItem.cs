using dougnlamb.budget.dao;
using dougnlamb.budget.models;
using dougnlamb.core;
using dougnlamb.core.collections;
using dougnlamb.core.security;
using System;

namespace dougnlamb.budget {
    public class BudgetItem : BaseObject, IBudgetItem {

        public BudgetItem(ISecurityContext securityContext) : base(securityContext) {
            mIsLoaded = true;
        }

        public BudgetItem(ISecurityContext securityContext, int oid) : base(securityContext) {
            mIsLoaded = false;
            this.oid = oid;
        }

        public int oid { get; internal set; }

        private IObservableList<IAllocation> mAllocations;
        public IObservableList<IAllocation> Allocations {
            get {
                //if(mAllocations == null) {
                //    mAllocations = Allocation.GetDao().Retrieve(this);
                //}
                //return mAllocations;
                throw new NotImplementedException();
            }
        }

        private IMoney mAmount;
        public IMoney Amount {
            get {
                Load();
                return mAmount;
            }
            internal set {
                mAmount = value;
            }
        }

        private IMoney mBalance;
        public IMoney Balance {
            get {
                Load();
                return mBalance;
            }
            internal set {
                mBalance = value;
            }
        }

        private IBudget mBudget;
        public IBudget Budget {
            get {
                Load();
                return mBudget;
            }
            internal set {
                mBudget = value;
            }
        }

        private IUser mClosedBy;
        public IUser ClosedBy {
            get {
                Load();
                return mClosedBy;
            }
            internal set {
                mClosedBy = value;
            }
        }

        private  DateTime mClosedDate;
        public DateTime ClosedDate {
            get {
                Load();
                return mClosedDate;
            }
            internal set {
                mClosedDate = value;
            }
        }

        private IAccount mDefaultAccount;
        public IAccount DefaultAccount {
            get {
                Load();
                return mDefaultAccount;
            }
            internal set {
                mDefaultAccount = value;
            }
        }

        private DateTime mDueDate;
        public DateTime DueDate {
            get {
                Load();
                return mDueDate;
            }
            internal set {
                mDueDate = value;
            }
        }

        private bool mIsClosed;
        public bool IsClosed {
            get {
                Load();
                return mIsClosed;
            }
            internal set {
                mIsClosed = value;
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

        private string mNotes;
        public string Notes {
            get {
                Load();
                return mNotes;
            }
            internal set {
                mNotes = value;
            }
        }

        private DateTime mReminderDate;
        public DateTime ReminderDate {
            get {
                Load();
                return mReminderDate;
            }
            internal set {
                mReminderDate = value;
            }
        }

        public override bool CanRead(IUser user) {
            throw new NotImplementedException();
        }

        public override bool CanUpdate(IUser user) {
            throw new NotImplementedException();
        }

        public IBudgetItemEditorModel Edit(ISecurityContext securityContext) {
            return new BudgetItemEditorModel(securityContext, this);
        }

        public void Save(ISecurityContext securityContext, IBudgetItemEditorModel model) {
            if (this.oid != model.oid) {
                throw new InvalidOperationException("Oid mismatch.");
            }

            BudgetItem budgetItem = new BudgetItem(securityContext) {
                oid = this.oid,
                Amount = model.Amount,
                Balance = model.Balance,
                Budget = model.Budget,
                DueDate = model.DueDate,
                Name = model.Name,
                Notes = model.Notes,
                ReminderDate = model.ReminderDate,
                //Period = model.Period,
                CreatedBy = this.CreatedBy,
                CreatedDate = this.CreatedDate,
                // TODO: Fix UpdatedBy
                //UpdatedBy = model.UpdatedBy,
                UpdatedDate = DateTime.Now
            };
            budgetItem.DefaultAccount = model.DefaultAccount;
            budgetItem.Budget = model.Budget;

            this.oid = GetDao().Save(securityContext, budgetItem);
            if (budgetItem.oid == 0) {
                budgetItem.oid = this.oid;
            }

            RefreshFrom(budgetItem);
        }

        public IBudgetItemViewModel View(ISecurityContext securityContext) {
            return new BudgetItemViewModel(securityContext, this);
        }

        public override void Refresh() {
            IBudgetItem budgetItem = GetDao().Retrieve(mSecurityContext, this.oid);
            RefreshFrom(budgetItem);
        }

        private void RefreshFrom(IBudgetItem budgetItem) {
            if (this.oid != budgetItem.oid) {
                throw new InvalidOperationException("Oid mismatch.");
            }

            this.Amount = budgetItem.Amount;
            this.Balance = budgetItem.Balance;
            this.Budget = budgetItem.Budget;
            this.ClosedBy = budgetItem.ClosedBy;
            this.ClosedDate = budgetItem.ClosedDate;
            this.DefaultAccount = budgetItem.DefaultAccount;
            this.DueDate = budgetItem.DueDate;
            this.IsClosed = budgetItem.IsClosed;
            this.Name = budgetItem.Name;
            this.Notes = budgetItem.Notes;
            this.ReminderDate = budgetItem.ReminderDate;

            base.RefreshFrom(budgetItem);
        }

        public static IBudgetItemDao GetDao() {
            return new BudgetItemDao();
        }

    }
}