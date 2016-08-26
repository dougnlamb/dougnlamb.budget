using System;

namespace dougnlamb.budget {
    public class Budget : IBudget {
        public IObservable<IBudgetItem> BudgetItems {
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

        public bool IsClosed {
            get {
                throw new NotImplementedException();
            }
        }

        public string Name { get; internal set;}

        public int oid { get; internal set; }

        public IUser Owner { get; internal set; }

        public IBudgetPeriod Period { get; internal set;}
        public IUser UpdatedBy { get; internal set; }

        public DateTime UpdatedDate { get; internal set; }

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

        public IBudgetEditorModel Edit(IUser editingUser) {
            return new BudgetEditorModel(editingUser, this);
        }

        public void Save(IUser user, IBudgetEditorModel model) {
            throw new NotImplementedException();
        }
    }
}