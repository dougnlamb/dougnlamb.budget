using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dougnlamb.budget.models;
using dougnlamb.core.security;
using dougnlamb.core;
using dougnlamb.budget.dao;

namespace dougnlamb.budget {
    public class Allocation : BaseObject, IAllocation {
        public Allocation(ISecurityContext securityContext) : base(securityContext) {
            mIsLoaded = true;
        }

        public Allocation(ISecurityContext securityContext, int oid) : base(securityContext) {
            mIsLoaded = false;
            this.oid = oid;
        }

        public int oid { get; internal set; }

        private IMoney mAmount;
        public IMoney Amount {
            get {
                Load();
                return mAmount;
            }
            set {
                mAmount = value;
            }
        }

        private IBudgetItem mBudgetItem;
        public IBudgetItem BudgetItem {
            get {
                Load();
                return mBudgetItem;
            }
            set {
                mBudgetItem = value;
            }
        }

        private string mNotes;
        public string Notes {
            get {
                Load();
                return mNotes;
            }
            set {
                mNotes = value;
            }
        }

        private ITransaction mTransaction;
        public ITransaction Transaction {
            get {
                Load();
                return mTransaction;
            }
            set {
                mTransaction = value;
            }
        }

        public override bool CanRead(IUser user) {
            throw new NotImplementedException();
        }

        public override bool CanUpdate(IUser user) {
            throw new NotImplementedException();
        }

        public IAllocationEditorModel Edit(ISecurityContext securityContext) {
            return new AllocationEditorModel(securityContext, this);
        }

        public override void Refresh() {
            IAllocation allocation = GetDao().Retrieve(mSecurityContext, this.oid);
            RefreshFrom(allocation);
        }

        private void RefreshFrom(IAllocation allocation) {
            if (this.oid != allocation.oid) {
                throw new InvalidOperationException("Oid mismatch.");
            }

            this.Amount = allocation.Amount;
            this.BudgetItem = allocation.BudgetItem;
            this.Notes = allocation.Notes;
            this.Transaction = allocation.Transaction;

            base.RefreshFrom(allocation);
        }

        public void Save(ISecurityContext securityContext, IAllocationEditorModel model) {
            if( this.oid != model.oid) {
                throw new InvalidOperationException("Oid mismatch.");
            }

            Allocation allocation = new Allocation(securityContext) {
                oid = this.oid,
                Amount = model.Amount,
                BudgetItem = model.BudgetItem,
                CreatedBy = this.CreatedBy,
                CreatedDate = this.CreatedDate,
                Notes = model.Notes,
                Transaction = model.Transaction,
                UpdatedBy = null,
                UpdatedDate = DateTime.Now
            };

            this.oid = GetDao().Save(securityContext, allocation);
            if(allocation.oid == 0 ) {
                allocation.oid = this.oid;
            }

            RefreshFrom(allocation);
        }

        public static IAllocationDao GetDao() {
            return new AllocationDao();
        }
    }
}
