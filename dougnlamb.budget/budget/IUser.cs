using System;

namespace dougnlamb.budget {
    public interface IUser {
        string UserId { get; }
        string DisplayName { get; }
        string Email { get; }

        IObservable<IAccount> Accounts { get; }
        IObservable<IBudget> Budgets { get; }

        IAccountEditorModel CreateAccount();
        IBudgetEditorModel CreateBudget();

        IUserEditorModel Edit(IUser editingUser);
        void Save(IUser savingUser, IUserEditorModel model);
    }
}