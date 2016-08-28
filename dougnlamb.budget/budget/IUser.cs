using dougnlamb.core;
using dougnlamb.core.security;
using System;

namespace dougnlamb.budget {
    public interface IUser : IBaseObject {
        int oid { get; }
        string UserId { get; }
        string DisplayName { get; }
        string Email { get; }

        IObservable<IAccount> Accounts { get; }
        IObservable<IBudget> Budgets { get; }

        IAccountEditorModel CreateAccount(ISecurityContext securityContext);
        IBudgetEditorModel CreateBudget(ISecurityContext securityContext);

        IUserEditorModel Edit(ISecurityContext securityContext);
        void Save(ISecurityContext securityContext, IUserEditorModel model);
    }
}