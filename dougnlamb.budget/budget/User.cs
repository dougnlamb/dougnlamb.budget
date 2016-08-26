using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dougnlamb.budget {
    public class User : IUser {
        public string UserId { get; internal set; }

        public string DisplayName { get; internal set; }

        public string Email { get; internal set;}

        public IObservable<IAccount> Accounts {
            get {
                throw new NotImplementedException();
            }
        }

        public IObservable<IBudget> Budgets {
            get {
                throw new NotImplementedException();
            }
        }

        public IAccountEditorModel CreateAccount() {
            IAccount account = new Account();
            return account.Edit(this);
        }

        public IBudgetEditorModel CreateBudget() {
            IBudget budget = new Budget();
            return budget.Edit(this);
        }

        public IUserEditorModel Edit(IUser editingUser) {
            return new UserEditorModel(this);
        }

        public void Save(IUser savingUser, IUserEditorModel model) {
            if(model.UserId != this.UserId) {
                throw new InvalidOperationException("User id mismatch.");
            }
            this.Email = model.Email;
            this.DisplayName = model.DisplayName;
        }
    }
}
