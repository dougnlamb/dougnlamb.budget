using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dougnlamb.budget.models;
using dougnlamb.core.collections;
using dougnlamb.core.security;
using dougnlamb.budget.dao;

namespace dougnlamb.budget {
    public class Transaction : ITransaction {
        public IObservableList<IAllocation> Allocations {
            get {
                throw new NotImplementedException();
            }
        }

        public DateTime ClearedDate { get; internal set; }
        public IUser CreatedBy { get; internal set; }
        public DateTime CreatedDate { get; internal set; }

        public bool IsAllocated { get; internal set; }
        public bool IsCleared { get; internal set; }

        public string Note { get; internal set; }

        public int oid { get; internal set; }

        public IUser ReportedBy { get; internal set; }

        public DateTime ReportedDate { get; internal set; }

        public IMoney TransactionAmount { get; internal set; }

        public DateTime TransactionDate { get; internal set; }

        public IUser UpdatedBy { get; internal set; }

        public DateTime UpdatedDate { get; internal set; }

        public IAccount Account { get; internal set; }

        public IAllocation AddAllocation(ISecurityContext securityContext, IAllocationEditorModel model) {
            throw new NotImplementedException();
        }

        public bool CanRead(IUser user) {
            throw new NotImplementedException();
        }

        public bool CanUpdate(IUser user) {
            throw new NotImplementedException();
        }

        public IAllocationEditorModel CreateAllocation(ISecurityContext securityContext) {
            throw new NotImplementedException();
        }

        public ITransactionEditorModel Edit(ISecurityContext securityContext) {
            throw new NotImplementedException();
        }

        public IMoney GetAllocationDiscrepency() {
            throw new NotImplementedException();
        }

        public void Save(ISecurityContext securityContext, ITransactionEditorModel model) {
            if (model.oid != this.oid) {
                throw new InvalidOperationException("Oid mismatch.");
            }

            Transaction transaction = new Transaction() {
                oid = this.oid,
                CreatedBy = this.CreatedBy,
                CreatedDate = this.CreatedDate,
                Account = new Account(model.AccountSelector.SelectedAccountId),
                Note = model.Note,
                TransactionDate = model.TransactionDate,
                TransactionAmount = new Money() {
                    Amount = model.TransactionAmount.Amount,
                    Currency = Currency.GetDao().Retrieve(securityContext, model.TransactionAmount.CurrencyId)
                },
                // TODO: Fix UpdatedBy
                //UpdatedBy = model.UpdatedBy,
                UpdatedDate = DateTime.Now
            };

            this.oid = GetDao().Save(securityContext, transaction);
            if (transaction.oid == 0) {
                transaction.oid = this.oid;
            }

            RefreshFrom(transaction);
        }

        private void RefreshFrom(ITransaction transaction) {
            if(this.oid != transaction.oid) {
                throw new InvalidOperationException("Oid mismatch.");
            }

            this.Account = transaction.Account;
            this.ClearedDate = transaction.ClearedDate;
            this.CreatedBy = transaction.CreatedBy;
            this.CreatedDate = transaction.CreatedDate;
            this.IsAllocated = transaction.IsAllocated;
            this.IsCleared = transaction.IsCleared;
            this.Note = transaction.Note;
            this.ReportedBy = transaction.ReportedBy;
            this.ReportedDate = transaction.ReportedDate;
            this.TransactionAmount = transaction.TransactionAmount;
            this.TransactionDate = transaction.TransactionDate;
            this.UpdatedBy = transaction.UpdatedBy;
            this.UpdatedDate = transaction.UpdatedDate;
        }

        public static ITransactionDao GetDao() {
            return new TransactionDao();
        }

    }
}
