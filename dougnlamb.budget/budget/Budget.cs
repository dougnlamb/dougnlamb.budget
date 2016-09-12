using dougnlamb.budget.dao;
using dougnlamb.budget.models;
using dougnlamb.core;
using dougnlamb.core.collections;
using dougnlamb.core.security;
using System;

namespace dougnlamb.budget {
    public class Budget : BaseObject, IBudget {

        public Budget(ISecurityContext securityContext) : base(securityContext) {
            mIsLoaded = true;
        }

        public Budget(ISecurityContext securityContext, int oid) : base(securityContext) {
            mIsLoaded = false;
            this.oid = oid;
        }

        public IObservableList<IBudgetItem> BudgetItems {
            get {
                return MockDatabase.RetrieveBudgetItems(this);
            }
        }

        public IPagedList<ITransaction> Transactions {
            get {
                throw new NotImplementedException();
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

        private IMoney mPlannedBalance;
        public IMoney PlannedBalance {
            get {
                Load();
                return mPlannedBalance;
            }
            internal set {
                mPlannedBalance = value;
            }
        }

        private IMoney mActualBalance;
        public IMoney ActualBalance {
            get {
                Load();
                return mActualBalance;
            }
            internal set {
                mActualBalance = value;
            }
        }

        public void AddUserAccess(IUser user, UserAccessMode accessMode) {
            throw new NotImplementedException();
        }

        public override bool CanRead(IUser user) {
            throw new NotImplementedException();
        }

        public override bool CanUpdate(IUser user) {
            throw new NotImplementedException();
        }

        public IBudgetViewModel View(ISecurityContext securityContext) {
            return new BudgetViewModel(securityContext, this);
        }

        public IBudgetEditorModel Edit(ISecurityContext securityContext) {
            return new BudgetEditorModel(securityContext, this);
        }

        public void Save(ISecurityContext securityContext, IBudgetEditorModel model) {
            if (this.oid != model.oid) {
                throw new InvalidOperationException("Oid mismatch.");
            }

            Budget budget = new Budget(securityContext) {
                oid = this.oid,
                Name = model.Name,
                Owner = User.GetDao().Retrieve(securityContext, model.Owner.oid),
                DefaultCurrency = model.DefaultCurrency,
                //Period = model.Period,
                CreatedBy = this.CreatedBy,
                CreatedDate = this.CreatedDate,
                // TODO: Fix UpdatedBy
                //UpdatedBy = model.UpdatedBy,
                UpdatedDate = DateTime.Now
            };

            this.oid = GetDao().Save(securityContext, budget);
            if (budget.oid == 0) {
                budget.oid = this.oid;
            }

            RefreshFrom(budget);
        }

        public override void Refresh() {
            IBudget budget = GetDao().Retrieve(mSecurityContext, this.oid);
            RefreshFrom(budget);
        }

        private void RefreshFrom(IBudget budget) {
            if (this.oid != budget.oid) {
                throw new InvalidOperationException("Oid mismatch.");
            }

            this.Name = budget.Name;
            this.Owner = budget.Owner;
            this.Period = budget.Period;
            //this.DefaultCurrency = budget.DefaultCurrency;

            base.RefreshFrom(budget);
        }

        public static IBudgetDao GetDao() {
            return new BudgetDao();
        }

        public IBudgetItemEditorModel CreateBudgetItem(ISecurityContext securityContext) {
            return new BudgetItemEditorModel(securityContext, new BudgetItem(securityContext) { Budget = this });
        }

        public IBudgetItem AddBudgetItem(ISecurityContext securityContext, IBudgetItemEditorModel model) {
            IBudgetItem budgetItem = model.Save(securityContext);
            BudgetItems.Add(budgetItem);
            return budgetItem;
        }
    }
}