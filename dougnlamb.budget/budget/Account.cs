using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dougnlamb.core.collections;

namespace dougnlamb.budget {
    public class Account : IAccount {
        public int oid { get; internal set; }

        public string Name { get; internal set; }

        public IUser Owner { get; internal set; }

        public ICurrency DefaultCurrency { get; internal set; }

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

        public DateTime CreatedBy {
            get {
                throw new NotImplementedException();
            }
        }

        public DateTime CreatedDate {
            get {
                throw new NotImplementedException();
            }
        }

        public IPagedList<ITransaction> Transactions {
            get {
                throw new NotImplementedException();
            }
        }

        public IUser UpdatedBy {
            get {
                throw new NotImplementedException();
            }
        }

        public DateTime UpdatedDate {
            get {
                throw new NotImplementedException();
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

        public IAccountEditorModel Edit(IUser editingUser) {
            return new AccountEditorModel(editingUser, this);
        }

        public void Save(IUser savingUser, IAccountEditorModel model) {
            throw new NotImplementedException();
        }
    }
}
