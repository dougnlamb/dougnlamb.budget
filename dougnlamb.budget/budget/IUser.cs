using System;

namespace dougnlamb.budget {
    public interface IUser {
        string UserId { get; set; }
        string DisplayName { get; set; }
        string Email { get; set; }

        IObservable<IAccount> Accounts { get; }
        IObservable<IBudget> Budgets { get; }

        IAccount CreateAccount(string name);
        IBudget CreateBudget(string name, DateTime startDate, DateTime endDate);
    }
}